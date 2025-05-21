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
    public class B_HeavensEmbrace : Buff, IP_SkillUse_User
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(Usestate_F.GetStat.reg * 0.4f * StackNum)).ToString());
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                BarrierHP += (int)(Usestate_F.GetStat.reg * 0.4f * StackNum);
            }
        }
    }
}