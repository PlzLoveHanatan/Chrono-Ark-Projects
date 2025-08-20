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
    /// <summary>
    /// Wet Dream
    /// </summary>
    public class B_Xao_WetDream : Buff, IP_SkillUse_User_After
    {
        public void SkillUseAfter(Skill SkillD)
        {
            if (SkillD.Master == BChar && BChar.Overload >= 1)
            {
                BChar.Overload = 0;
            }
        }
    }
}