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
	/// Mistress' Supremacy
	/// </summary>
    public class B_Xao_MistressSupremacy : Buff, IP_BuffAdd
    {
        private bool IsBuffAdding;

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (BuffUser != BChar || IsBuffAdding) return;
            IsBuffAdding = true;

            BuffTaker.BuffAdd(addedbuff.BuffData.Key, BChar, false, 0, false, -1, false);
            IsBuffAdding = false;
        }
    }
}