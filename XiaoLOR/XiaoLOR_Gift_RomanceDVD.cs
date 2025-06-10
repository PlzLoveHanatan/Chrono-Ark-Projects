using System;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;

namespace XiaoLOR
{
    public class XiaoLOR_Gift_RomanceDVD
    {
        public static string DialogueTreePath_DVD
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_DVD))
                {
                    ModInfo modInfo = ModManager.getModInfo("XiaoLOR");
                    DialogueTree dialogueTreeGiftDVD = DialogueCreator.CreateDialogueTree<XiaoLOR_Gift_RomanceDVD.Gift_XiaoLOR_DVD>();
                    _DialogueTreePath_DVD = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTreeGiftDVD);
                }
                return _DialogueTreePath_DVD;
            }
        }

        private static string _DialogueTreePath_DVD;
        public class Gift_XiaoLOR_DVD : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_1);

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

        public class Gift_XiaoLOR_DVD_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/001"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_2);
        }

        public class Gift_XiaoLOR_DVD_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/002"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_3);
        }

        public class Gift_XiaoLOR_DVD_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/003"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_4);
        }

        public class Gift_XiaoLOR_DVD_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/004"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_5);
        }
        public class Gift_XiaoLOR_DVD_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/005"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_6);
        }
        public class Gift_XiaoLOR_DVD_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/006"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Gift_XiaoLOR_DVD_Node_7);
        }
        public class Gift_XiaoLOR_DVD_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("XiaoLOR").localizationInfo.DialogueLocalizeUpdate("Dialogue/XiaoLOR_Gift_RomanceDVD/007"),
                    Standing_Path = ModManager.getModInfo("XiaoLOR").assetInfo.ImageFromAsset("xiao_character", "Assets/XiaoLOR/Dummy.png")
                };
            }
        }
    }
}