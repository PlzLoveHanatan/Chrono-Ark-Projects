using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using GameDataEditor;
using I2.Loc;

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
				PlayData.TSavedata.UseItemKeys.Add(ModItemKeys.Item_Consume_C_EmotionSystem_Fractured);
				return true;
			}
		}

		public class EntryTicket : UseitemBase
		{
			public override bool Use()
			{
				var list = new List<ItemBase>()
				{
					ItemBase.GetItem(ModItemKeys.Item_Potions_P_EmotionSystem_DarkTune),
					ItemBase.GetItem(ModItemKeys.Item_Potions_P_EmotionSystem_PureTune)
				};
				InventoryManager.Reward(list);
				PlayData.TSavedata.UseItemKeys.Add(ModItemKeys.Item_Consume_C_EmotionSystem_EntryTicket);
				return true;
			}
		}

		public class GoldenEntryTicket : UseitemBase
		{
			public override bool Use()
			{
				var list = new List<ItemBase>()
				{
					ItemBase.GetItem(ModItemKeys.Item_Potions_P_EmotionSystem_DarkTune),
					ItemBase.GetItem(ModItemKeys.Item_Potions_P_EmotionSystem_PureTune),
				};
				InventoryManager.Reward(list);
				PlayData.TSavedata.UseItemKeys.Add(ModItemKeys.Item_Consume_C_EmotionSystem_GoldenEntryTicket);

				var skill = new List<Skill>()
				{
					Skill.TempSkill(GDEItemKeys.Skill_S_Lucy_25, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam).CloneSkill()
				};

				if (skill != null)
				{
					FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skill, new SkillButton.SkillClickDel(LearnSkill),
						ScriptLocalization.System_Item.SkillAdd, false, true, true, true, false));
					MasterAudio.PlaySound("BookFlip", 1f);
				}
				
				return true;
			}

			public void LearnSkill(SkillButton Mybutton)
			{
				PlayData.TSavedata.LucySkills.Add(Mybutton.Myskill.MySkill.KeyID);
				UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();
			}
		}
	}
}
