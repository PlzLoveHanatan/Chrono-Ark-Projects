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

            string plusSkillView = ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty;

            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Aqua_Bunny_Skin_H"))
            {
                plusSkillView = ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty_H;
            }
            else if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Aqua_Bunny_Skin_R"))
            {
                plusSkillView = ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty_R;
            }

            GDESkillData gdeskillData = new GDESkillData(MySkill.MySkill.KeyID);
            MySkill.Init(gdeskillData, BChar, BChar.MyTeam);
            MySkill.MySkill.PlusSkillView = plusSkillView;
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
        
        private readonly List<string> AquaRandomSkillSelectionH = new List<string>
        {
            ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty_H,
            ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty_H,
            ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick_H,
            ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick_H,
            ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill_H,
            ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant_H,
            ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket_H,
        };

        private readonly List<string> AquaRandomSkillSelectionR = new List<string>
        {
            ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty_R,
            ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty_R,
            ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick_R,
            ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick_R,
            ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill_R,
            ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant_R,
            ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket_R,
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
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.KeyData == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            List<string> aquaSkills = new List<string>();


            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Aqua_Bunny_Skin_H"))
            {
                aquaSkills.AddRange(AquaRandomSkillSelectionH);
            }
            else if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Aqua_Bunny_Skin_R"))
            {
                aquaSkills.AddRange(AquaRandomSkillSelectionR);
            }
            else
            {
                aquaSkills.AddRange(AquaRandomSkillSelection);
            }

            Skill randomSkil = Skill.TempSkill(aquaSkills.Random(this.BChar.GetRandomClass().Main), this.MySkill.Master, this.MySkill.Master.MyTeam);
            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkil }, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
        }

        public void Selection(SkillButton Mybutton)
        {
            BattleSystem.DelayInput(RandomSkillSelect(Mybutton));
        }

        private IEnumerator RandomSkillSelect(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
            var allTargets = allies.Concat(enemies).ToList();
            var stun = GDEItemKeys.Buff_B_Common_Rest;

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty || key == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty_R)
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

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty || key == ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty_R)
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

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick || key == ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick_R)
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

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick || key == ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick_R)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick);
                }

                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, enemies.Count);
                BattleChar randomTarget = enemies[randomIndex];

                randomTarget.BuffAdd(ModItemKeys.Buff_B_Aqua_UnstablePosture, this.BChar, false, 0, false, -1, false);
                Skill Telekinesis = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick, this.BChar, this.BChar.MyTeam);
                randomTarget.Damage(BChar, 10, false, true, false, 0, false, false, false);
                Telekinesis.PlusHit = true;
                Telekinesis.FreeUse = true;

                BChar.ParticleOut(Telekinesis, randomTarget);
            }

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill || key == ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill_R)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill);
                }

                for (int i = 0; i < 3; i++)
                {
                    foreach (var target in allTargets)
                    {
                        string auqaVeil = target.Info.Ally ? ModItemKeys.Buff_B_Aqua_AquaVeil_0 : ModItemKeys.Buff_B_Aqua_AquaVeil;
                        target.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
                        target.BuffAdd(auqaVeil, this.BChar, false, 0, false, -1, false);

                        if (target.Info.Ally)
                        {
                            target.BuffAdd(ModItemKeys.Buff_B_Aqua_Type100RecoveryMode, this.BChar, false, 0, false, -1, false);
                        }
                    }
                }
            }

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant || key == ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant_R)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant);
                }

                InventoryManager.Reward(InventoryManager.RewardKey(GDEItemKeys.Reward_R_GetPotion, false));
            }

            if (key == ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket || key == ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket_H || key == ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket_R)
            {
                if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
                {
                    Utils.PlaySound(ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket);
                }

                int roll = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, 101);

                foreach (var entry in RewardTable)
                {
                    if (roll < entry.Threshold)
                    {
                        GainReward(entry.RewardId);
                        break;
                    }
                }
            }
            yield break;
        }

        private struct RewardChance
        {
            public int Threshold;
            public string RewardId;
        }

        private readonly RewardChance[] RewardTable =
        {
            new RewardChance { Threshold = 40, RewardId = GDEItemKeys.Item_Consume_SkillBookCharacter },
            new RewardChance { Threshold = 50, RewardId = GDEItemKeys.Item_Consume_SkillBookInfinity },
            new RewardChance { Threshold = 75, RewardId = GDEItemKeys.Item_Consume_SkillBookCharacter_Rare },
            new RewardChance { Threshold = 80, RewardId = GDEItemKeys.Item_Consume_SkillBookLucy },
            new RewardChance { Threshold = 100, RewardId = GDEItemKeys.Item_Consume_SkillBookLucy_Rare }
        };

        public void GainReward(string reward)
        {
            InventoryManager.Reward(ItemBase.GetItem(reward));
        }
    }
}