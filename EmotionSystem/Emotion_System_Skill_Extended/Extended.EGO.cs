using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Extended
	{
		public class EGO
		{
			public class Cooldown : Skill_Extended
			{
				public Ex_EGO EGO_Extended;

				public override void FixedUpdate()
				{
					if (EGO_Extended == null || EGO_Extended.NowCooldown <= 0)
					{
						SelfDestroy();
					}
					else if (EGO_Extended != null)
					{
						BuffIconStackNum = EGO_Extended.NowCooldown;
					}
				}
			}

			public class Ex_EGO : Skill_Extended
			{
				//private bool Removed;
				public bool OncePerFight;
				public int Cooldown;
				public int NowCooldown;

				public string EGO_Once => ModLocalization.EGO_Skill_Once ?? "";
				public string EGO_Cooldown => ModLocalization.EGO_Skill_Cooldown ?? "";

				public override void Init()
				{
					base.Init();
					Cooldown = 3;
					NowCooldown = 0;
				}

				public override string DescExtended(string desc)
				{
					return CooldownText + "" + base.DescExtended(desc);
				}

				public override bool Terms()
				{
					return NowCooldown <= 0;
				}

				public override void SkillUseHand(BattleChar Target)
				{
					UseEGO();
				}

				public override void TurnUpdate()
				{
					if (NowCooldown > 0)
					{
						NowCooldown--;
						BattleSystem.DelayInput(UpdateExtended());
					}
				}

				public IEnumerator UpdateExtended()
				{
					if (NowCooldown > 0)
					{
						var exFind = MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_EGO_Cooldown) as Cooldown;
						if (exFind == null)
						{
							var ex = MySkill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_EGO_Cooldown) as Cooldown;
							ex.EGO_Extended = this;
						}
					}
					yield return null;
				}

				public string CooldownText
				{
					get
					{
						var text = "";

						if (OncePerFight)
						{
							text = EGO_Once + "\n";
						}
						else if (NowCooldown > 0)
						{
							text = EGO_Cooldown.Replace("&a", NowCooldown.ToString()) + "\n";
						}
						return text;
					}
				}

				public void UseEGO()
				{
					try
					{
						if (OncePerFight)
						{
							EmotionSystem_EGO_Button.instance?.RemoveEGOSkill(MySkill);
						}
						else
						{
							NowCooldown = Cooldown;
							BattleSystem.DelayInput(UpdateExtended());
						}
						BattleSystem.DelayInput(ChangeHand());
					}
					catch (Exception e)
					{
						Debug.Log(e.ToString());
					}
				}

				public IEnumerator ChangeHand()
				{
					if (EmotionSystem_EGO_Button.instance.OpenEGOHand)
					{
						EmotionSystem_EGO_Button.instance.ChangeHand();
					}

					yield break;
				}
			}
		}
	}
}
