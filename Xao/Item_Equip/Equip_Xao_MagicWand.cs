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
    /// Magic Wand
    /// </summary>
    public class Equip_Xao_MagicWand : EquipBase, IP_SkillUse_User
    {
        public override void Init()
        {
            OnePassive = true;
            PlusPerStat.Damage = 20;
            PlusPerStat.Heal = 20;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Xao_Combo.ComboChange(1);
            }
        }
    }
}