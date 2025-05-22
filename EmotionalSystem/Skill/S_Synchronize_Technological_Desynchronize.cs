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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace EmotionalSystem
{
    /// <summary>
    /// <color=#ffc500>Desynchronize</color>
    /// </summary>
    public class S_Synchronize_Technological_Desynchronize : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var team = BattleSystem.instance.AllyTeam;

            for (int j = team.Skills.Count - 1; j >= 0; j--)
            {
                Skill skill = team.Skills[j];
                if (skill.Master == BChar)
                {
                    skill.Remove();
                }
            }
            var buff = BChar.BuffReturn(ModItemKeys.Buff_B_LucyEGO_Technological_MagicBullet, false) as B_LucyEGO_Technological_MagicBullet;
            var fakeBuff = BChar.BuffReturn(ModItemKeys.Buff_B_LucyEGO_Technological_MagicBullet_0, false) as B_LucyEGO_Technological_MagicBullet_0;

            team.Skills.RemoveAll(s => s.Master == BChar);
            team.Skills_Deck.RemoveAll(s => s.Master == BChar);
            team.Skills_UsedDeck.RemoveAll(s => s.Master == BChar);

            team.Skills_Deck.AddRange(fakeBuff.DrawPile);
            team.Skills_UsedDeck.AddRange(fakeBuff.DiscardPile);

            foreach (var skill in fakeBuff.Hand)
            {
                BattleSystem.instance.StartCoroutine(team.AddSkillNoDrawEffect(skill, -1));
            }

            buff.SelfDestroy();
            fakeBuff.SelfDestroy();
        }
    }
}