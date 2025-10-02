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
namespace Aqua
{
    /// <summary>
    /// Goddess's Secret Weapon (Don't Tell Kazuma)
    /// Draw 2 skill's and cast Nature's Beauty to all allies.
    /// </summary>
    public class S_Aqua_LucyDraw : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveAqua = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Aqua);

            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && aliveAqua != null)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            int drawnum = 0;

            if (aliveAqua != null)
            {
                foreach (var ally in allies)
                {
                    ally.Heal(BattleSystem.instance.DummyChar, 5f, false, true, null);
                    ally.BuffAdd("B_PopcornGirl_Lucy_1", BattleSystem.instance.AllyTeam.LucyChar, false, 0, false, -1, false);

                    Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, this.BChar, this.BChar.MyTeam);
                    healingParticle.PlusHit = true;
                    healingParticle.FreeUse = true;

                    this.BChar.ParticleOut(healingParticle, ally);
                    drawnum = 3;
                }
            }
            else
            {
                MySkill.isExcept = true;
                drawnum = 1;
            }
            BattleSystem.instance.AllyTeam.Draw(drawnum);
        }
    }
}