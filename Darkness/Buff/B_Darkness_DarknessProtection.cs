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
namespace Darkness
{
	/// <summary>
	/// Darkness Protection
	/// </summary>
    public class B_Darkness_DarknessProtection : Buff, IP_TargetedAlly
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.Usestate_L != null)
            {
                if (base.Usestate_L.IsDead)
                {
                    base.SelfDestroy(false);
                    return;
                }
                this.PlusStat.Strength = base.Usestate_L.GetStat.Strength;
                this.PlusStat.def = base.Usestate_L.GetStat.def;
            }
        }


        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            bool flag = false;
            for (int i = 0; i < SaveTargets.Count; i++)
            {
                if (SaveTargets[i] == base.Usestate_L)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                for (int j = 0; j < SaveTargets.Count; j++)
                {
                    if (SaveTargets[j] == this.BChar)
                    {
                        SaveTargets[j] = base.Usestate_L;
                        EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
                        return null;
                    }
                }
            }
            return null;
        }
    }
}