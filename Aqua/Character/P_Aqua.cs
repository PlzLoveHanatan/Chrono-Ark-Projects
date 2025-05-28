using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using NLog.Targets;
namespace Aqua
{
    /// <summary>
    /// Aqua
    /// Passive:
    /// </summary>
    public class P_Aqua : Passive_Char, IP_DamageTake, IP_BuffAddAfter, IP_PlayerTurn, IP_BattleStart_UIOnBefore, IP_BattleStart_Ones
    {
        private int AquaDamageTaken;
        private bool AquaCurseRemoval;

        public override void Init()
        {
            OnePassive = true;
        }

        public void BattleStart(BattleSystem Ins)
        {
            AquaCurseRemoval = false;

            if (Utils.CleanseAllCurses)
            {
                for (int i = 0; i < PlayData.TSavedata.LucySkills.Count; i++)
                {
                    var skillData = new GDESkillData(PlayData.TSavedata.LucySkills[i]);
                    if (skillData.User == "LucyCurse" || skillData.KeyID == GDEItemKeys.Skill_S_S1_LittleMaid_0_Lucy)
                    {
                        PlayData.TSavedata.LucySkills.RemoveAt(i);
                        i--;
                        AquaCurseRemoval = true;
                    }
                }

                if (AquaCurseRemoval)
                {
                    var team = BattleSystem.instance.AllyTeam;
                    var deck = team.Skills.Concat(team.Skills_Deck).ToList();

                    for (int i = 0; i < deck.Count; i++)
                    {
                        var skill = deck[i];

                        if (skill?.MySkill != null && skill.MySkill.User == "LucyCurse" || skill?.MySkill?.KeyID == GDEItemKeys.Skill_S_S1_LittleMaid_0_Lucy)
                        {
                            team.Skills.Remove(skill);
                            team.Skills_Deck.Remove(skill);
                        }
                    }

                    MasterAudio.StopBus("SE");
                    MasterAudio.PlaySound("WaterSpell", 100f, null, 0f, null, null, false, false);
                }
            }
        }

        public void Turn()
        {
            AquaDamageTaken = 0;
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg >= 1)
            {
                foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    if (ally != BChar && ally != null)
                    {
                        ally.Heal(BattleSystem.instance.DummyChar, 2f, false, true, null);

                        Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, this.BChar, this.BChar.MyTeam);
                        healingParticle.PlusHit = true;
                        healingParticle.FreeUse = true;

                        this.BChar.ParticleOut(healingParticle, ally);
                    }
                }

                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    if (enemy != null)
                    {
                        enemy.BuffAdd(ModItemKeys.Buff_B_Aqua_CryingShame, this.BChar, false, 0, false, -1, false);
                    }
                }

                if (AquaDamageTaken > 2) return;

                AquaDamageTaken++;

                if (AquaDamageTaken == 2)
                {
                    foreach (var target in BattleSystem.instance.AllyTeam.AliveChars.Concat(BattleSystem.instance.EnemyTeam.AliveChars_Vanish))
                    {
                        if (target != null)
                        {
                            target.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
                        }
                    }
                }
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false);

            var ignoreKeys = new HashSet<string>
            {
                GDEItemKeys.Buff_B_BloodyMist_ShareDamage,
                GDEItemKeys.Buff_B_ProgramMaster_LucyMain,
                GDEItemKeys.Buff_B_S4_King_P2_Last,
                GDEItemKeys.Buff_B_LBossFirst_Summons_CantHeal_Taget,
                GDEItemKeys.Buff_B_LBossFirst_Summons_CC_Taget,
                GDEItemKeys.Buff_B_DorchiX_0_T,
                GDEItemKeys.Buff_B_Neardeath,
            };

            if (BuffTaker == BChar && debuffs != null)
            {
                foreach (var debuff in debuffs)
                {
                    if (ignoreKeys.Contains(debuff.BuffData.Key)) continue;

                    debuff.SelfDestroy();
                }
            }
        }

        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            if (Utils.MoreAquaVoice && BChar is BattleAlly aqua)
            {
                Vector3 basePos = aqua.GetTopPos();

                Vector3 offset = new Vector3(1.25f, 0.85f, 0f);
                Vector3 finalPos = basePos + offset;

                createIconButton("Aqua_Chibi", aqua.transform, "AquaChibi.png", new Vector3(130f, 158f), finalPos);
            }
        }

        private void createIconButton(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos)
        {
            GameObject aquaChibiButton = Utils.creatGameObject(name, parent);
            if (aquaChibiButton == null) return;

            aquaChibiButton.transform.SetParent(parent);

            aquaChibiButton.transform.position = worldPos;

            Image image = aquaChibiButton.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null) return;
            image.sprite = sprite;

            Utils.ImageResize(image, size);

            Aqua_ChibiButton aquaChibi = aquaChibiButton.AddComponent<Aqua_ChibiButton>();
            aquaChibiButton.AddComponent<Aqua_ChibiButton_Script>();

            Aqua_ChibiButton.instance = aquaChibi;

            aquaChibiButton.SetActive(true);
        }
    }
}