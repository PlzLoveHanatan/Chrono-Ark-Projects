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
namespace Akari
{
    public class Ex_ThreefoldTenacity : BuffSkillExHand
    {
        public override void Init()
        {
            PlusSkillPerFinal.Damage = 20;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            PlusSkillPerFinal.Damage = 20;

            if (BattleSystem.instance.TurnNum <= 3 && !SkillD.FreeUse && !SkillD.BasicSkill && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                BattleSystem.DelayInput(BuffRemove());
            }
        }

        private IEnumerator BuffRemove()
        {
            yield return null;

            if (BChar.BuffReturn(ModItemKeys.Buff_B_ThreefoldTenacity_0, false) != null)
            {
                BChar.BuffRemove(ModItemKeys.Buff_B_ThreefoldTenacity_0);
            }

            //var skill = BattleSystem.instance.AllyTeam.Skills_Basic.Where(s => s.Master.Info == Utils.Akari.Info && s.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_ThreefoldTenacity) != null).FirstOrDefault();
            //skill?.ExtendedDelete(ModItemKeys.SkillExtended_Ex_ThreefoldTenacity);
        }
    }
}