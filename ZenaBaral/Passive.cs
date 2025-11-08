using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using UnityEngine;

namespace ZenaBaral
{
	public class Passive : Passive_Char, IP_PlayerTurn_1
	{
		public override void Init()
		{
			OnePassive = true;
		}

		public override void FixedUpdate()
		{
			int plusStat = BChar.EmotionLevel() * 5;
			PlusStat.PlusCriDmg = plusStat;
		}

		public void Turn1()
		{
			int skills = Utils.AllyTeam.Skills.Count;
			//Utils.AllyTeam.AP += 1;
			if (skills >= 6) return;

			BChar.StartCoroutine(ZenaDraw(skills));
		}

		private IEnumerator ZenaDraw(int skillNum = 0)
		{
			while (skillNum < 6 && Utils.AllyTeam.Skills_Deck.Count > 0)
			{
				Utils.AllyTeam.Draw();
				skillNum++;
			}
			yield break;
		}
	}
}
