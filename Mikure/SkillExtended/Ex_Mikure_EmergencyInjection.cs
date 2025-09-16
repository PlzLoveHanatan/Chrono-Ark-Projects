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
namespace Mikure
{
    public class Ex_Mikure_EmergencyInjection : BuffSkillExHand
    {
        public override void Init()
        {
            PlusSkillPerFinal.Heal = 100;
        }
    }
}