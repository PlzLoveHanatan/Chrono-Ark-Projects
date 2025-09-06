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
    /// Blooming ✿ Dress
    /// </summary>
    public class E_Rinne_BloomingDress : EquipBase, IP_SkillUse_User, IP_BattleStart_Ones
    {
        public override void Init()
        {
            PlusStat.atk = 3;
            PlusStat.reg = 3;
            PlusStat.hit = 10;
        }
        
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && !SkillD.BasicSkill && SkillD.MySkill.UseAp >= 1 && !SkillD.FreeUse)
            {
                BattleSystem.DelayInput(CreateCopy(SkillD));
            }
        }

        public void BattleStart(BattleSystem Ins)
        {
            foreach (Skill skill in Ins.AllyTeam.Skills_Deck)
            {
                if (skill.Master == BChar && skill.MySkill.UseAp >= 1)
                {
                    skill.ExtendedAdd_Battle(ModItemKeys.SkillExtended_S_Ex_E_BloomingDress);
                }
            }
        }

        public IEnumerator CreateCopy(Skill skill)
        {
            yield return null;

            bool swift = skill.MySkill.NotCount;
            int ap = skill.MySkill.UseAp;

            Skill skillCopy = Utils.CreateSkill(BChar, skill.MySkill.KeyID, true, true, 1, ap, swift, true);
            skillCopy?.ExtendedAdd(ModItemKeys.SkillExtended_S_Ex_E_BloomingDress);
        }
    }
}