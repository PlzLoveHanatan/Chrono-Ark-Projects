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
using Steamworks;
namespace EmotionalSystem
{
    public class B_Abnormality_TechnologicalLv1_Request : Buff, IP_SkillUse_Target, IP_PlayerTurn, IP_Awake
    {
        private bool FirstAttack;
        
        public void Awake()
        {
            FirstAttack = true;
            BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_2, this.BChar, false, 0, false, -1, false);
        }
        public void Turn()
        {
            FirstAttack = true;
            BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_2, this.BChar, false, 0, false, -1, false);
        }
        public override void BuffStat()
        {
            PlusStat.IgnoreTaunt = true;
            PlusStat.cri = 10;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            var request = ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_0;

            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit && !SP.SkillData.FreeUse
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {

                if (FirstAttack && SP.SkillData.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
                {
                    var targets = BattleSystem.instance.EnemyList.Where(t => t != null).ToList();
                    int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, targets.Count);
                    var randomTarget = targets[index];

                    randomTarget.BuffAdd(request, this.BChar, false, 0, false, -1, false);

                    if (randomTarget.IsDead)
                    {
                        BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_1, this.BChar, false, 0, false, -1, false);
                        SelfDestroy();
                    }

                    FirstAttack = false;
                }
                else if (FirstAttack && SP.SkillData.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
                {
                    hit.BuffAdd(request, this.BChar, false, 0, false, -1, false);

                    if (hit.IsDead)
                    {
                        BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_1, this.BChar, false, 0, false, -1, false);
                        SelfDestroy();
                    }
                }
                else if (hit.BuffReturn(request, false) != null && hit.IsDead)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_1, this.BChar, false, 0, false, -1, false);
                    SelfDestroy();
                }
            }
        }
    }
}