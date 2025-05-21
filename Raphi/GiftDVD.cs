using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Raphi
{
    public class GiftDVD
    {
        public static string DialogueTreePath_DVD
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_DVD))
                {
                    ModInfo modInfo = ModManager.getModInfo("Raphi");
                    DialogueTree dialogueTreeGiftDVD = DialogueCreator.CreateDialogueTree<GiftDVD.Gift_DVD>();
                    _DialogueTreePath_DVD = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftDVD);
                }
                return _DialogueTreePath_DVD;
            }
        }

        private static string _DialogueTreePath_DVD;
        public class Gift_DVD : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_DVD_Node_1);

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

        public class Gift_DVD_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/001"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_2);
        }

        public class Gift_DVD_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/002"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_3);
        }

        public class Gift_DVD_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/003"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_4);
        }

        public class Gift_DVD_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/004"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_5);
        }
        public class Gift_DVD_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/005"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_6);
        }
        public class Gift_DVD_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/006"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_7);
        }
        public class Gift_DVD_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/007"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_8);
        }

        public class Gift_DVD_Node_8 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_DVD/008"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }
        }
    }
}