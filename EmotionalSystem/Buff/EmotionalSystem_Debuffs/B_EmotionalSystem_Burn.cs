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
    public class B_EmotionalSystem_Burn : Buff, IP_TurnEnd, IP_BuffObject_Updata
	{
		public int Burn;

		public void BuffObject_Updata(BuffObject obj)
		{
			string num = Burn.ToString();
			obj.StackText.text = num;
		}

		public override string DescExtended()
		{
			string text = ModLocalization.EmotionalSystem_Burn_1;
			if (BChar is BattleEnemy)
			{
				text = ModLocalization.EmotionalSystem_Burn_0;
			}
			return base.DescExtended().Replace("Description", text).Replace("&a", Burn.ToString()).Replace("&b", (Burn * 2).ToString());
		}

		public override void Init()
		{
			OnePassive = true;
			isStackDestroy = true;
		}

		public void TurnEnd()
		{
			BChar.Damage(BattleSystem.instance.DummyChar, Burn * 2, false, true, true, 0, false, false, false);

			if (Burn >= 3)
			{
				Burn -= Burn / 3;
			}
			else
			{
				Burn = 1;
			}
		}
	}
}