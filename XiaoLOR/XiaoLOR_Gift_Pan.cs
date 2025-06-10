using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace XiaoLOR
{
    public class XiaoLOR_Gift_Pan
    {
        public static string DialogueTreePath_Pan
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_Pan))
                {
                    ModInfo modInfo = ModManager.getModInfo("XiaoLOR");
                    DialogueTree dialogueTreeGiftPan = DialogueCreator.CreateDialogueTree<XiaoLOR_Gift_Pan.Gift_XiaoLOR_Pan>();
                    _DialogueTreePath_Pan = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftPan);
                }
                return _DialogueTreePath_Pan;
            }
        }

        private static string _DialogueTreePath_Pan;
        public class Gift_XiaoLOR_Pan : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_1);

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

        public class Gift_XiaoLOR_Pan_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/001"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_2);
        }

        public class Gift_XiaoLOR_Pan_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/002"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_3);
        }

        public class Gift_XiaoLOR_Pan_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/003"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_4);
        }

        public class Gift_XiaoLOR_Pan_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/004"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_5);
        }
        public class Gift_XiaoLOR_Pan_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/005"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_6);
        }
        public class Gift_XiaoLOR_Pan_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/006"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_Pan_Node_7);
        }
        public class Gift_XiaoLOR_Pan_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_Pan/007"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }
        }
    }
}