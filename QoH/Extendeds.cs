using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace QoH
{
	public class Extendeds
	{
		public class Hysteria : Skill_Extended
		{
			public override bool Terms()
			{
				bool canUseAttack = false;
				var sanity = Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_QoH_Sanity) as Buffs.QoHSanity;
				if (sanity != null)
				{
					canUseAttack = !sanity.MagicalGirlMode;
				}
				return canUseAttack && MySkill.Master.Info.KeyData == ModItemKeys.Character_QoH;
			}
		}
	}
}
