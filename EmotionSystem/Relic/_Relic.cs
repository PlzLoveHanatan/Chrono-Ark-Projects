using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using TileTypes;

namespace EmotionSystem
{
	public partial class Relic
	{
		public class Artistic : PassiveItemBase, IP_PlayerTurn, IP_BattleStart_Ones
		{
			private bool isPositivePoints = true;

			public override string DescExtended(string desc)
			{
				string points = isPositivePoints ? ModLocalization.EmotionSystem_Points_Positive : ModLocalization.EmotionSystem_Points_Negative;
				return base.DescExtended(desc).Replace("&a", points);
			}

			public override void Init()
			{
				OnePassive = true;
			}

			public void BattleStart(BattleSystem Ins)
			{
				List<Skill> list = new List<Skill>
				{
					Skill.TempSkill(ModItemKeys.Skill_S_Relic_Artistic, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
					Skill.TempSkill(ModItemKeys.Skill_S_Relic_Artistic_0, Utils.AllyTeam.LucyAlly, Utils.AllyTeam)
				};

				BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, Selection, ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
			}

			private void Selection(SkillButton button)
			{
				if (button.Myskill.MySkill.KeyID == ModItemKeys.Skill_S_Relic_Artistic)
				{
					isPositivePoints = true;
				}
				else
				{
					isPositivePoints = false;
				}
			}

			public void Turn()
			{
				foreach (var ally in Utils.AllyTeam.AliveChars)
				{
					if (isPositivePoints)
					{
						EmotionalManager.GetPosEmotion(ally, null, 3);
					}
					else
					{
						EmotionalManager.GetNegEmotion(ally, null, 3);
					}
				}
			}
		}

		public class Bloody : PassiveItemBase, IP_PlayerTurn, IP_Draw
		{
			public override void Init()
			{
				OnePassive = true;
			}

			public void Turn()
			{
				var skill = Skill.TempSkill(GDEItemKeys.Skill_S_Transcendence_Main, Utils.AllyTeam.LucyAlly, Utils.AllyTeam);
				Utils.InsertSkillInDeck(skill);
			}

			public IEnumerator Draw(Skill Drawskill, bool NotDraw)
			{
				if (!NotDraw && (Drawskill.MySkill.User == "LucyCurse" || Drawskill.MySkill.KeyID == GDEItemKeys.Skill_S_Transcendence_Main))
				{
					ShinyEffect();
					Utils.AllyTeam.AP += 2;
					Utils.AllyTeam.Draw();
				}
				yield return null;
				yield break;
			}
		}
	}
}
