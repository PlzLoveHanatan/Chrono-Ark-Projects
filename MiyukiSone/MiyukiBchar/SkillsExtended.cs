using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MiyukiSone
{
	public class SkillsExtended
	{
		public class GlitchedSkill : Skill_Extended
		{
			public override void Init()
			{
				base.Init();
			}

			private GameObject glitchEffect;

			public override void FixedUpdate()
			{
				if (glitchEffect == null && BattleSystem.instance != null)
				{
					var prefab = Resources.Load<GameObject>("StoryGlitch/GlitchSkillEffect");
					glitchEffect = UnityEngine.Object.Instantiate(prefab, MySkill.MyButton.transform);
					glitchEffect.SetActive(true);
				}
			}
		}
	}
}
