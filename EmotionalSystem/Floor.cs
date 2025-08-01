﻿using System;
using System.Collections.Generic;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using UnityEngine;

namespace EmotionalSystem
{
    public static class AllLibraryFloors
    {
        public static FloorInfo NowFloorInfo
        {
            get
            {
                var info = Floors[Floor];
                return info;
            }
        }

        public static FloorCode Floor => (FloorCode)ModManager.getModInfo("EmotionalSystem").GetSetting<DropdownSetting>("Library Floor").Value;

        public static Dictionary<FloorCode, FloorInfo> Floors = new Dictionary<FloorCode, FloorInfo>
        {
            {
                FloorCode.History, new FloorInfo
                {
                    Abnomalities = new List<Abnormality>
                    {
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_Ashes_Pos, AbnoType.Pos, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_HappyMemories_Pos, AbnoType.Pos, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_Pos, AbnoType.Pos, 1),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_DisplayofAffection_Neg, AbnoType.Neg, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_TheFairiesCare_Neg, AbnoType.Neg, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_Matchlight_Neg, AbnoType.Neg, 1),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Gluttony_Pos, AbnoType.Pos, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Vines_Pos, AbnoType.Pos, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Spores_Pos, AbnoType.Pos, 2),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Predation_Neg, AbnoType.Neg, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Footfalls_Neg, AbnoType.Neg, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_WorkerBee_Neg, AbnoType.Neg, 2),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv3_Malice_Pos, AbnoType.Pos, 3),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv3_BarrierofThorns_Pos, AbnoType.Pos, 3),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv3_Loyalty_Neg, AbnoType.Neg, 3),
                    },

                    EGOs = new List<string>
                    {
                        ModItemKeys.Skill_S_LucyEGO_History_FourthMatchFlame,
                        ModItemKeys.Skill_S_LucyEGO_History_GreenStem,
                        ModItemKeys.Skill_S_LucyEGO_History_Hornet,
                        ModItemKeys.Skill_S_LucyEGO_History_TheForgotten,
                        ModItemKeys.Skill_S_LucyEGO_History_Wingbeat,
                    }
                }
            },

            {
                FloorCode.Technological, new FloorInfo
                {
                    Abnomalities = new List<Abnormality>
                    {
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_MetallicRinging_Pos, AbnoType.Pos, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_Pos, AbnoType.Pos, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Request_Pos, AbnoType.Pos, 1),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Violence_Neg, AbnoType.Neg, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Rhythm_Neg, AbnoType.Neg, 1),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Lament_Neg, AbnoType.Neg, 1),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_Recharge_Pos, AbnoType.Pos, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_Clean_Pos, AbnoType.Pos, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_EternalRest_Pos, AbnoType.Pos, 2),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_MusicalAddiction_Neg, AbnoType.Neg, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_TheSeventhBullet_Neg, AbnoType.Neg, 2),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_ChainedWrath_Neg, AbnoType.Neg, 2),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Coffin_Pos, AbnoType.Pos, 3),

                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Music_Neg, AbnoType.Neg, 3),
                        new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_DarkFlame_Neg, AbnoType.Neg, 3),
                        

                    },
                    EGOs = new List<string>
                    {
                        ModItemKeys.Skill_S_LucyEGO_Technological_GrinderMk,
                        ModItemKeys.Skill_S_LucyEGO_Technological_MagicBullet,
                        ModItemKeys.Skill_S_LucyEGO_Technological_Regret,
                        ModItemKeys.Skill_S_LucyEGO_Technological_SolemnLament,
                        ModItemKeys.Skill_S_LucyEGO_Technological_Harmony

                    }
                }
            }
        };
    }

    public enum FloorCode
    {
        History = 0,
        Technological = 1
    }

    public class FloorInfo
    {
        public List<Abnormality> Abnomalities;
        public List<string> EGOs;
    }
}
