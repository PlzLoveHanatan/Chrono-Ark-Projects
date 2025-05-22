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
namespace Aqua
{
	/// <summary>
	/// Telekinesis Trick
	/// Deal &a (equal 50% Aqua's Attack power) damage and apply "Unstable Posture" to a random target.
	/// <color=#919191>Totally Controlled!</color>
	/// </summary>
    public class S_Aqua_PartyTrick_TelekinesisTrick : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.5f)).ToString());
        }
    }
}