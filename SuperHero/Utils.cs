using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero
{
    public static class Utils
    {
        public static int Attack;

        public static readonly List<string> HeroAttacks = new List<string>
        {
            ModItemKeys.Skill_S_SuperHero_ErasetheMobs,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0,
            ModItemKeys.Skill_S_SuperHero_UnwantedSuccessStory,
            ModItemKeys.Skill_S_SuperHero_BloodstainedDress,
        };
    }
}
