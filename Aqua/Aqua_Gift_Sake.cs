using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Aqua
{
    public class Aqua_Gift_Sake
    {
        private static string _DialogueTreePath_GiftSake;

        public static string DialogueTreePath_GiftSake
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_GiftSake))
                {
                    ModInfo modInfo = ModManager.getModInfo("Aqua");
                    DialogueTree dialogueTreeGiftSake = DialogueCreator.CreateDialogueTree<Aqua_Gift_Sake.Gift_Sake>();
                    _DialogueTreePath_GiftSake = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftSake);
                }
                return _DialogueTreePath_GiftSake;
            }
        }
        
        public class Gift_Sake : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_Sake_Node_1);

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

        public class Gift_Sake_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/001"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_2);
        }

        public class Gift_Sake_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/002"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_3);
        }
        public class Gift_Sake_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/003"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_4);
        }
        public class Gift_Sake_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/004"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_5);
        }
        public class Gift_Sake_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/005"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_6);
        }
        public class Gift_Sake_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/006"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_7);
        }
        public class Gift_Sake_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/007"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_8);
        }
        public class Gift_Sake_Node_8 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/008"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_9);
        }
        public class Gift_Sake_Node_9 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/009"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_10);
        }
        public class Gift_Sake_Node_10 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/010"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Sake_Node_11);
        }
        public class Gift_Sake_Node_11 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Sake/011"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }
        }
    }
}