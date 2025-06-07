using System;
using System.Collections.Generic;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using Xiao;

namespace Xiao
{
    public class XiaoCharacter : CustomCharacterGDE<XiaoDialogue_Def>
    {
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Add;
        }

        public override void SetValue()
        {
            //base.Dialogue_NomalGiftTalk = GiftNormal.DialogueTreePath_GiftNormal;
            base.Dialogue_GoodGiftTalks = new List<string>
            {
                Xiao_Gift_Pan.DialogueTreePath_Pan,
                Xiao_Gift_RomanceDVD.DialogueTreePath_DVD
                //GiftDogcollar.DialogueTreePath_Dogcollar,
                
                //GiftMagazine.DialogueTreePath_Magazine
            };
            //base.Dialogue_FriendShipLVTalks = new List<string>
            //{
            //    FriendShip1.DialogueTreePath_FriendShip_1,
            //    FriendShip2.DialogueTreePath_FriendShip_2,
            //    FriendShip3.DialogueTreePath_FriendShip_3
            //};
        }

        public override string Key()
        {
            return "Xiao";
        }
    }
}