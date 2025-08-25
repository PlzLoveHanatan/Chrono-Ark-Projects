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
    public class Ex_Xao_1 : Skill_Extended, IP_BattleStart_UIOnBefore
    {
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            Xao_Hearts.SavedStackAllySynergy = 0;
            Xao_Hearts.SavedStackAlly = 0;
        }

        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD == null || BChar == null) return;

            Xao_Combo.SaveComboBetweenTurns = true;
            Xao_Combo.ComboChange(1);

            string buff = ModItemKeys.Buff_B_Xao_Affection_Ally_Synergy;

            if (MyChar.Equip != null)
            {
                if (MyChar.Equip.Exists(item => item != null && item.itemkey == ModItemKeys.Item_Equip_Equip_Xao_LoveEgg))
                {
                    buff = ModItemKeys.Buff_B_Xao_Affection_Ally;
                }
            }
            Utils.AllyHentaiText(BChar);
            Utils.AddBuff(BChar, buff, 1);
        }
    }
}