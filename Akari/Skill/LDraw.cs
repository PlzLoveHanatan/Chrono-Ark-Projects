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
using Spine.Unity;
namespace Akari
{
    public class LDraw : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool flag = false;
            using (List<BattleChar>.Enumerator enumerator = BattleSystem.instance.AllyTeam.AliveChars.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Info.KeyData == ModItemKeys.Character_Akari)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                BattleSystem.instance.AllyTeam.Draw(2);
                return;
            }

            base.SkillUseSingle(SkillD, Targets);
            BattleSystem.instance.AllyTeam.Draw(2);
            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill>
        {
            Skill.TempSkill(ModItemKeys.Skill_LDraw_0, this.BChar, this.BChar.MyTeam),
            Skill.TempSkill(ModItemKeys.Skill_LDraw_1, this.BChar, this.BChar.MyTeam)
        }, new SkillButton.SkillClickDel(this.Del), "", false, false, true, false, true));
        }
        private void Del(SkillButton Mybutton)
        {
            BattleChar battleChar = null;
            foreach (BattleChar battleChar2 in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (battleChar2.Info.KeyData == ModItemKeys.Character_Akari)
                {
                    battleChar = battleChar2;
                    break;
                } 
            }
            if (battleChar == null)
            {
                return;
            }

            if (Mybutton.Myskill.MySkill.KeyID == ModItemKeys.Skill_LDraw_0)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }
            if (Mybutton.Myskill.MySkill.KeyID == ModItemKeys.Skill_LDraw_1)
            {
                MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);

                Utils.CreateRandomAmmunition(BChar, 2);
            }
        }
    }
}
                    