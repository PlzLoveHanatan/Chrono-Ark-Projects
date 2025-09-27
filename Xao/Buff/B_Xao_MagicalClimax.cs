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
	/// Magical Climax
	/// </summary>
    public class B_Xao_MagicalClimax : Buff, IP_Awake, IP_PlayerTurn
    {
        public override void Init()
        {
            PlusStat.dod = 10f;
        }

        public void Awake()
        {
            Xao_Combo.SaveComboBetweenTurns = true;
        }

        public void Turn()
        {
            Utils.AllyTeam.AP += 1;
            SelfDestroy();
        }		
    }
}