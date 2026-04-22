using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod.ModData;
using ChronoArkMod;
using Dialogical;
using static MiyukiSone.GiftNode;
using static MiyukiSone.Gifts.Normal;
using static MiyukiSone.Gifts;

namespace MiyukiSone
{
	public class Gifts
	{
		public class Normal
		{
			private static string _DialogueTree_Normal;

			public static string DialogueTree_Normal
			{
				get
				{
					if (string.IsNullOrEmpty(_DialogueTree_Normal))
					{
						DialogueTree dialogueTree = DialogueCreator.CreateDialogueTree<Gift_Normal>();
						_DialogueTree_Normal = Utils.ThisMod.assetInfo.ConstructObjectByCode(dialogueTree);
					}
					return _DialogueTree_Normal;
				}
			}

			public class Gift_Normal: BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(Gift_Normal_Node_1);
			}

			public class Gift_Normal_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "";
				protected override string CharacterPosePath() => "";
				//public override Type NextDialogueNodeCreatorType => typeof(Gift_Doll_Node_2);
			}
		}

		public class FriendShip
		{
			private static string _DialogueTree_FriendShip_Lv_1;

			public static string DialogueTree_FriendShip_Lv_1
			{
				get
				{
					if (string.IsNullOrEmpty(_DialogueTree_FriendShip_Lv_1))
					{
						DialogueTree dialogueTree = DialogueCreator.CreateDialogueTree<FriendShip_Lv_1>();
						_DialogueTree_FriendShip_Lv_1 = Utils.ThisMod.assetInfo.ConstructObjectByCode(dialogueTree);
					}
					return _DialogueTree_FriendShip_Lv_1;
				}
			}

			public class FriendShip_Lv_1 : BaseGiftDialogue
			{
				public override Type FirstNodeCreatorType => typeof(FriendShip_Lv_1_Node_1);
			}

			public class FriendShip_Lv_1_Node_1 : BaseGiftNode
			{
				protected override string NodePath() => "";
				protected override string CharacterPosePath() => "";
				//public override Type NextDialogueNodeCreatorType => typeof(Gift_Doll_Node_2);
			}
		}
	}
}
