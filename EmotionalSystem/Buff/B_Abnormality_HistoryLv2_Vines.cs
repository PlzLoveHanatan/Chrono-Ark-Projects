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
namespace EmotionalSystem
{
    /// <summary>
    /// Vines
    /// </summary>
    public class B_Abnormality_HistoryLv2_Vines : Buff, IP_PlayerTurn_1, IP_Awake
    {
        public void Awake()
        {
            foreach (BattleChar battleChar in this.BChar.MyTeam.AliveChars)
            {
                battleChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_1, this.BChar, false, 0, false, -1, false);

                MasterAudio.PlaySound("Vines", 100f, null, 0f, null, null, false, false);
            }
        }
        public void Turn1()
        {
            MasterAudio.PlaySound("Vines", 100f, null, 0f, null, null, false, false);

            var Vines = ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0;

            foreach (var b in BattleSystem.instance.EnemyTeam.AliveChars)
            {
                if (b.BuffReturn(Vines, false) == null)
                {
                    b.BuffAdd(Vines, this.BChar, false, 0, false, -1, false);
                }
            }
            //if (BattleSystem.instance.EnemyList.Count > 0)
            //{
            //    List<BattleEnemy> list = new List<BattleEnemy>();
            //    list.AddRange(BattleSystem.instance.EnemyList);
            //    if (list.Count != 0)
            //    {
            //        list.Random(this.BChar.GetRandomClass().Main).BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0, this.BChar, false, 100, false, -1, false);
            //        return;
            //    }
            //}
        }
    }
}