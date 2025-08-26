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
namespace Xao
{
    /// <summary>
    /// Wet Dream
    /// </summary>
    public class B_Xao_WetDream : Buff, IP_SkillUse_User_After, IP_Awake
    {
        public void Awake()
        {
            OverloadCheck(BChar);
        }

        public void SkillUseAfter(Skill SkillD)
        {
            if (SkillD.Master == BChar)
            {
                OverloadCheck(BChar);
            }
        }

        public void OverloadCheck(BattleChar bchar)
        {
            if (bchar.Overload >= 1)
            {
                bchar.Overload = 0;
            }
        }
    }
}