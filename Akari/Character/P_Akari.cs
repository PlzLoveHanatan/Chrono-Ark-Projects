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
using NLog.Targets;
using System.Diagnostics.Eventing.Reader;
using Spine;
using System.Web.Compilation;
using Steamworks;
using System.Runtime.InteropServices.WindowsRuntime;
namespace Akari
{
    /// <summary>
    /// Passive:
    /// After every three successful attacks with Melee Combat Pages, add a random Ammunition to hand.
    /// </summary>
    public class P_Akari : Passive_Char, IP_PlayerTurn, IP_SkillUse_User, IP_DamageChange_sumoperation, IP_Discard, IP_BattleStart_Ones
    {
        public int currentTurn;
        private int akariAttacksPlayed;

        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void BattleStart(BattleSystem Ins)
        {
            currentTurn = 0;
            akariAttacksPlayed = 0;
        }

        public void Turn()
        {
            var threeFold = ModItemKeys.Buff_B_ThreefoldTenacity;

            if (currentTurn > 3) return;

            if (currentTurn == 3)
            {
                BChar.BuffAdd(threeFold, BChar, false, 0, false, -1, false);
            }

            currentTurn++;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            var threeFold = ModItemKeys.Buff_B_ThreefoldTenacity;
            var ammoSupply = ModItemKeys.Buff_AmmoSupply;

            if (currentTurn >= 3) return; 

            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                BChar.BuffAdd(ammoSupply, BChar, false, 0, false, -1, false);

                akariAttacksPlayed++;

                if (akariAttacksPlayed == 2)
                {
                    BChar.BuffAdd(threeFold, BChar, false, 0, false, -1, false);
                }
            }
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (!Click && !HandFullWaste && Utils.Ammunition.Contains(skill.MySkill.KeyID))
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_TacticalReload, BChar, false, 0, false, -1, false);
            }
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if ((currentTurn >= 3 || akariAttacksPlayed == 3) && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse)
                {
                    PlusDamage += (int)(Damage * 0.15f);
                    akariAttacksPlayed = 0;
                }
            }
        }
    }
}