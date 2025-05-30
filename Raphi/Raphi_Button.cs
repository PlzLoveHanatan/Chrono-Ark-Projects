using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using UnityEngine;

namespace Raphi
{
    public class Raphi_Button : MonoBehaviour
    {
        public static Raphi_Button instance;

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

        public void CelestialConnection()
        {
            var buffKey = ModItemKeys.Buff_B_CelestialConnection;
            var mia = ModItemKeys.Character_Raphi;
            var target = BattleSystem.instance.AllyTeam.AliveChars.Find(c => c.Info.KeyData == mia && c.BuffReturn(buffKey, false) != null);
            var celestialConnection = target.BuffReturn(buffKey, false) as B_CelestialConnection;

            if (celestialConnection != null)
            {
                celestialConnection.RaphiCall();
            }
        }
    }
}
