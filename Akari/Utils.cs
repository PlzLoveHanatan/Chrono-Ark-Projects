using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using UnityEngine;
using Debug = UnityEngine.Debug;
using I2.Loc;

namespace Akari
{
    public static class Utils
    {
        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
        public static BattleChar Akari => AllyTeam.AliveChars.FirstOrDefault(x => x.Info.KeyData == ModItemKeys.Character_Akari);

        public static bool ThumbEquip => ModManager.getModInfo("Akari").GetSetting<ToggleSetting>("Thumb Equip").Value;

        public static bool Equip;

        public static int ClassandRespectScale = 0;


        public static readonly List<string> Ammunition = new List<string>
        {
            ModItemKeys.Skill_Armor_piercingAmmunition,
            ModItemKeys.Skill_FlameAmmunition,
            ModItemKeys.Skill_FrostAmmunition,
        };

        public static readonly List<string> RangeAttacks = new List<string>
        {
            ModItemKeys.Skill_FocusFire,
            ModItemKeys.Skill_ShockRound,
            ModItemKeys.Skill_SummaryJudgment,
            ModItemKeys.Skill_SuppressingShot
        };

        public static void CreateRandomAmmunition(BattleChar user, int stack = 1)
        {
            for (int i = 0; i < stack; i++)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, Ammunition.Count);
                string newAmmunition = Ammunition[index];
                Skill randomAummuniton = Skill.TempSkill(newAmmunition, user, user.MyTeam);

                BattleSystem.instance.AllyTeam.Add(randomAummuniton, true);
            }
        }

        public static void AkariCastingWasteFixed(this BattleActWindow window, CastingSkill cast)
        {
            SkillButton[] componentsInChildren = window.CastingGroup.GetComponentsInChildren<SkillButton>();
            SkillButton skillButton = componentsInChildren.FirstOrDefault(bt => bt.castskill == cast);
            foreach (IP_SkillCastingQuit ip_SkillCastingQuit in cast.skill.IReturn<IP_SkillCastingQuit>())
            {
                if (ip_SkillCastingQuit != null)
                {
                    ip_SkillCastingQuit.SkillCastingQuit(cast);
                }
            }
            if (skillButton != null)
            {
                skillButton.UseWaste();
            }
            window.SetCountSkillVL((window.CastingGroup.GetComponentsInChildren<SkillButton>().Length >= 13) ? 30 : 45);
        }

        public static int DiscardAndApplyAmmunition(BattleChar user, int discardNum, BattleChar target)
        {
            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills.FindAll(skill => Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0)
            {
                return 0;
            }

            int bonusDiscard = 0;
            var TheBossOrders = user.BuffReturn(ModItemKeys.Buff_B_TheBossOrders, false);

            if (TheBossOrders?.StackNum >= 1)
            {
                bonusDiscard = 1;
            }

            int totalDiscard = discardNum + bonusDiscard;

            if (totalDiscard > ammunitionInHand.Count)
            {
                totalDiscard = ammunitionInHand.Count;
            }

            List<Skill> randomAmmunition = RandomManager.Random(ammunitionInHand,user.GetRandomClass().SkillSelect,totalDiscard);

            foreach (var ammunition in randomAmmunition)
            {
                ammunition.Delete(false);
            }

            int FlameCheck = randomAmmunition.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_FlameAmmunition);
            int FrostCheck = randomAmmunition.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_FrostAmmunition);
            int PiercingCheck = randomAmmunition.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_Armor_piercingAmmunition);

            for (int i = 0; i < FlameCheck; i++)
            {
                target.BuffAdd(ModItemKeys.Buff_B_FlameAmmunition, user, false, 0, false, -1, false);
            }

            for (int j = 0; j < FrostCheck; j++)
            {
                target.BuffAdd(ModItemKeys.Buff_B_FrostAmmunition, user, false, 0, false, -1, false);
            }

            for (int k = 0; k < PiercingCheck; k++)
            {
                target.BuffAdd(ModItemKeys.Buff_B_Armor_piercingAmmunition, user, false, 0, false, -1, false);
            }

            return randomAmmunition.Count;
        }

        public static void ChargeMag(BattleChar bchar)
        {
            if (PartyInventory.InvenM.InventoryItems.FirstOrDefault(a => a != null && a.itemkey == ModItemKeys.Item_Active_Standart_Mag) is Item_Active mag)
            {
                mag.ChargeNow++;
            }
            else
            {
                InventoryManager.Reward(ItemBase.GetItem(ModItemKeys.Item_Active_Standart_Mag));
            }

            GameObject gameObject = Misc.UIInst(bchar.BattleInfo.EffectViewOb);
            if (bchar.Info.Ally)
            {
                gameObject.transform.position = bchar.GetPos();
            }
            else
            {
                gameObject.transform.position = bchar.GetTopPos();
            }
            gameObject.GetComponent<EffectView>().TextOut(bchar.Info.Ally, ModLocalization.Reload);
        }
    }
}
