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
using static CharacterDocument;
namespace SuperHero
{
    public class B_SuperHero_IntheNameofJustice : Buff, IP_Discard
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (skill.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0)
            {
                if (!HandFullWaste)
                {
                    BattleSystem.instance.AllyTeam.Draw();
                }
            }
        }
    }
}