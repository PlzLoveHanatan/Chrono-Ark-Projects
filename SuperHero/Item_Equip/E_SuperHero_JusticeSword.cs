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
namespace SuperHero
{
    public class E_SuperHero_JusticeSword : EquipBase, IP_SkillUse_User
    {
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];
            var superHero = ModItemKeys.Character_SuperHero;
            var buff = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            var buff2 = target.BuffReturn(buff, false) as B_SuperHero_MarkofJustice;

            if (SkillD.Master.Info.KeyData == superHero)
            foreach (var t in Targets)
            {
                if (!t.Info.Ally && t.BuffReturn(buff, false) != null)
                {
                    if (buff2 != null)
                    {
                        buff2.BuffData.MaxStack++;
                    }
                }
            }
        }
    }
}