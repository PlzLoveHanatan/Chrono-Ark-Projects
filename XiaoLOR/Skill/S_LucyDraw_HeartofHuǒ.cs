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
namespace XiaoLOR
{
    /// <summary>
    /// Heart of Huǒ
    /// </summary>
    public class S_LucyDraw_HeartofHuǒ : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool isMaxLv = BattleSystem.instance.AllyTeam.AliveChars.All(bc => bc.EmotionLevel() >= 5);
            int burnNum = isMaxLv ? 5 : 2;
            int drawNum = isMaxLv ? 3 : 2;

			BattleSystem.instance.AllyTeam.Draw(drawNum);


            foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
            {
                Utils.ApplyBurn(battleEnemy, this.BChar, burnNum);
            }

            if (isMaxLv) return;

            foreach (BattleChar ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
				EmotionalManager.GetNegEmotion(ally, null, 3);
            }
        }
    }
}