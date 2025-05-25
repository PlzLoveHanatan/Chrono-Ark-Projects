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
    public class P_Aqua : Passive_Char, IP_DamageTake, IP_BuffAddAfter, IP_PlayerTurn, IP_BattleStart_UIOnBefore
    {
        private int AquaDamageTaken;
        public override void Init()
        {
            OnePassive = true;
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
                // Get the reference position above the character (usually top of the sprite or head)
                Vector3 basePos = aqua.GetTopPos();

                // Add an offset to place the icon slightly above or beside the character
                Vector3 offset = new Vector3(0f, 0.6f, 0f); // Adjust as needed
                Vector3 finalPos = basePos + offset;

                // Create the icon button at the calculated position
                createIconButton("Aqua_Chibi", aqua.transform, "AquaChibi.png",
                    new Vector3(160f, 160f), // size of the button
                    finalPos                 // final position in world space
                );
            }
        }

        private void createIconButton(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos)
        {
            // Create a new GameObject under the parent
            GameObject aquaChibiButton = Utils.creatGameObject(name, parent);
            if (aquaChibiButton == null) return;

            aquaChibiButton.transform.SetParent(parent);

            // Convert world position to local position relative to the parent
            aquaChibiButton.transform.position = worldPos;

            // Add and configure the Image component
            Image image = aquaChibiButton.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null) return;
            image.sprite = sprite;

            // Resize the image using utility function
            Utils.ImageResize(image, size);

            // Add button components
            Aqua_ChibiButton aquaChibi = aquaChibiButton.AddComponent<Aqua_ChibiButton>();
            aquaChibiButton.AddComponent<Aqua_ChibiButton_Script>();

            // Save reference to singleton instance
            Aqua_ChibiButton.instance = aquaChibi;

            aquaChibiButton.SetActive(true);
        }
    }
}