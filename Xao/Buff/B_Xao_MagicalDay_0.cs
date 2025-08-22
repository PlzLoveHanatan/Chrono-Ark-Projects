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
namespace Xao
{
    /// <summary>
    /// Magical Day
    /// </summary>
    public class B_Xao_MagicalDay_0 : Buff, IP_SkillUse_User
    {
        public override string DescExtended()
        {
            int damage = (int)(BChar.GetStat.atk * 0.2);
            return base.DescExtended().Replace("&a", damage.ToString());
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && SkillD.IsDamage && !SkillD.FreeUse)
            {
                bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);

                if (alwaysLucky)
                {
                    Skill magicalStars = Skill.TempSkill(ModItemKeys.Skill_S_Xao_B_MagicalDay, BChar, BChar.MyTeam);
                    magicalStars.PlusHit = true;
                    magicalStars.FreeUse = true;
                    foreach (var b in Targets)
                    {
                        if (b.Info.Ally) continue;

                        BChar.ParticleOut(magicalStars, b);
                    }
                }
            }
        }
    }
}