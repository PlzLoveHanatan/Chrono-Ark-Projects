using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Extended
	{
		public class Emotion
		{
			public class Draw : BuffSkillExHand
			{
				public override void FixedUpdate()
				{
					base.FixedUpdate();

					var mainBuff = (Investigators.EmotionBuff.Draw)MainBuff;

					if (mainBuff != null)
					{
						BuffIconStackNum = ((Investigators.EmotionBuff.Draw)MainBuff).skillUse;
					}
				}
			}

			public class ManaReduction : BuffSkillExHand
			{
				public override void Init()
				{
					base.Init();
					APChange = -1;
				}
			}
		}
	}
}
