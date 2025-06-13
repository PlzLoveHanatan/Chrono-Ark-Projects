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
namespace Aqua
{
    /// <summary>
    /// Splash of Judgment
    /// Apply Taunt status on the target's.
    /// If target's have 
    /// </summary>
    public class S_Aqua_SplashofJudgment : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.NotCount = true;
                base.SkillParticleOn();
                return;
            }

            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.KeyData == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            var target = Targets[0];
            var drenched = target.BuffReturn(ModItemKeys.Buff_B_Aqua_Drenched, false) as B_Aqua_Drenched;

            target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);

            if (drenched?.StackNum >= 1)
            {
                BattleSystem.DelayInput(PlusAttack(target));
            }
        }

        public IEnumerator PlusAttack(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.2f);

            Skill tempSkill;
            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Aqua_Bunny_Skin_H"))
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_SplashofJudgment_0_H, this.BChar, this.BChar.MyTeam);
                skill.FreeUse = true;
                skill.Counting = 1;
                skill.PlusHit = true;
                tempSkill = skill;
            }
            else
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_SplashofJudgment_0, this.BChar, this.BChar.MyTeam);
                skill.FreeUse = true;
                skill.Counting = 1;
                skill.PlusHit = true;
                tempSkill = skill;
            }


            if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
            {
                if (!Target.IsDead)
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(tempSkill, Target, false, false, true, null));
                    Target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);

                }
                else if (BattleSystem.instance.EnemyList.Count > 0)
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(tempSkill, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main), false, false, true, null));
                    Target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);
                }
            }
            yield break;
        }
    }
}
