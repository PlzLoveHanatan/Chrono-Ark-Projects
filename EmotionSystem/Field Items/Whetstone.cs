using System;
using DarkTonic.MasterAudio;
using GameDataEditor;
using UnityEngine;
using Scrolls;

namespace EmotionSystem
{
	public class Whetstone : UseitemBase
	{
		public override string DescExtended(string desc)
		{
			return base.DescExtended(desc).Replace("&a", Utils.Data.WhetstoneCharge.ToString());
		}

		public override bool Use()
		{
			if (Utils.Data.WhetstoneCharge >= 1)
			{
				UIManager.InstantiateActiveAddressable(new GDEGameobjectDatasData(GDEItemKeys.GameobjectDatas_GUI_ItemEnchant).Gameobject_Path,
					AddressableLoadManager.ManageType.None).GetComponent<UI_Enchant>().UseItem = this.MyItem;

				return false;
			}
			return false;
		}
	}
}