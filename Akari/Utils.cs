using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Akari
{
    public static class Utils
    {
        public static readonly List<string> Ammunition = new List<string>
        {
            ModItemKeys.Skill_Armor_piercingAmmunition,
            ModItemKeys.Skill_FlameAmmunition,
            ModItemKeys.Skill_FrostAmmunition,
        };

        public static readonly List<string> RangeAttacks = new List<string>
        {
            ModItemKeys.Skill_FocusFire,
            ModItemKeys.Skill_ShockRound,
            ModItemKeys.Skill_SummaryJudgment,
            ModItemKeys.Skill_SuppressingShot
        };

        public static void CreateRandomAmmunition(BattleChar user, int stack = 1)
        {
            for (int i = 0; i < stack; i++)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, Ammunition.Count);
                string newAmmunition = Ammunition[index];
                Skill randomAummuniton = Skill.TempSkill(newAmmunition, user, user.MyTeam);

                BattleSystem.instance.AllyTeam.Add(randomAummuniton, true);
            }
        }

        public static void AkariCastingWasteFixed(this BattleActWindow window, CastingSkill cast)
        {
            SkillButton[] componentsInChildren = window.CastingGroup.GetComponentsInChildren<SkillButton>();
            SkillButton skillButton = componentsInChildren.FirstOrDefault(bt => bt.castskill == cast);
            foreach (IP_SkillCastingQuit ip_SkillCastingQuit in cast.skill.IReturn<IP_SkillCastingQuit>())
            {
                if (ip_SkillCastingQuit != null)
                {
                    ip_SkillCastingQuit.SkillCastingQuit(cast);
                }
            }
            if (skillButton != null)
            {
                skillButton.UseWaste();
            }
            window.SetCountSkillVL((window.CastingGroup.GetComponentsInChildren<SkillButton>().Length >= 13) ? 30 : 45);
        }
    }
}
