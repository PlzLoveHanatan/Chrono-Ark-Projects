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
using System.Windows.Markup;

namespace Urunhilda
{
    public static class Urunhilda_Items
    {
        public static bool IsRare(GDESkillData skillData)
        {
            return skillData.Rare;
        }

        public static List<GDESkillData> GetAllRareSkills()
        {
            var skillList = PlayData.ALLSKILLLIST;
            skillList = skillList.FindAll(data => IsRare(data));
            return skillList;
        }

        //public static List<GDESkillData> GetAllDamageSkills()
        //{
        //    var skillList = PlayData.ALLSKILLLIST;
        //    skillList = skillList.FindAll(data => IsDamage(data));
        //    return skillList;
        //}

        //public static List<GDESkillData> GetAllOtherSkills()
        //{
        //    var skillList = PlayData.ALLSKILLLIST;
        //    skillList = skillList.FindAll(data => !IsHeal(data) && !IsDamage(data));
        //    return skillList;
    }
}
