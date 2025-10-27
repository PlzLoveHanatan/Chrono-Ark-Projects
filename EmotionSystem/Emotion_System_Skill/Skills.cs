using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using static EmotionSystem.EmotionalManager;
using GameDataEditor;
using Steamworks;
using DarkTonic.MasterAudio;
using I2.Loc;
using System.Collections;
using UnityEngine;

namespace EmotionSystem
{
	public class Skills
	{
		public class Guest
		{
			public class Present : Skill_Extended, IP_Draw
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public IEnumerator Draw(Skill Drawskill, bool NotDraw)
				{
					if (Drawskill == MySkill)
					{
						var ally = Utils.RandomAlly();
						Utils.TakeNonLethalDamage(ally, 10, true);
						Utils.AllyTeam.Draw();
					}
					return base.DrawAction();
				}
			}

			public class DimensionalRefraction : Skill_Extended
			{
				public override bool Terms()
				{
					return false;
				}

				private GameObject glitchEffect;

				public override void FixedUpdate()
				{
					if (BattleSystem.instance == null || MySkill?.MyButton == null) return;

					if (glitchEffect == null)
					{
						var prefab = Resources.Load<GameObject>("StoryGlitch/GlitchSkillEffect");

						glitchEffect = UnityEngine.Object.Instantiate(prefab, MySkill.MyButton.transform);
						glitchEffect.SetActive(true);
					}
				}
			}

			public class WitchCurses
			{
				public class Pain : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						Utils.CreateSkill(Utils.AllyTeam.LucyAlly, GDEItemKeys.Skill_S_Witch_P_0, true);
					}
				}

				public class Weak : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						Utils.CreateSkill(Utils.AllyTeam.LucyAlly, GDEItemKeys.Skill_S_Witch_2, true);
					}
				}
			}
		}

		public class Potion
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

					if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is Investigators.EmotionLucy lucyEmotion)
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

					if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is Investigators.EmotionLucy lucyEmotion)
					{
						BattleSystem.DelayInputAfter(lucyEmotion.LucyEmotionLevelUp(2, true));
					}
				}
			}

			public class DistortionFragment : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is Investigators.EmotionLucy lucyEmotion)
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
							EmotionalManager.SetEmotionCapInvestigator(ally);
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
							EmotionalManager.SetEmotionCapGuest(enemy, true);
						}
					}
				}
			}
		}

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

		public class Lucy
		{
			public class MusicBox : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw();

					foreach (var ally in Targets)
					{
						if (ally != null)
						{
							EmotionalManager.SetEmotionCapInvestigator(ally);
						}
					}
				}
			}

			public class HippityHop : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(2);

					foreach (BattleChar ally in Utils.AllyTeam.AliveChars)
					{
						ally.GetPosEmotion(null, 3);
					}
				}
			}

			public class Candy : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(2);

					var ally = Utils.AllyTeam.AliveChars.Where(a => a != null).OrderBy(a => a.MyEmotion().Level).ThenBy(a => a.MyEmotion().CoinNum).FirstOrDefault();

					ally?.AddEmotionLevel(true);
				}
			}

			public class RainbowSea : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(3);
					Utils.AllyTeam.AP += 2;

					foreach (BattleChar enemy in Utils.EnemyTeam.AliveChars)
					{
						if (Utils.ReturnBuff(enemy, ModItemKeys.Buff_B_Guest_Emotional_Level) is Guests.Emotion.Level buff && !buff.EmotionBlock)
						{
							enemy.GetNegEmotion(null, 3);
						}
					}
				}
			}
		}
	}
}
