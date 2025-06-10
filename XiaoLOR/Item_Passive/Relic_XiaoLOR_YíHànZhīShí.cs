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
using EmotionalSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Yí Hàn Zhī Shí
	/// </summary>
    public class Relic_XiaoLOR_YíHànZhīShí : PassiveItemBase, IP_PlayerTurn_1
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        public void Turn1()
        {
            foreach (var b in BattleSystem.instance.AllyTeam.AliveChars)
            {
                //BattleChar battleChar = BattleSystem.instance.AllyTeam.AliveChars.Random(BattleRandom.PassiveItem);
                int random = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
                Utils.GiveEmotionsToChar(b, random);
            }
        }
    }
}