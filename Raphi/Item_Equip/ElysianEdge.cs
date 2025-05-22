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
using Spine;
namespace Raphi
{
    public class ElysianEdge : EquipBase, IP_SkillUse_User, IP_PlayerTurn
    {
        private int skillUsesThisTurn = 0;

        public override void Init()
        {
            PlusPerStat.Damage = 15;
            PlusStat.hit = 10f;
            PlusStat.HIT_CC = 15f;
            PlusStat.HIT_DEBUFF = 15f;
            PlusStat.HIT_DOT = 15f;
            base.Init();
        }
        
        public void Turn()
        {
            skillUsesThisTurn = 0;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && !SkillD.BasicSkill && !SkillD.FreeUse && skillUsesThisTurn < 4)
            {
                skillUsesThisTurn++;

                BChar.BuffAdd(ModItemKeys.Buff_B_ElysianEdge, BChar, false, 0, false, -1, false);
            }
        }
    }
}