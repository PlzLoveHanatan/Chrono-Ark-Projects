using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.DialogueCreate;
using ChronoArkMod;
using UnityEngine;

namespace MiyukiSone
{
	public class GiftNode
	{
		public abstract class BaseGiftNode : DialogueNodeCreator
		{
			protected abstract string NodePath();
			protected abstract string CharacterPosePath();

			public override DialogueNodeParameter SetDialogueNodeParameter()
			{
				string posePath = CharacterPosePath();
				string spriteAddress = UtilsUI.GetSpriteAddressFromAsset(posePath);

				return new DialogueNodeParameter
				{
					Text = Utils.ThisMod.localizationInfo.DialogueLocalizeUpdate(NodePath()),
					Standing_Path = spriteAddress,
				};
			}
		}

		public abstract class BaseGiftDialogue : DialogueCreator
		{
			public override DialogueParameter SetDialogueParameter(GameObject gameObject)
			{
				return SetDialogueParameter(gameObject);
			}
		}
	}
}