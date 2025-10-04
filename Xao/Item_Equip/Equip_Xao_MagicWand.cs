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
            PlusPerStat.Damage = 25;
            PlusPerStat.Heal = 25;
            PlusStat.dod = 3;
            PlusStat.cri = 3;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit)
            {
                Utils.AllyHentaiText(BChar);
                Xao_Combo.ComboChange(1);
            }
        }
    }
}