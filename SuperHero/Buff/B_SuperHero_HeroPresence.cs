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
    public class B_SuperHero_HeroPresence : Buff, IP_Discard
    {
        public override string DescExtended()
        {
            if (!this.View && this.BChar != null && this.BChar.Info.Ally)
            {
                return base.DescExtended() + "\n" + string.Format(ScriptLocalization.UI_Battle_Buff.RestBuffDesc, this.BChar.Info.Name);
            }
            return base.DescExtended();
        }

        public override void TurnUpdate()
        {
            if (!this.BChar.Info.Ally)
            {
                this.Destr();
                return;
            }
            if (!this.flag)
            {
                this.flag = true;
                return;
            }
            this.Destr();
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

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (skill.Master == this.BChar)
            {
                this.Destr();
            }
        }

        public bool flag;
    }
}