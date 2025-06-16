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
namespace Darkness
{
    /// <summary>
    /// Iron Maiden Mode
    /// </summary>
    public class B_Darkness_IronMaidenMode : Buff, IP_DamageTakeChange, IP_PlayerTurn
    {
        private bool RemoveTaunt;
        private bool RemoveTauntOnce;
        private List<Skill> DynamicList = new List<Skill>();

        private readonly List<string> ChoiceSkill = new List<string>
        {
            ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_0,
            ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_1,
            ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_2,
            ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_3,
        };

        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            var button = BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(DarknessCall);
        }

        public void DarknessCall()
        {
            if (!BattleSystem.instance.ActWindow.CanAnyMove) return;
            if (BChar.GetStat.Stun) return;

            BattleSystem.DelayInput(Del());
        }

        private IEnumerator Del()
        {
            yield return new WaitForFixedUpdate();

            DynamicList.Clear();

            foreach (var key in ChoiceSkill)
            {
                var skill = Skill.TempSkill(key, BChar, BChar.MyTeam);
                if (skill == null || skill.MySkill == null) continue;
                DynamicList.Add(skill);
            }

            if (RemoveTaunt)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_1);
            }

            if (!RemoveTaunt)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_2);
            }

            if (RemoveTauntOnce)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_3);
            }

            if (DynamicList.Count == 0) yield break;

            BattleSystem.DelayInput(
                BattleSystem.I_OtherSkillSelect(
                    DynamicList,
                    SkillButton,
                    ScriptLocalization.System_SkillSelect.EffectSelect,
                    true, false
                )
            );
        }


        public void SkillButton(SkillButton myButton)
        {
            if (myButton == null || myButton.Myskill == null || myButton.Myskill.MySkill == null) return;

            string key = myButton.Myskill.MySkill.KeyID;

            if (key == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_0)
            {
                SelfDestroy();
            }
            else if (key == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_1)
            {
                RemoveTaunt = true;
            }
            else if (key == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_2)
            {
                RemoveTaunt = false;
            }
            else if (key == ModItemKeys.Skill_S_Darkness_Rare_UnbreakableWill_3)
            {
                var buff = ModItemKeys.Buff_B_Darkness_BustyTaunt;

                foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    if (e != null && e.BuffReturn(buff, false) != null)
                    {
                        e.BuffRemove(buff);
                    }
                }
                RemoveTauntOnce = true;
            }
        }

        public override void Init()
        {
            base.Init();
            PlusStat.AggroPer = 50;
        }

        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (!Preview && Dmg >= 1)
            {
                Dmg = (int)(Dmg * 0.5f);
            }
            if (Dmg <= 1)
            {
                Dmg = 1;
            }
            return Dmg;
        }

        public void Turn()
        {
            if (!RemoveTaunt) return;

            foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                e.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 999, false, -1, false);
            }
        }
    }
}