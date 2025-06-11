using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace Kazuma
{
    public static class Utils
    {
        public static readonly HashSet<string> FemaleEnemy = new HashSet<string>
        {
            GDEItemKeys.Enemy_S1_Pharos_Healer,
            GDEItemKeys.Enemy_S1_Maid,
            GDEItemKeys.Enemy_S1_LittleMaid,
            GDEItemKeys.Enemy_S1_Kitchenmaid,
            GDEItemKeys.Enemy_S1_WitchBoss,
            GDEItemKeys.Enemy_Story_Witch,
            GDEItemKeys.Enemy_Story_Maid,
            GDEItemKeys.Enemy_S2_Pierrot_Bat,
            GDEItemKeys.Enemy_S2_MainBoss_0_Left,
            GDEItemKeys.Enemy_S2_MainBoss_0_Right,
            GDEItemKeys.Enemy_S2_Pharos_Healer,
            GDEItemKeys.Enemy_S2_PharosWitch,
            GDEItemKeys.Enemy_S2_Ghost,
            GDEItemKeys.Enemy_S2_PopcornGirl,
            GDEItemKeys.Enemy_S2_Shiranui,
            GDEItemKeys.Enemy_SR_Blade,
            GDEItemKeys.Enemy_SR_GuitarList,
            GDEItemKeys.Enemy_SR_Sniper,
            GDEItemKeys.Enemy_S3_Boss_Pope,
            GDEItemKeys.Enemy_S3_Boss_TheLight,
            GDEItemKeys.Enemy_S4_Summoner,
        };
    }
}
