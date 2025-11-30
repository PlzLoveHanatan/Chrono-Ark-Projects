using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Equip
	{
		public class Literature
		{
			public class Umbrella : EquipBase, IP_DamageTake
			{
				public override void Init()
				{
					PlusStat.AggroPer = 100;
				}

				public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
				{
					bool alwaysLucky = RandomManager.RandomPer(BChar.GetRandomClass().Target, 100, 50);

					if (alwaysLucky && !resist)
					{
						resist = true;
					}
				}
			}

			public class Rags : EquipBase, IP_PlayerTurn
			{
				public override void Init()
				{
					PlusStat.DMGTaken = -10;
					PlusStat.Strength = true;
				}

				public void Turn()
				{
					Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_LiteratureLv3_LovingFamily_0);
				}
			}
		}
	}
}
