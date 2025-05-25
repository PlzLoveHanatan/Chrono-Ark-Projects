using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace Aqua
{
    public class Aqua_Gift_Stuffed_Animal
    {
        private static string _DialogueTreePath_StuffedAnimal;

        public static string DialogueTreePath_StuffedAnimal
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_StuffedAnimal))
                {
                    ModInfo modInfo = ModManager.getModInfo("Aqua");
                    DialogueTree dialogueTreeStuffedAnimal = DialogueCreator.CreateDialogueTree<Aqua_Gift_Stuffed_Animal.Gift_StuffedAnimal>();
                    _DialogueTreePath_StuffedAnimal = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeStuffedAnimal);
                }
                return _DialogueTreePath_StuffedAnimal;
            }
        }

        public class Gift_StuffedAnimal : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_StuffedAnimal_Node_1);

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

        public class Gift_StuffedAnimal_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/001"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_2);
        }

        public class Gift_StuffedAnimal_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/002"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_3);
        }
        public class Gift_StuffedAnimal_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/003"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_4);
        }
        public class Gift_StuffedAnimal_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/004"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_5);
        }
        public class Gift_StuffedAnimal_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/005"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_6);
        }
        public class Gift_StuffedAnimal_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/006"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_7);
        }
        public class Gift_StuffedAnimal_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/007"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_8);
        }
        public class Gift_StuffedAnimal_Node_8 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/008"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_9);
        }
        public class Gift_StuffedAnimal_Node_9 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/009"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_10);
        }
        public class Gift_StuffedAnimal_Node_10 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/010"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_11);
        }
        public class Gift_StuffedAnimal_Node_11 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/011"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_StuffedAnimal_Node_12);
        }

        public class Gift_StuffedAnimal_Node_12 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Aqua").localizationInfo.DialogueLocalizeUpdate("Dialogue/Aqua_Gift_StuffedAnimal/012"),
                    Standing_Path = ModManager.getModInfo("Aqua").assetInfo.ImageFromAsset("Aqua_character", "Assets/Aqua/Dummy.png")
                };
            }
        }
    }
}