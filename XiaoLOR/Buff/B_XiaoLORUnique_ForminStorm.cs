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
namespace XiaoLOR
{
    public class B_XiaoLORUnique_ForminStorm : Buff, IP_PlayerTurn_1
    {
        public void Turn1()
        {
            for (int i = 0; i < StackNum; i++)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORUnique_RagingStormHarm, this.BChar, this.BChar.MyTeam);
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }

            SelfDestroy();
        }   
    }
}