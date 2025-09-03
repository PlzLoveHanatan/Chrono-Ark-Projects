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
using System.Security.Cryptography;
namespace Darkness
{
    /// <summary>
    /// Battle Prep
    /// Draw 3 skills and gain &a barrier
    /// </summary>
    public class S_Darkness_LucyDraw : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveDarkness = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Darkness);
            int drawNum = 0;

            if (aliveDarkness != null)
            {
                Utils.PlayDarknessSound(MySkill.MySkill.KeyID);
                BChar.MyTeam.partybarrier.BarrierHP += (int)(aliveDarkness.GetStat.maxhp * 0.5f);
                drawNum = 3;
            }
            else
            {
                drawNum = 1;
                MySkill.isExcept = true;
            }
            BattleSystem.instance.AllyTeam.Draw(drawNum);
        }
    }
}