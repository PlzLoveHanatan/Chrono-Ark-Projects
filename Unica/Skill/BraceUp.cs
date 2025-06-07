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

namespace Unica
{
    /// <summary>
    /// BraceUp
    /// 
    /// </summary>
    public class BraceUp : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse) return;

            base.SkillUseSingle(SkillD, Targets);

            var discardList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill
            && skill.Master != BattleSystem.instance.AllyTeam.LucyAlly).ToList();

            if (discardList.Count == 0) return;

            var skillToDiscard = discardList.FirstOrDefault(skill => skill.ExtendedFind_DataName(ModItemKeys.SkillExtended_ExSheathe) != null)
                ?? discardList.Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Unica).OrderBy(skill => skill.AP).FirstOrDefault()
                ?? discardList.OrderBy(skill => skill.AP).FirstOrDefault();

            skillToDiscard?.Delete(false);

            if (this.BChar.Info.KeyData == ModItemKeys.Character_Unica)
            {
                new P_Unica().ApplyEffects(this.BChar, 1);
            }

        }
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(ModItemKeys.Buff_FavorableHand, this.BChar, false, 0, false, -1, false);
        }
    }
}
