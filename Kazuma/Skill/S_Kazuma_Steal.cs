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
using ChronoArkMod.InUnity.Dialogue;
namespace Kazuma
{
    /// <summary>
    /// Steal
    /// </summary>
    public class S_Kazuma_Steal : Skill_Extended
    {
        private static readonly Dictionary<string, string> PantiesDict = new Dictionary<string, string>()
        {
            { ModItemKeys.Skill_S_Panties_01_Standart, ModItemKeys.Item_Consume_Item_Kazuma_Panties_01_Standart },
        };


        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];

            if (target is BattleEnemy enemy && Utils.FemaleEnemy.Contains(enemy.Info.KeyData) ||
                target is BattleAlly ally && new GDECharacterData(ally.Info.KeyData).Gender == 1)
            {
                PantiesDrop();
            }
        }

        public void PantiesDrop()
        {
            string randomSkillKey = PantiesDict.Keys.ElementAt(RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, PantiesDict.Count));
            Skill randomPanties = Skill.TempSkill(randomSkillKey, MySkill.Master, MySkill.Master.MyTeam);

            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(
                new List<Skill> { randomPanties },
                new SkillButton.SkillClickDel(Selection),
                ScriptLocalization.System_SkillSelect.EffectSelect,
                false, false, true, false, false
            ));

            Debug.Log($"[Kazuma] Stole panties: {randomSkillKey}");
        }


        public void Selection(SkillButton Mybutton)
        {
            BattleSystem.DelayInput(RandomPantiesSelect(Mybutton));
        }
        private IEnumerator RandomPantiesSelect(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;
            
            if (PantiesDict.TryGetValue(key, out var rewardAction))
            {
                InventoryManager.Reward(ItemBase.GetItem(rewardAction, 1));
                Debug.Log($"[Kazuma] Applied reward for {key}");
            }
            else
            {
                Debug.LogWarning($"[Kazuma] No reward mapped for {key}");
            }
            yield break;
        }
    }
}