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
using static System.Windows.Forms.LinkLabel;
using NLog.Targets;
using static System.Net.Mime.MediaTypeNames;
namespace Urunhilda
{
    /// <summary>
    /// Urunhilda
    /// Passive:
    /// </summary>
    public class P_Urunhilda : Passive_Char, IP_LevelUp, IP_Targeted, IP_BattleStart_Ones, IP_PlayerTurn, IP_BattleEndRewardChange
    {
        public bool BeastkinInstinct;
        public bool GentleViolence;
        public bool RuttingInstinct;

        public int RuttingStacks;

        public void BattleEndRewardChange()
        {
            int stack = (int)(PlayData.Gold * 0.1f);
            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, stack));
        }

        public void BattleStart(BattleSystem Ins)
        {
            RuttingStacks = 0;
            BeastkinInstinct = false;
            GentleViolence = false;
            RuttingInstinct = false;
        }

        public override void Init()
        {
            OnePassive = true;
        }

        public void LevelUp()
        {
            FieldSystem.DelayInput(Reward());
        }

        public IEnumerator Reward()
        {
            Utils.UrunhildaReward(1);
            Utils.IncreaseArkPassiveNum(1);

            yield return null;
            yield break;
        }

        public void Targeted(Skill SkillD, List<BattleChar> Targets)
        {
            string urunhilda = ModItemKeys.Character_Urunhilda;

            var maleTargets = BattleSystem.instance.AllyTeam.AliveChars
                .Where(x => x != null && x.Info.KeyData != urunhilda && new GDECharacterData(x.Info.KeyData).Gender == 0).ToList();

            if (Targets.Contains(BChar) && maleTargets.Count > 0 && SkillD.IsDamage && SkillD.Master.Info.KeyData != urunhilda)
            {
                var randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, maleTargets.Count);
                var randomMale = maleTargets[randomIndex];

                Targets.Clear();
                Targets.Add(randomMale);
                string text = ModLocalization.AllyTakesDamage;
                var position = randomMale.GetTopPos();
                BattleSystem.DelayInput(BattleText.InstBattleTextAlly_Co(position, text));

                randomMale.Heal(BattleSystem.instance.DummyChar, 3, false, true, null);

                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Urunhilda_DummyHeal, BChar, BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                randomMale.ParticleOut(healingParticle, randomMale);
            }
        }

        public void Turn()
        {
            string beastkinInstinct = ModItemKeys.Buff_B_Urunhilda_BeastkinInstinct;
            string gentleViolence = ModItemKeys.Buff_B_Urunhilda_GentleViolence;
            string ruttingInstinct = ModItemKeys.Buff_B_Urunhilda_RuttingInstinct;

            if (BeastkinInstinct && BChar.BuffReturn(beastkinInstinct) == null)
            {
                Utils.AddBuff(BChar, beastkinInstinct, 1);
            }
            if (GentleViolence && BChar.BuffReturn(gentleViolence) == null)
            {
                Utils.AddBuff(BChar, gentleViolence, 1);
            }
            if (RuttingInstinct && BChar.BuffReturn(ruttingInstinct) == null)
            {
                for (int i = 0; i < RuttingStacks; i++)
                {
                    Utils.AddBuff(BChar, ruttingInstinct, 1);
                }
            }
        }
    }
}