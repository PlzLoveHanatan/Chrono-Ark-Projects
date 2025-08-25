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
namespace Xao
{
    public class B_Xao_S_SimpleExchange : Buff, IP_Awake, IP_TurnEnd
    {
        public bool FirstAwake;

        public void Awake()
        {
            if (!FirstAwake)
            {
                Utils.RareNum = 0;
                FirstAwake = true;
            }
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (skill.MySkill.KeyID == ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_0)
            {
                Utils.RareNum = 0;
                Utils.RareBuffAwake = false;
            }
        }

        public void TurnEnd()
        {
            Utils.RareNum = 0;
            Utils.RareBuffAwake = false;
        }
    }
}