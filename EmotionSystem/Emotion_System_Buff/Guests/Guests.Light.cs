using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace EmotionSystem
{
	public partial class Guests
	{
		public class Light : Buff, IP_DamageChange
		{
			public bool eternal = false;
			public override string DescExtended()
			{
				int damage = BChar.EmotionLevel() * 5;
				var str = base.DescExtended().Replace("&a", damage.ToString());
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
				int damage = BChar.EmotionLevel() * 5;
				return (int)Misc.PerToNum(Damage, 100 + damage);
			}

			private IEnumerator DelayDestory()
			{
				SelfDestroy();
				yield break;
			}
		}
	}
}
