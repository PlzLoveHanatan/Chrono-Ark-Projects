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
using Spine;
namespace Darkness
{
    public class Ex_Darkness_1 : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (BChar.BarrierHP >= 15)
            {
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BarrierHP >= 15 && SkillD.Master == BChar)
            {
                //Skill cloneSkill = MySkill.CloneSkill(true, null, null, true);
                BattleSystem.DelayInputAfter(AdditionalAttack(Targets[0]));
            }
        }

        public IEnumerator AdditionalAttack(BattleChar Target)
        {
            yield return null;

            Skill skill = Skill.TempSkill(MySkill.MySkill.KeyID, BChar, BChar.MyTeam);

            if (BChar != null && !BChar.Dummy && !BChar.IsDead)
            {
                if (!Target.IsDead)
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, Target, false, false, true, null));

                }
                else if (BattleSystem.instance.EnemyList.Count > 0)
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, BChar.BattleInfo.EnemyList.Random(BChar.GetRandomClass().Main), false, false, true, null));
                }
            }
            yield break;
        }
    }
}