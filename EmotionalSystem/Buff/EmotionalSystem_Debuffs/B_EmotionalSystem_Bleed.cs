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
namespace EmotionalSystem
{
	/// <summary>
	/// Bleed
	/// </summary>
    public class B_EmotionalSystem_Bleed : Buff, IP_BuffObject_Updata
    {
        public int Bleed;

		public void BuffObject_Updata(BuffObject obj)
		{
			string num = Bleed.ToString();
			obj.StackText.text = num;
		}

		public override string DescExtended()
		{
			string text = ModLocalization.EmotionalSystem_Bleed_1;
			if (BChar is BattleEnemy)
			{
				text = ModLocalization.EmotionalSystem_Bleed_0;
			}
			return base.DescExtended().Replace("Description", text).Replace("&a", Bleed.ToString()).Replace("&b", (Bleed * 3).ToString());
		}

		public override void Init()
        {
            isStackDestroy = true;
        }

        public override void TurnUpdate()
        {
            BChar.Damage(BattleSystem.instance.DummyChar, (Bleed * 3), false, true, true, 0, false, false, false);

            if (Bleed >= 3)
            {
				Bleed -= (int)Math.Ceiling(Bleed / 3f);
			}
            else
            {
				Bleed = 1;
			}
        }
    }
}