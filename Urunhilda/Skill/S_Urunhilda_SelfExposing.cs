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
    /// Self Exposing
    /// Apply 'Lustful Desire' to a random Male ally.
    /// </summary>
    public class S_Urunhilda_SelfExposing : Skill_Extended
    {
        public override bool Terms()
        {
            if (new GDECharacterData(BChar.Info.KeyData).Gender == 1)
                return true;

            return false;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
            string urunhilda = ModItemKeys.Character_Urunhilda;
            string rutting = ModItemKeys.Buff_B_Urunhilda_RuttingInstinct;
            var ruttingInstincts = BChar.BuffReturn(rutting, false) as B_Urunhilda_RuttingInstinct;
            //string lustfulDesire = ModItemKeys.Buff_B_Urunhilda_LustfulDesire_0;

            //var maleTargets = BattleSystem.instance.AllyTeam.AliveChars
            //    .Where(x => x != null && x.Info.KeyData != urunhilda && new GDECharacterData(x.Info.KeyData).Gender == 0).ToList();

            if (ruttingInstincts?.StackNum >= 5)
            {
                for (int i = 0; i < 2; i++)
                {
                    BattleSystem.instance.AllyTeam.CharacterDraw(Targets[0], null);
                }
            }
            //if (maleTargets.Count > 0)
            //{
            //    var randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, maleTargets.Count);
            //    var randomMale = maleTargets[randomIndex];
            //    int num = 1;
            //    var team = BattleSystem.instance.AllyTeam;
            //    Utils.AddBuff(randomMale, lustfulDesire, num);
        }
    }
}