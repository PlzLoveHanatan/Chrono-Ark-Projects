using System;
using System.Collections.Generic;
using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using XiaoLOR;
using static XiaoLOR.XiaoLORCharacter;

namespace XiaoLOR
{
    public class XiaoLORCharacter : CustomCharacterGDE<XiaoLORDialogue_Def>
    {
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Add;
        }

        public override void SetValue()
        {
            Dialogue_GoodGiftTalks = new List<string>
            {
                //XiaoLOR_Gift_Pan.DialogueTreePath_Pan,
                //XiaoLOR_Gift_RomanceDVD.DialogueTreePath_DVD
            };
        }

        public override string Key()
        {
            return "XiaoLOR";
        }
        public class XiaoLORDialogue_Def : ModDefinition
        {

        }
    }
}