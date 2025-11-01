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
using EmotionSystem;
using static EmotionSystem.Extended.EGO;
using NLog.Targets;
namespace XiaoLOR
{
    public class S_XiaoLOREGO_TāoTiè : Ex_EGO
    {
        public override void Init()
        {
            base.Init();
            OncePerFight = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 4)
            {
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("EGOHit");
            Utils.ApplyBurn(Targets[0], this.BChar, 8);

            var target = Targets[0];
            var Burn = target.BuffReturn(EmotionSystem.ModItemKeys.Buff_B_EmotionSystem_Burn, false) as Debuffs.Burn;

            if (BChar.EmotionLevel() >= 4 && Burn != null)
            {
                BattleSystem.DelayInputAfter(BurnTrigger(Targets[0]));
            }
        }

        public IEnumerator BurnTrigger(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            if (Target.IsDead) yield break;

            var Burn = Target.BuffReturn(EmotionSystem.ModItemKeys.Buff_B_EmotionSystem_Burn, false) as Debuffs.Burn;
			int BurnDamage = Burn.CurrentBurn * 2;

            Target.Damage(this.BChar, BurnDamage, false, true, false, 0, false, false, false);
            yield break;
        }
    }
}