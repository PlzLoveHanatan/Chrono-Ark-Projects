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
using NLog.Targets;
namespace Mikure
{
    public class Ex_Mikure_1 : Skill_Extended
    {
        public override void Init()
        {
            CanUseStun = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            for (int i = 0; i < 2; i++)
            {
                var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).Random(BChar.GetRandomClass().Main, 1);

                if (debuffs.Count > 0)
                {
                    foreach (var buff in debuffs)
                    {
                        BChar.BuffRemove(buff.BuffData.Key);
                    }
                }
            }
        }
    }
}