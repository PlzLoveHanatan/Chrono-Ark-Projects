using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;

namespace Raphi
{
    public static class Utils
    {
        public static void AddExHeavenlyGrace(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_1) == null && skill.ExtendedFind(GDEItemKeys.SkillExtended_SkillEn_Ilya_0) == null)
            {
                skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Raphi_1);
            }
        }

        public static void RemoveExHeavenlyGrace(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_1) != null)
            {
                skill.ExtendedDelete_Dataname(ModItemKeys.SkillExtended_Ex_Raphi_1);
            }
        }

        public static void AddExHeavenlyWrath(Skill skill)
        {

            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_2) == null && skill.ExtendedFind(GDEItemKeys.SkillExtended_SkillEn_Ilya_0) == null)
            {
                skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Raphi_2);
            }
        }

        public static void RemoveExHeavenlyWrath(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_2) != null)
            {
                skill.ExtendedDelete_Dataname(ModItemKeys.SkillExtended_Ex_Raphi_2);
            }
        }
    }
}
