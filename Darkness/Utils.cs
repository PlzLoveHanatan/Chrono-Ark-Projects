using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using DarkTonic.MasterAudio;
using static System.Windows.Forms.LinkLabel;
using System.Collections;
using UnityEngine;

namespace Darkness
{
    public static class Utils
    {
        public static bool DarknessVoiceSkills => ModManager.getModInfo("Darkness").GetSetting<ToggleSetting>("Voice Skills").Value;
        public static bool DarknessVoice => ModManager.getModInfo("Darkness").GetSetting<ToggleSetting>("Voice").Value;

        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleChar Darkness => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_Darkness);

        //public static bool DarknessVoiceDialogue => ModManager.getModInfo("Darkness").GetSetting<ToggleSetting>("Voice Dialogue").Value;
        //public static float DarknessMoreEnemies => ModManager.getModInfo("Darkness").GetSetting<SliderSetting>("Additional enemies").Value;

        public static float DarknessMoreEnemies
        {
            get
            {
                return ModManager.getModInfo("Darkness").GetSetting<SliderSetting>("Additional enemies").Value;
            }
        }



        private static readonly Dictionary<string, string> DarknessSkillSounds = new Dictionary<string, string>
        {
            { ModItemKeys.Skill_S_Darkness_ClumsySlash, "DarknessClumsy" },
            { ModItemKeys.Skill_S_Darkness_CrusaderDomination, "DarknessCrusader" },
            { ModItemKeys.Skill_S_Darkness_HerosParry, "DarknessHeroes" },
            { ModItemKeys.Skill_S_Darkness_KnightsResolve, "DarknessKnight" },
            { ModItemKeys.Skill_S_Darkness_LastStand, "DarknessLast" },
            { ModItemKeys.Skill_S_Darkness_LucyDraw, "DarknessLucy" },
            { ModItemKeys.Skill_S_Darkness_MasochistsCourage, "DarknessMasochistic" },
            { ModItemKeys.Skill_S_Darkness_PartyKnight, "DarknessParty" },
            { ModItemKeys.Skill_S_Darkness_Rare_GuardiansGrace, "DarknessGuardian" },
            { ModItemKeys.Skill_S_Darkness_Rare_IronMaidensEmbrace, "DarknessIron" },
            { ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill, "DarknessWill" },
            { ModItemKeys.Skill_S_Darkness_ShieldofFaith, "DarknessShield" },
            { ModItemKeys.Skill_S_Darkness_SideSlash, "DarknessClumsy" },
            { ModItemKeys.Skill_S_Darkness_SideSlash_0, "DarknessClumsy" },

        };
        private static readonly Dictionary<string, List<string>> DarknessBattleDialogue = new Dictionary<string, List<string>>
        {
            {
                ModItemKeys.Skill_S_Darkness_ClumsySlash,
                new List<string>
                {
                    ModLocalization.DarknessAttackLands_0,
                    ModLocalization.DarknessAttackLands_1
                }
            },
            {
                ModItemKeys.Skill_S_Darkness_SideSlash,
                new List<string>
                {
                    ModLocalization.DarknessAttackLands_0,
                    ModLocalization.DarknessAttackLands_1
                }
            },
            {
                ModItemKeys.Skill_S_Darkness_SideSlash_0,
                new List<string>
                {
                    ModLocalization.DarknessAttackLands_0,
                    ModLocalization.DarknessAttackLands_1
                }
            }
        }; 
        
        public static void PlayDarknessSound(string skill)
        {
            if (!Utils.DarknessVoiceSkills || skill == null) return;

            if (!DarknessSkillSounds.TryGetValue(skill, out string baseSound)) return;

            string soundToPlay = baseSound;

            MasterAudio.StopBus("SE");
            MasterAudio.PlaySound(soundToPlay, 100f);
        }
        public static void TryPlayDarknessSound(Skill skill, BattleChar bChar)
        {
            if (!Utils.DarknessVoiceSkills || skill == null || bChar != Darkness) return;

            PlayDarknessSound(skill.MySkill.KeyID);
        }

        public static void PlayDarknessBattleDialogue(Skill skill, BattleChar bChar)
        {
            if (!Utils.DarknessVoiceSkills || skill == null || bChar != Darkness) return;

            if (DarknessBattleDialogue.TryGetValue(skill.MySkill.KeyID, out List<string> lines) && lines.Count > 0)
            {
                string text = lines[RandomManager.RandomInt(BattleRandom.PassiveItem, 0, lines.Count)];
                var position = bChar.GetTopPos();

                bChar.StartCoroutine(ShowSkillText(position, text));
            }
        }

        public static void PlayDarknessBattleDialogue2(Skill skill, BattleChar bChar)
        {
            if (!Utils.DarknessVoiceSkills || skill == null || bChar != Darkness) return;

            string text = ModLocalization.DarknessAttackMisses;
            var position = bChar.GetTopPos();
            bChar.StartCoroutine(ShowSkillText(position, text));
        }

        public static IEnumerator ShowSkillText(Vector3 position, string text)
        {
            var topText = BattleText.CustomText(position, text);
            yield return new WaitForSecondsRealtime(2f);

            topText?.End();
        }
    }
}
