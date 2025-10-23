using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod.ModEditor;
using GameDataEditor;

namespace Meeyu
{
	public static class Utils
	{
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
				target.BuffAdd(buffKey, BattleSystem.instance.DummyChar, false, 0, false, -1, false);
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

		public static void TakeNonLethalDamage(BattleChar bchar, int damage = 0, bool isPain = true)
		{
			if (bchar != null)
			{
				AddBuff(bchar, GDEItemKeys.Buff_B_Momori_P_NoDead);
				bchar.Damage(BattleSystem.instance.DummyChar, damage, false, isPain, true);
				RemoveBuff(bchar, GDEItemKeys.Buff_B_Momori_P_NoDead, true);
			}
		}

		public static void PlaySound(string sound)
		{
			if (!string.IsNullOrEmpty(sound))
			{
				float volume = MasterAudio.MasterVolumeLevel;
				MasterAudio.PlaySound(sound, volume, null, 0f, null, null, false, false);
			}
		}
	}
}
