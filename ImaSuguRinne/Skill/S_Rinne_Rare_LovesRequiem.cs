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
namespace ImaSuguRinne
{
    /// <summary>
    /// Blooming Rinne
    /// Deal &a additional damage per Rinne's skill's played this turn.
    /// </summary>
    public class S_Rinne_Rare_LovesRequiem : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.5f)).ToString());
        }
        public override void Init()
        {
            if (MySkill.AllExtendeds != null && MySkill.AllExtendeds.Count > 0 && MySkill.MySkill.Name != ModLocalization.BloomingRequiem)
            {
                MySkill.MySkill.Name = ModLocalization.BloomingRequiem;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.SkillBasePlus.Target_BaseDMG = (int)(RinneSkillUsed() * (BChar.GetStat.atk * 0.5f));
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BChar.StartCoroutine(Utils.RetryNow(BChar));
            this.SkillBasePlus.Target_BaseDMG = (int)(RinneSkillUsed() * (BChar.GetStat.atk * 0.5f));

            int randomIndex = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, BChar.MyTeam.Skills_Deck.Count + 1);
            BChar.MyTeam.Skills_Deck.Insert(randomIndex, MySkill);
        }

        private int RinneSkillUsed()
        {
            int currentTurn = BattleSystem.instance.TurnNum;
            int count = 0;

            foreach (BattleLog log in BattleSystem.instance.BattleLogs.ReturnAllLogs())
            {
                if (log.WhoUse.Info.KeyData == ModItemKeys.Character_Rinne && log.Turn == currentTurn)
                {
                    count++;
                }
            }
            return count;
        }
    }
}