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
using System.Threading;
using System.Security.Cryptography;
namespace Darkness
{
    /// <summary>
    /// Busty Buffer
    /// </summary>
    public class B_Darkness_BustyBuffer : Buff, IP_SkillUse_User
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(Usestate_F.GetStat.maxhp * 0.25f)).ToString());
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            int barrierHP = (int)(base.Usestate_L.GetStat.maxhp * 0.25f);
            BChar.BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight, BChar, false, 0, false, -1, false).BarrierHP += barrierHP;
            if (SkillD.FreeUse) return;
            base.SelfStackDestroy();
        }
    }
}