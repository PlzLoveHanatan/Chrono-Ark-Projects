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
namespace EmotionalSystem
{
    /// <summary>
    /// Revenge Strike
    /// </summary>
    public class S_EmotionalSystem_RevengeStrike : Skill_Extended, IP_DamageTake, IP_PlayerTurn, IP_SkillCastingStart
    {
        private int maxDamage;
        private int plusDamage;

        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", maxDamage.ToString());
        }
        public override void Init()
        {
            base.Init();
            EnemyPreviewNoArrow = true;
            plusDamage = 0;
            CountingExtedned = true;
            SetMaxDamageByStage();
        }

        private void SetMaxDamageByStage()
        {
            switch (PlayData.TSavedata.StageNum)
            {
                case 1: maxDamage = 10; break;
                case 2: maxDamage = 15; break;
                case 3: maxDamage = 20; break;
                case 4: maxDamage = 25; break;
                case 5: maxDamage = 30; break;
                default: maxDamage = 10; break;
            }
        }

        public void DamageTake(BattleChar user, int dmg, bool cri, ref bool resist, bool noDef = false, bool noEffect = false, BattleChar target = null)
        {
            if (target == BChar)
            {
                plusDamage += dmg;
                if (plusDamage > maxDamage)
                    plusDamage = maxDamage;

                UpdateSkillBase();
            }
        }

        public void Turn()
        {
            ResetDamage();
        }

        public override void SkillUseSingleAfter(Skill skillD, List<BattleChar> targets)
        {
            BattleSystem.DelayInput(ResetDamageEnum());
        }

        public IEnumerator ResetDamageEnum()
        {
            yield return new WaitForFixedUpdate();
            ResetDamage();
        }

        public void SkillCasting(CastingSkill thisSkill)
        {
            ResetDamage();
        }

        private void ResetDamage()
        {
            plusDamage = 0;
            UpdateSkillBase();
        }

        private void UpdateSkillBase()
        {
            SkillBasePlus.Target_BaseDMG = plusDamage;
        }
    }
}