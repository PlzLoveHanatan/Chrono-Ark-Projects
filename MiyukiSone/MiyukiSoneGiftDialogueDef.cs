using System;
using System.Collections.Generic;
using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;

namespace MiyukiSone
{
	public class RaphiCharacter : CustomCharacterGDE<MiyukiSone_ModDefinition>
	{
		public override ModGDEInfo.LoadingType GetLoadingType()
		{
			return ModGDEInfo.LoadingType.Add;
		}

		public override void SetValue()
		{
			Dialogue_NomalGiftTalk = Gifts.Normal.DialogueTree_Normal;

			//Dialogue_GoodGiftTalks = new List<string>
			//{
			//	GiftDoll.DialogueTreePath_Doll,
			//	GiftDogcollar.DialogueTreePath_Dogcollar,
			//	GiftDVD.DialogueTreePath_DVD,
			//	GiftMagazine.DialogueTreePath_Magazine
			//};

			Dialogue_FriendShipLVTalks = new List<string>
			{
				Gifts.FriendShip.DialogueTree_FriendShip_Lv_1,
			};
		}

		public override string Key()
		{
			return "Miyuki";
		}
	}
}