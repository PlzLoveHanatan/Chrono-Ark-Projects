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
namespace Raphi
{
	/// <summary>
	/// Blessed Descent
	/// Cost is reduced by 1 if this skill is a fixed ability.
	/// When played from hand, discard the bottom skill and restore Mana equal to its cost (Max 2).
	/// Create "Blessed Ascension" in hand.  
	/// Sheathe: Restore 1 mana and draw 1 skill, prioritizing Healing skills.
	/// </summary>
    public class BlessedDescent : SkillExtedned_IlyaP
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;

            if (!SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "SummerRaphiel")) return;
            {
                GDESkillData gdeskillData = new GDESkillData(MySkill.MySkill.KeyID);

                MySkill.Init(gdeskillData, BChar, BChar.MyTeam);

                MySkill.MySkill.PlusSkillView = ModItemKeys.Skill_BlessedAscensionSummer;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "SummerRaphiel"))
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_BlessedAscensionSummer, BChar, BChar.MyTeam);
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }
            else
            {
                Skill skill1 = Skill.TempSkill(ModItemKeys.Skill_BlessedAscension, BChar, BChar.MyTeam);
                BattleSystem.instance.AllyTeam.Add(skill1, true);
            }

            List<Skill> list = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            if (list.Count == 0) return;

            int num = Math.Min(list[list.Count - 1].AP, 2);

            list[list.Count - 1].Delete(false);
            BattleSystem.instance.AllyTeam.AP += num;
        }
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.AP += 1;
            BattleSystem.DelayInput(Draw());
        }
        public IEnumerator Draw()
        {
            Skill skill2 = BattleSystem.instance.AllyTeam.Skills_Deck.Find((Skill skill) => skill.IsHeal);
            if (skill2 == null)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }
            else
            {
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill2, null));
            }

            yield return null;
            yield break;
        }
    }
}