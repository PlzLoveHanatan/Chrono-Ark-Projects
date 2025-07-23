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
namespace Urunhilda
{
    /// <summary>
    /// Golden Ecstasy Feet
    /// Gain 50% chance to obtain <color=#FF1493>Rutting Instinct</color>.
    /// </summary>
    public class S_Urunhilda_GoldenStrokingFeet_1 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
            //bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);
            //if (alwaysLucky)
            //{
            //    Utils.AddBuff(BChar, ModItemKeys.Buff_B_Urunhilda_RuttingInstinct, 1);
            //}
        }
    }
}