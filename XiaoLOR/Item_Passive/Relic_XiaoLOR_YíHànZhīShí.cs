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
using EmotionSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Yí Hàn Zhī Shí
	/// </summary>
    public class Relic_XiaoLOR_YíHànZhīShí : PassiveItemBase, IP_PlayerTurn
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        public void Turn()
        {
            foreach (var b in BattleSystem.instance.AllyTeam.AliveChars)
            {
				EmotionalManager.GetNegEmotion(b, null, 3);
            }
        }
    }
}