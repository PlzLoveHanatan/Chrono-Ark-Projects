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
using DarkTonic.MasterAudio;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms;

namespace MiyukiSone
{
	public class Skills
	{
		#region Miyuki Main Data
		public class MiyukiSkill : Skill_Extended, IP_MiyukiSkillPreviewChange
		{
			public override void Init()
			{
				MySkill.MiyukiInit();
				base.Init();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
			}
		}

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
			{ 6, new ArtData[] { new ArtData(new Vector3(-325, -230, 45), 60f), new ArtData(new Vector3(535, 275, 0), 345f) } },
			{ 7, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
			{ 8, new ArtData[] { new ArtData(new Vector3(195, 180, 40), 345f), new ArtData(new Vector3(75, -250, 40), 50) } },
			{ 9, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
			{ 10, new ArtData[] { new ArtData(new Vector3(-135, -210, 0), 345f), new ArtData(new Vector3(50, -410, 0), 5f) } },
			{ 11, new ArtData[] { new ArtData(new Vector3(0, 0, 0)) } },
		};

		public class GlitchingPhoneData
		{
			private class GlitchingPhoneText : IP_MiyukiLocalizable
			{
				public string Key { get; set; }
				public string Type { get; set; }
				public string English { get; set; }
				public string Korean { get; set; }
				public string Japanese { get; set; }
				public string Chinese { get; set; }
				public string Chinese_TW { get; set; }
				public string AudioFile { get; set; }
			}

			private static List<GlitchingPhoneText> GlicthingPhoneText;

			private static void LoadEvents()
			{
				if (GlicthingPhoneText != null) return;

				string jsonContent = MiyukiJsonReader.LoadJson("GlitchingPhone.json");
				if (jsonContent == null) return;

				try
				{
					GlicthingPhoneText = JsonConvert.DeserializeObject<List<GlitchingPhoneText>>(jsonContent);
				}
				catch (Exception ex)
				{
					Debug.LogError($"[Miyuki] LoadEvents: {ex.Message}");
				}
			}

			public static string GetRandomNormalMessage()
			{
				if (GlicthingPhoneText == null) LoadEvents();
				var normalMessages = GlicthingPhoneText?.Where(t => t.Type == "Normal").ToList();
				if (normalMessages == null || normalMessages.Count == 0) return null;
				var randomLine = normalMessages.RandomElement();
				return GetLocalizedText(randomLine);
			}

			public static string GetRandomGlitchedMessage()
			{
				if (GlicthingPhoneText == null) LoadEvents();
				var glitchedMessages = GlicthingPhoneText?.Where(t => t.Type == "Glitched").ToList();
				if (glitchedMessages == null || glitchedMessages.Count == 0) return null;
				var randomLine = glitchedMessages.RandomElement();;
				return GetLocalizedText(randomLine);
			}
		}
		#endregion

		#region Misc Skills	

		public class CloseRangeShot : S_Mement_P, IP_MiyukiSkillPreviewChange
		{
			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview(GDEItemKeys.Skill_S_Mement_LucyDraw);
			}
		}

		public class EternalKiss : Extended_Prime_S_1, IP_MiyukiSkillPreviewChange
		{
			private int Heal => (int)(BChar.GetStat.reg * 0.99f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void Init()
			{
				MySkill.MiyukiInit();
				base.Init();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				CurrentAffection = MiyukiAffection.DereDere;
				BattleSystem.instance.AllyTeam.AliveChars.Concat(BattleSystem.instance.EnemyTeam.AliveChars).Where(a => a != BChar && a != Targets[0]).ToList().ForEach(t => t.Heal(BChar, Heal, false, false));
				base.SkillUseSingle(SkillD, Targets);
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

		public class Joker : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				DiscardSingle(false);
				BattleSystem.instance.AllyTeam.Draw();
			}

			public override void DiscardSingle(bool Click)
			{
				base.DiscardSingle(Click);
				BattleSystem.DelayInput(HitEffect());
			}

			public IEnumerator HitEffect()
			{
				foreach (BattleAlly ally in BattleSystem.instance.AllyList)
				{
					if (ally.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false).Count > 0 || ally.GetBuffs(BattleChar.GETBUFFTYPE.CC, false).Count > 0)
					{
						Skill skill = Skill.TempSkill(GDEItemKeys.Skill_S_JokerCard_Effect, BattleSystem.instance.AllyTeam.LucyChar, BattleSystem.instance.AllyTeam.LucyAlly.MyTeam);
						skill.PlusHit = true;
						skill.FreeUse = true;
						BattleSystem.instance.AllyTeam.DummyChar.ParticleOut(MySkill, skill, ally);
					}
				}

				yield return new WaitForSecondsRealtime(1f);
				string sound = MiyukiDecides ? "Laugh_02" : "Laugh_04";
				MasterAudio.PlaySound(sound, 1f, null, 0f, null, null, false, false);
			}
		}

		public class HelpingHand : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var allSkills = new List<GDESkillData>();

				foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki))
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

		public class MiyukiMight : Skill_Extended, IP_MiyukiSkillPreviewChange
		{
			public override void Init()
			{
				base.Init();
				ChoiceSkillList = GetMiyukiPassive.MiyukiChoiceList ?? new List<string>();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview(GDEItemKeys.Skill_S_Phoenix_Draw);
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

		public class MiyukiPhone : Skill_Extended, IP_TurnEnd
		{
			private int apReduce;

			public override void Init()
			{
				base.Init();
				OnePassive = true;
				MySkill.APChange = apReduce;
			}

			public void TurnEnd()
			{
				apReduce--;
				Init();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				for (int i = 0; i < 10; i++)
				{
					Skill randomRare = PlayData.GetLucySkill(false).Random("RandomLucyRare");
					Skill skill = Skill.TempSkill(randomRare.MySkill.KeyID, BattleSystem.instance.AllyTeam.LucyAlly, BattleSystem.instance.AllyTeam.LucyAlly.MyTeam);
					skill.isExcept = true;
					BattleSystem.instance.EffectDelaysAfter.Enqueue(CreateSkills(skill));
				}
				base.SkillUseSingle(SkillD, Targets);
			}

			private IEnumerator CreateSkills(Skill skill)
			{
				if (skill == null) yield break;
				if (BattleSystem.instance.AllyTeam.Skills.Count < 10) BattleSystem.instance.AllyTeam.Add(skill, true);
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

		public class SacrificedKnowledge : Skill_Extended
		{
			public override bool Terms()
			{
				return false;
			}
		}

		public class YabeleyTomatoJuice : Skill_Extended
		{
			public override void BattleStartDeck(List<Skill> Skills_Deck)
			{
				//Skills_Deck.Remove(MySkill);
				//Skills_Deck.Insert(0, MySkill);
			}

			public override IEnumerator DrawAction()
			{
				//BattleSystem.instance.AllyTeam.Draw();
				return base.DrawAction();
			}

			public override bool Terms()
			{
				return false;
			}
		}
		#endregion

		#region Miyuki's class Skills
		public class EternalPromise : SkillExtended_LerynShield, IP_MiyukiSkillImgChange, IP_MiyukiSkillPreviewChange
		{
			private int Heal => (int)Misc.PerToNum(BChar.GetStat.reg, 180f);
			private bool effectTriggered = false;
			private readonly string EternalVow = ModItemKeys.Skill_S_Miyuki_EternalVow;

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit();
				effectTriggered = false;
			}

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}

			public override void SkillUseHand(BattleChar Target)
			{
				effectTriggered = false;
			}

			public override void FixedUpdate()
			{
				if (BattleSystem.instance != null) UpdateAPCost();
				if (effectTriggered || MySkill.MyButton == null || MySkill.BasicSkill || MySkill.MyButton.AlreadyWasted) return;

				int adjIndex = MySkill.AdjacentSkillIndex(EternalVow);
				if (adjIndex != -1)
				{
					int myIndex = BattleSystem.instance.AllyTeam.Skills.FindIndex(s => s == MySkill);
					if (myIndex < adjIndex)
					{
						effectTriggered = true;
						Skill otherSkill = BattleSystem.instance.AllyTeam.Skills[adjIndex];
						BattleSystem.DelayInput(MiyukiExtension.CreateEternalKiss(MySkill, otherSkill, BChar));
					}
				}
			}

			public override void AddShieldPersent()
			{
				base.AddShieldPersent();
				Percent = 140;
			}

			private void UpdateAPCost()
			{
				bool inDeck = BattleSystem.instance.AllyTeam.Skills_Deck.Contains(MySkill);
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
		}

		public class EternalVow : MissChainPBase, IP_MiyukiSkillPreviewChange
		{
			private bool effectTriggered = false;
			private readonly string EternalPromise = ModItemKeys.Skill_S_Miyuki_EternalPromise;

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit();
				SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
				effectTriggered = false;
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public override void SkillUseHand(BattleChar Target)
			{
				effectTriggered = false;
			}

			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (BattleSystem.instance != null) UpdateAPCost();
				if (effectTriggered || MySkill.MyButton == null || MySkill.BasicSkill || MySkill.MyButton.AlreadyWasted) return;

				int adjIndex = MySkill.AdjacentSkillIndex(EternalPromise);
				if (adjIndex != -1)
				{
					int myIndex = BattleSystem.instance.AllyTeam.Skills.FindIndex(s => s == MySkill);
					if (myIndex < adjIndex)
					{
						effectTriggered = true;
						Skill otherSkill = BattleSystem.instance.AllyTeam.Skills[adjIndex];
						BattleSystem.DelayInput(MiyukiExtension.CreateEternalKiss(MySkill, otherSkill, BChar));
					}
				}
			}

			private void UpdateAPCost()
			{
				bool inDiscardPile = BattleSystem.instance.AllyTeam.Skills_UsedDeck.Concat(BattleSystem.instance.AllyTeam.Skills_Deck).Contains(MySkill);
				MySkill.APChange = inDiscardPile ? 3 : 0;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				if (Fire) BattleSystem.DelayInput(IncreaseDebuffs());
			}

			public IEnumerator IncreaseDebuffs()
			{
				var debuffs = BattleSystem.instance.EnemyList.SelectMany(enemy => enemy.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false).Concat(enemy.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false)));

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
			private readonly float interval = RandomManager.RandomFloat("MiyukiGlitchTime", 0.5f, 3f);

			private static readonly Dictionary<string, string> FixedKeys = new Dictionary<string, string>()
			{
				{ ModItemKeys.Skill_S_Miyuki_Special_Might, ModItemKeys.Buff_B_Miyuki_Might},
				{ ModItemKeys.Skill_S_Miyuki_Special_Close, ModItemKeys.Buff_B_Miyuki_CloseRangeShot},
				{ GDEItemKeys.Skill_S_AllyDoll_0, ModItemKeys.Buff_B_Miyuki_Recover},
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
						MySkill.MySkill.Description = MiyukiDecides ? GlitchingPhoneData.GetRandomNormalMessage() : GlitchingPhoneData.GetRandomGlitchedMessage();
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
					BChar.MyTeam.BasicSkillRefill(BChar, BChar.BattleBasicskillRefill);
					int charIndex = BChar.MyTeam.Chars.IndexOf(BChar);
					ally.BasicSkill = myBasicSkill;
					BChar.MyTeam.Skills_Basic[charIndex] = myBasicSkill;
					BChar.BattleBasicskillRefill = myBasicSkill;
					ally.MyBasicSkill.SkillInput(myBasicSkill);
				}

				string buffKey = FixedKeys[randomSkillKey];
				if (BChar.BuffReturn(buffKey, false) == null)
				{
					if (!string.IsNullOrEmpty(MiyukiData.LastGlitchedPhoneBuff) && MiyukiData.LastGlitchedPhoneBuff != buffKey) BChar.BuffRemove(MiyukiData.LastGlitchedPhoneBuff);
					BChar.BuffAdd(buffKey, DummyChar);
					MiyukiData.LastGlitchedPhoneBuff = buffKey;
				}

				if (buffKey == ModItemKeys.Buff_B_Miyuki_CloseRangeShot)
				{
					string skillKey = MiyukiDecides ? GDEItemKeys.Skill_S_Mement_5 : GDEItemKeys.Skill_S_Mement_0;
					var skill = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);
					if (skill != null) BattleSystem.instance.AllyTeam.Add(skill, true);
					skill.isExcept = true;
					skill.NotCount = true;
					skill.MySkill.Name = "Miyuki's " + skill.MySkill.Name;
				}

				if (buffKey == ModItemKeys.Buff_B_Miyuki_Might)
				{
					string rewardKey = Utils.RandomPer(5) ? GDEItemKeys.Item_Consume_GoldenBread : GDEItemKeys.Item_Consume_Bread;
					InventoryManager.Reward(ItemBase.GetItem(rewardKey));
				}

				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class GracefulSwing : PriestPBase, IP_DamageChange, IP_MiyukiSkillPreviewChange
		{
			private int Damage => (int)(BChar.GetStat.atk * 1.5f);
			private bool _lastPassiveDraw;

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Damage.ToString());
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}
			public override void Init()
			{
				base.Init();
				OnePassive = true;
				MySkill.MiyukiInit();
			}

			private bool Conditions(BattleChar target)
			{
				return target.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false).Count != 0 || BChar.HP <= BChar.GetStat.maxhp / 2;
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

		public class HappyBirthday : SkillExtedned_IlyaP, IP_MiyukiSkillImgChange, IP_MiyukiSkillPreviewChange
		{
			private int BonusDamage => (int)(BChar.GetStat.atk * 1.4f);
			private bool AllyStunned => BChar.MyTeam.AliveChars.Any(a => a.GetStat.Stun);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", BonusDamage.ToString());
			}

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit();
				SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}

			private bool HasCC(BattleChar target)
			{
				return target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Any();
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
				if (AllyStunned || HasCC(Targets[0])) SkillBasePlus.Target_BaseDMG = BonusDamage;

				if (!BattleSystem.instance.AllyTeam.AliveChars.Any(a => a.Info.KeyData == GDEItemKeys.Character_SilverStein))
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
		}

		public class MeasuredLove : S_Lian_12, IP_MiyukiSkillImgChange, IP_MiyukiSkillPreviewChange
		{
			public override void Init()
			{
				MySkill.MiyukiInit();
				base.Init();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
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

		public class Pandemonium : S_Queen_11, IP_MiyukiSkillPreviewChange
		{
			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit();
				if (!MySkill.MySkill.Name.Contains("!")) MySkill.MySkill.Name += new string('!', RandomManager.RandomInt("MiyukiRandomExclamation", 1, 4));
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets.Any(t => t.BuffFind(GDEItemKeys.Buff_B_SilverStein_P_1, false))) MySkill.MySkill.NODOD = true;
				if (MySkill.ExtendedFind_DataName(GDEItemKeys.SkillExtended_Azar_Ex_0) != null) RecastSkill(Targets[0], BChar, MySkill);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class QueenBee : S_Sizz_9, IP_MiyukiSkillPreviewChange
		{
			public override void Init()
			{
				MySkill.MiyukiInit();
				base.Init();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}
		}

		public class StepToward : Extended_Azar_0, IP_MiyukiSkillPreviewChange
		{
			private int Heal => (int)(BChar.GetStat.reg * 0.18f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void Init()
			{
				base.Init();
				MySkill.MiyukiInit();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				SkillBasePlus.Target_BaseHeal = BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).Count() * Heal;
			}
		}

		public class SweetRestraint : S_Queen_13, IP_MiyukiSkillPreviewChange
		{
			public override void Init()
			{
				MySkill.MiyukiInit();
				base.Init();
			}

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}
		}

		public class WarningStrike : Skill_Momori, IP_MiyukiSkillImgChange, IP_MiyukiSkillPreviewChange
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

			public Skill SkillPreviewChange()
			{
				return MySkill.MiyukiSkillPreview();
			}

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Targets.Where(t => t.BuffReturn(GDEItemKeys.Buff_B_Control_P, false) != null).ToList().ForEach(t => t.AddBuff(GDEItemKeys.Buff_B_Momori_0));
				var momoriPskill = GetMomoriPskill();
				if (momoriPskill.Any())
				{
					var p = momoriPskill.First();
					p.SaveDMG = Math.Max(0, p.SaveDMG - Heal);
				}
				base.SkillUseSingle(SkillD, Targets);
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

			public void SkillImgChange(Skill mySkill)
			{
				mySkill.ChangeSkillImg();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Targets.ForEach(t =>
				{
					var debuffs = t.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).ToList();
					if (debuffs.Any()) debuffs.Random("MiyukiRandomDebuff").SelfDestroy();
				});

				if (MiyukiForces) CurrentAffection = MiyukiAffection.DereDere;
				UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
				MiyukiData.PauseOpen = true;
				var availableIndexes = pauseArts.Keys.Where(i => MiyukiData.LastArtIndex == -1 || i != MiyukiData.LastArtIndex).ToList();
				int randomIndex = availableIndexes.RandomElement();
				MiyukiData.LastArtIndex = randomIndex;
				ArtData[] arts = pauseArts[randomIndex];
				arts.Where(v => v.Position != Vector3.zero).ToList().ForEach(art => Dialogue.CreateDialogue(DialogueState.love, art.Position, 1, true, art.Rotation));
				MiyukiData.MiyukiArtIndex = randomIndex;
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class FinalView : MiyukiSkill
		{
			public override void Init()
			{
				PlusSkillStat.Penetration = 100f;
				base.Init();
			}

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", (MiyukiData.FinalViewDamage * 10).ToString()).Replace("&b", "10").Replace("&c", MiyukiData.FinalViewCharge >= 1 ? "Active" : "Inactive".ToString());
			}

			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (BattleSystem.instance != null) SkillBasePlus.Target_BaseDMG = MiyukiData.FinalViewDamage * 10;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (MiyukiForces) CurrentAffection = MiyukiAffection.Yandere;
				SkillBasePlus.Target_BaseDMG = MiyukiData.FinalViewDamage;
				BattleSystem.DelayInput(TargetCheck(Targets[0]));
				for (int i = 0; i < 2; i++)
				{
					BattleSystem.DelayInput(RecastSkill(Targets[0]));
				}
				base.SkillUseSingle(SkillD, Targets);
			}

			private IEnumerator RecastSkill(BattleChar target)
			{
				Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Rare_FinalView_Particle, BChar, BChar.MyTeam);
				Skill_Extended ex = new Skill_Extended()
				{
					IsDamage = true,
					PlusSkillStat = new Stat() { Penetration = 100f },
					SkillBasePlus = new SkillBasestat() { Target_BaseDMG = MiyukiData.FinalViewDamage * 10 }
				};
				skill.ExtendedAdd(ex);
				target = (target.IsDead || target == null || target.Info.Ally) && BattleSystem.instance.EnemyTeam.AliveChars.Count > 0 ? BattleSystem.instance.EnemyTeam.AliveChars.Random("RandomEnemy") : target;
				BattleSystem.DelayInput(TargetCheck(target));
				if (target == null) yield break;
				yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(skill, target, false, false, false, null));
			}

			private IEnumerator TargetCheck(BattleChar target)
			{
				yield return null;
				if (target.Info.Ally && target.IsDead) MiyukiData.FinalViewDamage++;
			}
		}

		public class GameUpdate : MiyukiSkill, IP_MiyukiSkillImgChange
		{
			public override bool Terms()
			{
				return !MiyukiData.GameUpdated && MiyukiInParty;
			}

			public override void Init()
			{
				APChange = MiyukiResult() ? -PlayData.TSavedata.StageNum : 0;
				base.Init();
			}

			public void SkillImgChange(Skill mySkill)
			{
				MySkill.ChangeSkillImg();
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var skillData = MyChar.SkillDatas.FirstOrDefault(sd => sd == MySkill.CharinfoSkilldata);
				if (skillData != null)
				{
					if (skillData.SKillExtended != null) skillData.SKillExtended = null;
					skillData.SKillExtended = DataToExtended(GDEItemKeys.SkillExtended_SkillWe_NoExchange);
				}

				//MiyukiSaveManager.Instance.CurrentData.LockedState = (int)CurrentAffection;

				MiyukiData.GameUpdated = true;
				MiyukiSaveManager.Instance.CurrentData.GameUpdated = true;
				MiyukiSaveManager.Instance.Save();
				Events.RestartStage();
				base.SkillUseSingle(SkillD, Targets);
			}
		}
		#endregion
	}
}
