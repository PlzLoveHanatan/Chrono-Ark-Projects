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
namespace Urunhilda
{
    /// <summary>
    /// Golden Beastkin Fortune
    /// </summary>
    public class S_Urunhilda_LucyDraw_0 : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
        }

        private readonly List<string> UrunhildaRandomSkillSelection = new List<string>
        {
            ModItemKeys.Skill_S_Urunhilda_LucyDraw_1,
            ModItemKeys.Skill_S_Urunhilda_LucyDraw_2,
            ModItemKeys.Skill_S_Urunhilda_LucyDraw_3,
            ModItemKeys.Skill_S_Urunhilda_LucyDraw_4,
        };

        private readonly List<string> GoldenBeastkinSkills = new List<string>
        {
            ModItemKeys.Skill_S_Urunhilda_BeatskinFluffyTease_0,
            ModItemKeys.Skill_S_Urunhilda_BeatskinLustfulHand_0,
            ModItemKeys.Skill_S_Urunhilda_GoldenRide_0,
            ModItemKeys.Skill_S_Urunhilda_GoldenStrokingFeet_0,
            ModItemKeys.Skill_S_Urunhilda_GoldenTwistedPleasure_0,
            ModItemKeys.Skill_S_Urunhilda_SelfExposing,
        };

        private readonly List<string> SuccubusSkills = new List<string>
        {
            ModItemKeys.Skill_S_Urunhilda_SuccubusDrillingFeet_0,
            ModItemKeys.Skill_S_Urunhilda_SuccubusDrillingHand_0,
            ModItemKeys.Skill_S_Urunhilda_SuccubusRidingPussy_0,
        };

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<string> urunhildaNewList = new List<string>();
            urunhildaNewList.AddRange(UrunhildaRandomSkillSelection);
            Skill randomSkil = Skill.TempSkill(urunhildaNewList.Random(this.BChar.GetRandomClass().Main), this.MySkill.Master, this.MySkill.Master.MyTeam);
            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkil }, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
        }

        public void Selection(SkillButton Mybutton)
        {
            BattleSystem.DelayInput(RandomSkillSelect(Mybutton));
        }

        private Skill GetRandomSkillFromList(List<string> skillList, BattleChar Bchar)
        {
            if (skillList == null || skillList.Count == 0 || Bchar == null)
                return null;

            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, skillList.Count);
            string skillKey = skillList[index];
            return Skill.TempSkill(skillKey, Bchar, Bchar.MyTeam);
        }

        private IEnumerator RandomSkillSelect(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;
            Skill skill = null;
            var team = BattleSystem.instance.AllyTeam;
            var urunhilda = team.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == ModItemKeys.Character_Urunhilda);

            int draw = 0;

            if (key == ModItemKeys.Skill_S_Urunhilda_LucyDraw_1)
            {
                draw = 2;
                skill = GetRandomSkillFromList(GoldenBeastkinSkills, urunhilda);
            }
            else if (key == ModItemKeys.Skill_S_Urunhilda_LucyDraw_2)
            {
                draw = 2;
                skill = GetRandomSkillFromList(SuccubusSkills, urunhilda);
            }
            else if (key == ModItemKeys.Skill_S_Urunhilda_LucyDraw_3)
            {
                draw = 3;
                foreach (var ally in team.AliveChars)
                {
                    if (ally != null)
                    {
                        Utils.AddBuff(ally, ModItemKeys.Buff_B_Urunhilda_EcstasyRush_0, 1);
                        Utils.AddBuff(ally, ModItemKeys.Buff_B_Urunhilda_LustfulDesire_0, 2);
                        ally.Heal(BattleSystem.instance.DummyChar, 3, false, true, null);

                        Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Urunhilda_DummyHeal, urunhilda, urunhilda.MyTeam);
                        healingParticle.PlusHit = true;
                        healingParticle.FreeUse = true;

                        urunhilda.ParticleOut(healingParticle, ally);
                    }
                }
                Utils.AddBuff(urunhilda, ModItemKeys.Buff_B_Urunhilda_RuttingInstinct, 1);
            }
            else if (key == ModItemKeys.Skill_S_Urunhilda_LucyDraw_4)
            {
                draw = 3;
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 500));
                Utils.IncreaseArkPassiveNum(1);
            }

            if (skill != null)
            {
                Utils.CreateSkill(skill.MySkill.KeyID, skill.Master, true, true, 2);
            }

            team.Draw(draw);

            yield return null;
        }
    }
}