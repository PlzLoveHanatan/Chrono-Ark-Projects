using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace EmotionalSystem
{
	public class EmotionalSystem_Scripts
	{
		public static void ChargeLucyNeck(BattleChar bchar)
		{
			var necklaceKeys = new[]
			{
				GDEItemKeys.Item_Active_LucysNecklace,
				GDEItemKeys.Item_Active_LucysNecklace2,
				GDEItemKeys.Item_Active_LucysNecklace3,
				GDEItemKeys.Item_Active_LucysNecklace4
			};

			if (PartyInventory.InvenM.InventoryItems.FirstOrDefault(a => a != null && necklaceKeys.Contains(a.itemkey)) is Item_Active neck)
			{
				neck.ChargeNow++;
			}

			GameObject gameObject = Misc.UIInst(bchar.BattleInfo.EffectViewOb);
			if (bchar.Info.Ally)
			{
				gameObject.transform.position = bchar.GetPos();
			}
			else
			{
				gameObject.transform.position = bchar.GetTopPos();
			}
			gameObject.GetComponent<EffectView>().TextOut(bchar.Info.Ally, "Recharged");
		}

		public static void AttackRedirect(BattleChar bchar, Skill skillD, List<BattleChar> targets, int damage = 0)
		{
			var newTargets = Utils.AllyTeam.AliveChars.Where(a => a != null && a != bchar)
				.Concat(Utils.EnemyTeam.AliveChars.Where(e => e != null)).ToList();

			if (newTargets.Count == 0) return;

			int index = RandomManager.RandomInt(bchar.GetRandomClass().Target, 0, newTargets.Count);
			var randomTarget = newTargets[index];

			if (skillD.IsDamage && skillD.Master == bchar && !skillD.FreeUse && !skillD.PlusHit)
			{
				targets.Clear();

				if (skillD.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
				{
					foreach (var enemy in Utils.EnemyTeam.AliveChars)
					{
						if (enemy != randomTarget)
						{
							Utils.AddBuff(bchar, enemy, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_1);
						}
					}

					targets.Add(randomTarget);
					randomTarget?.Damage(bchar, damage, false, false);

					Utils.EnemyTeam.AliveChars.ForEach(e => Utils.RemoveBuff(e, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_1, true));
				}

				else if (randomTarget != null)
				{
					targets.Add(randomTarget);
				}
			}
		}
	}
}
