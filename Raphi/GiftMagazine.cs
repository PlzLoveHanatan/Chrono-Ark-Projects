using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Raphi
{
    public class GiftMagazine
    {
        public static string DialogueTreePath_Magazine
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_Magazine))
                {
                    ModInfo modInfo = ModManager.getModInfo("Raphi");
                    DialogueTree dialogueTreeGiftMagazine = DialogueCreator.CreateDialogueTree<GiftMagazine.Gift_Magazine1>();
                    _DialogueTreePath_Magazine = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftMagazine);
                }
                return _DialogueTreePath_Magazine;
            }
        }

        private static string _DialogueTreePath_Magazine;
        public class Gift_Magazine1 : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_Magazine_Node_1);

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

        public class Gift_Magazine_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/001"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Magazine_Node_2);
        }

        public class Gift_Magazine_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/002"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Magazine_Node_3);
        }

        public class Gift_Magazine_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/003"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Magazine_Node_4);
        }

        public class Gift_Magazine_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/004"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Magazine_Node_5 );
        }
        public class Gift_Magazine_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/005"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Magazine_Node_6);
        }
        public class Gift_Magazine_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/006"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Magazine_Node_7);
        }

        public class Gift_Magazine_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Gift_Magazine/007"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }
        }
    }
}