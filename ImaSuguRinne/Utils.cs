using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData;
using ChronoArkMod;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Experimental.U2D;
using static ChronoArkMod.ModEditor.Console.ConsoleManager;
using UnityEngine.UI;
using TMPro;
using ChronoArkMod.ModData.Settings;
using GameDataEditor;
using DarkTonic.MasterAudio;
using System.Collections;
using static TMPro.SpriteAssetUtilities.TexturePacker;
using System.Runtime.InteropServices.WindowsRuntime;
using static CharacterDocument;
using System.Web;
using Spine;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Net.Sockets;
using TileTypes;
using NLog.Targets;

namespace ImaSuguRinne
{
    public static class Utils
    {
        public static BattleChar Rinne => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_Rinne);
        public static Character RinneChar => PlayData.TSavedata.Party.FirstOrDefault(x => x.KeyData == ModItemKeys.Character_Rinne);
        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;

        public static bool GettingTwoMemory;

        public static readonly List<string> RinneSkills = new List<string>
        {
            ModItemKeys.Skill_S_Rinne_BrokenEnd,
            ModItemKeys.Skill_S_Rinne_CascadeofPain,
            //ModItemKeys.Skill_S_Rinne_CycleofBloom_0,
            //ModItemKeys.Skill_S_Rinne_CycleofEchoes_0,
            //ModItemKeys.Skill_S_Rinne_CycleofSuffering_0,
            ModItemKeys.Skill_S_Rinne_DesperateSpiral,
            ModItemKeys.Skill_S_Rinne_EndlessSorrow,
            ModItemKeys.Skill_S_Rinne_FragmentofMemory,
            ModItemKeys.Skill_S_Rinne_ResonanceofPain,
        };

        public static void CreateSkill(BattleChar bchar, string skill)
        {
            Skill newSkill = Skill.TempSkill(skill, bchar, bchar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(newSkill, true);
        }

        public static Skill CreateSkill(BattleChar bchar, string skillKey, bool isExcept = false, bool isDiscarded = false, int discardedAfter = 0, int mana = 0, bool isNotCount = false, bool isAddToHand = true)
        {
            Skill newSkill = Skill.TempSkill(skillKey, bchar, bchar.MyTeam);
            newSkill.isExcept = isExcept;

            if (isDiscarded)
            {
                newSkill.AutoDelete = discardedAfter;
            }

            newSkill.AP = mana;
            newSkill.NotCount = isNotCount;

            if (isAddToHand)
            {
                BattleSystem.instance.AllyTeam.Add(newSkill, true);
            }
            return newSkill;
        }

        public static Skill CreateSkill(BattleChar bchar, Skill keepExtendedFrom, string skillKey, bool isExcept = false, bool isDiscarded = false,
    int discardedAfter = 0, int mana = 0, bool isNotCount = false, bool isAddToHand = true, bool keepExtended = false)
        {
            Skill newSkill = Skill.TempSkill(skillKey, bchar, bchar.MyTeam);
            newSkill.isExcept = isExcept;

            if (isDiscarded)
                newSkill.AutoDelete = discardedAfter;

            // Копируем экстенды
            if (keepExtended && keepExtendedFrom != null)
            {
                foreach (Skill_Extended skill_Extended in keepExtendedFrom.AllExtendeds)
                {
                    bool isBase = keepExtendedFrom.MySkill.SkillExtended.Any(text => text.Contains(skill_Extended.Name));
                    if (!isBase)
                    {
                        Skill_Extended clone = skill_Extended.Clone() as Skill_Extended;
                        if (clone != null)
                        {
                            if (clone.BattleExtended)
                                newSkill.ExtendedAdd_Battle(clone);
                            else
                                newSkill.ExtendedAdd(clone);
                        }
                    }
                    skill_Extended.SelfDestroy();
                }
            }

            // **ВАЖНО**: перезаписываем AP и Swift **после всех операций с экстендом**
            newSkill.AP = mana;
            newSkill.NotCount = isNotCount;

            // Обновляем CharinfoSkilldata, чтобы свойства сохранились при повторном добавлении в деку
            if (newSkill.CharinfoSkilldata != null)
            {
                newSkill.CharinfoSkilldata.SkillInfo = newSkill.MySkill;
            }

            if (isAddToHand)
                BattleSystem.instance.AllyTeam.Add(newSkill, true);

            return newSkill;
        }


        //public static Skill CreateSkill(BattleChar bchar, Skill keepExtendedFrom, string skillKey, bool isExcept = false, bool isDiscarded = false, int discardedAfter = 0, int mana = 0, bool isNotCount = false, bool isAddToHand = true, bool keepExtended = false)
        //{
        //    Skill newSkill = Skill.TempSkill(skillKey, bchar, bchar.MyTeam);
        //    newSkill.isExcept = isExcept;

        //    if (isDiscarded)
        //    {
        //        newSkill.AutoDelete = discardedAfter;
        //    }

        //    newSkill.AP = mana;
        //    newSkill.NotCount = isNotCount;

        //    if (keepExtended && keepExtendedFrom != null)
        //    {
        //        List<Skill_Extended> extendedToKeep = new List<Skill_Extended>();

        //        foreach (Skill_Extended skill_Extended in keepExtendedFrom.AllExtendeds)
        //        {
        //            bool isBase = keepExtendedFrom.MySkill.SkillExtended.Any(text => text.Contains(skill_Extended.Name));

        //            if (!isBase)
        //            {
        //                Skill_Extended clone = skill_Extended.Clone() as Skill_Extended;
        //                if (clone != null)
        //                {
        //                    extendedToKeep.Add(clone);
        //                }
        //            }
        //            skill_Extended.SelfDestroy();
        //        }

        //        foreach (var ex in extendedToKeep)
        //        {
        //            if (ex.BattleExtended)
        //            {
        //                newSkill.ExtendedAdd_Battle(ex);
        //            }
        //            else
        //            {
        //                newSkill.ExtendedAdd(ex);
        //            }
        //        }
        //    }

        //    if (isAddToHand)
        //    {
        //        BattleSystem.instance.AllyTeam.Add(newSkill, true);
        //    }
        //    return newSkill;
        //}



        public static void AddBuff(BattleChar bchar, string buffKey, int buffNum = 1)
        {
            for (int i = 0; i < buffNum; i++)
            {
                if (bchar == null || buffKey.IsNullOrEmpty()) return;
                bchar.BuffAdd(buffKey, bchar, false, 0, false, -1, false);
            }
        }

        public static void AddDebuff(BattleChar enemy, BattleChar ally, string buffKey, int debuffNum = 1, int percentage = 0)
        {
            for (int i = 0; i < debuffNum; i++)
            {
                if (enemy == null || buffKey.IsNullOrEmpty() || enemy.Info.Ally) return;
                enemy.BuffAdd(buffKey, ally, false, percentage, false, -1, false);
            }
        }

        public static void GlitchEffect(this Skill changeFrom, float time = 0.5f)
        {
            if (changeFrom.MyButton != null)
            {
                UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("StoryGlitch/GlitchSkillEffect"), changeFrom.MyButton.transform);
                UnityEngine.Object.Destroy(obj, time);
            }
        }

        public static void UnlockSkillPreview(string key)
        {
            if (!SaveManager.NowData.unlockList.SkillPreView.Contains(key))
            {
                SaveManager.NowData.unlockList.SkillPreView.Add(key);
            }
        }

        public static void CopyAndExtendDebuffs(BattleChar target)
        {
            if (target == null || target.Info.Ally) return;

            var debuffs = target.GetBuffs(BattleChar.GETBUFFTYPE.ALL, true, false).ToList();

            foreach (Buff debuff in debuffs)
            {
                debuff.BuffData.MaxStack++;
                target.BuffAdd(debuff.BuffData.Key, debuff.Usestate_L, false, 999, false, debuff.StackInfo[debuff.StackInfo.Count - 1].RemainTime, false);

                foreach (StackBuff stackBuff in debuff.StackInfo)
                {
                    if (stackBuff.RemainTime != 0)
                    {
                        stackBuff.RemainTime++;
                    }
                }
            }
        }

        public static void EternalRare(BattleChar bchar)
        {
            Skill skill = Utils.CreateSkill(bchar, ModItemKeys.Skill_S_Rinne_Rare_EternalFate_1, false, false, 0, 0, true, false);
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = RandomManager.RandomInt(bchar.GetRandomClass().Main, 0, bchar.MyTeam.Skills_Deck.Count + 1);
                bchar.MyTeam.Skills_Deck.Insert(randomIndex, skill);
            }
        }
    }
}
