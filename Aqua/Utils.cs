using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using DarkTonic.MasterAudio;

namespace Aqua
{
    public static class Utils
    {
        public static bool AquaVoice => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Voice").Value;

        public static bool AquaVoiceSkills => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Voice Skills").Value;
        
        private static readonly Dictionary<string, string> AquaSkillsSounds = new Dictionary<string, string>
        { 
            { ModItemKeys.Skill_S_Aqua_AquaGradePurification, "AquaGradePurification" },
            { ModItemKeys.Skill_S_Aqua_BlessingoftheAxisCult, "BlessingoftheAxisCult" },
            { ModItemKeys.Skill_S_Aqua_DivineLottery, "DivineLottery" },
            { ModItemKeys.Skill_S_Aqua_FogofBlessings, "FogofBlessings" },
            { ModItemKeys.Skill_S_Aqua_LucyDraw, "GoddessSecretWeapon" },
            { ModItemKeys.Skill_S_Aqua_OverflowingGrace, "OverflowingGrace" },
            { ModItemKeys.Skill_S_Aqua_PartyDrunkard, "PartyDrunkard" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick, "PartyTrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty, "NaturesBeauty" }, // 2 sounds
            { ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty, "PhantasmalBeauty" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick, "VanishTrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick, "TelekinesisTrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill, "Certainkillpartytrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant, "UnusualPlant" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket, "Minorpocketdimension" },
            { ModItemKeys.Skill_S_Aqua_Rare_AxisCultRecruitment, "AxisCultRecruitment" },
            { ModItemKeys.Skill_S_Aqua_Rare_GodsBlow, "GodsBlow" }, // 2 sounds
            { ModItemKeys.Skill_S_Aqua_SplashofJudgment, "SplashofJudgment" }, // 2 sounds
            { ModItemKeys.Skill_S_Aqua_TorrentialTears, "TorrentialTears" },
        };

        public static void PlaySound(string skillId)
        {
            if (!AquaSkillsSounds.TryGetValue(skillId, out string baseSound)) return;

            string soundToPlay = baseSound;

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty ||
                skillId == ModItemKeys.Skill_S_Aqua_SplashofJudgment ||
                skillId == ModItemKeys.Skill_S_Aqua_Rare_GodsBlow)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, 2);

                if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty)
                    soundToPlay = $"NaturesBeauty_{index}";
                if (skillId == ModItemKeys.Skill_S_Aqua_Rare_GodsBlow)
                    soundToPlay = $"GodsBlow_{index}";
                if (skillId == ModItemKeys.Skill_S_Aqua_SplashofJudgment)
                    soundToPlay = $"SplashofJudgment_{index}";
            }

            MasterAudio.StopBus("SE");
            MasterAudio.PlaySound(soundToPlay, 100f);
        }
    }
}
