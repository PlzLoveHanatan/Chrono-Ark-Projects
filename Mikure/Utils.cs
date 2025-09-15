using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using I2.Loc;
using NLog.Targets;
using UnityEngine;

namespace Mikure
{
    public static class Utils
    {
        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
        public static BattleChar Mikure => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_Mikure);

        public static Dictionary<BattleChar, List<Skill>> AllySkills = new Dictionary<BattleChar, List<Skill>>();

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

        public static Skill CreateSkill(BattleChar bchar, Skill keepExtendedFrom, string skillKey, bool isExcept = false, bool isDiscarded = false, int discardedAfter = 0, int mana = 0, bool isNotCount = false, bool isAddToHand = true, bool keepExtended = false)
        {
            Skill newSkill = Skill.TempSkill(skillKey, bchar, bchar.MyTeam);
            newSkill.isExcept = isExcept;

            if (isDiscarded)
            {
                newSkill.AutoDelete = discardedAfter;
            }

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

            newSkill.AP = mana;
            newSkill.NotCount = isNotCount;

            if (newSkill.CharinfoSkilldata != null)
            {
                newSkill.CharinfoSkilldata.SkillInfo = newSkill.MySkill;
            }

            if (isAddToHand)
            {
                BattleSystem.instance.AllyTeam.Add(newSkill, true);
            }
            return newSkill;
        }

        public static void AddBuff(BattleChar target, BattleChar user, string buffKey, int buffNum = 1)
        {
            for (int i = 0; i < buffNum; i++)
            {
                if (target == null || buffKey.IsNullOrEmpty()) return;
                target.BuffAdd(buffKey, user, false, 0, false, -1, false);
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

        public static BattleChar.GETBUFFTYPE[] NegativeBuffTypes = new[]
        {
            BattleChar.GETBUFFTYPE.DOT,
            BattleChar.GETBUFFTYPE.DEBUFF,
            BattleChar.GETBUFFTYPE.CC
        };

        public static Dictionary<BattleChar.GETBUFFTYPE, string> DebuffRemovalSkill = new Dictionary<BattleChar.GETBUFFTYPE, string>
        {
            { BattleChar.GETBUFFTYPE.DOT, ModItemKeys.Skill_S_Mikure_AnytimeNow_0 },
            { BattleChar.GETBUFFTYPE.DEBUFF, ModItemKeys.Skill_S_Mikure_AnytimeNow_1 },
            { BattleChar.GETBUFFTYPE.CC, ModItemKeys.Skill_S_Mikure_AnytimeNow_2 }
        };

        public static Dictionary<string, BattleChar.GETBUFFTYPE> Strings = new Dictionary<string, BattleChar.GETBUFFTYPE>
        {
            { ModItemKeys.Skill_S_Mikure_AnytimeNow_0, BattleChar.GETBUFFTYPE.DOT },
            { ModItemKeys.Skill_S_Mikure_AnytimeNow_1, BattleChar.GETBUFFTYPE.DEBUFF },
            { ModItemKeys.Skill_S_Mikure_AnytimeNow_2, BattleChar.GETBUFFTYPE.CC }
        };

        public static List<Skill> DynamicList = new List<Skill>();

        public static BattleChar Target;

        public static IEnumerator RemoveDebuff(BattleChar user, BattleChar target)
        {
            yield return null;

            DynamicList.Clear();

            foreach (var type in NegativeBuffTypes)
            {
                var buffs = target.GetBuffs(type, true, false);
                if (buffs.Count == 0) continue;

                if (DebuffRemovalSkill.TryGetValue(type, out string skillKey))
                {
                    var skill = Skill.TempSkill(skillKey, user, user.MyTeam);
                    if (skill == null || skill.MySkill == null) continue;
                    DynamicList.Add(skill);
                }
            }

            if (DynamicList.Count > 0)
            {
                Target = target;
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(DynamicList, new SkillButton.SkillClickDel(Selection), ModLocalization.Anytime, false, true, false, false, true));
            }
        }

        public static void Selection(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;

            if (Strings.TryGetValue(key, out BattleChar.GETBUFFTYPE type))
            {
                var debuffs = Target.GetBuffs(type, true, false);
                foreach (var buff in debuffs)
                {
                    Target.BuffRemove(buff.BuffData.Key);
                }
            }
        }

        public static int GetDebuffTypes(BattleChar bchar)
        {
            int result = 0;

            foreach (var t in NegativeBuffTypes)
            {
                var buffs = bchar.GetBuffs(t, false, false);
                if (buffs != null && buffs.Count > 0)
                {
                    result++;
                }
            }
            return result;
        }

        public static void ReviveAllies(List<BattleChar> targets)
        {
            foreach (var target in targets)
            {
                if (target.Info.Incapacitated)
                {
                    target.Info.Incapacitated = false;
                    target.Info.Hp = 0;
                    int hp = target.GetStat.maxhp / 4;
                    BattleSystem.DelayInput(HealingParticle(target, BattleSystem.instance.DummyChar, hp, true));
                    BattleSystem.DelayInputAfter(RemoveAllySkill(target));
                    BattleSystem.DelayInputAfter(AddAllySkill(target));
                }
            }
        }

        public static IEnumerator HealingParticle(BattleChar target, BattleChar user, int healingNum = 0, bool isHealing = false)
        {
            yield return null;

            if (isHealing)
            {
                target.Heal(user, healingNum, false, true, null);
            }

            Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Mikure_DummyHeal, user, user.MyTeam);
            healingParticle.PlusHit = true;
            healingParticle.FreeUse = true;

            target.ParticleOut(healingParticle, target);
        }


        public static void SaveAllySkill()
        {
            foreach (var skill in AllyTeam.Skills_Deck)
            {
                var master = skill.Master;
                if (master == null || master == BattleSystem.instance.AllyTeam.LucyAlly) continue;

                if (!AllySkills.ContainsKey(master))
                {
                    AllySkills[master] = new List<Skill>();
                }

                AllySkills[master].Add(skill);

                Debug.Log($"[BattleStart] Добавил скилл {skill.MySkill.KeyID} для {master.Info.KeyData}");
            }
        }

        public static IEnumerator RemoveAllySkill(BattleChar ally)
        {
            yield return null;

            foreach (var skill in AllyTeam.Skills_UsedDeck.Concat(AllyTeam.Skills_Deck))
            {
                if (skill != null && skill.Master == ally)
                {
                    skill.Remove();
                }
            }
        }

        public static IEnumerator AddAllySkill(BattleChar ally)
        {
            yield return null;

            if (AllySkills.TryGetValue(ally, out var skills))
            {
                foreach (var skill in skills)
                {
                    int randomIndex = RandomManager.RandomInt(ally.GetRandomClass().Main, 0, ally.MyTeam.Skills_Deck.Count + 1);
                    ally.MyTeam.Skills_Deck.Insert(randomIndex, skill);
                    Debug.Log($"[AddAllySkill] Вставил {skill.MySkill.KeyID} в колоду {ally.Info.KeyData} на позицию {randomIndex}");
                }
            }
        }
    }
}
