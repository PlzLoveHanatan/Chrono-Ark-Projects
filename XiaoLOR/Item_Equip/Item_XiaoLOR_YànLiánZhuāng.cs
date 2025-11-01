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
    /// Yàn Lián Zhuāng
    /// <color=#919191>The lotus that bloomed on the ashes of chaos. She burned the garden to save a single flower.</color>
    /// </summary>
    public class Item_XiaoLOR_YànLiánZhuāng : EquipBase, IP_PlayerTurn
    {
        public override void Init()
        {
            PlusStat.atk = 3;
            PlusStat.def = 25f;
        }

        public void Turn()
        {
            foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
            {
                battleEnemy.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);

                Utils.ApplyBurn(battleEnemy, BChar, 4);
            }

            if (BChar.EmotionLevel() >= 3)
            {
                var buffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).Random(BChar.GetRandomClass().Main, 1);

                foreach (var funnybuff in buffs)
                {
                    funnybuff.SelfDestroy();
                }
            }           
        }
    }
}