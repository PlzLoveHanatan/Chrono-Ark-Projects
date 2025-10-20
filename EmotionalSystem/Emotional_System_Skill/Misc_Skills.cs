using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using static EmotionalSystem.EmotionalManager;
using GameDataEditor;
using Steamworks;
using EmotionalSystemBuff;
using DarkTonic.MasterAudio;
using I2.Loc;

namespace EmotionalSystemSkill
{
	public class MiscSkills
	{
		public class Potions
		{
			public class DistilledSuffering : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.ApplyBleed(Targets[0], Utils.DummyChar, 6);
				}
			}

			public class IgnitedRemorse : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.ApplyBurn(Targets[0], Utils.DummyChar, 8);
				}
			}

			public class EssenceWrath : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					//EmotionalManager.GetNegEmotion(Targets[0], null, 5);

					if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is EmotionsLucy lucyEmotion)
					{
						BattleSystem.DelayInputAfter(lucyEmotion.LucyEmotionLevelUp(2, false, true));
					}
				}
			}

			public class EssenceTranquility : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					//EmotionalManager.GetPosEmotion(Targets[0], null, 5);

					if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is EmotionsLucy lucyEmotion)
					{
						BattleSystem.DelayInputAfter(lucyEmotion.LucyEmotionLevelUp(2, true));
					}
				}
			}

			public class DistortionFragment : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is EmotionsLucy lucyEmotion)
					{
						BattleSystem.DelayInputAfter(lucyEmotion.SelectEGO());
					}
				}
			}

			public class PureTune : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					foreach (var ally in Targets)
					{
						if (ally != null)
						{
							EmotionalManager.SetEmotionCapAlly(ally);
						}
					}
				}
			}

			public class DarkTune : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					foreach (var enemy in Targets)
					{
						if (enemy != null)
						{
							EmotionalManager.SetEmotionCapEnemy(enemy, true);
						}
					}
				}
			}
		}

		public class SkillBook
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
						case 0: skillKey = ModItemKeys.Skill_S_EmotionalSystem_Lucy_Candy; break;
						case 1: skillKey = ModItemKeys.Skill_S_EmotionalSystem_Lucy_HippityHop; break;
						case 2: skillKey = ModItemKeys.Skill_S_EmotionalSystem_Lucy_RainbowSea; break;
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

					PlayData.TSavedata.UseItemKeys.Add(ModItemKeys.Item_Consume_C_EmotionalSystem_DreamingCurrent);

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

		public class Relic
		{
			public class GoldenSound : PassiveItemBase, IP_PlayerTurn
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void Turn()
				{
					Utils.AllyTeam.Draw();

					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						EmotionalManager.GetPosEmotion(ally, null, 2);
					}
				}
			}
		}
	}
}
