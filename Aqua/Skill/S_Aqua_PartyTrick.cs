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
using DG.Tweening;
using NLog.Targets;
using Steamworks;
namespace Aqua
{
    /// <summary>
    /// Party Trick
    /// </summary>
    public class S_Aqua_PartyTrick : Skill_Extended, IP_DamageChange
    {
        private bool Vanish = true;
        public override void Init()
        {
            OnePassive = true;
        }

        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.5f)).ToString());
        }

        private readonly List<string> AquaRandomSkillSelection = new List<string>
        {
            ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty,
            ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty,
            ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick,
            ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick,
            ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill,
            ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant,
            ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket,
        };

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars;

            if (!Vanish && enemies.Count > 0)
            {
                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, enemies.Count);
                BattleChar randomEnemy = enemies[randomIndex];

                if (randomEnemy is BattleEnemy enemy && enemy.Boss ||
                    randomEnemy.Info.KeyData == GDEItemKeys.Enemy_TrialofStrength_Enemy ||
                    randomEnemy.Info.KeyData == GDEItemKeys.Enemy_TrialofBrave_Enemy1)
                {
                    randomEnemy.Damage(BChar, 40, false, true, false, 0, false, false, false);
                }
                else
                {
                    randomEnemy.HPToZero();
                }
            }

            return Damage;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            List<string> aquaSkills = new List<string>();
            aquaSkills.AddRange(AquaRandomSkillSelection);

            Skill randomSkil = Skill.TempSkill(aquaSkills.Random(this.BChar.GetRandomClass().Main), this.MySkill.Master, this.MySkill.Master.MyTeam);
            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkil }, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
        }

        public void Selection(SkillButton Mybutton)
        {
            BattleSystem.DelayInput(RandomSkillSelect(Mybutton));
        }

        private IEnumerator RandomSkillSelect(SkillButton Mybutton)
        {
            string skillId = Mybutton.Myskill.MySkill.KeyID;
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
            var allTargets = allies.Concat(enemies).ToList();
            var stun = GDEItemKeys.Buff_B_Common_Rest;

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty);
                }

                foreach (BattleChar ally in allies)
                {
                    ally.Heal(BattleSystem.instance.DummyChar, 5f, false, true, null);
                    ally.BuffAdd("B_PopcornGirl_Lucy_1", BattleSystem.instance.AllyTeam.LucyChar, false, 0, false, -1, false);

                    Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, this.BChar, this.BChar.MyTeam);
                    healingParticle.PlusHit = true;
                    healingParticle.FreeUse = true;

                    this.BChar.ParticleOut(healingParticle, ally);
                }
            }

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty);
                }

                foreach (BattleChar enemy in enemies)
                {
                    enemy.BuffAdd(stun, this.BChar, false, 150, false, -1, false);
                }
            }

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick);
                }

                Vanish = false;

                Skill dummySkill = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick, this.BChar, this.BChar.MyTeam);
                BattleChar dummyTarget = BattleSystem.instance.EnemyTeam.AliveChars.FirstOrDefault();
                int dummyDamage = 1;
                bool dummyCri = false;

                int result = DamageChange(dummySkill, dummyTarget, dummyDamage, ref dummyCri, false);
            }

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick);
                }

                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, allTargets.Count);
                BattleChar randomTarget = allTargets[randomIndex];

                randomTarget.BuffAdd(ModItemKeys.Buff_B_Aqua_UnstablePosture, this.BChar, false, 0, false, -1, false);
                Skill Telekinesis = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick, this.BChar, this.BChar.MyTeam);
                randomTarget.Damage(BChar, 10, false, true, false, 0, false, false, false);
                Telekinesis.PlusHit = true;
                Telekinesis.FreeUse = true;

                BChar.ParticleOut(Telekinesis, randomTarget);
            }

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill);
                }

                for (int i = 0; i < 3; i++)
                {
                    foreach (var target in allTargets)
                    {
                        target.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
                        target.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, this.BChar, false, 0, false, -1, false);

                        if (target.Info.Ally)
                        {
                            target.BuffAdd(ModItemKeys.Buff_B_Aqua_Type100RecoveryMode, this.BChar, false, 0, false, -1, false);
                        }
                    }
                }
            }

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant);
                }

                InventoryManager.Reward(InventoryManager.RewardKey(GDEItemKeys.Reward_R_GetPotion, false));
            }

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket);
                }

                int roll = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, 101);

                if (roll < 50)
                {
                    InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter, 1));
                }
                else if (roll < 60)
                {
                    InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity, 1));
                }
                else if (roll < 85)
                {
                    InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare, 1));
                }
                else if (roll < 90)
                {
                    InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy, 1));
                }
                else
                {
                    InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy_Rare, 1));
                }
            }

            yield break;
        }
    }
}