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
using UnityEngine.Experimental.UIElements;
using System.Web.Compilation;
namespace Mia
{
    public class B_Mia_E_Meowpiercer : Buff
    {
        public override void Init()
        {
            if (BattleSystem.instance != null)
            {
                if (StackNum >= 2 && BChar.Info.KeyData == ModItemKeys.Character_Mia)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_SavageImpulse, BChar, false, 0, false, -1, false);
                    SelfDestroy();
                }

                else if (StackNum >= 2)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_InstinctSurge, BChar, false, 0, false, -1, false);
                    SelfDestroy();
                }
            }
        }
    }
}