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
namespace Raphi
{
    /// <summary>
    /// Passive:
    /// </summary>
    public class P_Raphi : Passive_Char, IP_LevelUp, IP_PlayerTurn
    {
        public bool ItemTake;

        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void Turn()
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection, BChar, false, 0, false, -1, false);
        }

        public void LevelUp()
        {
            FieldSystem.DelayInput(Delay());
        }

        public IEnumerator Delay()
        {
            if (!ItemTake)
            {
                ItemTake = true;
                InventoryManager.Reward(ItemBase.GetItem(ModItemKeys.Item_Consume_Raphi_Consume, 3));
            }
            yield return null;
            yield break;
        }
    }
}
