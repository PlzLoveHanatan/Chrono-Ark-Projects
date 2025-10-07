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
using EmotionalSystem;
using NLog.Targets;
namespace XiaoLOR
{
    public class S_XiaoLOREGO_TāoTiè : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Once = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
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

            //BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 100));

            Utils.ApplyBurn(Targets[0], this.BChar, 8);

            var target = Targets[0];
            var Burn = target.BuffReturn(EmotionalSystem.ModItemKeys.Buff_B_Xiao_Burn, false) as B_Xiao_Burn;

            if (BChar.EmotionLevel() >= 4 && Burn != null)
            {
                BattleSystem.DelayInputAfter(BurnTrigger(Targets[0]));
            }

            //if (BChar.EmotionLevel() >= 4)
            //{
            //    BattleSystem.DelayInput(this.GainGold(Targets[0]));
            //}
        }
        public IEnumerator BurnTrigger(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            if (Target.IsDead) yield break;

            var Burn = Target.BuffReturn(EmotionalSystem.ModItemKeys.Buff_B_Xiao_Burn, false) as B_Xiao_Burn;
            int BurnDamage = Burn.Burn * 2;

            Target.Damage(this.BChar, BurnDamage, false, true, false, 0, false, false, false);
            yield break;
        }

        //public IEnumerator GainGold(BattleChar Target)
        //{
        //    yield return new WaitForSecondsRealtime(0.3f);

        //    if (Target.IsDead)
        //    {
        //        BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 200));
        //    }

        //    yield break;
        //}
    }
}