using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using UnityEngine;

namespace Aqua
{
    public class Aqua_Button : MonoBehaviour
    {
        public static Aqua_Button instance;

        private static readonly List<string> AquaVoiceLines = new List<string>
        {
            "AquaGradePurification",
            "BlessingoftheAxisCult",
            "DivineLottery",
            "FogofBlessings",
            "GoddessSecretWeapon",
            "OverflowingGrace",
            "PartyDrunkard",
            "PartyTrick",
            "NaturesBeauty_0",
            "NaturesBeauty_1",
            "PhantasmalBeauty",
            "VanishTrick",
            "TelekinesisTrick",
            "Certainkillpartytrick",
            "UnusualPlant",
            "Minorpocketdimension",
            "AxisCultRecruitment",
            "GodsBlow_0",
            "GodsBlow_1",
            "SplashofJudgment_0",
            "SplashofJudgment_1",
            "TorrentialTears",
            "BattleStart_0",
            "BattleStart_1",
            "BattleStart_2",
            "IdlingB_0",
            "IdlingB_1",
            "DeathA_0",
            "DeathA_1",
            "DeathA_2",
            "DeathAlly_0",
            "DeathAlly_1",
            "Kill_0",
            "Kill_1",
            "Cri_0",
            "Chest_0",
            "Potion_0",
            "IdlingF_0",
            "IdlingF_1",
            "IdlingF_2",
            "Heal_0",
            "Heal_1",
            "Heal_2",
            "Curse",
            "Pharos",
            "Other_0",
            "Other_1",
            "Master",
            "Extra_0",
            "Extra_1",
            "Extra_2",
            "Extra_3",
            "Extra_4",
            "Extra_5",
            "Extra_6",
            "Extra_7",
            "Extra_8",
            "Extra_9",
            "Extra_10",
            "Extra_11",
            "Extra_12"
        };


        public void Awake()
        {
            instance = this;
        }

        public void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public void AquaYapping()
        {
            string randomSound = AquaVoiceLines[RandomManager.RandomInt(BattleRandom.PassiveItem, 0, AquaVoiceLines.Count)];
            PlayData.TSavedata._Gold += 1;
            MasterAudio.StopBus("SE");
            MasterAudio.PlaySound(randomSound, 100f);
        }
    }
}
