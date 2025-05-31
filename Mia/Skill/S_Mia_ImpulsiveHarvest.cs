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
namespace Mia
{
	/// <summary>
	/// Impulsive Harvest
	/// </summary>
    public class S_Mia_ImpulsiveHarvest : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            new List<Skill>();
            List<GDESkillData> list = new List<GDESkillData>();

            foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST)
            {
                if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_PublicSkill && !gdeskillData.Disposable && (gdeskillData.Effect_Target.DMG_Base >= 1 || gdeskillData.Effect_Target.DMG_Per >= 1))
                {
                    list.Add(gdeskillData);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                GDESkillData gdeskillData2 = list.Random(BChar.GetRandomClass().Main);
                list.Remove(gdeskillData2);
                Skill skill = Skill.TempSkill(gdeskillData2.Key, BChar, BattleSystem.instance.AllyTeam).CloneSkill(false, null, null, false);
                skill.isExcept = true;
                skill.AutoDelete = 2;
                BattleSystem.instance.EffectDelaysAfter.Enqueue(AddSkill(skill));
            }
        }
        public IEnumerator AddSkill(Skill skill)
        {
            if (BattleSystem.instance.AllyTeam.Skills.Count < 10)
            {
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }
            yield break;
        }

        public override void IlyaWaste()
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.DelayInput(Waste());
        }

        public IEnumerator Waste()
        {
            var miaSkills = BChar.MyTeam.Skills_UsedDeck
                .Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Mia)
                .ToList();

            if (miaSkills.Count > 0)
            {
                foreach (var skill in miaSkills)
                {
                    BChar.MyTeam.Skills_UsedDeck.Remove(skill);
                    BChar.MyTeam.Skills_Deck.Add(skill);
                }

                BChar.MyTeam.ShuffleDeck();
                BChar.MyTeam.Draw();
            }

            yield return null;
        }
    }
}