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
namespace Xiao
{
    public class B_XiaoUnique_ForminStorm : Buff, IP_PlayerTurn
    {
        public void Turn()
        {
            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_XiaoUnique_RagingStormHarm, this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
            SelfDestroy();
        }   
    }
}