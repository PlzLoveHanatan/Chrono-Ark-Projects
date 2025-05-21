using System;
using System.Collections.Generic;
using ChronoArkMod;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using Dialogical;
using UnityEngine;
using static Raphi.FriendShip2;

namespace Raphi
{
    public class FriendShip3
    {
        public static string DialogueTreePath_FriendShip_3
        {
            get
            {
                if (string.IsNullOrEmpty(_DialogueTreePath_FriendShip_3))
                {
                    ModInfo modInfo = ModManager.getModInfo("Raphi");
                    DialogueTree dialogueTree = DialogueCreator.CreateDialogueTree<FriendShip3.Raphi_Friendship>();
                    _DialogueTreePath_FriendShip_3 = modInfo.assetInfo.ConstructObjectByCode<DialogueTree>(dialogueTree);
                }
                return _DialogueTreePath_FriendShip_3;
            }
        }

        private static string _DialogueTreePath_FriendShip_3;

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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/001"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/002"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/003"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/004"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/005"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/006"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/007"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/008"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/009"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/010"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/011"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/012"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/013"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/014"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/015"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/016"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/017"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/018"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/019"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/020"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/021"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/022"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/023"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/024"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/025"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/026"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/027"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/028"),
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
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/029"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_30);
        }
        public class Raphi_Friendship_Node_30 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/030"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_31);
        }
        public class Raphi_Friendship_Node_31 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/031"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_32);
        }
        public class Raphi_Friendship_Node_32 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/032"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_33);
        }
        public class Raphi_Friendship_Node_33 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/033"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_34);
        }
        public class Raphi_Friendship_Node_34 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/034"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_35);
        }
        public class Raphi_Friendship_Node_35 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/035"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_36);
        }
        public class Raphi_Friendship_Node_36 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/036"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_37);
        }
        public class Raphi_Friendship_Node_37 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/037"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_38);
        }
        public class Raphi_Friendship_Node_38 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/038"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_39);
        }
        public class Raphi_Friendship_Node_39 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/039"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_40);
        }
        public class Raphi_Friendship_Node_40 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/040"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_41);
        }
        public class Raphi_Friendship_Node_41 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/041"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_42);
        }
        public class Raphi_Friendship_Node_42 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/042"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_43);
        }
        public class Raphi_Friendship_Node_43 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/043"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_44);
        }
        public class Raphi_Friendship_Node_44 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/044"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_45);
        }
        public class Raphi_Friendship_Node_45 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/045"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_46);
        }
        public class Raphi_Friendship_Node_46 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/046"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_47);
        }
        public class Raphi_Friendship_Node_47 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/047"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }

            public override Type NextDialogueNodeCreatorType => typeof(Raphi_Friendship_Node_48);
        }
        public class Raphi_Friendship_Node_48 : DialogueNodeCreator
        {
            public override DialogueNodeParameter SetDialogueNodeParameter()
            {
                return new DialogueNodeParameter
                {
                    Text = ModManager.getModInfo("Raphi").localizationInfo.DialogueLocalizeUpdate("Dialogue/Raphi_Friendship_3/048"),
                    Standing_Path = ModManager.getModInfo("Raphi").assetInfo.ImageFromAsset("raphi_character", "Assets/Raphi/Dummy.png")
                };
            }
        }
    }
}
