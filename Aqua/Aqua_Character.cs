using System;
using System.Collections.Generic;
using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using static Aqua.AquaCharacter;

namespace Aqua
{
    public class AquaCharacter : CustomCharacterGDE<Aqua_Dialogue_Def>
    {
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Add;
        }

        public override void SetValue()
        {
            Dialogue_NomalGiftTalk = Aqua_Gift_Normal.DialogueTreePath_GiftNormal;

            Dialogue_GoodGiftTalks = new List<string>
            {
                Aqua_Gift_Sake.DialogueTreePath_GiftSake,
                Aqua_Gift_Beer.DialogueTreePath_GiftBeer,
                Aqua_Gift_Fishing_Rod.DialogueTreePath_FishingRod,
                Aqua_Gift_Stuffed_Animal.DialogueTreePath_StuffedAnimal
            };
        }

        public override string Key()
        {
            return "Aqua";
        }
        public class Aqua_Dialogue_Def : ModDefinition
        {

        }
    }
}