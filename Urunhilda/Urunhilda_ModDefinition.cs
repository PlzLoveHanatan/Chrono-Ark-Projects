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
using ChronoArkMod.ModData;
namespace Urunhilda
{
    public class Urunhilda_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);
        public override List<object> BattleSystem_ModIReturn()
        {
            var list = base.BattleSystem_ModIReturn();

            list.Add(new ModIReturn());

            return list;
        }

        public class ModIReturn : IP_BattleStart_Ones
        {
            public void BattleStart(BattleSystem Ins)
            {
                string urunhilda = ModItemKeys.Character_Urunhilda;
                string arousal = ModItemKeys.Buff_B_Urunhilda_RuttingInstinct;
                var aliveUrunhilda = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == urunhilda);
                if (aliveUrunhilda != null)
                {
                    Urunhilda_Visual.CreateLocks(aliveUrunhilda);
                    //aliveUrunhilda.BuffAdd(arousal, aliveUrunhilda, false, 0, false, -1, false);
                    Urunhilda_Visual.SavedStackNum = 0;
                }
            }
        }
    }
}