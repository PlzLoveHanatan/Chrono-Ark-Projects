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
	public class GiftNodeParameter
	{
		public abstract class BaseGiftNode : DialogueNodeCreator
		{
			private static string DressPath
			{
				get
				{
					string dressType = Affection.MiyukiDecides ? "Dress" : "Swimsuit";
					var affections = new[] { "Dere", "Kuudere", "Yandere" };
					string affection = affections.RandomElement();
					return $"Assets/Images/DialoguePose/{dressType}{affection}.png";
				}
			}

			protected abstract string NodePath();
			protected abstract string CharacterPosePath();

			public override DialogueNodeParameter SetDialogueNodeParameter()
			{
				string posePath = string.IsNullOrEmpty(CharacterPosePath()) ? DressPath : CharacterPosePath();
				string spriteAddress = UtilsUI.GetSpriteAddressFromAsset(posePath);

				return new DialogueNodeParameter
				{
					Text = Utils.ThisMod.localizationInfo.DialogueLocalizeUpdate("Dialogue/Miyuki_" + NodePath()),
					Standing_Path = spriteAddress,
				};
			}
		}

		public abstract class BaseGiftDialogue : DialogueCreator
		{
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
	}
}