using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionExtensions;
using Steamworks;
using UnityEngine;

namespace Mia
{
    public class Mia_Button : MonoBehaviour
    {
        public static Mia_Button instance;

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

        public void SavageImpulse()
        {
            var buffKey = ModItemKeys.Buff_B_Mia_SavageImpulse;
            var mia = ModItemKeys.Character_Mia;
            var target = BattleSystem.instance.AllyTeam.AliveChars.Find(c => c.Info.Name == mia && c.BuffReturn(buffKey, false) != null);
            var savageImpulse = target.BuffReturn(buffKey, false) as B_Mia_SavageImpulse;

            if (savageImpulse != null)
            {
                savageImpulse.MiaCall();
            }
        }
    }
}
