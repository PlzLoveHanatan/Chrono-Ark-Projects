using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.Affection;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.Skills.Class;
using NLog.Targets;

namespace MiyukiSone
{
	public class Skills
	{
		public class Test : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ChangeAffectionPoints(10);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class Test2 : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ChangeAffectionPoints(-10);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class SacrificedKnowledge : Skill_Extended
		{
			public override bool Terms()
			{
				return false;
			}
		}

		public class Class
		{
			public class MiyukiSoneSkill : Skill_Extended
			{
				public override void Init()
				{
					MySkill.MiyukiInit();
					base.Init();
				}
			}

			public class QueenBee : MiyukiSoneSkill
			{

			}

			public class EternalPromise : Skill_Extended, IP_MiyukiSkillImgChange
			{
				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class GlitchingPhone : Skill_Extended
			{
				private float timer = 0f;
				private readonly float interval = 3f;

				public override void FixedUpdate()
				{
					base.FixedUpdate();

					timer += Time.fixedDeltaTime;

					if (timer >= interval)
					{
						timer = 0f;
						ChangeImg();
					}
				}

				private void ChangeImg()
				{
					string[] suffixes = { "01", "02", "03" };
					var available = Enumerable.Range(0, suffixes.Length).Where(i => i != MiyukiData.LastPhoneImage || suffixes.Length == 1).ToList();
					int selected = available[RandomManager.RandomInt("MiyukiPhone", 0, available.Count)];
					MiyukiData.LastPhoneImage = selected;
					string path = $"Assets/Images/Skills/GlitchingPhone/{suffixes[selected]}/";
					MySkill.ChangeSkillImage(path + "skill", path + "button", path + "basic", isGlicthEffect: true);
				}
			}

			public class GracefulSwing : PriestPBase
			{
				private int Damage => (int)(BChar.GetStat.atk * 1.5f);
				private bool _lastPassiveDraw;

				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Damage.ToString());
				}

				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);

				}

				public override void FixedUpdate()
				{
					if (PassiveDraw != _lastPassiveDraw)
					{
						_lastPassiveDraw = PassiveDraw;

						if (PassiveDraw)
						{
							string path = "Assets/Images/Skills/GracefulSwing/Prophecy/";
							SkillBasePlus.Target_BaseDMG = Damage;
							MySkill.ChangeSkillImage(path + "skill", path + "button", path + "basic", isGlicthEffect: true);
						}
						else MySkill.ChangeSkillImage(isRestoreImg: true);
					}
					base.FixedUpdate();
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					if (PassiveDraw) SkillBasePlus.Target_BaseDMG = Damage;
					base.SkillUseSingle(SkillD, Targets);
				}
			}

			public class HappyBirthday : SkillExtedned_IlyaP, IP_MiyukiSkillImgChange
			{
				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
				}

				public override void IlyaWaste()
				{
					if (!MySkill.IsMiyukiOwner()) return;
					AllyTeam.Draw();
					BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(BChar, MySkill, false, true, false));
					base.IlyaWaste();
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class MeasuredLove : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class SweetRestraint : MiyukiSoneSkill, IP_SkillCastingStart
			{
				public void SkillCasting(CastingSkill ThisSkill)
				{
					BChar.AddBuff(GDEItemKeys.Buff_B_Lian_P_0);
				}
			}

			public class WarningStrike : Skill_Momori, IP_MiyukiSkillImgChange
			{
				private int Heal => (int)(BChar.GetStat.reg);

				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Heal.ToString());
				}

				public override void Init()
				{
					MySkill.MiyukiInit();
					base.Init();
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					var momoriPskill = GetMomoriPskill();
					if (momoriPskill.Any())
					{
						var p = momoriPskill.First();
						p.SaveDMG = Math.Max(0, p.SaveDMG - Heal);
					}
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}
		}

		public class Rare
		{
			public class JustforYOU : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public override bool Terms()
				{
					return !IsYandere && base.Terms();
				}

				public override void FixedUpdate()
				{
					base.FixedUpdate();
					if (Bs != null) UpdateAPCost();
				}

				private void UpdateAPCost()
				{
					bool inDeck = AllyTeam.Skills_Deck.Concat(AllyTeam.Skills_UsedDeck).Contains(MySkill);
					MySkill.APChange = inDeck ? 3 : 0;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Targets.ForEach(t => t.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).ForEach(b => b.SelfDestroy()));
					DialogueState state = IsDere && MiyukiPoints >= 30 ? DialogueState.kiss : DialogueState.love;
					Dialogue.CreateDialogue(state);
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class GameUpdate : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public override bool Terms()
				{
					return !MiyukiSoneSaveManager.Instance.CurrentData.GameUpdated && base.Terms();
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					MiyukiSoneSaveManager.Instance.CurrentData.LockedState = (int)CurrentAffectionState;
					MiyukiSoneSaveManager.Instance.CurrentData.GameUpdated = true;
					MiyukiSoneSaveManager.Instance.Save();
					EventRandom.RestartCurrentRun();
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					MySkill.ChangeImg();
				}
			}
		}

		public class Lucy
		{

		}
	}
}
