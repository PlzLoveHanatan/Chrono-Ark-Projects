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

		public class MiyukiDistortion : IP_BattleStart_Ones
		{

			//public void BattleEnd()
			//{
			//	if (MiyukiInParty)
			//	{
			//		PlayData.TSavedata.Party.FindAll(c => c.Incapacitated).ForEach(c => { c.Incapacitated = false; c.Hp = c.get_stat.maxhp / 2; });
			//		Affection.GetRandomAffection();
			//	}
			//}

			public void BattleStart(BattleSystem Ins)
			{
				if (MiyukiInParty)
				{
					//PlayData.TSavedata.Party.FirstOrDefault(c => c.KeyData == ModItemKeys.Character_Miyuki && c.Equip != null)?.Equip.OfType<Item_Equip>().Where(e => e.IsCurse).Select(e => { e.Curse = new EquipCurse(); return e; }).ToList();
					PlayData.TSavedata.Party.FirstOrDefault(c => c.KeyData == ModItemKeys.Character_Miyuki && c.Equip != null)?.Equip.OfType<Item_Equip>().Where(e => e.IsCurse)
						.ToList().ForEach(e => { e.Curse = new EquipCurse(); Events.CurseRandomEquip(); });
				}
				else if (Affection.MiyukiDecides)
				{
					Events.YandereActionCut();
					EventsData.MiyukiTextEvent(MiyukiAffection.Yandere);
					if (Affection.MiyukiDecides) UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
				}
			}
		}
	}
}