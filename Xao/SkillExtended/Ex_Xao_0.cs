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
    public class Ex_Xao_0 : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Xao_Combo.ComboChange(1);

            if (Xao_Combo.CurrentCombo >= 4)
            {
                ApplyRoleStatBonus(BChar.Info);
            }
        }

        public static void ApplyRoleStatBonus(Character character)
        {
            if (character.GetData.Role.Key == GDEItemKeys.CharRole_Role_DPS)
            {
                character.OriginStat.atk += 1;
            }
            else if (character.GetData.Role.Key == GDEItemKeys.CharRole_Role_Tank)
            {
                character.OriginStat.def += 3;
            }
            else if (character.GetData.Role.Key == GDEItemKeys.CharRole_Role_Support)
            {
                character.OriginStat.reg += 1;
            }
        }
    }
}