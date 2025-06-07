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
using EmotionSystem;
using static EnemyCastingLineV2;
using EmotionalSystem;
namespace Xiao
{
    /// <summary>
    /// Heart of Huǒ
    /// </summary>
    public class S_LucyDraw_HeartofHuǒ : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.instance.AllyTeam.Draw(2);

            if (BattleSystem.instance.AllyTeam.AliveChars.All(bc => bc.EmotionLevel() >= 5))
            {
                BattleSystem.instance.AllyTeam.Draw();
                BattleSystem.instance.AllyTeam.AP += 1;

                foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
                {
                    Utils.ApplyBurn(battleEnemy, this.BChar, 4);
                }
                    return;
            }

            foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
            {
                Utils.ApplyBurn(battleEnemy, this.BChar, 2);
            }

            foreach (BattleChar ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
                Utils.GiveEmotionsToChar(ally, 3);
            }
        }
    }
}