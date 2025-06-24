using System;
using System.Collections.Generic;
using System.Linq;
using static EmotionalSystem.B_LucyEmotionalLevel;
using EmotionSystem;
using UnityEngine;
using System.Collections;
using EmotionalSystem;
using GameDataEditor;

namespace EmotionalSystem
{
    public class B_EnemyTeamEmotionalLevel : Buff, IP_EnemyNewTurn, IP_EmotionLvUpBefore
    {
        private static B_EnemyTeamEmotionalLevel _Instance;

        public static B_EnemyTeamEmotionalLevel Instance
        {
            get
            {
                if (BattleSystem.instance == null) return null;
                if (_Instance == null || _Instance.BS == null)
                {
                    _Instance = new B_EnemyTeamEmotionalLevel(BattleSystem.instance);
                }
                return _Instance;
            }
        }

        public int LastEmotionalLevel;
        public int HighestEmotionLevel;
        public BattleSystem BS;

        public B_EnemyTeamEmotionalLevel(BattleSystem bs)
        {
            LastEmotionalLevel = 0;
            HighestEmotionLevel = 0;
            BS = bs;
            DynamicAbnormalitiesList.Clear();
            DynamicAbnormalitiesList.AddRange(Abnormalities);
        }

        public List<Abnormality> DynamicAbnormalitiesList = new List<Abnormality>();

        private static readonly List<Abnormality> Abnormalities = new List<Abnormality>
        {
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_EnergyConversion, AbnoType.Neg, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_MirrorAdjustment, AbnoType.Neg, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Shelter, AbnoType.Pos, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Strengthen, AbnoType.Pos, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Stress, AbnoType.Neg, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Unity, AbnoType.Pos, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_BehaviorAdjustment, AbnoType.Pos, 0),
            new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_YouMustBeHappy, AbnoType.Neg, 0)

            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Strengthen, AbnoType.Pos, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Stress, AbnoType.Neg, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Unity, AbnoType.Pos, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Despair, AbnoType.Neg, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_Stress, AbnoType.Neg, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_MirrorAdjustment, AbnoType.Neg, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_BehaviorAdjustment, AbnoType.Pos, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_YouMustBeHappy, AbnoType.Pos, 0),
            //new Abnormality(ModItemKeys.Buff_B_EnemyAbnormality_EnergyConversion, AbnoType.Neg, 0)
        };
        private static readonly Dictionary<string, List<string>> BannedAbnormalitiesBosses = new Dictionary<string, List<string>>()
        {
            { GDEItemKeys.Enemy_MBoss_0, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_EnergyConversion, ModItemKeys.Buff_B_EnemyAbnormality_Shelter } },
            { GDEItemKeys.Enemy_S1_ArmorBoss, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_EnergyConversion, ModItemKeys.Buff_B_EnemyAbnormality_Shelter } },
            { GDEItemKeys.Enemy_S1_WitchBoss, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Stress, ModItemKeys.Buff_B_EnemyAbnormality_BehaviorAdjustment} },
            { GDEItemKeys.Enemy_Boss_Golem, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_MirrorAdjustment } },
            { GDEItemKeys.Enemy_S1_BossDorchiX, new List<string> {   } },
            { GDEItemKeys.Enemy_MBoss2_0, new List<string> { } },
            { GDEItemKeys.Enemy_S2_Joker, new List<string> { } },
            { GDEItemKeys.Enemy_S2_Shiranui, new List<string> { } },
            { GDEItemKeys.Enemy_TheDealer, new List<string> { } },
            { GDEItemKeys.Enemy_S2_MainBoss_1_0, new List<string> { } },
            { GDEItemKeys.Enemy_S2_MainBoss_1_1, new List<string> { } },
            { GDEItemKeys.Enemy_S2_BombClownBoss, new List<string> { } },
            { GDEItemKeys.Enemy_MBoss2_1, new List<string> { } },
            { GDEItemKeys.Enemy_SR_GunManBoss, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter } },
            { GDEItemKeys.Enemy_S3_Boss_Pope, new List<string> { } },
            { GDEItemKeys.Enemy_S3_Boss_TheLight, new List<string> { } },
            { GDEItemKeys.Enemy_S3_Boss_Reaper, new List<string> { } },
            { GDEItemKeys.Enemy_S3_FanaticBoss, new List<string> { } },
            { GDEItemKeys.Enemy_LBossFirst, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter } },
            { GDEItemKeys.Enemy_S4_King_0, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter } },
            { GDEItemKeys.Enemy_ProgramMaster, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter } },
        };

        private static readonly Dictionary<string, List<string>> BannedAbnormalitiesEnemies = new Dictionary<string, List<string>>()
        {
            { GDEItemKeys.Enemy_S2_Ballon, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter} },
            { GDEItemKeys.Enemy_S2_BoomBalloon, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter} },
            { GDEItemKeys.Enemy_S2_HealBallon, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter} },
            { GDEItemKeys.Enemy_S4_King_minion_0, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter} },
            { GDEItemKeys.Enemy_S4_King_minion_1, new List<string> { ModItemKeys.Buff_B_EnemyAbnormality_Shelter} }
        };

        public int EmotionalLevel
        {
            get
            {
                int sumLevel = 0;
                int sumCharacter = 0;
                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    var emotion = enemy.MyEmotion();
                    if (emotion != null)
                    {
                        sumCharacter++;
                        sumLevel += emotion.Level;
                    }
                }
                if (sumLevel == 0) return 0;
                return sumLevel / sumCharacter;
            }
        }

        public int AllPosCoinNum
        {
            get
            {
                int num = 0;
                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    var emotion = enemy.MyEmotion();
                    if (emotion != null)
                    {
                        num += emotion.AccumPosCoin;
                    }
                }
                return num;
            }
        }

        public int AllNegCoinNum
        {
            get
            {
                int num = 0;
                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    var emotion = enemy.MyEmotion();
                    if (emotion != null)
                    {
                        num += emotion.AccumNegCoin;
                    }
                }
                return num;
            }
        }

        public void EnemyNewTurn()
        {
            if (EmotionalLevel > LastEmotionalLevel)
            {
                for (int i = LastEmotionalLevel + 1; i <= EmotionalLevel; i++)
                {
                    var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.ToList();
                    var availableList = AvailableAbnormalities(enemies);
                    if (availableList.Count == 0) break;

                    // select: negative or positive
                    int pos = AllPosCoinNum;
                    int neg = AllNegCoinNum;
                    var isPos = RandomManager.RandomPer("EnemyAbnormality", pos + neg, pos);
                    var selectFrom = availableList.FindAll(abno => abno.Type == (isPos ? AbnoType.Pos : AbnoType.Neg));
                    if (selectFrom.Count == 0)
                    {
                        selectFrom = availableList.ToList();
                    }
                    var abnoReceive = selectFrom.Random("EnemyAbnormality");

                    // select: enemy target
                    var enemyCanGet = enemies.FindAll(enemy => !IsBanned(enemy, abnoReceive.Name));
                    if (enemyCanGet.Count == 0) continue; // shouldn't read here
                    var enemyTarget = enemyCanGet.Random("EnemyAbnormality");

                    enemyTarget.BuffAdd(abnoReceive.Name, enemyTarget);
                    DynamicAbnormalitiesList.Remove(abnoReceive);
                }

                LastEmotionalLevel = EmotionalLevel;
            }
        }

        public List<Abnormality> AvailableAbnormalities(List<BattleChar> characters)
        {
            var list = new HashSet<Abnormality>();
            var boss = characters.FindAll(bc => bc is BattleEnemy enemy && enemy.Boss);

            foreach (var enemy in boss)
            {
                foreach (var abno in DynamicAbnormalitiesList)
                {
                    if (!IsBanned(enemy, abno.Name))
                    {
                        list.Add(abno);
                    }
                }
            }
            if (list.Count > 0)
            {
                characters.RemoveAll(bc => !(bc is BattleEnemy enemy && enemy.Boss));
            }
            else
            {
                // Удаляем боссов
                characters.RemoveAll(bc => bc is BattleEnemy enemy && enemy.Boss);

                // Проверяем обычных врагов
                foreach (var enemy in characters)
                {
                    foreach (var abno in DynamicAbnormalitiesList)
                    {
                        if (!IsBanned(enemy, abno.Name) && !IsBannedForEnemy(enemy, abno.Name))
                        {
                            list.Add(abno);
                        }
                    }
                }
            }

            return list.ToList();
        }

        public static bool IsBanned(BattleChar bc, string abnoName)
        {
            if (BannedAbnormalitiesBosses.ContainsKey(bc.Info.KeyData))
            {
                return BannedAbnormalitiesBosses[bc.Info.KeyData].Contains(abnoName);
            }
            return false;
        }

        public static bool IsBannedForEnemy(BattleChar bc, string abnoName)
        {
            if (BannedAbnormalitiesEnemies.ContainsKey(bc.Info.KeyData))
            {
                return BannedAbnormalitiesEnemies[bc.Info.KeyData].Contains(abnoName);
            }
            return false;
        }

        /*
        private static bool BuffAtMax(BattleChar bc, string buffKey)
        {
            var buff = bc.BuffReturn(buffKey);
            if (buff == null) return false;
            return buff.StackNum >= buff.BuffData.MaxStack;
        }
        */

        public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
        {
            if (charEmotion.BChar is BattleEnemy)
            {
                IEnumerator UpdateMaxLevel()
                {
                    yield return new WaitForEndOfFrame();
                    int levelNow = EmotionalLevel;
                    if (levelNow > HighestEmotionLevel) HighestEmotionLevel = levelNow;
                    yield break;
                }
                BattleSystem.instance.StartCoroutine(UpdateMaxLevel());
            }
        }
    }
}