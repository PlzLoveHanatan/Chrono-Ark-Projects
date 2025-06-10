using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;

namespace XiaoLOR
{
    public static class XiaoUtils
    {
        public static bool IronLotusSong => ModManager.getModInfo("XiaoLOR").GetSetting<ToggleSetting>("Iron Lotus Song").Value;
        public static bool IronLotusSongKeyIngredient => ModManager.getModInfo("XiaoLOR").GetSetting<ToggleSetting>("IronLotusSongKeyIngredient").Value;
    }
}
