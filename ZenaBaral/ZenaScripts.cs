using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace ZenaBaral
{
	public class ZenaScripts
	{
		public static void PlaySounds(string key)
		{
			if (!ZenaUtils.HeadSFX || string.IsNullOrEmpty(key)) return;

			Utils.PlaySound(key);
		}

		public static void Selection(SkillButton button, bool isLucySkill = false)
		{
			var key = button.Myskill;
			Utils.RemoveSkill(key);
			BattleSystem.instance.ActWindow.Draw(Utils.AllyTeam, false);
			Utils.AllyTeam.AP += key.MySkill.UseAp * 2;

			if (isLucySkill)
			{
				PlaySounds("Zena_Lucy_Directive");
			}
		}

		public static void IncreaseStats(Character character)
		{
			character.OriginStat.PlusCriDmg += 5;
		}
	}
}
