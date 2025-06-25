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
    public class S_EmotionalSystem_EmotionBurst : Skill_Extended
    {
        private int PlusDamage;
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", PlusDamage.ToString());
        }
        public override void Init()
        {
            switch (PlayData.TSavedata.StageNum)
            {
                case 1: PlusDamage = 10; break;
                case 2: PlusDamage = 15; break;
                case 3: PlusDamage = 20; break;
                case 4: PlusDamage = 25; break;
                case 5: PlusDamage = 30; break;
                default: PlusDamage = 10; break;
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            SkillBasePlus.Target_BaseDMG += PlusDamage;

            var aliveAllies = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null).ToList();

            int emotionalLevel = aliveAllies.Min(x => x.EmotionLevel());

            var lowestEmotionAllies = aliveAllies.Where(x => x.EmotionLevel() == emotionalLevel);
            Targets.Clear();
            Targets.AddRange(lowestEmotionAllies);
        }
    }
}