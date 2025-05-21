using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Raphi
{
    public class GiftNormal
    {
        public static string DialogueTreePath_GiftNormal
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_GiftNormal))
                {
                    ModInfo modInfo = ModManager.getModInfo("Raphi");
                    DialogueTree dialogueTreeGiftNormal = DialogueCreator.CreateDialogueTree<GiftNormal.Gift_Normal>();
                    _DialogueTreePath_GiftNormal = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftNormal);
                }
                return _DialogueTreePath_GiftNormal;
            }
        }

        private static string _DialogueTreePath_GiftNormal;
        public class Gift_Normal : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_Normal_Node_1);

            public override DialogueParameter SetDialogueParameter(GameObject gameObject)
            {
                return new DialogueParameter
                {
                    AutoPlay = true,
                    UIOffDialogue = true,
                    StoryDialogue = true
                };
            }
        }

        public class Gift_Normal_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Normal/001"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Normal_Node_2);
        }

        public class Gift_Normal_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Normal/002"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Normal_Node_3);
        }
        public class Gift_Normal_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Normal/003"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Normal_Node_4);
        }
        public class Gift_Normal_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Normal/004"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }
        }
    }
}