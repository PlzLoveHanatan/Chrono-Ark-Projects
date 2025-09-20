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
namespace SuperHero
{
    public class SuperHero_ModDefinition : ModDefinition
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
            if (SuperHero_Plugin.SuperHeroInParty())
            {
                Utils.SuperStats = false;
                Utils.RemovePainSharingBuffsFromAllAllies();
                Utils.RemovePainSharingBuffsFromLucy();
                //MasterAudio.PlaySound("BattleStart", 1f, null, 0f, null, null, false, false);
            }
        }

        public void BattleEnd()
        {
            if (SuperHero_Plugin.SuperHeroInParty())
            {
                Utils.SuperStats = false;
                //BattleSystem.DelayInput(Utils.StopSong());
                MasterAudio.StopBus("BGM");
                MasterAudio.FadeBusToVolume("BGM", 0f, 1f, null, false, false);
                FieldSystem.FieldBGMOn();
            }
        }
    }
}