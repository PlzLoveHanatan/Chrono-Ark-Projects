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
namespace Akari
{
    public class S_Akari_StandartMag : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);
            var bhcar = Utils.Akari ? Utils.Akari : BattleSystem.instance.AllyTeam.LucyAlly;
            Utils.CreateRandomAmmunition(Targets[0], 2);
        }
    }
}