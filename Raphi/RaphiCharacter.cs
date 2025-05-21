using System;
using System.Collections.Generic;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;

namespace Raphi
{
    public class RaphiCharacter : CustomCharacterGDE<RaphiDialogue_Def>
    {
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Add;
        }

        public override void SetValue()
        {
            base.Dialogue_NomalGiftTalk = GiftNormal.DialogueTreePath_GiftNormal;
            base.Dialogue_GoodGiftTalks = new List<string>
            {
                GiftDoll.DialogueTreePath_Doll,
                GiftDogcollar.DialogueTreePath_Dogcollar,
                GiftDVD.DialogueTreePath_DVD,
                GiftMagazine.DialogueTreePath_Magazine
            };
            base.Dialogue_FriendShipLVTalks = new List<string>
            {
                FriendShip1.DialogueTreePath_FriendShip_1,
                FriendShip2.DialogueTreePath_FriendShip_2,
                FriendShip3.DialogueTreePath_FriendShip_3
            };
        }

        public override string Key()
        {
            return "Raphi";
        }
    }
}