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
namespace Raphi
{
	/// <summary>
	/// Angelic Gamble
	/// When played from hand, if you have another <color=#FF69B4>Angelic Gamble</color> in hand, discard it, cast this skill on the target, and draw 1 skill (Max 3).
	/// If 1 Angelic Gambles are discarded, gain <color=#D2691E>Heaven's Touch</color>.
	/// If 2 Angelic Gambles are discarded, gain <color=#7B68EE>Celestial Connection</color>.
	/// Sheathe: Shuffle a random <color=#FF69B4>Angelic Gamble</color> from the discard pile back into your deck.
	/// </summary>
    public class AngelicGamble : SkillExtedned_IlyaP
    {
        private int useCount = 0;
        private int DiscardCount = 0;
        private BattleChar TargetTemp;
        public override void IlyaWaste()
        {
            BattleSystem.DelayInput(Waste());
        }
        public IEnumerator Waste()
        {
            Skill targetSkill = BChar.MyTeam.Skills_UsedDeck.FirstOrDefault(skill => skill.MySkill.KeyID == ModItemKeys.Skill_AngelicGamble);

            if (targetSkill != null)
            {
                BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(targetSkill);
                BattleSystem.instance.AllyTeam.Skills_Deck.Add(targetSkill);
            }

            BattleSystem.instance.AllyTeam.Draw();

            yield return null;
            yield break;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse || MySkill.BasicSkill) return;

            if (Targets.Count > 0)
            {
                TargetTemp = Targets[0];
            }

            var AngelicGamble = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill.MySkill.KeyID == ModItemKeys.Skill_AngelicGamble
            && skill != MySkill).ToList();

            if (AngelicGamble.Count > 0)
            {
                BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(AngelicGamble, new SkillButton.SkillClickDel(Del), ScriptLocalization.System_SkillSelect.WasteSkill));
            }
        }

        public void Del(SkillButton skillbutton)
        {
            if (useCount >= 3) return;
            Skill selectedSkill = skillbutton.Myskill;
            BattleSystem.DelayInput(Except(selectedSkill));
        }

        public IEnumerator Except(Skill selectedSkill)
        {
            yield return BattleSystem.instance.StartCoroutine(
                BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));

            if (selectedSkill == null || selectedSkill.MySkill.KeyID != ModItemKeys.Skill_AngelicGamble) yield break;

            if (useCount >= 3) yield break;

            selectedSkill.Delete(false);
            //BattleSystem.instance.AllyTeam.Draw();

            useCount++;
            DiscardCount++;

            Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_AngelicGamble, BChar, BChar.MyTeam);
            newSkill.PlusHit = true;
            newSkill.FreeUse = true;

            if (DiscardCount == 1)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_HeavensTouch, BChar, false, 0, false, -1, false);
            }
            if (DiscardCount == 2)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection, BChar, false, 0, false, -1, false);
            }

            BattleChar target;

            if (TargetTemp.IsDead)
            {
                target = BChar.BattleInfo.EnemyList.Random(BChar.GetRandomClass().Main);
            }
            else
            {
                target = TargetTemp;
            }

            BChar.ParticleOut(MySkill, newSkill, target);

            if (useCount < 3)
            {
                var AngelicGamble = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill
                && skill.MySkill.KeyID == ModItemKeys.Skill_AngelicGamble).ToList();

                if (AngelicGamble.Count > 0)
                {
                    BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(AngelicGamble, new SkillButton.SkillClickDel(Del), ScriptLocalization.System_SkillSelect.WasteSkill));
                }
            }
        }
    }
}