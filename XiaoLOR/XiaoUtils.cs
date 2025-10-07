using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using UnityEngine.UI;
using HarmonyLib;
using ChronoArkMod.ModData;
using System.Runtime.InteropServices.WindowsRuntime;
using DarkTonic.MasterAudio;
using System.Collections;
using UnityEngine;

namespace XiaoLOR
{
    public static class XiaoUtils
    {
        private static readonly ModInfo Modinfo = ModManager.getModInfo("XiaoLOR");
        public static bool IronLotusSong => Modinfo.GetSetting<ToggleSetting>("Iron Lotus Song").Value;
        public static bool IronLotusKeySong => Modinfo.GetSetting<ToggleSetting>("Key Ingredient Song").Value;
        public static bool XiaoSkillSounds => Modinfo.GetSetting<ToggleSetting>("Xiao Skill Sounds").Value;
		public static bool LiuAssociationEquip => Modinfo.GetSetting<ToggleSetting>("Liu Association Equip").Value;

		public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
		public static BattleChar Xiao => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_XiaoLOR);
		public static Character XiaoChar => PlayData.TSavedata.Party.FirstOrDefault(x => x.KeyData == ModItemKeys.Character_XiaoLOR);

		public static void AddBuff(BattleChar user, BattleChar target, string buffKey, int buffNum = 1)
		{
			for (int i = 0; i < buffNum; i++)
			{
				if (user == null || buffKey.IsNullOrEmpty()) return;
				target.BuffAdd(buffKey, target, false, 0, false, -1, false);
			}
		}

		public static void AddDebuff(BattleChar target, BattleChar user, string buffKey, int debuffNum = 1, int percentage = 0)
		{
			for (int i = 0; i < debuffNum; i++)
			{
				if (target == null || buffKey.IsNullOrEmpty() || target.Info.Ally) return;
				user.BuffAdd(buffKey, user, false, percentage, false, -1, false);
			}
		}

		public static void StartXiaoSong()
        {
            string song = "";

            if (IronLotusKeySong && IronLotusKeySong)
            {
                bool randomSong = RandomManager.RandomInt(BattleSystem.instance.AllyTeam.DummyChar.GetRandomClass().Main, 0, 2) == 0;
                song = randomSong ? "IronLotus" : "IronLotusKey";
            }
            else if (IronLotusSong)
            {
                song = "IronLotus";
            }
            else if (IronLotusKeySong)
            {
                song = "IronLotusKey";

			}
             
            if (!string.IsNullOrEmpty(song))
            {
                MasterAudio.FadeBusToVolume("FieldBGM", 0f, 1f, null, false, false);
                MasterAudio.FadeBusToVolume("BattleBGM", 0f, 1f, null, false, false);
                MasterAudio.StopBus("BGM");
                MasterAudio.StopBus("BGM");
                MasterAudio.StopBus("StoryBGM");
                MasterAudio.PlaySound(song, 100f, null, 0f, null, null, false, false);
            }
        }

        public static void StopXiaoSong()
        {
            MasterAudio.StopBus("BGM");
            MasterAudio.FadeBusToVolume("BGM", 0f, 1f, null, false, false);
            FieldSystem.FieldBGMOn();
        }

        public static void PlaySound(string sound)
        {
            if (!XiaoSkillSounds || string.IsNullOrEmpty(sound)) return;

			MasterAudio.PlaySound(sound, 100f, null, 0f, null, null, false, false);
		}
    }
}
