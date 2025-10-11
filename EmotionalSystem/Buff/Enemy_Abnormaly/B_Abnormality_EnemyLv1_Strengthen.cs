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
using EmotionSystem;
namespace EmotionalSystem
{
	/// <summary>
	/// Strengthen
	/// Attack increased by &a <color=#FF7C34>(Emotional Level * 5)</color>.
	/// </summary>
    public class B_Abnormality_EnemyLv1_Strengthen : Buff, IP_EmotionLvUpBefore
    {
		public override void BuffStat()
		{
			PlusPerStat.Damage = 5 * BChar.EmotionLevel();
		}

		public override string DescExtended()
		{
			return base.DescExtended().Replace("&a", BChar.EmotionLevel().ToString());
		}

		public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
		{
			if (charEmotion == BChar)
			{
				BuffStat();
			}
		}
	}
}