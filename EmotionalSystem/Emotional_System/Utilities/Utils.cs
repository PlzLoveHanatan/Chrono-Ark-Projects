using System;
using ChronoArkMod;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using EmotionSystem;
using ChronoArkMod.ModData.Settings;
using EmotionalSystem;
using GameDataEditor;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using DarkTonic.MasterAudio;
using static EmotionalSystemBuff.Debuffs;
using EmotionalSystemBuff;
using UnityEngine.Playables;
using static EmotionalSystem.LibraryFloor;


namespace EmotionalSystem
{
	public static class Utils
	{
		public static bool EmotionalCoinPaticles => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Emotional Coin Paticles").Value;
		public static bool EGOButtonHotkey => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("EGO Button Hotkey").Value;

		public static bool AllyEmotions => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Ally Emotions").Value;
		public static bool EnemyEmotions => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Enemy Emotions").Value;
		public static bool BossInvitations => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Boss Invitations").Value;
		public static bool EmotionalSounds => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Emotional Sounds").Value;
		public static bool AdditionalSkill => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Additional Skill").Value;

		public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
		public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
		public static BattleChar DummyChar => AllyTeam.DummyChar;

		public static void EmotionsCheck()
		{
			if (!AllyEmotions && !EnemyEmotions)
			{
				var mod = ModManager.getModInfo("EmotionalSystem");
				mod.GetSetting<ToggleSetting>("Ally Emotions").Value = true;
				mod.SaveSetting();
			}
		}

		public static bool EmotionalSystemTutorial
		{
			get
			{
				return ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Tutorial").Value;
			}
			set
			{
				var mod = ModManager.getModInfo("EmotionalSystem");
				mod.GetSetting<ToggleSetting>("Tutorial").Value = value;
				mod.SaveSetting();
			}
		}

		public static Skill CreateSkill(BattleChar bchar, string skill, bool isAddToHand = false, bool isInsert = false, int position = -1)
		{
			Skill newSkill = Skill.TempSkill(skill, bchar, bchar.MyTeam);

			if (isAddToHand)
			{
				BattleSystem.instance.AllyTeam.Add(newSkill, true);
			}

			if (isInsert)
			{
				BattleSystem.DelayInput(AddSkillNoDrawEffect(bchar.MyTeam, newSkill, position));
			}
			return newSkill;
		}

		public static void AddSkillNoDrawEffectVoid(this BattleTeam team, Skill skill, int position = -1)
		{
			if (position == -1)
			{
				team.Skills.Add(skill);
			}
			else
			{
				team.Skills.Insert(position, skill);
			}

			BattleSystem.instance.ActWindow.Draw(team, false);
		}

		public static IEnumerator AddSkillNoDrawEffect(this BattleTeam team, Skill skill, int position = -1)
		{
			if (position == -1)
			{
				team.Skills.Add(skill);
			}
			else
			{
				team.Skills.Insert(position, skill);
			}

			BattleSystem.instance.ActWindow.Draw(team, false);
			yield break;
		}

		public static void AddBuff(BattleChar user, BattleChar target, string buffKey, int buffNum = 1)
		{
			if (user == null || string.IsNullOrEmpty(buffKey)) return;

			for (int i = 0; i < buffNum; i++)
			{
				target.BuffAdd(buffKey, user, false, 0, false, -1, false);
			}
		}

		public static void AddBuff(BattleChar target, string buffKey, int buffNum = 1)
		{
			if (target == null || string.IsNullOrEmpty(buffKey)) return;

			for (int i = 0; i < buffNum; i++)
			{
				target.BuffAdd(buffKey, DummyChar, false, 0, false, -1, false);
			}
		}

		public static void AddDebuff(BattleChar target, BattleChar user, string buffKey, int debuffNum = 1, int percentage = 0)
		{
			if (target == null || string.IsNullOrEmpty(buffKey) || target.Info.Ally) return;

			for (int i = 0; i < debuffNum; i++)
			{
				target.BuffAdd(buffKey, user, false, percentage, false, -1, false);
			}
		}

		public static void RemoveBuff(BattleChar bchar, string buffKey, bool isForceRemove = false)
		{
			if (bchar == null || string.IsNullOrEmpty(buffKey)) return;

			if (bchar.BuffReturn(buffKey, false) != null)
			{
				bchar.BuffRemove(buffKey, isForceRemove);
			}
		}

		public static Buff GetOrAddBuff(BattleChar target, BattleChar user, string buffKey)
		{
			var buff = target.BuffReturn(buffKey, false) ?? target.BuffAdd(buffKey, user, false, 0, false, -1, false);
			return buff;
		}

		public static Buff ReturnBuff(BattleChar bchar, string buffKey)
		{
			var buff = bchar.BuffReturn(buffKey, false) ?? null;
			return buff;
		}

		public static void InsertSkillInDeck(BattleChar bchar, Skill skill, int createSkills = 1)
		{
			if (skill == null) return;

			for (int i = 0; i < createSkills; i++)
			{
				bchar.MyTeam.Skills_Deck.Insert(RandomDeckIndex(bchar), skill);
			}
		}

		public static int RandomDeckIndex(BattleChar bchar)
		{
			return RandomManager.RandomInt(bchar.GetRandomClass().Main, 0, bchar.MyTeam.Skills_Deck.Count + 1);
		}

		public static void PlaySound(string sound)
		{
			if (!string.IsNullOrEmpty(sound) && EmotionalSounds)
			{
				float volume = MasterAudio.MasterVolumeLevel;
				MasterAudio.PlaySound(sound, volume, null, 0f, null, null, false, false);
			}
		}

		public static void TakeNonLethalDamage(BattleChar bchar, int damage = 0, bool isPain = true)
		{
			if (bchar != null)
			{
				AddBuff(bchar, GDEItemKeys.Buff_B_Momori_P_NoDead);
				bchar.Damage(DummyChar, damage, false, isPain, true);
				RemoveBuff(bchar, GDEItemKeys.Buff_B_Momori_P_NoDead, true);
			}
		}

		public static void ApplyBurn(BattleChar target, BattleChar user, int stack = 1)
		{
			if (target.Info.Ally || target == null) return;

			var burn = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionalSystem_Burn) as Burn;

			if (burn != null)
			{
				burn.CurrentBurn += stack;
			}
		}

		public static void ApplyBleed(BattleChar target, BattleChar user, int stack = 1)
		{
			if (target.Info.Ally || target == null) return;


			var bleed = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionalSystem_Bleed) as Bleed;

			if (bleed != null)
			{
				bleed.CurrentBleed += stack;
			}
		}

		public static IEnumerator HealingParticle(BattleChar target, BattleChar user, int healingNum = 0, bool isHealing = false, bool isParticleOut = false, bool isHealLowestAlly = false, bool isPlusHit = false, bool isFreeUse = false)
		{
			yield return null;

			if (target == null) yield break;

			if (isHealing)
			{
				if (isHealLowestAlly)
				{
					target = AllyTeam.AliveChars.Where(x => x != null && x.HP < x.GetStat.maxhp).OrderBy(x => x.HP).FirstOrDefault();

					if (target == null)
					{
						target = AllyTeam.FindChar_LowHP();
					}
				}
				target.Heal(user, healingNum, false, true, null);
			}

			if (isParticleOut)
			{
				Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_EmotionalSystem_DummyHeal, user, user.MyTeam);
				healingParticle.PlusHit = isPlusHit;
				healingParticle.FreeUse = isFreeUse;

				target.ParticleOut(healingParticle, target);
			}
		}

		/// <summary>
		/// Changes one skill to another.
		/// </summary>
		/// <param name="changeFrom">The original skill to be replaced.</param>
		/// <param name="changeTo">The new skill to replace with.</param>
		/// <param name="keepID">
		/// If set to true, keeps the CharinfoSkilldata of the original skill.
		/// The new skill won't be regarded as a created skill if the original skill is not a created skill.
		/// </param>
		/// <param name="keepExtended">
		/// Whether to keep upgrades or other extended data added during battle.
		/// </param>
		public static void SkillChange(this Skill changeFrom, Skill changeTo, bool keepID = true, bool keepExtended = true)
		{
			if (changeFrom.MyButton != null)
			{
				UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("StoryGlitch/GlitchSkillEffect"), changeFrom.MyButton.transform);
				UnityEngine.Object.Destroy(obj, 1f);
			}

			List<Skill_Extended> ExtendedToKeep = new List<Skill_Extended>();
			ExtendedToKeep.AddRange(changeTo.AllExtendeds.Select(ex => ex.Clone() as Skill_Extended));
			foreach (Skill_Extended skill_Extended in changeFrom.AllExtendeds)
			{
				foreach (string text in changeFrom.MySkill.SkillExtended)
				{
					if (keepExtended && !text.Contains(skill_Extended.Name))
					{
						ExtendedToKeep.Add(skill_Extended.Clone() as Skill_Extended);
					}
					skill_Extended.SelfDestroy();
				}
			}

			bool createExcept = keepExtended && changeFrom.isExcept;
			changeFrom.Init(changeTo.MySkill, changeFrom.Master, changeFrom.Master.MyTeam);
			if (createExcept) changeFrom.isExcept = true;

			foreach (var skill_Extended in ExtendedToKeep)
			{
				if (skill_Extended.BattleExtended)
				{
					changeFrom.ExtendedAdd_Battle(skill_Extended);
				}
				else
				{
					changeFrom.ExtendedAdd(skill_Extended);
				}
			}

			changeFrom.Image_Skill = changeTo.Image_Skill;
			changeFrom.Image_Button = changeTo.Image_Button;
			changeFrom.Image_Basic = changeTo.Image_Basic;

			if (changeFrom.CharinfoSkilldata == null) changeFrom.CharinfoSkilldata = new CharInfoSkillData(changeFrom.MySkill);

			changeFrom.CharinfoSkilldata.SkillInfo = changeFrom.MySkill;
			Skill_Extended oldUpgrade = changeFrom.CharinfoSkilldata.SKillExtended;
			if (!keepID)
			{
				changeFrom.CharinfoSkilldata.CopyData(changeTo.CharinfoSkilldata);
			}
			if (keepExtended)
			{
				changeFrom.CharinfoSkilldata.SKillExtended = oldUpgrade;
			}
			else
			{
				changeFrom.CharinfoSkilldata.SKillExtended = changeTo.CharinfoSkilldata.SKillExtended;
			}

			BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));

		}

		
		public static void CastingWasteFixed(this BattleActWindow window, CastingSkill cast)
		{
			SkillButton[] componentsInChildren = window.CastingGroup.GetComponentsInChildren<SkillButton>();
			SkillButton skillButton = componentsInChildren.FirstOrDefault(bt => bt.castskill == cast);
			foreach (IP_SkillCastingQuit ip_SkillCastingQuit in cast.skill.IReturn<IP_SkillCastingQuit>())
			{
				if (ip_SkillCastingQuit != null)
				{
					ip_SkillCastingQuit.SkillCastingQuit(cast);
				}
			}
			if (skillButton != null)
			{
				skillButton.UseWaste();
			}
			window.SetCountSkillVL((window.CastingGroup.GetComponentsInChildren<SkillButton>().Length >= 13) ? 30 : 45);
		}

		public static void UnlockSkillPreview(string key)
		{
			if (!SaveManager.NowData.unlockList.SkillPreView.Contains(key))
			{
				SaveManager.NowData.unlockList.SkillPreView.Add(key);
			}
		}

		public static void UnlockSkillPreview(bool isAllyAbnormality = false, bool isEnemyAbnormality = false, bool isEgo = false)
		{
			if (isAllyAbnormality)
			{
				foreach (var skill in GetAllyAbnormalitySkill())
				{
					UnlockSkillPreview(skill);
				}
			}

			if (isEnemyAbnormality)
			{
				foreach (var skill in GetEnemyAbnormalitySkill())
				{
					UnlockSkillPreview(skill);
				}
			}

			if (isEgo)
			{
				foreach (var skill in GetEgoSkill())
				{
					UnlockSkillPreview(skill);
				}
			}
		}

		public static List<string> GetAllyAbnormalitySkill()
		{
			var instance = DataStore.Instance;

			switch (CurrentFloorType)
			{
				case LibraryFloorType.History:
					return instance.Abnormalities.HistoryKeys;
				case LibraryFloorType.Technological:
					return instance.Abnormalities.TechnologicalKeys;
				case LibraryFloorType.Literature:
					return instance.Abnormalities.LiteratureKeys;
				default:
					return new List<string>();
			}
		}

		public static List<string> GetEnemyAbnormalitySkill()
		{
			return DataStore.Instance.Enemies.EnemyAbnormalityKeyList;
		}

		public static List<string> GetEgoSkill()
		{
			var instance = DataStore.Instance;

			switch (CurrentFloorType)
			{
				case LibraryFloorType.History:
					return instance.EGO.HistoryKeyList;
				case LibraryFloorType.Technological:
					return instance.EGO.TechnologicalKeyList;
				case LibraryFloorType.Literature:
					return instance.EGO.LiteratureKeyList;
				default:
					return new List<string>();
			}
		}

		public static BattleChar RandomAlly()
		{
			var newTargets = AllyTeam.AliveChars.Where(a => a != null).ToList();

			if (newTargets.Count == 0)
			{
				return null;
			}

			int index = RandomManager.RandomInt(DummyChar.GetRandomClass().Target, 0, newTargets.Count);
			var randomTarget = newTargets[index];
			return randomTarget;
		}

		public static List<Skill> AlliesPreview(string key)
		{
			var alliesList = new List<Skill>();

			foreach (var ally in BattleSystem.instance.AllyList)
			{
				var skill = Skill.TempSkill(key, ally, ally.MyTeam);
				if (skill != null)
				{
					alliesList.Add(skill);
				}
			}
			return alliesList;
		}

		public static void RemoveSkill(Skill skill, bool isExclude = true)
		{
			if (skill == null) return;

			skill.isExcept = isExclude;
			skill?.Remove();
		}

		public static void RemoveSkill(Skill skill)
		{
			if (skill == null) return;

			BattleSystem.instance.AllyTeam.Skills.Remove(skill);
			BattleSystem.instance.AllyTeam.Skills_Deck.Remove(skill);
			BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(skill);
			BattleSystem.instance.ActWindow.Draw(skill.Master.MyTeam, false);
		}

		public static IEnumerator RemoveSkillCoroutine(Skill skill, bool isExclude = true)
		{
			yield return null;
			skill.isExcept = isExclude;
			skill?.Remove();
		}
	}
}