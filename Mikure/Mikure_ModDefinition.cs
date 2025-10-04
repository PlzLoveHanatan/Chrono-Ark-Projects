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
namespace Mikure
{
    public class Mikure_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);
        public override List<object> BattleSystem_ModIReturn()
        {
            var list = base.BattleSystem_ModIReturn();

            list.Add(new ModIReturn());

            return list;
        }
    }

    public class ModIReturn : IP_BattleStart_Ones, IP_BattleEnd
    {
        public void BattleStart(BattleSystem Ins)
        {
            if (Utils.Mikure)
            {
                Utils.AllySkills.Clear();
                Utils.SaveAllySkill();

                if (Utils.RemovePainSharing)
                {
                    Utils.PainSharingRemoveAlly();
                    Utils.PainSharingRemoveLucy();
                }
            }
        }

        public void BattleEnd()
        {
            if (Utils.Mikure)
            {
                Utils.AllySkills.Clear();
            }   
        }
    }
}