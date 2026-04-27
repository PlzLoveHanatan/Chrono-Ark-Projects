using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using ChronoArkMod;
using Dialogical;
using static MiyukiSone.GiftNodeParameter;
using static MiyukiSone.Gifts.Normal;
using static MiyukiSone.Gifts;
using System.Collections;
using UnityEngine;

namespace MiyukiSone
{
	public class Gifts
	{
		public class FriendShip
		{
			public static string DialogueTree_FriendShip_Lv_1
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<FriendShip_Lv_1>());
				}
			}

			public class FriendShip_Lv_1 : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(FriendShip_Lv_1_Node_1);
			}

			public class FriendShip_Lv_1_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Normal/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(FriendShip_Lv_1_Node_2);
			}

			public class FriendShip_Lv_1_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Normal/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(FriendShip_Lv_1_Node_3);
			}

			public class FriendShip_Lv_1_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Normal/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}

			public static string DialogueTree_FriendShip_Lv_2
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<FriendShip_Lv_2>());
				}
			}

			public class FriendShip_Lv_2 : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(FriendShip_Lv_2_Node_1);
			}

			public class FriendShip_Lv_2_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "FriendShip_Lv_2/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(FriendShip_Lv_2_Node_2);
			}

			public class FriendShip_Lv_2_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "FriendShip_Lv_2/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(FriendShip_Lv_2_Node_3);
			}

			public class FriendShip_Lv_2_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "FriendShip_Lv_2/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}

			public static string DialogueTree_FriendShip_Lv_3
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<FriendShip_Lv_3>());
				}
			}

			public class FriendShip_Lv_3 : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(FriendShip_Lv_3_Node_1);
			}

			public class FriendShip_Lv_3_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "FriendShip_Lv_3/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(FriendShip_Lv_3_Node_2);
			}

			public class FriendShip_Lv_3_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "FriendShip_Lv_3/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(FriendShip_Lv_3_Node_3);
			}

			public class FriendShip_Lv_3_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "FriendShip_Lv_3/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}
		}

		public class Normal
		{
			public static string DialogueTree_Normal
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<Gift_Normal>());
				}
			}

			public class Gift_Normal : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(Gift_Normal_Node_1);
			}

			public class Gift_Normal_Node_1 : BaseGiftNode
			{
				
				protected override string NodePath() => $"Gift_Normal/0{RandomManager.RandomInt("RandomInt", 1, 7)}"; // Enumerable.Range(1, 6)?.RandomElement()
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}
		}

		public class Preferable
		{
			public static string DialogueTree_Collar
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<Gift_Collar>());
				}
			}

			public class Gift_Collar : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(Gift_Collar_Node_1);
			}

			public class Gift_Collar_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Collar/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_Collar_Node_2);
			}

			public class Gift_Collar_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Collar/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_Collar_Node_3);
			}

			public class Gift_Collar_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Collar/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}

			public static string DialogueTree_DVD
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<Gift_DVD>());
				}
			}

			public class Gift_DVD : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(Gift_DVD_Node_1);
			}

			public class Gift_DVD_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_DVD/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_2);
			}

			public class Gift_DVD_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_DVD/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_DVD_Node_3);
			}

			public class Gift_DVD_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_DVD/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}

			public static string DialogueTree_Knife
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<Gift_Knife>());
				}
			}

			public class Gift_Knife : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(Gift_Knife_Node_1);
			}

			public class Gift_Knife_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Knife/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_Knife_Node_2);
			}

			public class Gift_Knife_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Knife/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_Knife_Node_3);
			}

			public class Gift_Knife_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_Knife/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}

			public static string DialogueTree_SF_Novel
			{
				get
				{
					return Utils.ThisMod.assetInfo.ConstructObjectByCode(DialogueCreator.CreateDialogueTree<Gift_SF_Novel>());
				}
			}

			public class Gift_SF_Novel : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(Gift_SF_Novel_Node_1);
			}

			public class Gift_SF_Novel_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_SF_Novel/01";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_SF_Novel_Node_2);
			}

			public class Gift_SF_Novel_Node_2 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_SF_Novel/02";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => typeof(Gift_SF_Novel_Node_3);
			}

			public class Gift_SF_Novel_Node_3 : BaseGiftNode
			{
				protected override string NodePath() => "Gift_SF_Novel/03";
				protected override string CharacterPosePath() => "";
				public override Type NextDialogueNodeCreatorType => null;
			}
		}
	}
}
