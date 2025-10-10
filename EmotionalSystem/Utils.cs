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

namespace EmotionalSystem
{
	public static class Utils
	{
		public static bool UseParticle => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Use Particle").Value;

		public static bool EGOButtonHotkey => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("EGO Button Hotkey").Value;

		public static bool AllyEmotions => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Ally Emotions").Value;
		public static bool EnemyEmotions => ModManager.getModInfo("EmotionalSystem").GetSetting<ToggleSetting>("Enemy Emotions").Value;

		public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
		public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
		public static BattleChar DummyChar => AllyTeam.DummyChar;


		public static GameObject EmotionTrajectoryPos;
		public static GameObject EmotionTrajectoryNeg;

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

		public static void InsertSkillInDeck(BattleChar bchar, Skill skill, int createSkills = 1)
		{
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
			if (!string.IsNullOrEmpty(sound))
			{
				MasterAudio.PlaySound(sound, 100f, null, 0f, null, null, false, false);
			}
		}

		public static void ApplyBurn(BattleChar target, BattleChar user, int stack = 1)
		{
			if (target.Info.Ally || target == null) return;

			var burn = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionalSystem_Burn) as B_EmotionalSystem_Burn;

			if (burn != null)
			{
				burn.Burn += stack;
			}
		}

		public static void ApplyBleed(BattleChar target, BattleChar user, int stack = 1)
		{
			if (target.Info.Ally || target == null) return;


			var bleed = GetOrAddBuff(target, user, ModItemKeys.Buff_B_EmotionalSystem_Bleed) as B_EmotionalSystem_Bleed;

			if (bleed != null)
			{
				bleed.Bleed += stack;
			}
		}

		public static IEnumerator HealingParticle(BattleChar target, BattleChar user, int healingNum = 0, bool isHealing = false, bool isParticleOut = false, bool isHealLowestAlly = false)
		{
			yield return null;
			
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
				//healingParticle.PlusHit = true;
				healingParticle.FreeUse = true;

				target.ParticleOut(healingParticle, target);
			}
		}

		public static void GiveEmotionsToChar(BattleChar character, int amount, Vector3? source = null)
		{
			for (int i = 0; i < amount; i++)
			{
				character.GetRandomEmotion(source);
			}
		}

		public static void GiveEmotionsToAllies(int amount, Vector3? source = null)
		{
			foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
			{
				GiveEmotionsToChar(ally, amount, source);
			}
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

		public static void GetSprite(string path, Image img)
		{
			string path2 = ModManager.getModInfo("EmotionalSystem").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
		}

		public static Sprite GetSprite(string path)
		{
			string path2 = ModManager.getModInfo("EmotionalSystem").assetInfo.ImageFromFile(path);
			return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
		}

		public static void GetSpriteAsync(string path, Action<AsyncOperationHandle> collback)
		{
			string path2 = ModManager.getModInfo("EmotionalSystem").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
		}

		public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
		{
			var mod = ModManager.getModInfo("EmotionalSystem");
			if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
			var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
			//Debug.Log($"[EmotionalSystem] Getting asset address: {address}");
			return AddressableLoadManager.LoadAddressableAsset<T>(address);
		}

		public static GameObject CreatGameObject(string name, Transform parent)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.SetActive(false);
			gameObject.transform.SetParent(parent, false);
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = 8;
			return gameObject;
		}

		public static GameObject GetChildByName(GameObject obj, string name)
		{
			Transform transform = obj.transform.Find(name);
			bool flag = transform != null;
			GameObject result;
			if (flag)
			{
				result = transform.gameObject;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static void ImageResize(Image img, Vector2 size, Vector2 pos)
		{
			img.rectTransform.anchorMin = new Vector2(0f, 1f);
			img.rectTransform.anchorMax = new Vector2(0f, 1f);
			img.rectTransform.sizeDelta = size;
			img.rectTransform.transform.localPosition = pos;
		}

		public static void TextResize(TextMeshProUGUI txt, Vector2 size, Vector2 pos, string text, float fontSize)
		{
			txt.rectTransform.anchorMin = new Vector2(0f, 1f);
			txt.rectTransform.anchorMax = new Vector2(0f, 1f);
			txt.rectTransform.sizeDelta = size;
			txt.rectTransform.transform.localPosition = pos;
			txt.text = text;
			txt.fontSize = fontSize;
			txt.color = Color.white;
			txt.alignment = TextAlignmentOptions.Left;
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

		public static void GetEmotion(this BattleChar bc, Vector3? source, bool isPos)
		{
			var emotion = bc.MyEmotion();
			if (emotion == null) return;
			if (UseParticle && source != null)
			{
				GameObject prefab;
				if (isPos)
				{
					if (EmotionTrajectoryPos == null)
					{
						EmotionTrajectoryPos = GetAssets<GameObject>("Assets/ModAssets/EmotionTrajectoryPos.prefab", "emotionsystemunityassetbundle");
					}
					prefab = EmotionTrajectoryPos;
				}
				else
				{
					if (EmotionTrajectoryNeg == null)
					{
						EmotionTrajectoryNeg = GetAssets<GameObject>("Assets/ModAssets/EmotionTrajectoryNeg.prefab", "emotionsystemunityassetbundle");
					}
					prefab = EmotionTrajectoryNeg;
				}
				if (prefab == null)
				{
					emotion.ObtainCoin(isPos);
					return;
				}
				var trajectory = Object.Instantiate(prefab, bc.UI.transform.GetChild(0));
				trajectory.GetComponent<EmotionTrajectory>().Init(source.Value, emotion);
				trajectory.SetActive(true);
				//Debug.Log("Emotion coin from " + source.Value + " to " + emotion.transform.position);
				return;
			}
			else
			{
				emotion.ObtainCoin(isPos);
			}
		}

		public static void GetPosEmotion(this BattleChar bc, Vector3? source = null)
		{
			GetEmotion(bc, source, true);
		}

		public static void GetNegEmotion(this BattleChar bc, Vector3? source = null)
		{
			GetEmotion(bc, source, false);
		}

		public static void GetRandomEmotion(this BattleChar bc, Vector3? source = null)
		{
			GetEmotion(bc, source, RandomManager.RandomPer("EmotionCoin", 2, 1));
		}

		public static CharEmotion MyEmotion(this BattleChar bc)
		{
			if (bc is BattleAlly)
			{
				var buff = bc.BuffReturn(ModItemKeys.Buff_B_EmotionalLevel) as B_EmotionalLevel;
				return buff?.Emotion;
			}
			else if (bc is BattleEnemy)
			{
				var buff = bc.BuffReturn(ModItemKeys.Buff_B_EnemyEmotionalLevel) as B_EnemyEmotionalLevel;
				return buff?.Emotion;
			}
			return null;
		}

		public static int EmotionLevel(this BattleChar bc)
		{
			var emotion = bc.MyEmotion();
			if (emotion == null) return 0;
			return emotion.Level;
		}

		public static Vector3? GetPosUI(this Skill skill)
		{
			if (skill.MyButton != null)
			{
				return skill.MyButton.transform.position;
			}
			if (skill.BasicSkillButton != null)
			{
				return skill.BasicSkillButton.transform.position;
			}
			return null;
		}
		public static Vector3? GetPosUI(this BattleChar bc)
		{
			if (bc.Dummy || bc.IsLucyNoC) return null;
			if (bc is BattleAlly ally)
			{
				return ally.GetPos();
			}
			if (bc is BattleEnemy enemy)
			{
				try
				{
					var UICamera = UIManager.inst.UIcamera;
					var FieldCamera = BattleSystem.instance.battlecamera.ObjectCam;
					var enemyPos = enemy.GetPos();
					var posScreen = FieldCamera.WorldToScreenPoint(enemyPos);
					return UICamera.ScreenToWorldPoint(posScreen);
				}
				catch
				{
					return null;
				}
			}
			return null;
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
		public static T GetField<T>(this object obj, string fieldName)
		{
			return Traverse.Create(obj).Field<T>(fieldName).Value;
		}

		public static void SetField<T>(this object obj, string fieldName, T value)
		{
			Traverse.Create(obj).Field<T>(fieldName).Value = value;
		}
		public static string GetTranslation(this string key)
		{
			try
			{
				return ModManager.getModInfo(EmotionalSystem_Plugin.modname).localizationInfo.
					SystemLocalizationUpdate(EmotionalSystem_Plugin.modname + "/" + key);
			}
			catch
			{
			}
			return key;
		}
		public static void FitRectTransformToTarget(RectTransform toFit, RectTransform target, Vector3 localPositionOffset)
		{
			if (toFit == null || target == null)
			{
				Debug.LogWarning("RectTransform is null!");
				return;
			}

			// Stretch across the parent (full width and height)
			toFit.anchorMin = new Vector2(0f, 0f);
			toFit.anchorMax = new Vector2(1f, 1f);
			toFit.pivot = target.pivot;

			// Add padding of 20 pixels on all sides
			toFit.offsetMin = new Vector2(10f, 80f);   // Left and bottom
			toFit.offsetMax = new Vector2(-10f, -35f); // Right and top

			// Additional manual offset (if you want to move the image further)
			toFit.localPosition += localPositionOffset;
		}
	}
}