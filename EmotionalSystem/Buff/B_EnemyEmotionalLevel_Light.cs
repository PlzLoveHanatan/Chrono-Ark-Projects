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
using System.ServiceModel.Configuration;
namespace EmotionalSystem
{
    /// <summary>
    /// Light
    /// Attack damage or heal amount increases by &a.
    /// Last until the next attack or heal.
    /// </summary>
    public class B_EnemyEmotionalLevel_Light : Buff, IP_DamageChange
    {
        public bool eternal = false;

        public const float ratio = 30;

        public override string DescExtended()
        {
            var str = base.DescExtended().Replace("&a", ratio.ToString());
            if (eternal)
            {
                int index = str.IndexOf("\n");
                return str.Substring(0, index);
            }
            return str;
        }

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (!eternal && !View && SkillD.IsDamage)
            {
                BattleSystem.DelayInputAfter(DelayDestory());
            }
            return (int)Misc.PerToNum(Damage, 100 + ratio);
        }

        private IEnumerator DelayDestory()
        {
            SelfDestroy();
            yield break;
        }
    }
}