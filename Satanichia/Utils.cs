using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using static CharacterDocument;

namespace Satanichia
{
    public static class Utils
    {
        public static int TwilightTrickScale = 0;

        public static void AddExSheathe(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Satanichia_Sheathe) == null && skill.ExtendedFind(GDEItemKeys.SkillExtended_SkillEn_Ilya_0) == null)
            {
                skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Satanichia_Sheathe);
            }
        }

        public static void RemoveExSheathe(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Satanichia_Sheathe) != null)
            {
                skill.ExtendedDelete_Dataname(ModItemKeys.SkillExtended_Ex_Satanichia_Sheathe);
            }
        }
    }
}
