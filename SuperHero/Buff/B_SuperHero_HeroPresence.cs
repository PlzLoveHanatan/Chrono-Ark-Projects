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
namespace SuperHero
{
	/// <summary>
	/// Hero Presence
	/// Cannot take action.
	/// </summary>
    public class B_SuperHero_HeroPresence : Buff, IP_Awake
    {
        private int StunDuration;
        public override string DescExtended()
        {
            int turnsLeft = Mathf.Max(0, 2 - StunDuration);
            int turnsLeftEnemy = Mathf.Max(0, 1 - StunDuration);
            if (turnsLeft == 0)
                return base.DescExtended();

            string value = turnsLeft.ToString();
            string valueEnemy = turnsLeftEnemy.ToString();
            if (!this.View && this.BChar != null && this.BChar.Info.Ally)
            {
                return base.DescExtended() + "\n" + string.Format(ModLocalization.SuperHero_Stun, this.BChar.Info.Name).Replace("&a", value);
            }
            if (!this.View && this.BChar != null && !this.BChar.Info.Ally)
            {
                return base.DescExtended() + "\n" + string.Format(ModLocalization.SuperHero_Stun_Enemy).Replace("&a", valueEnemy);
            }
            return base.DescExtended();
        }

        public override void TurnUpdate()
        {
            StunDuration++;
            if (StunDuration >= 1 && !BChar.Info.Ally)
            {
                Destr();
            }
            else if (StunDuration >= 2)
            {
                Destr();
            }
        }

        public void Destr()
        {
            base.SelfDestroy(false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
        }

        public override void Init()
        {
            this.PlusStat.Stun = true;
            this.NoShowTimeNum_Tooltip = true;
        }

        public void Awake()
        {
            StunDuration = 0;
        }
    }
}