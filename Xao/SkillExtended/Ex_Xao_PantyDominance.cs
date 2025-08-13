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
	/// Panty Dominance
	/// </summary>
    public class Ex_Xao_PantyDominance : BuffSkillExHand
    {
        public override void Init()
        {
            base.Init();
            APChange = -1;
        }
    }
}