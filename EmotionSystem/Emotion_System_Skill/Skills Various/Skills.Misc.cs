using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using I2.Loc;

namespace EmotionSystem
{
	public partial class Skills
	{
		public class Book
		{
			public class DreamingCurrent : UseitemBase
			{
				public override bool Use()
				{
					var lucySkills = new List<Skill>();
					string skillKey = "";

					var roll = RandomManager.RandomInt(RandomClassKey.LucyDraw, 0, 3);

					switch (roll)
					{
						case 0: skillKey = ModItemKeys.Skill_S_EmotionSystem_Lucy_Candy; break;
						case 1: skillKey = ModItemKeys.Skill_S_EmotionSystem_Lucy_HippityHop; break;
						case 2: skillKey = ModItemKeys.Skill_S_EmotionSystem_Lucy_RainbowSea; break;
					}

					if (string.IsNullOrEmpty(skillKey))
					{
						return false;
					}

					Skill lucySkill = Skill.TempSkill(skillKey, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam).CloneSkill();

					if (lucySkill == null)
					{
						return false;
					}

					lucySkills.Add(lucySkill);

					PlayData.TSavedata.UseItemKeys.Add(ModItemKeys.Item_Consume_C_EmotionSystem_DreamingCurrent);

					FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(lucySkills, new SkillButton.SkillClickDel(this.SkillAdd),
						ScriptLocalization.System_Item.SkillAdd, false, true, true, true, false));

					MasterAudio.PlaySound("BookFlip", 1f);

					return base.Use();
				}

				public void SkillAdd(SkillButton Mybutton)
				{
					PlayData.TSavedata.LucySkills.Add(Mybutton.Myskill.MySkill.KeyID);
					UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();
				}
			}
		}
	}
}
