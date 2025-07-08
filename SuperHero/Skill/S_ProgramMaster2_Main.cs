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
namespace SuperHero
{
    public class S_ProgramMaster2_Main : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.EnemyPreviewNoArrow = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var aliveAllies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero).ToList();
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, aliveAllies.Count);
            var randomTarget = aliveAllies[index];
            base.SkillUseSingle(SkillD, Targets);

            if (Targets[0].Info.KeyData == superHero)
            {
                Targets[0] = randomTarget;
                if (Targets[0] is BattleAlly)
                {
                    (Targets[0] as BattleAlly).ForceDead();
                    BattleSystem.instance.StartCoroutine(this.Delay(Targets[0]));
                }
            }
            else if (Targets[0] is BattleAlly)
            {
                (Targets[0] as BattleAlly).ForceDead();
                BattleSystem.instance.StartCoroutine(this.Delay(Targets[0]));
            }
        }

        public IEnumerator Delay(BattleChar Target)
        {
            if (BattleSystem.instance.AllyTeam.AliveChars.Count == 0)
            {
                yield return new WaitForSecondsRealtime(1f);
            }
            if (Target is BattleAlly)
            {
                (Target as BattleAlly).UI.gameObject.SetActive(false);
            }
            yield break;
        }
    }
}