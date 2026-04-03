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
using ChronoArkMod.ModData;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
namespace MiyukiSone
{
	public class MiyukiSone_ModDefinition : ModDefinition
	{
		public override Type ModItemKeysType => typeof(ModItemKeys);
		public override List<object> BattleSystem_ModIReturn()
		{
			var list = base.BattleSystem_ModIReturn();

			list.Add(new MiyukiDistortion());

			return list;
		}

		public class MiyukiDistortion : IP_BattleStart_Ones, IP_BattleEndRewardChange
		{
			public void BattleEndRewardChange()
			{
				List<ItemBase> rewardList = BattleSystem.instance.Reward;

				if (MiyukiDecides && rewardList.Count > 0)
				{
					var item = rewardList.RandomElement();
					MiyukiAffection state = MiyukiAffection.Kuudere;
					if (item != null)
					{
						if (IsYandere)
						{
							rewardList.Remove(item);
							state = MiyukiAffection.Yandere;
						}
						else if (IsDere)
						{
							rewardList.Add(item);
							state = MiyukiAffection.DereDere;
						}
						EventsData.MiyukiTextEvent(state);
						Debug.Log($"Random item is {item.itemkey} the amount is {item.StackCount}");
					}
				}
			}

			public void BattleStart(BattleSystem Ins)
			{
				if (MiyukiInParty)
				{
					//PlayData.TSavedata.Party.FirstOrDefault(c => c.KeyData == ModItemKeys.Character_Miyuki && c.Equip != null)?.Equip.OfType<Item_Equip>().Where(e => e.IsCurse).Select(e => { e.Curse = new EquipCurse(); return e; }).ToList();
					PlayData.TSavedata.Party.FirstOrDefault(c => c.KeyData == ModItemKeys.Character_Miyuki && c.Equip != null)?.Equip.OfType<Item_Equip>().Where(e => e.IsCurse)
						.ToList().ForEach(e => { e.Curse = new EquipCurse(); Events.CurseRandomEquip(); });
				}


				if (!MiyukiInParty)
				{
					if (MiyukiDecides)
					{
						(MiyukiDecides ? (Action)MiyukiPassive.PawsWithDeck : Events.YandereActionCut)();						
					}
					EventsData.MiyukiTextEvent(MiyukiAffection.Yandere);
				}
			}
		}
	}
}