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
using static CharacterDocument;
namespace ImaSuguRinne
{
    /// <summary>
    /// Blooming Petal ✿
    /// </summary>
    public class E_Rinne_BloomingPetal : EquipBase, IP_Draw, IP_CriPerChange, IP_BattleStart_Ones
    {
        public override void Init()
        {
            PlusStat.hit = 20;
            PlusPerStat.Damage = 20;
            PlusPerStat.Heal = 20;
            PlusStat.HitMaximum = true;
        }

        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            if (Drawskill.Master == BChar && Drawskill.ExtendedFind_DataName(ModItemKeys.SkillExtended_S_Ex_E_BloomingPetal) != null)
            {
                Utils.GlitchEffect(Drawskill, 1);
            }
            yield break;
        }

        public void BattleStart(BattleSystem Ins)
        {
            foreach (Skill skill in Ins.AllyTeam.Skills_Deck)
            {
                if (skill.Master == BChar && !Utils.RinnOnceSkills.Contains(skill.MySkill.KeyID))
                {
                    skill.ExtendedAdd_Battle(ModItemKeys.SkillExtended_S_Ex_E_BloomingPetal);
                }
            }
        }

        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if (Target.NullCheck())
            {
                return;
            }
            int num = Target.HitPerNum(skill.Master, skill, false);
            int num2 = 0;
            if (num > 100)
            {
                num2 = num - 100;
            }
            if (num2 > 0)
            {
                CriPer += (float)num2;
            }
        }
    }
}