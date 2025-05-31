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
    /// Burst of Flavor
    /// </summary>
    public class S_Mia_BurstofFlavor : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
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

            for (int i = 0; i < 1; i++)
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
    }
}