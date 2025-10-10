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
	/// Lament
	/// </summary>
	public class B_Abnormality_TechnologicalLv1_Lament : Buff, IP_TurnEnd
	{
		public override string DescExtended()
		{
			int damage = (int)(BChar.GetStat.maxhp * 0.2f);
			return base.DescExtended().Replace("&a", damage.ToString());
		}

		public override void Init()
		{
			PlusPerStat.Damage = 20;
		}

		public void TurnEnd()
		{
			Utils.PlaySound("Floor_Technological_Lament");
			int damage = (int)(BChar.GetStat.maxhp * 0.2f);
			BChar.Damage(BChar, damage, false, true, false, 0, false, false, false);
		}
	}
}