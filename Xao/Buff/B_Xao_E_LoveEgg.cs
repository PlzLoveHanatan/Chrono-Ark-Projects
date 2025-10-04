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
	/// Love Egg
	/// </summary>
    public class B_Xao_E_LoveEgg : Buff
    {
        public override void BuffOneAwake()
        {
            BuffIcon.AddComponent<Button>().onClick.AddListener(Call);
        }

        public void Call()
        {
            Utils.AffectionSelection(BChar, true);
        }
    }
}