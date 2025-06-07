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
namespace Unica
{
	/// <summary>
	/// Double Down
	/// Next skill will cost 1 less.
	/// </summary>
    public class DoubleDown : Buff, IP_SkillUseHand_Team
    {
        public override void Init()
        {
            base.Init();
        }
        public override void BuffStat()
        {
            this.PlusStat.PlusMPUse.PlusMP_Skills = -1;
        }
        public void SKillUseHand_Team(Skill skill)
        {
            if (skill.Master == this.BChar)
            {
                base.SelfDestroy(false);
            }
        }
    }
}