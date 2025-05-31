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
namespace Mia
{
    /// <summary>
    /// Camel Sprint
    /// Gain 2 stacks of <color=#FF0070>Savage Impulse</color>,
    /// or 2 stacks of <color=#FF4E00>Instinct Surge</color> if cast by an ally.
    /// </summary>
    public class S_Mia_CamelSprint : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.instance.AllyTeam.Draw();

            if (BChar.Info.Name == ModItemKeys.Character_Mia)
            {
                for (int i = 0; i < 2; i++)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_SavageImpulse, BChar, false, 0, false, -1, false);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_InstinctSurge, BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}