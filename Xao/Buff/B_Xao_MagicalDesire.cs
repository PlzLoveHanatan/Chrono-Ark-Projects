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
namespace Xao
{
	/// <summary>
	/// Magical Desire
	/// </summary>
    public class B_Xao_MagicalDesire : Buff, IP_SkillUse_User
    {
        public override void BuffStat()
        {
            //PlusStat.Strength = true;
            PlusStat.dod = 3 * StackNum;
            PlusStat.cri = 3 * StackNum;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Xao_Combo.ComboChange();
                SelfStackDestroy();
            }
        }
    }
}