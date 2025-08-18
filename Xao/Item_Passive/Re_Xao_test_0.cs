using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using static Xao.Utils;
namespace Xao
{
    public class Re_Xao_test_0 : PassiveItemBase, IP_TurnEnd
    {
        // Кэшированный список, создаётся один раз
        public static class SkillCache
        {
            public static List<GDESkillData> CachedSkills;

            public static void Init()
            {
                if (CachedSkills != null) return; // Уже инициализировано

                CachedSkills = new List<GDESkillData>();

                foreach (var gdeskillData in PlayData.ALLSKILLLIST)
                {
                    if (string.IsNullOrEmpty(gdeskillData.User)) continue;
                    if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_LucySkill) continue;
                    if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_DefultSkill) continue;
                    if (gdeskillData.NoDrop || gdeskillData.Lock) continue;
                    if (gdeskillData.KeyID == GDEItemKeys.Skill_S_Phoenix_6) continue;

                    var gdecharacterData = new GDECharacterData(gdeskillData.User);
                    if (gdecharacterData != null && Misc.IsUseableCharacter(gdecharacterData.Key))
                    {
                        CachedSkills.Add(gdeskillData);
                    }
                }

                Debug.Log($"[SkillCache] Cached {CachedSkills.Count} usable skills");
            }
        }

        public void TurnEnd()
        {
            if (BattleSystem.instance.AllyTeam.AP < 1) return;

            base.ShinyEffect();

            // гарантируем, что кэш инициализирован
            SkillCache.Init();

            // быстрый выбор скилла
            var skillData = SkillCache.CachedSkills.Random(BattleRandom.PassiveItem);
            var battleChar = BattleSystem.instance.AllyList.Random(BattleRandom.PassiveItem);

            Skill skill = Skill.TempSkill(skillData.Key, battleChar, battleChar.MyTeam);
            skill.FreeUse = true;

            BattleSystem.DelayInput(
                BattleSystem.instance.SkillRandomUseIenum(battleChar, skill, false, false, false)
            );
        }
    }
}