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
namespace Unica
{
    public class P_Unica : Passive_Char, IP_BattleStart_Ones
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        public void BattleStart(BattleSystem Ins)
        {
            this.BChar.BuffAdd(ModItemKeys.Buff_Margin, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(ModItemKeys.Buff_InTimesLikeThese, this.BChar, false, 0, false, 1, false);
        }
        public void ApplyEffects(BattleChar BChar, int discardedSkillsCount)
        {
            BChar.BuffAdd(ModItemKeys.Buff_BottomDeal, BChar, false, 0, false, -1, false);
            BChar.BuffAdd(ModItemKeys.Buff_StackingtheDeck, BChar, false, 0, false, 1, false);

            BChar.Heal(BattleSystem.instance.DummyChar, discardedSkillsCount * 3f, false, true, null);
        }
    }
}