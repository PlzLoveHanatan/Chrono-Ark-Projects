using System;
using System.Collections.Generic;
using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;

namespace MiyukiSone
{
	// Based on Feiyap code
	public class GiftDialogueDef : CustomCharacterGDE<MiyukiSone_ModDefinition>
	{
		public override ModGDEInfo.LoadingType GetLoadingType()
		{
			return ModGDEInfo.LoadingType.Add;
		}

		public override void SetValue()
		{
			Dialogue_NomalGiftTalk = Gifts.Normal.DialogueTree_Normal;

			Dialogue_GoodGiftTalks = new List<string>
			{
				Gifts.Preferable.DialogueTree_Collar,
				Gifts.Preferable.DialogueTree_DVD,
				Gifts.Preferable.DialogueTree_Knife,
				Gifts.Preferable.DialogueTree_SF_Novel,
			};

			Dialogue_FriendShipLVTalks = new List<string>
			{
				Gifts.FriendShip.DialogueTree_FriendShip_Lv_1,
				Gifts.FriendShip.DialogueTree_FriendShip_Lv_2,
				Gifts.FriendShip.DialogueTree_FriendShip_Lv_3,
			};

			Dialogue_TrueEnd = new List<string>()
			{

			};
		}

		public override string Key()
		{
			return ModItemKeys.Character_Miyuki;
		}
	}
}