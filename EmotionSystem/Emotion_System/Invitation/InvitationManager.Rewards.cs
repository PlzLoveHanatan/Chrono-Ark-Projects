using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace EmotionSystem
{
	public partial class InvitationManager
	{
		public readonly List<ItemBase> ReceptionRewards = new List<ItemBase>();

		public void PrepareReceptionRewards(int totalRewards = 1)
		{
			ReceptionRewards.Clear();

			int soulStones = 2;
			int timeMoney = PlayData.TSavedata.StageNum + 2;

			AddReward(GDEItemKeys.Item_Misc_TimeMoney, timeMoney, totalRewards);
			AddReward(GDEItemKeys.Item_Misc_Soul, soulStones, totalRewards);
			AddReward(GDEItemKeys.Item_Consume_SkillBookCharacter, 1, totalRewards);
			AddReward(ModItemKeys.Item_Consume_C_EmotionSystem_DreamingCurrent, 1);

			if (PlayData.TSavedata.StageNum <= 4)
			{
				AddReward(GDEItemKeys.Item_Consume_FriendShipPouch, 1, totalRewards);
			}

			if (RandomManager.RandomPer(RandomClassKey.BattleClear, 100, 25))
			{
				AddReward(GDEItemKeys.Item_Misc_Item_Key, 1, totalRewards);
			}

			if (RandomManager.RandomPer(RandomClassKey.BattleClear, 100, 15))
			{
				AddReward(GDEItemKeys.Item_Consume_ArtifactPouch, 1, totalRewards);
			}
		}

		public void AddReward(string itemKey, int itemAmount = 1, int repeatCount = 1)
		{
			if (string.IsNullOrEmpty(itemKey)) return;

			for (int i = 0; i < repeatCount; i++)
			{
				ReceptionRewards.Add(ItemBase.GetItem(itemKey, itemAmount));
			}
		}
	}
}
