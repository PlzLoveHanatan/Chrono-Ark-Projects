using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Aqua
{
    public class Aqua_Gift_Beer
    {
        private static string _DialogueTreePath_GiftBeer;

        public static string DialogueTreePath_GiftBeer
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_GiftBeer))
                {
                    ModInfo modInfo = ModManager.getModInfo("Aqua");
                    DialogueTree dialogueTreeGiftNormal = DialogueCreator.CreateDialogueTree<Aqua_Gift_Beer.Gift_Beer>();
                    _DialogueTreePath_GiftBeer = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftNormal);
                }
                return _DialogueTreePath_GiftBeer;
            }
        }

        
        public class Gift_Beer : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_Beer_Node_1);

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

        public class Gift_Beer_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/001"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_2);
        }

        public class Gift_Beer_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/002"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_3);
        }
        public class Gift_Beer_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/003"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_4);
        }
        public class Gift_Beer_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/004"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_5);
        }
        public class Gift_Beer_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/005"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_6);
        }
        public class Gift_Beer_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/006"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_7);
        }
        public class Gift_Beer_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/007"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_8);
        }
        public class Gift_Beer_Node_8 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/008"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_9);
        }
        public class Gift_Beer_Node_9 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/009"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_10);
        }
        public class Gift_Beer_Node_10 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/010"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_11);
        }
        public class Gift_Beer_Node_11 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/011"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_12);
        }
        public class Gift_Beer_Node_12 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/012"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_13);
        }
        public class Gift_Beer_Node_13 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/013"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_14);
        }
        public class Gift_Beer_Node_14 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/014"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_15);
        }
        public class Gift_Beer_Node_15 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/015"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_16);
        }
        public class Gift_Beer_Node_16 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/016"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_Beer_Node_17);
        }
        public class Gift_Beer_Node_17 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_Beer/017"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }
        }
    }
}