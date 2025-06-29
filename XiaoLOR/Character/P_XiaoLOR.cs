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
using System.Windows.Markup;
using EmotionSystem;
using Steamworks;
using UnityEngine.Analytics;
using Spine;
using DG.Tweening;
using NLog.Targets;
using ChronoArkMod.ModData.Settings;
using EmotionalSystem;
using static CharacterDocument;
using static UnityEngine.UI.Image;
using static DarkTonic.MasterAudio.MasterAudio;
namespace XiaoLOR
{
    /// <summary>
    /// XiaoLOR
    /// Passive:
    /// </summary>
    public class P_XiaoLOR : Passive_Char, IP_PlayerTurn, IP_EmotionLvUpBefore, IP_Draw, IP_SkillUse_BasicSkill, IP_DamageChange_sumoperation, IP_SkillUse_User, IP_BattleEndRewardChange, IP_BattleStart_Ones
    {
        private bool LastFight;

        public static readonly Dictionary<string, string> SkillsList = new Dictionary<string, string>
        {
            { ModItemKeys.Skill_S_XiaoLORLv1_FrontalAssault, ModItemKeys.Skill_S_XiaoLORLv2_SinglePointStab },
            { ModItemKeys.Skill_S_XiaoLORLv1_HighKick, ModItemKeys.Skill_S_XiaoLORLv2_GaleKick },
            { ModItemKeys.Skill_S_XiaoLORLv1_InnerArdor, ModItemKeys.Skill_S_XiaoLORLv2_AlloutWar },
            { ModItemKeys.Skill_S_XiaoLORLv1_RushDown, ModItemKeys.Skill_S_XiaoLORLv2_FleetEdge },
            { ModItemKeys.Skill_S_XiaoLORLv1_ViolentFlame, ModItemKeys.Skill_S_XiaoLORLv2_JiāoTú },
            { ModItemKeys.Skill_S_XiaoLORLv1_SturdyDefense, ModItemKeys.Skill_S_XiaoLORLv2_IronWall },
            { ModItemKeys.Skill_S_XiaoLORLv1_FlowoftheSword, ModItemKeys.Skill_S_XiaoLORLv2_BìXì },
            { ModItemKeys.Skill_S_XiaoLORLv1_FieryDragonSlash, ModItemKeys.Skill_S_XiaoLORLv2_BìÀn },
            { ModItemKeys.Skill_S_XiaoLORLv1_FieryWaltz, ModItemKeys.Skill_S_XiaoLORLv2_FervidEmotions },
            { ModItemKeys.Skill_S_XiaoLORRareLv1_ChīWěn, ModItemKeys.Skill_S_XiaoLORRareLv2_JīnNí },
            { ModItemKeys.Skill_S_XiaoLORRareLv1_FlamingDragonFist, ModItemKeys.Skill_S_XiaoLORRareLv2_Tiěshānkào },
        };

        public override void Init()
        {
            OnePassive = true;
        }
        public override void FixedUpdate()
        {
            PlusStat.DeadImmune = MyChar.LV * 5;
        }
        public void BattleStart(BattleSystem Ins)
        {
            if (PlayData.BattleQueue == GDEItemKeys.EnemyQueue_LastBoss_MasterBattle_1)
            {
                LastFight = true;
                XiaoUtils.XiaoSongStart();
            }
        }

        public void Turn()
        {
            if (BChar.EmotionLevel() >= 2)
            {
                foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
                {
                    Utils.ApplyBurn(battleEnemy, BChar, 2);

                    if (BChar.EmotionLevel() >= 2)
                    {
                        Utils.ApplyBurn(battleEnemy, BChar);
                    }
                }
            }
        }
        public void BattleEndRewardChange()
        {
            if (XiaoUtils.IronLotusSong || XiaoUtils.IronLotusSongKeyIngredient)
                BattleSystem.DelayInputAfter(XiaoUtils.XiaoSongEnd());
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.EmotionLevel() >= 1 && SkillD.IsDamage && SkillD.Master == BChar)
            {
                foreach (var target in Targets)
                {
                    if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                    {
                        Utils.ApplyBurn(target, BChar);
                    }

                    if (BChar.EmotionLevel() >= 5)
                    {
                        Utils.ApplyBurn(target, BChar);
                    }
                }
            }
        }
        public void SkillUseBasicSkill(Skill skill)
        {
            if (skill.Master == BChar)
            {
                IEnumerator FixedSkillFix()
                {
                    yield return new WaitForEndOfFrame();
                    (BChar as BattleAlly)?.MyBasicSkill.SkillInput(BChar.BattleBasicskillRefill);
                    yield break;
                }
                BattleSystem.instance.StartCoroutine(FixedSkillFix());
            }
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (BChar.EmotionLevel() >= 3 && SkillD.IsDamage && SkillD.Master == BChar && Target != null && !Target.Info.Ally && !Target.Dummy && !Target.IsDead)
            {
                PlusDamage += (int)(Damage * 0.15f);
            }
            else
            {
                PlusDamage = 0;
            }
        }

        // Note: this is before level up
        public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
        {
            if (charEmotion.BChar == BChar && charEmotion.Level + 1 == 4 && !LastFight)
                XiaoUtils.XiaoSongStart();
            
            if (charEmotion.BChar == BChar && charEmotion.Level + 1 == 4)
            {
                MasterAudio.PlaySound("Roar", 100f, null, 0f, null, null, false, false);
                BChar.BuffAdd(ModItemKeys.Buff_B_XiaoLOR_FuriousFireRendingtheSkies, BChar, false, 0, false, -1, false);
            }

            if (charEmotion.BChar == BChar && charEmotion.Level + 1 == 2)
            {
                var egoKeys = new[]
                {
                    ModItemKeys.Skill_S_XiaoLOREGO_PúLáo,
                    ModItemKeys.Skill_S_XiaoLOREGO_YáZì,
                    ModItemKeys.Skill_S_XiaoLOREGO_TāoTiè,
                    ModItemKeys.Skill_S_XiaoLORUnique_FormingStorm
                };

                foreach (var key in egoKeys)
                {
                    var skill = Skill.TempSkill(key, BChar);
                    if (skill != null)
                    {
                        EGO_System.instance?.AddEGOSkill(skill);
                        Utils.UnlockSkillPreview(key);
                    }
                }

                MasterAudio.PlaySound("RoarEmotionalLevel", 100f, null, 0f, null, null, false, false);
                BattleSystem.DelayInput(ChangeFix());
            }
        }
        public IEnumerator ChangeFix()
        {
            if (EGO_System.instance != null && EGO_System.instance.egoActive)
            {
                EGO_System.instance.SwitchToNormal();
            }

            var team = BattleSystem.instance.AllyTeam;

            var fixedSkill = (BChar as BattleAlly)?.MyBasicSkill?.buttonData;

            if (fixedSkill?.MySkill != null && P_XiaoLOR.SkillsList.TryGetValue(fixedSkill.MySkill.KeyID, out var newSkillID2))
            {
                var newSkill = Skill.TempSkill(newSkillID2, fixedSkill.Master, fixedSkill.Master.MyTeam);
                Skill refillSkill = newSkill.CloneSkill();
                (BChar as BattleAlly).MyBasicSkill.SkillInput(refillSkill);
                (BChar as BattleAlly).BattleBasicskillRefill = refillSkill;
                (BChar as BattleAlly).BasicSkill = refillSkill;
                int ind = BChar.MyTeam.Chars.IndexOf(BChar);
                if (ind >= 0)
                {
                    BChar.MyTeam.Skills_Basic[ind] = refillSkill;
                }
            }

            var allSkillsToChange = team.Skills
                .Concat(team.Skills_Deck)
                .Concat(team.Skills_UsedDeck)
                .Where(skill => skill?.MySkill != null && P_XiaoLOR.SkillsList.ContainsKey(skill.MySkill.KeyID))
                .ToList();

            foreach (Skill skill in allSkillsToChange)
            {
                if (P_XiaoLOR.SkillsList.TryGetValue(skill.MySkill.KeyID, out var newSkillID))
                {
                    var newSkill = Skill.TempSkill(newSkillID, skill.Master, skill.Master.MyTeam);
                    skill.SkillChange(newSkill);
                }
            }

            yield break;
        }
        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            if (Drawskill?.MySkill == null || BChar.EmotionLevel() < 2) yield break;

            if (P_XiaoLOR.SkillsList.TryGetValue(Drawskill.MySkill.KeyID, out var newSkillID))
            {
                var newSkill = Skill.TempSkill(newSkillID, Drawskill.Master, Drawskill.Master.MyTeam);
                Drawskill.SkillChange(newSkill);
            }

            yield break;
        }       
    }
}