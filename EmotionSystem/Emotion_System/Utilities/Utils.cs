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
using GameDataEditor;
using Newtonsoft.Json.Linq;
using DarkTonic.MasterAudio;
using UnityEngine.Playables;
using static EmotionSystem.DataStore.LibraryFloor;
using Spine;


namespace EmotionSystem
{
	public static class Utils
	{
		public static bool EmotionPointsParticles => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Emotion Points Particles").Value;
		public static bool EGOButtonHotkey => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("EGO Button Hotkey").Value;

		public static bool InvestigatorEmotions => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Investigator Emotions").Value;
		public static bool GuestEmotions => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Guest Emotions").Value;
		public static bool BossInvitations => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Boss Invitations").Value;
		public static bool EmotionalSounds => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Emotional Sounds").Value;
		public static bool DistortedBosses => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Distorted Bosses").Value;
		public static bool Distortions => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Distortions").Value;
		public static bool ChibiAngela => ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Chibi Angela").Value;

		public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
		public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
		public static BattleChar DummyChar => AllyTeam.DummyChar;

		public static void EmotionsCheck()
		{
			if (!InvestigatorEmotions && !GuestEmotions)
			{
				var mod = ModManager.getModInfo("EmotionSystem");
				mod.GetSetting<ToggleSetting>("Ally Emotions").Value = true;
				mod.SaveSetting();
			}
		}

		public static bool EmotionSystemTutorial
		{
			get
			{
				return ModManager.getModInfo("EmotionSystem").GetSetting<ToggleSetting>("Tutorial").Value;
			}
			set
			{
				var mod = ModManager.getModInfo("EmotionSystem");
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

		public static void AddBarrier(BattleChar target, BattleChar user, string buffKey, int barrierNum = 0)
		{
			if (target == null || user == null || string.IsNullOrEmpty(buffKey)) return;

			target.BuffAdd(buffKey, user, false, 0, false, -1, false).BarrierHP += barrierNum;
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

		public static Buff GetOrAddBuff(BattleChar target, BattleChar user, string buffKey, int percentage = 0)
		{
			var buff = target.BuffReturn(buffKey, false) ?? target.BuffAdd(buffKey, user, false, percentage, false, -1, false);
			return buff;
		}

		public static Buff ReturnBuff(BattleChar bchar, string buffKey)
		{
			var buff = bchar.BuffReturn(buffKey, false) ?? null;
			return buff;
		}

		public static void InsertSkillInDeck(Skill skill, int createSkills = 1)
		{
			if (skill == null) return;

			for (int i = 0; i < createSkills; i++)
			{
				AllyTeam.Skills_Deck.Insert(RandomDeckIndex(), skill);
			}
		}

		public static int RandomDeckIndex()
		{
			return RandomManager.RandomInt(RandomClassKey.AllSkill, 0, AllyTeam.Skills_Deck.Count + 1);
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

		public static void ApplyExtended(Skill skill, string extendedKey, bool isMultipleExtended = false)
		{
			if (skill == null || string.IsNullOrEmpty(extendedKey)) return;

			var existing = skill.ExtendedFind_DataName(extendedKey);

			if (isMultipleExtended || existing == null)
			{
				skill.ExtendedAdd(extendedKey);
			}
		}

		public static void ApplyExtended(List<Skill> skillList, string extendedKey, bool? isHealingSkill = null, bool? isDamageSkill = null, bool isMultipleExtended = false, int extendedNum = 2, bool isRandomSkills = false)
		{
			if (skillList == null && skillList.Count == 0 && string.IsNullOrEmpty(extendedKey)) return;

			IEnumerable<Skill> filtered = skillList;

			if (isHealingSkill == true && isDamageSkill == true)
			{
				filtered = filtered.Where(s => s.IsHeal || s.IsDamage);
			}
			else if (isHealingSkill == true)
			{
				filtered = filtered.Where(s => s.IsHeal);
			}
			else if (isDamageSkill == true)
			{
				filtered = filtered.Where(s => s.IsDamage);
			}

			var list = filtered.ToList();

			if (list.Count == 0)
			{
				list = new List<Skill>(AllyTeam.Skills);
			}

			if (!isRandomSkills)
			{
				foreach (var skill in list.Take(Math.Min(extendedNum, list.Count)))
				{
					ApplyExtendedToSkill(skill, extendedKey, isMultipleExtended);
				}
			}
			else
			{
				for (int i = 0; i < extendedNum; i++)
				{
					int randomIndex = RandomManager.RandomInt(DummyChar.GetRandomClass().SkillSelect, 0, list.Count);
					ApplyExtendedToSkill(list[randomIndex], extendedKey, isMultipleExtended);
				}
			}
		}

		public static void ApplyExtendedToSkill(Skill skill, string extendedKey, bool isMultipleExtended)
		{
			if (skill != null)
			{
				var existing = skill.ExtendedFind_DataName(extendedKey);

				if (isMultipleExtended || existing == null)
				{
					skill.ExtendedAdd(extendedKey);
				}
			}
		}

		public static void ApplyBurn(BattleChar target, BattleChar user, int stack = 1, int percentage = 0)
		{
			if (target.Info.Ally || target == null) return;

			var burn = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionSystem_Burn, percentage) as Debuffs.Burn;

			if (burn != null)
			{
				burn.CurrentBurn += stack;
			}
		}

		public static void ApplyBurn(List<BattleChar> Targets, BattleChar user, int stack = 1, int percentage = 0)
		{
			foreach (var target in Targets)
			{
				if (target.Info.Ally || target == null) return;

				var burn = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionSystem_Burn, percentage) as Debuffs.Burn;

				if (burn != null)
				{
					burn.CurrentBurn += stack;
				}
			}
		}

		public static void ApplyBleed(BattleChar target, BattleChar user, int stack = 1, int percentage = 0)
		{
			if (target.Info.Ally || target == null) return;

			var bleed = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionSystem_Bleed, percentage) as Debuffs.Bleed;

			if (bleed != null)
			{
				bleed.CurrentBleed += stack;
			}
		}

		public static void ApplyBleed(List<BattleChar> Targets, BattleChar user, int stack = 1, int percentage = 0)
		{
			foreach (var target in Targets)
			{
				if (target.Info.Ally || target == null) return;

				var bleed = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionSystem_Bleed, percentage) as Debuffs.Bleed;

				if (bleed != null)
				{
					bleed.CurrentBleed += stack;
				}
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
				Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_EmotionSystem_DummyHeal, user, user.MyTeam);
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

		public static void UnlockSkillPreview(bool isAllyAbnormality = false, bool isEnemyAbnormality = false, bool isEgo = false, bool isLucy = false)
		{
			List<IEnumerable<string>> skillLists = new List<IEnumerable<string>>();

			if (isAllyAbnormality)
			{
				skillLists.Add(GetAllyAbnormalitySkill());
			}

			if (isEnemyAbnormality)
			{
				skillLists.Add(GetEnemyAbnormalitySkill());
			}

			if (isEgo)
			{
				skillLists.Add(GetEgoSkill());
			}

			if (isLucy)
			{
				skillLists.Add(GetLucySkill());
			}

			foreach (IEnumerable<string> list in skillLists)
			{
				foreach (string skill in list)
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
				case DataStore.LibraryFloorType.History:
					return instance.Abnormalities.HistoryKeys;
				case DataStore.LibraryFloorType.Technological:
					return instance.Abnormalities.TechnologicalKeys;
				case DataStore.LibraryFloorType.Literature:
					return instance.Abnormalities.LiteratureKeys;
				default:
					return new List<string>();
			}
		}

		public static List<string> GetEnemyAbnormalitySkill()
		{
			return DataStore.Instance.Guest.AbnormalityKeyList;
		}

		public static List<string> GetLucySkill()
		{
			return DataStore.Instance.LucyKeyList;
		}

		public static List<string> GetEgoSkill()
		{
			var instance = DataStore.Instance;

			switch (CurrentFloorType)
			{
				case DataStore.LibraryFloorType.History:
					return instance.EGO.HistoryKeyList;
				case DataStore.LibraryFloorType.Technological:
					return instance.EGO.TechnologicalKeyList;
				case DataStore.LibraryFloorType.Literature:
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

		public static List<Skill> EnemiesPreview(string key)
		{
			var alliesList = new List<Skill>();

			foreach (var ally in BattleSystem.instance.EnemyList)
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

			AllyTeam.Skills.Remove(skill);
			AllyTeam.Skills_Deck.Remove(skill);
			AllyTeam.Skills_UsedDeck.Remove(skill);
		}

		public static IEnumerator RemoveSkillCoroutine(Skill skill, bool isExclude = true)
		{
			yield return null;
			skill.isExcept = isExclude;
			skill?.Remove();
		}
	}
}