using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace Xao
{
    public class Xao_Combo_Rewards
    {
        public static bool AttackPowerOncePerFight;
        public static bool RelicPouchOncePerFight;
        public static bool Legendary_OncePerFight;

        public static readonly Dictionary<int, Action> СomboRewards = new Dictionary<int, Action>
        {
            { 4, () => { GainAffection(); } },
            { 6, () => { RestoreMana(); } },
            { 8, () => { DrawSkill(); } },
            { 10, () => { ReduceSkillsCost(); } },
            { 15, () => { IncreaseXaoAttackPower(); } },
            { 25, () => { GainRelicPouch(); } },
            { 50, () => { GainLegendaryEquip(); } },
        };

        public static void GainRewards(int currentCombo, bool isAdditionalReward = false)
        {
            if (currentCombo >= 4)
            {
                GainAffection();
            }

            if (currentCombo >= 6)
            {
                RestoreMana();
            }

            if (currentCombo >= 8)
            {
                DrawSkill();
            }

            if (currentCombo >= 10)
            {
                ReduceSkillsCost();
            }

            if (currentCombo >= 15)
            {
                IncreaseXaoAttackPower();
            }

            if (currentCombo >= 25)
            {
                GainRelicPouch(isAdditionalReward);
            }

            if (currentCombo >= 50)
            {
                GainLegendaryEquip(isAdditionalReward);
            }
        }

        private static void GainAffection()
        {
            Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection);
        }

        private static void RestoreMana()
        {
            Utils.AllyTeam.AP += 1;
        }

        private static void DrawSkill()
        {
            Utils.AllyTeam.Draw();
        }

        private static void ReduceSkillsCost()
        {
            foreach (var skill in Utils.AllyTeam.Skills)
            {
                Extended_Lucy_3_1 extended = new Extended_Lucy_3_1();
                skill?.ExtendedAdd(extended);
            }
        }

        private static void IncreaseXaoAttackPower()
        {
            if (!AttackPowerOncePerFight)
            {
                Utils.Xao.Info.OriginStat.atk++;
                AttackPowerOncePerFight = true;
            }
        }

        private static void GainRelicPouch(bool isAdditionalReward = false)
        {
            if (!RelicPouchOncePerFight || isAdditionalReward)
            {
                IncreaseArkPassiveNum();
                GainReward(GDEItemKeys.Item_Consume_ArtifactPouch);

                if (!isAdditionalReward)
                {
                    RelicPouchOncePerFight = true;
                }
            }
        }

        private static void GainLegendaryEquip(bool isAdditionalReward = false)
        {
            if (!Legendary_OncePerFight || isAdditionalReward)
            {
                var list = new List<ItemBase>();

                for (int i = 0; i < 3; i++)
                {
                    var key = PlayData.GetEquipRandom(4, false, new List<string>());
                    list.Add(ItemBase.GetItem(key));
                }
                UIManager.InstantiateActive(UIManager.inst.SelectItemUI).GetComponent<SelectItemUI>().Init(list, new RandomItemBtn.SelectItemClickDel(AddToSave));

                if (!isAdditionalReward)
                {
                    Legendary_OncePerFight = true;
                }
            } 
        }

        private static void AddToSave(ItemBase item)
        {
            PlayData.TSavedata.EquipList_Legendary.Add(item.itemkey);
            InventoryManager.Reward(item);
        }

        //private static void IncreaseXaoAttackPower(bool isAdditionalReward = false)
        //{
        //    if (!AttackPowerOncePerFight || isAdditionalReward)
        //    {
        //        Utils.Xao.Info.OriginStat.atk++;

        //        if (!isAdditionalReward)
        //        {
        //            AttackPowerOncePerFight = true;
        //        }
        //    }
        //}

        private static void IncreaseArkPassiveNum(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                PlayData.TSavedata.Passive_Itembase.Add(null);
            }
            PlayData.TSavedata.ArkPassivePlus += num;

            if (UIManager.NowActiveUI is ArkPartsUI)
            {
                UIManager.NowActiveUI.Delete();
            }
        }

        private static void GainReward(string key)
        {
            InventoryManager.Reward(ItemBase.GetItem(key, 1));
        }
    }
}