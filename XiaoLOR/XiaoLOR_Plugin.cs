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
using HarmonyLib;
using EmotionalSystem;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;

namespace XiaoLOR
{
    public class XiaoLOR_Plugin : ChronoArkPlugin
    {
        public const string modname = "XiaoLOR";

        public const string version = "1.0";

        public const string author = "Midana";

        Harmony harmony = new Harmony("XiaoLOR");

        public override void Dispose()
        {
            if (harmony != null)
            {
                harmony.UnpatchSelf();
            }
        }

        public override void Initialize()
        {
            try
            {
                harmony.PatchAll();
            }
            catch (Exception e)
            {
                Debug.Log("XiaoLOR: Patch Catch: " + e.ToString());
            }
        }

		public static bool XiaoInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_XiaoLOR);
		}

		[HarmonyPatch(typeof(FieldSystem), "StageStart")]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				if (XiaoInParty())
				{
					EnsureXiaoEquip();

					var faintResist = PlayData.TSavedata.GetCustomValue<XiaoLOR_Value>();

					if (PlayData.TSavedata.StageNum >= 0)
					{
						if (XiaoUtils.LiuAssociationEquip && !HasXiaoEquip())
						{
							PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Item_XiaoLOR_LiáoFēng));
							PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Item_XiaoLOR_YànLiánZhuāng));
							PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Passive_Relic_XiaoLOR_YíHànZhīShí));
							SetXiaoEquipFlag(true);
						}

						if (faintResist.IncreaseFaintResist)
						{
							XiaoLOR_Scripts.IncreaseStats(XiaoUtils.XiaoChar);
							faintResist.IncreaseFaintResist = false;
						}
					}
				}
			}
		}

		public static bool HasXiaoEquip() => PlayData.TSavedata.GetCustomValue<XiaoLOR_Value>()?.GainXiaoEquip ?? false;

		public static void EnsureXiaoEquip()
		{
			var equip = PlayData.TSavedata.GetCustomValue<XiaoLOR_Value>();
			if (equip == null)
			{
				equip = new XiaoLOR_Value { GainXiaoEquip = false };
				PlayData.TSavedata.AddCustomValue(equip);
			}
		}

		public static void SetXiaoEquipFlag(bool value)
		{
			var equip = PlayData.TSavedata.GetCustomValue<XiaoLOR_Value>();
			if (equip == null)
			{
				equip = new XiaoLOR_Value();
				PlayData.TSavedata.AddCustomValue(equip);
			}
			equip.GainXiaoEquip = value;
		}
	}
}