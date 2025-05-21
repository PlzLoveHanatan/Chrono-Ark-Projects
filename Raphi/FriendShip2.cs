using System;
using System.Collections.Generic;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;
using static Raphi.FriendShip1;

namespace Raphi
{
    public class FriendShip2
    {
        public static string DialogueTreePath_FriendShip_2
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_FriendShip_2))
                {
                    ModInfo modInfo = ModManager.getModInfo("Raphi");
                    DialogueTree dialogueTree = DialogueCreator.CreateDialogueTree<FriendShip2.Raphi_Friendship>();
                    _DialogueTreePath_FriendShip_2 = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTree);
                }
                return _DialogueTreePath_FriendShip_2;
            }
        }

        private static string _DialogueTreePath_FriendShip_2;

        public class Raphi_Friendship : DialogueCreator
        {
            public override Type FirstNodeCreatorType => typeof(Raphi_Friendship_Node_1);

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
        public class Raphi_Friendship_Node_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/001"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_2);
        }
        public class Raphi_Friendship_Node_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/002"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_3);
        }
        public class Raphi_Friendship_Node_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/003"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_4);
        }
        public class Raphi_Friendship_Node_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/004"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_5);
        }
        public class Raphi_Friendship_Node_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/005"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_6);
        }
        public class Raphi_Friendship_Node_6 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/006"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_7);
        }
        public class Raphi_Friendship_Node_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/007"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_8);
        }
        public class Raphi_Friendship_Node_8 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/008"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_9);
        }
        public class Raphi_Friendship_Node_9 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/009"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_10);
        }
        public class Raphi_Friendship_Node_10 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/010"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_11);
        }
        public class Raphi_Friendship_Node_11 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/011"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_12);
        }
        public class Raphi_Friendship_Node_12 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/012"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_13);
        }
        public class Raphi_Friendship_Node_13 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/013"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_14);
        }
        public class Raphi_Friendship_Node_14 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/014"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15);
        }
        public class Raphi_Friendship_Node_15 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override IEnumerable<Type> OptionCreatorTypes
            {
                get
                {
                    return new List<Type>
                    {
                        typeof(FriendShip2.Raphi_Friendship_Node_Option1),
                        typeof(FriendShip2.Raphi_Friendship_Node_Option2)
                    };
                }
            }
        }
        public class Raphi_Friendship_Node_Option1 : DialogueNodeOptionCreator
        {
            public override DialogueNodeOptionParameter SetDialogueNodeOptionParameter()
            {
                return new DialogueNodeOptionParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_Option1")
                };
            }

            public override Type TargetDialogueNodeCreatorType
            {
                get
                {
                    return typeof(FriendShip2.Raphi_Friendship_Node_15_1);
                }
            }
        }
        public class Raphi_Friendship_Node_15_1 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_1"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15_2);
        }
        public class Raphi_Friendship_Node_15_2 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_2"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15_3);
        }
        public class Raphi_Friendship_Node_15_3 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_3"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15_4);
        }
        public class Raphi_Friendship_Node_15_4 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_4"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15_5);
        }
        public class Raphi_Friendship_Node_15_5 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_6"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15_7);
        }
        public class Raphi_Friendship_Node_15_7 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_7"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_16);
        }
        public class Raphi_Friendship_Node_Option2 : DialogueNodeOptionCreator
        {
            public override DialogueNodeOptionParameter SetDialogueNodeOptionParameter()
            {
                return new DialogueNodeOptionParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_Option2")
                };
            }

            public override Type TargetDialogueNodeCreatorType
            {
                get
                {
                    return typeof(FriendShip2.Raphi_Friendship_Node_15_21);
                }
            }
        }
        public class Raphi_Friendship_Node_15_21 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_21"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_15_22);
        }
        public class Raphi_Friendship_Node_15_22: DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/015_22"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_16);
        }
        public class Raphi_Friendship_Node_16 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/016"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_17);
        }
        public class Raphi_Friendship_Node_17 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/017"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_18);
        }
        public class Raphi_Friendship_Node_18 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/018"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_19);
        }
        public class Raphi_Friendship_Node_19 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/019"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_20);
        }
        public class Raphi_Friendship_Node_20 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/020"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_21);
        }
        public class Raphi_Friendship_Node_21 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/021"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_22);
        }
        public class Raphi_Friendship_Node_22 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/022"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_23);
        }
        public class Raphi_Friendship_Node_23 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/023"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_24);
        }
        public class Raphi_Friendship_Node_24 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/024"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_25);
        }
        public class Raphi_Friendship_Node_25 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/025"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_26);
        }
        public class Raphi_Friendship_Node_26 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/026"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_27);
        }
        public class Raphi_Friendship_Node_27 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/027"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_28);
        }
        public class Raphi_Friendship_Node_28 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/028"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_29);
        }
        public class Raphi_Friendship_Node_29 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_2/029"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }
        }
    }
}




