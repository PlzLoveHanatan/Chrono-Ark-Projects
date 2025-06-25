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
namespace EmotionalSystem
{
    /// <summary>
    /// Emotion Burst
    /// </summary>
    public class S_EmotionalSystem_EmotionBurst : Skill_Extended, IP_TargetAI
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", Damage.ToString());
        }
        public override void Init()
        {
            base.Init();
            EnemyTargetAIOnly = true;
            IsDamage = true;
        }

        private int Damage => 10 + (PlayData.TSavedata.StageNum * 3);

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var aliveAllies = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null).ToList();

            int emotionalLevel = aliveAllies.Min(x => x.EmotionLevel());

            var lowestEmotionAllies = aliveAllies.Where(x => x.EmotionLevel() == emotionalLevel);

            int count = lowestEmotionAllies.Count();

            SkillBasePlus.Target_BaseDMG += Damage / count;

            Targets.Clear();
            Targets.AddRange(lowestEmotionAllies);
        }

        public List<BattleChar> TargetAI(BattleEnemy MyBchar)
        {
            var aliveAllies = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null).ToList();

            int emotionalLevel = aliveAllies.Min(x => x.EmotionLevel());

            var lowestEmotionAllies = aliveAllies.Where(x => x.EmotionLevel() == emotionalLevel);

            return lowestEmotionAllies.ToList();
        }
    }
}