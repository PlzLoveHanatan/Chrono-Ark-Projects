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
namespace Unica
{
    public class DrawNextTurn : Buff, IP_DrawNumChange, IP_PlayerTurn_1
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(base.StackNum)).ToString());
        }
        public void DrawNumChange(int DrawNum, out int OutNum)
        {
            OutNum = DrawNum + StackNum;
        }
        public override void Init()
        {
            base.Init();
            this.NoShowTimeNum_Tooltip = true;
        }
        public void Turn1()
        {
            base.SelfDestroy();
        }
    }
}