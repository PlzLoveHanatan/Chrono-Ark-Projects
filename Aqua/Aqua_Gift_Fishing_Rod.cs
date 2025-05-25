using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Aqua
{
    public class Aqua_Gift_Fishing_Rod
    {
        private static string _DialogueTreePath_FishingRod;

        public static string DialogueTreePath_FishingRod
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_FishingRod))
                {
                    ModInfo modInfo = ModManager.getModInfo("Aqua");
                    DialogueTree dialogueTreeFishingRod = DialogueCreator.CreateDialogueTree<Aqua_Gift_Fishing_Rod.Gift_FishingRod>();
                    _DialogueTreePath_FishingRod = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeFishingRod);
                }
                return _DialogueTreePath_FishingRod;
            }
        }

        public class Gift_FishingRod : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_FishingRod_Node_1);

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

        public class Gift_FishingRod_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/001"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_2);
        }

        public class Gift_FishingRod_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/002"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_3);
        }
        public class Gift_FishingRod_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/003"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_4);
        }
        public class Gift_FishingRod_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/004"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_5);
        }
        public class Gift_FishingRod_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/005"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_6);
        }
        public class Gift_FishingRod_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/006"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_7);
        }
        public class Gift_FishingRod_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/007"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_8);
        }
        public class Gift_FishingRod_Node_8 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/008"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_FishingRod_Node_9);
        }

        public class Gift_FishingRod_Node_9 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_FishingRod/009"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }
        }
    }
}