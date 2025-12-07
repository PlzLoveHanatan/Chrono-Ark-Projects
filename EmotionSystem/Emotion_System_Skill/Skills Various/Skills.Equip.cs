using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace EmotionSystem
{
	public partial class Skills
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

		public class Fractured : UseitemBase
		{
			public override bool Use()
			{
				Utils.Data.WhetstoneCharge = 3;
				return true;
			}
		}
	}
}
