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
namespace Raphi
{
	/// <summary>
	/// When played from hand, discard a random skill and draw 2 additional skills next turn.  
	/// Sheathe: Randomly activate one of the following 5 effects:  
	/// 1. Draw skills equal to the cost of this skill (Max 2).  
	/// 2. Restore Mana equal to the cost of this skill (Max 2).  
	/// 3. Heal all allies and gain <color=#7B68EE>Celestial Connection</color>.  
	/// 4. Heal all allies and apply 1 stack of <color=#D2691E>Heaven's Touch</color> to all allies.  
	/// 5. Create a party barrier (&a) equal to 1.5x user's Healing Power. 
	/// </summary>
    public class CapriciousBlessing : SkillExtedned_IlyaP
    {
        private readonly List<string> normalSkills = new List<string>
        {
            //ModItemKeys.Skill_CapriciousBlessing_1,
            //ModItemKeys.Skill_CapriciousBlessing_2,
            ModItemKeys.Skill_CapriciousBlessing_3,
            //ModItemKeys.Skill_CapriciousBlessing_4,
            //ModItemKeys.Skill_CapriciousBlessing_5
        };

        private readonly List<string> summerSkills = new List<string>
        { 
            ModItemKeys.Skill_CapriciousBlessingSummer_1,
            ModItemKeys.Skill_CapriciousBlessingSummer_2,
            ModItemKeys.Skill_CapriciousBlessingSummer_3,
            ModItemKeys.Skill_CapriciousBlessingSummer_4,
            ModItemKeys.Skill_CapriciousBlessingSummer_5
        };

        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.reg * 1.5)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_AngelsWhisper, BChar, false, 0, false, -1, false);
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_AngelsWhisper, BChar, false, 0, false, -1, false);

            List<Skill> list = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            if (list.Count == 0) return;

            int num = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, list.Count);
            list[num].Delete(false);
        }

        public override void IlyaWaste()
        {
            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "SummerRaphiel"))
            {
                List<string> raphiSkills = new List<string>();
                raphiSkills.AddRange(summerSkills);

                Skill randomSkill = Skill.TempSkill(raphiSkills.Random(BChar.GetRandomClass().Main), MySkill.Master, MySkill.Master.MyTeam);
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkill }, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
            }
            else
            {
                List<string> raphiSkills = new List<string>();
                raphiSkills.AddRange(normalSkills);

                Skill randomSkill = Skill.TempSkill(raphiSkills.Random(BChar.GetRandomClass().Main), MySkill.Master, MySkill.Master.MyTeam);
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkill }, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
            }
        }


        public void Selection(SkillButton Mybutton)
        {
            BattleSystem.DelayInput(RandomEffectSelect(Mybutton));
        }

        private IEnumerator RandomEffectSelect(SkillButton Mybutton)
        {
            string funnyButton = Mybutton.Myskill.MySkill.KeyID;

            if (funnyButton == ModItemKeys.Skill_CapriciousBlessing_1 || funnyButton == ModItemKeys.Skill_CapriciousBlessingSummer_1)
            {
                int num = Math.Min(MySkill.AP, 2);
                BattleSystem.instance.AllyTeam.Draw(num);
            }

            if (funnyButton == ModItemKeys.Skill_CapriciousBlessing_2 || funnyButton == ModItemKeys.Skill_CapriciousBlessingSummer_2)
            {
                int num = Math.Min(MySkill.AP, 2);
                BattleSystem.instance.AllyTeam.AP += num;
                yield return new WaitForSeconds(1f);
            }

            if (funnyButton == ModItemKeys.Skill_CapriciousBlessing_3 || funnyButton == ModItemKeys.Skill_CapriciousBlessingSummer_3)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_CapriciousBlessing, BChar, BChar.MyTeam);
                skill.PlusHit = true;
                skill.FreeUse = true;
                BChar.ParticleOut(skill, BattleSystem.instance.AllyTeam.AliveChars);

                if (BChar.Info.KeyData == ModItemKeys.Character_Raphi)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection, BChar, false, 0, false, -1, false);
                }

                else
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection_0, BChar, false, 0, false, -1, false);
                }
            }

            if (funnyButton == ModItemKeys.Skill_CapriciousBlessing_4 || funnyButton == ModItemKeys.Skill_CapriciousBlessingSummer_4)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_CapriciousBlessing, BChar, BChar.MyTeam);
                skill.PlusHit = true;
                skill.FreeUse = true;
                BChar.ParticleOut(skill, BattleSystem.instance.AllyTeam.AliveChars);

                foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    battleChar.BuffAdd(ModItemKeys.Buff_B_HeavensTouch, BChar, false, 0, false, -1, false);
                }
            }

            if (funnyButton == ModItemKeys.Skill_CapriciousBlessing_5 || funnyButton == ModItemKeys.Skill_CapriciousBlessingSummer_5)
            {
                BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.reg * 1.5f);
            }
        }
    }
}