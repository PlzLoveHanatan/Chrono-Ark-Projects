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
using System.Security.Cryptography;
namespace Darkness
{
    /// <summary>
    /// Hurt Me More, Please ♡
    /// </summary>
    public class B_Darkness_HurtMeMorePlease : Buff, IP_TargetedAlly
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)Misc.PerToNum(this.BChar.GetStat.atk, (float)new GDESkillData(ModItemKeys.Skill_S_Darkness_HerosParry_0).Effect_Target.DMG_Per)).ToString());
        }

        public override void Init()
        {
            base.Init();
            this.PlusStat.Strength = true;
        }

        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            using (List<BattleChar>.Enumerator enumerator2 = SaveTargets.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    if (enumerator2.Current.Info.Ally)
                    {
                        Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_HerosParry_0, base.Usestate_L, base.Usestate_L.MyTeam);
                        skill2.PlusHit = true;
                        skill2.FreeUse = true;
                        this.BChar.ParticleOut(skill2, Attacker);
                        EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
                        yield return new WaitForSeconds(0.2f);
                        break;
                    }
                }
            }
            List<BattleChar>.Enumerator enumerator = default(List<BattleChar>.Enumerator);
            yield break;
        }
    }
}
