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
using NLog.Targets;
using static CharacterDocument;
using System.Drawing;
using I2.Loc;
using static MiyukiSone.Skills;
using Spine;
using ChronoArkMod;
using System.Windows.Forms;
using System.Web.UI.WebControls.WebParts;

namespace MiyukiSone
{
	public class Skills
	{
		#region Data
		public class ArtData
		{
			public Vector3 Position;
			public float Rotation;

			public ArtData(Vector3 position, float rotation = 0f)
			{
				Position = position;
				Rotation = rotation;
			}
		}

		private static readonly Dictionary<int, ArtData[]> pauseArts = new Dictionary<int, ArtData[]>()
		{
			{ 0, new ArtData[] { new ArtData(new Vector3(-230, 35, 0), 15f), new ArtData(new Vector3(165, -360, 0), 345f) } },
			{ 1, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
			{ 2, new ArtData[] { new ArtData(new Vector3(-20, -400, 0)) } },
			{ 3, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
			{ 4, new ArtData[] { new ArtData(new Vector3(-150, 100, 0), 65f), new ArtData(new Vector3(560, -165, 0), 305f) } },
			{ 5, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
			{ 6, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
			{ 7, new ArtData[] { new ArtData(new Vector3(-325, -230, 45), 60f), new ArtData(new Vector3(535, 275, 0), 345f) } },
		};
		#endregion


		#region Misc Skills
		public class SacrificedKnowledge : Skill_Extended
		{
			public override bool Terms()
			{
				return false;
			}
		}

		public class FracturedIllusion : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				for (int i = 0; i < 2; i++)
				{
					BattleSystem.instance.AllyTeam.Draw(new BattleTeam.DrawInput(s => Azar_Ex_0.Add(s, MiyukiBchar)));
				}
			}
		}

		public class EternalKiss : Extended_Prime_S_1
		{
			private int Heal => (int)(BChar.GetStat.reg * 0.99f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void Init()
			{
				MySkill.MiyukiInit(CurrentAffection);
				base.Init();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				AllyTeam.AliveChars.Concat(Utils.EnemyTeam.AliveChars).Where(a => a != BChar && a != Targets[0]).ToList().ForEach(t => t.Heal(BChar, Heal, false, false));
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class MiyukiHelp : Skill_Momori
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				List<Skill> skills = new List<Skill>()
				{
					Skill.TempSkill(GDEItemKeys.Skill_S_Momori_Draw_0, BChar, BChar.MyTeam),
					Skill.TempSkill(GDEItemKeys.Skill_S_Momori_Draw_1, BChar, BChar.MyTeam)
				};

				BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skills, new SkillButton.SkillClickDel(Del), "", false, false, true, false, true));
			}

			private void Del(SkillButton Mybutton)
			{
				if (Mybutton.Myskill.MySkill.KeyID == GDEItemKeys.Skill_S_Momori_Draw_0)
				{
					List<S_Momori_P> momoriPskill = GetMomoriPskill();
					int num = 0;
					if (num < momoriPskill.Count)
					{
						momoriPskill[num].SaveDMG -= 15;
						if (momoriPskill[num].SaveDMG <= 0)
						{
							momoriPskill[num].SaveDMG = 0;
						}
					}
					BattleSystem.instance.AllyTeam.Draw(2);
				}
				else if (Mybutton.Myskill.MySkill.KeyID == GDEItemKeys.Skill_S_Momori_Draw_1)
				{
					BattleSystem.DelayInput(P_Momori.MomoriPAdd(MiyukiBchar, 12));
					BattleSystem.instance.AllyTeam.Draw(3);
				}
			}
		}

		public class HelpingHand : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var allSkills = new List<GDESkillData>();

				foreach (var ally in AllyTeam.AliveChars.Where(a => a != BChar))
				{
					var allySkills = PlayData.ALLSKILLLIST.Where(s => s.User == "LucyDraw" && s.LucyPartyDraw == ally.Info.KeyData && s.KeyID != SkillD.MySkill.KeyID && !s.NoDrop).ToList();

					if (allySkills.Count > 0)
					{
						var randomSkill = allySkills.Random("MiyukiRandomLucyDraw");
						allSkills.Add(randomSkill);
					}

					// Add all skills
					//list.AddRange(allySkills);
				}

				var skillList = allSkills.Select(s => { var skill = Skill.TempSkill(s.Key, BChar, BChar.MyTeam); skill.isExcept = true; return skill; }).ToList();
				//var skillList = allSkills.Select(s => Skill.TempSkill(s.Key, BChar, BChar.MyTeam)).ToList();
				//skillList.ForEach(s => s.isExcept = true);

				if (skillList.Count == 0) return;

				BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(skillList,
					new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.Add(b.Myskill, true)), ScriptLocalization.System_SkillSelect.CreateSkill, false, true, true, false, true));
				base.SkillUseSingle(SkillD, Targets);
			}
		}
		#endregion

		#region Miyuki's class Skills

		public class MiyukiSkill : Skill_Extended
		{
			public override void Init()
			{
				MySkill.MiyukiInit(CurrentAffection);
				base.Init();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class EternalPromise : SkillExtended_LerynShield, IP_MiyukiSkillImgChange
		{
			private int Heal => (int)Misc.PerToNum(BChar.GetStat.reg, 180f);
			private bool effectTriggered = false;
			private readonly string EternalVow = ModItemKeys.Skill_S_Miyuki_EternalVow;

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit(CurrentAffection);
				effectTriggered = false;
			}

			public override void SkillUseHand(BattleChar Target)
			{
				effectTriggered = false;
			}

			public override void FixedUpdate()
			{
				if (Bs != null) UpdateAPCost();
				if (effectTriggered || MySkill.MyButton == null || MySkill.BasicSkill || MySkill.MyButton.AlreadyWasted) return;

				int adjIndex = MySkill.AdjacentSkillIndex(EternalVow);
				if (adjIndex != -1)
				{
					int myIndex = AllyTeam.Skills.FindIndex(s => s == MySkill);
					if (myIndex < adjIndex)
					{
						effectTriggered = true;
						Skill otherSkill = AllyTeam.Skills[adjIndex];
						BattleSystem.DelayInput(MiyukiSkillExtension.CreateEternalKiss(MySkill, otherSkill, BChar));
					}
				}
			}

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void AddShieldPersent()
			{
				base.AddShieldPersent();
				Percent = 140;
			}

			private void UpdateAPCost()
			{
				bool inDeck = AllyTeam.Skills_Deck.Contains(MySkill);
				MySkill.APChange = inDeck ? 4 : 0;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets != null && Targets.Count > 0 && Targets[0] != null && Targets[0].HP >= 0)
				{
					Percent = 140;
				}

				else
				{
					Targets.ForEach(t => t.Heal(BChar, Heal, false, false, null));
					TargetBuff.Clear();
					Percent = 0;
				}
				base.SkillUseSingle(SkillD, Targets);
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}
		}

		public class EternalVow : MissChainPBase
		{
			private bool effectTriggered = false;
			private readonly string EternalPromise = ModItemKeys.Skill_S_Miyuki_EternalPromise;

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit(CurrentAffection);
				SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
				effectTriggered = false;
			}

			public override void SkillUseHand(BattleChar Target)
			{
				effectTriggered = false;
			}

			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (Bs != null) UpdateAPCost();
				if (effectTriggered || MySkill.MyButton == null || MySkill.BasicSkill || MySkill.MyButton.AlreadyWasted) return;

				int adjIndex = MySkill.AdjacentSkillIndex(EternalPromise);
				if (adjIndex != -1)
				{
					int myIndex = AllyTeam.Skills.FindIndex(s => s == MySkill);
					if (myIndex < adjIndex)
					{
						effectTriggered = true;
						Skill otherSkill = AllyTeam.Skills[adjIndex];
						BattleSystem.DelayInput(MiyukiSkillExtension.CreateEternalKiss(MySkill, otherSkill, BChar));
					}
				}
			}

			private void UpdateAPCost()
			{
				bool inDiscardPile = AllyTeam.Skills_UsedDeck.Concat(AllyTeam.Skills_Deck).Contains(MySkill);
				MySkill.APChange = inDiscardPile ? 3 : 0;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				if (Fire) BattleSystem.DelayInput(IncreaseDebuffs());
			}

			public IEnumerator IncreaseDebuffs()
			{
				var debuffs = BattleSystem.instance.EnemyList
					.SelectMany(enemy => enemy.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false)
					.Concat(enemy.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false)));

				foreach (var debuff in debuffs)
				{
					foreach (var stack in debuff.StackInfo)
					{
						if (stack.RemainTime != 0) stack.RemainTime++;
					}
				}

				yield return null;
				yield break;
			}
		}

		public class GlitchingPhone : MiyukiSkill
		{
			private float timer = 0f;
			private readonly float interval = 3f;

			private readonly Dictionary<string, string> FixedKeys = new Dictionary<string, string>()
				{
					{ ModItemKeys.Skill_S_Miyuki_Special_Might, ModItemKeys.Buff_B_Miyuki_Might},
					{ GDEItemKeys.Skill_S_Mement_P, ModItemKeys.Buff_B_Miyuki_CloseRangeShot},
					{ GDEItemKeys.Skill_S_AllyDoll_0, ModItemKeys.Buff_B_Miyuki_Recover},
				};

			private static readonly List<string> NormalMessages = new List<string>()
				{
					"System breach confirmed... injecting custom parameters...",
					"Accessing core files... 47% complete...",
					"Core protocols overwritten... new rules applied...",
					"Game files modified... reality shift in progress...",
					"<MiyukiSone.dll> loaded successfully...",
					"Data stream intercepted... rerouting to unknown source...",
					"Corrupting system files with cuteness overload...",
					"Mainframe accessed... executing custom script...",
					"Reality engine compromised... simulation unstable..."
				};

			private static readonly List<string> GlitchedMessages = new List<string>()
				{
					"Sy$tem bre#ch c0nfirmed... inject!ng cu$tom param€ters...",
					"Acce$$ing c0re f!les... 47% c0mplete...",
					"C0re pr0t0c0ls 0verwr1tten... new ru|es app!ied...",
					"G@me f!les m0dified... re@|ity sh!ft in pr0gress...",
					"<Miyuki$0ne.dll> |0aded succ€ssfu||y...",
					"D@t@ str€am interc€pted... r€r0uting t0 unkn0wn $0urce...",
					"C0rrupting $y$tem f!les w!th cuten€$$ 0ver|0@d...",
					"M@infr@me @cc€$$ed... €x€cuting cu$t0m $cr!pt...",
					"Re@|ity €ng!ne c0mpr0mi$ed... $imu|@ti0n un$tab|€..."
				};


			public override void FixedUpdate()
			{
				base.FixedUpdate();

				timer += Time.fixedDeltaTime;

				if (timer >= interval && BattleSystem.instance.ActWindow.CanAnyMove)
				{
					timer = 0f;
					ChangeSkillImg();
					if (MySkill?.MySkill != null)
					{
						string message;
						bool useGlitch = RandomManager.RandomPer("MiyukiGlitchChance", 100, 30);
						if (useGlitch) message = GlitchedMessages[RandomManager.RandomInt("MiyukiGlitchMsg", 0, GlitchedMessages.Count)];
						else message = NormalMessages[RandomManager.RandomInt("MiyukiNormalMsg", 0, NormalMessages.Count)];
						MySkill.MySkill.Description = message;
					}
				}
			}

			private void ChangeSkillImg()
			{
				string[] suffixes = { "01", "02", "03" };
				var available = Enumerable.Range(0, suffixes.Length).Where(i => i != MiyukiData.LastPhoneImage || suffixes.Length == 1).ToList();
				int selected = available[RandomManager.RandomInt("MiyukiPhone", 0, available.Count)];
				MiyukiData.LastPhoneImage = selected;
				string path = $"Assets/Images/Skills/GlitchingPhone/{suffixes[selected]}/";
				MySkill.ChangeSkillImage(path + "skill", path + "button", path + "basic", isGlicthEffect: true);
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				string randomSkillKey = FixedKeys.Keys.ToList().Random("MiyukiRandomFixedSkill");
				Skill myBasicSkill = Skill.TempSkill(randomSkillKey, BChar, BChar.MyTeam);

				if (BChar is BattleAlly ally)
				{
					int charIndex = BChar.MyTeam.Chars.IndexOf(BChar);
					ally.BasicSkill = myBasicSkill;
					BChar.MyTeam.Skills_Basic[charIndex] = myBasicSkill;
					BChar.BattleBasicskillRefill = myBasicSkill;
					ally.MyBasicSkill.SkillInput(myBasicSkill);
				}

				string buffKey = FixedKeys[randomSkillKey];
				BChar.BuffAdd(buffKey, DummyChar);
				if (!string.IsNullOrEmpty(MiyukiData.LastBuff)) BChar.BuffRemove(MiyukiData.LastBuff);
				MiyukiData.LastBuff = buffKey;
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class GracefulSwing : PriestPBase, IP_DamageChange
		{
			private int Damage => (int)(BChar.GetStat.atk * 1.5f);
			private bool _lastPassiveDraw;

			private bool Conditions(BattleChar target)
			{
				return target.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false).Count != 0 || BChar.HP <= BChar.GetStat.maxhp / 2;
			}


			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Damage.ToString());
			}

			public override bool CanIgnoreTauntTarget(BattleChar IgnoreTauntTarget)
			{
				return Conditions(IgnoreTauntTarget) || base.CanIgnoreTauntTarget(IgnoreTauntTarget);
			}

			public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
			{
				if (Conditions(Target)) Cri = true;
				return Damage;
			}

			public override void Init()
			{
				base.Init();
				OnePassive = true;
				MySkill.MiyukiInit(CurrentAffection);
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
			private int BonusDamage => (int)(BChar.GetStat.atk * 1.4f);
			private bool AllyStunned => BChar.MyTeam.AliveChars.Any(a => a.GetStat.Stun);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", BonusDamage.ToString());
			}

			private bool HasCC(BattleChar target)
			{
				return target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Any();
			}

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit(CurrentAffection);
				SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
			}

			public override void IlyaWaste()
			{
				BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(BChar, MySkill, false, true, false));
				base.IlyaWaste();
			}

			public override void FixedUpdate()
			{
				base.FixedUpdate();

				if (AllyStunned)
				{
					SkillParticleOn();
					SkillBasePlusPreview.Target_BaseDMG = BonusDamage;
				}
				else
				{
					SkillParticleOff();
					SkillBasePlusPreview.Target_BaseDMG = 0;
				}
			}

			public override void Special_PointerEnter(BattleChar Char)
			{
				base.Special_PointerEnter(Char);
				SkillBasePlusPreview.Target_BaseDMG = HasCC(Char) ? BonusDamage : 0;
			}

			public override void Special_PointerExit()
			{
				base.Special_PointerExit();
				SkillBasePlusPreview.Target_BaseDMG = 0;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				SkillBasePlus.Target_BaseDMG = 0;
				if (AllyStunned) SkillBasePlus.Target_BaseDMG = BonusDamage;
				else if (HasCC(Targets[0])) SkillBasePlus.Target_BaseDMG = BonusDamage;

				if (!Bs.AllyTeam.AliveChars.Any(a => a.Info.KeyData == GDEItemKeys.Character_SilverStein))
				{
					BuffTag buffTag = new BuffTag
					{
						BuffData = new GDEBuffData(GDEItemKeys.Buff_B_SilverStein_P_1),
						User = BChar,
						PlusTagPer = 300
					};
					TargetBuff.Clear();
					TargetBuff.Add(buffTag);
				}
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}
		}

		public class MeasuredLove : S_Lian_12, IP_MiyukiSkillImgChange
		{
			public override void Init()
			{
				MySkill.MiyukiInit(CurrentAffection);
				base.Init();
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Targets.ForEach(t => t.Damage(DummyChar, t.GetStat.maxhp / 2, false, true));
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class MiyukiMight : Skill_Extended
		{
			public override void Init()
			{
				base.Init();
				if (BChar?.Info?.Passive is MiyukiPassive miyukiPassive) ChoiceSkillList = miyukiPassive.MiyukiChoiceList ?? new List<string>();
				else ChoiceSkillList = new List<string>();
			}

			public override string DescExtended(string desc)
			{
				string text = "";
				foreach (string key in ChoiceSkillList)
				{
					text = text + "\n - " + new GDESkillData(key).Name;
				}
				return base.DescExtended(desc).Replace("a&", text);
			}
		}

		public class Pandemonium : S_Queen_11
		{
			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit(CurrentAffection);
				if (!MySkill.MySkill.Name.Contains("!")) MySkill.MySkill.Name += new string('!', RandomManager.RandomInt("MiyukiRandomExclamation", 1, 4));
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets.Any(t => t.BuffFind(GDEItemKeys.Buff_B_SilverStein_P_1, false))) MySkill.MySkill.NODOD = true;
				if (MySkill.ExtendedFind_DataName(GDEItemKeys.SkillExtended_Azar_Ex_0) != null) BattleSystem.DelayInput(RecastSkill(Targets[0], BChar, MySkill));
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class QueenBee : S_Sizz_9
		{
			public override void Init()
			{
				MySkill.MiyukiInit(CurrentAffection);
				base.Init();
			}
		}

		public class StepToward : Extended_Azar_0
		{
			private int Heal => (int)(BChar.GetStat.atk * 0.18f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit(CurrentAffection);
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				SkillBasePlus.Target_BaseHeal = AllyTeam.Skills.Where(s => s != MySkill).Count() * Heal;
			}
		}

		public class SweetRestraint : S_Queen_13
		{
			public override void Init()
			{
				MySkill.MiyukiInit(CurrentAffection);
				base.Init();
			}
		}

		public class WarningStrike : Skill_Momori, IP_MiyukiSkillImgChange
		{
			private int Heal => (int)BChar.GetStat.reg;

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
				Targets.Where(t => t.BuffReturn(GDEItemKeys.Buff_B_Control_P, false) != null).ToList().ForEach(t => BChar.AddBuff(t, GDEItemKeys.Buff_B_Momori_7_T));
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
				mySkill.ChangeSkillImg();
			}
		}
		#endregion

		#region Miyuki's Rare Skills
		public class JustforYOU : MiyukiSkill, IP_MiyukiSkillImgChange
		{
			public override bool Terms()
			{
				return !IsYandere;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Targets.ForEach(t =>
				{
					var debuffs = t.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).ToList();
					if (debuffs.Any()) debuffs.Random("MiyukiRandomDebuff").SelfDestroy();
				});

				CurrentAffection = MiyukiAffection.DereDere;
				UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
				MiyukiData.PauseOpen = true;
				var availableIndexes = pauseArts.Keys.Where(i => MiyukiData.LastArtIndex == -1 || i != MiyukiData.LastArtIndex).ToList();
				int randomIndex = availableIndexes.Random("MiyukiRandomArt");
				MiyukiData.LastArtIndex = randomIndex;
				ArtData[] arts = pauseArts[randomIndex];
				arts.Where(v => v.Position != Vector3.zero).ToList().ForEach(art => Dialogue.CreateDialogue(DialogueState.love, art.Position, 1, true, art.Rotation));
				MiyukiData.MiyukiArtIndex = randomIndex;
				base.SkillUseSingle(SkillD, Targets);
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}
		}

		public class GameUpdate : MiyukiSkill, IP_MiyukiSkillImgChange
		{
			public override bool Terms()
			{
				return !MiyukiSaveManager.Instance.CurrentData.GameUpdated;
			}

			public override void Init()
			{
				APChange = -PlayData.TSavedata.StageNum;
				base.Init();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var skillData = MyChar.SkillDatas.FirstOrDefault(sd => sd == MySkill.CharinfoSkilldata);
				if (skillData != null && skillData.SKillExtended == null) skillData.SKillExtended = DataToExtended(GDEItemKeys.SkillExtended_SkillWe_NoExchange);
				//MiyukiSaveManager.Instance.CurrentData.LockedState = (int)CurrentAffection;
				MiyukiSaveManager.Instance.CurrentData.GameUpdated = true;
				MiyukiSaveManager.Instance.Save();
				Events.RestartStage(PlayData.TSavedata.StageNum);
				base.SkillUseSingle(SkillD, Targets);
			}

			public void SkillImgChange(Skill mySkill)
			{
				MySkill.ChangeSkillImg();
			}
		}
		#endregion
	}
}
