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
namespace SuperHero
{
    public class B_SuperHero_Test : Buff, IP_BuffAddAfter
    {
        private List<Skill> DynamicList = new List<Skill>(); // your Dynamic List

        private readonly List<string> DummyList = new List<string> // your skills
        {
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1,
        };

        public override void BuffOneAwake() // Awake buff on Click
        {
            base.BuffOneAwake();
            var button = BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(YourCall);
        }

        public void YourCall()
        {
            if (!BattleSystem.instance.ActWindow.CanAnyMove || BChar.GetStat.Stun) return; // Prevent Activate Buff effect if User is stunned or enemy action + coroutine

            BattleSystem.DelayInput(FunnyAction()); // start corotuine
        }

        private IEnumerator FunnyAction()
        {
            yield return new WaitForFixedUpdate();

            DynamicList.Clear();

            // your buffs

            var yourFirstBuff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var yourSecondBuff = ModItemKeys.Buff_B_SuperHero_HeroComplex_0;
            var yourThirdBuff = ModItemKeys.Buff_B_SuperHero_GloryofJustice;

            foreach (var key in DummyList) // fill the Dynamic List from DummyList
            {
                var skill = Skill.TempSkill(key, BChar, BChar.MyTeam);
                if (skill == null || skill.MySkill == null) continue;
                DynamicList.Add(skill);
            }


            // Check conditions to remove skills
            // Can add skills instead of removing by using DynamicList.Add(key);
            if (BChar.BuffReturn(yourFirstBuff, false) != null)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_IntheNameofJustice);
            }

            if (BChar.BuffReturn(yourSecondBuff, false) != null)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0);
                
            }

            if (BChar.BuffReturn(yourThirdBuff, false) != null)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1);
            }

            Debug.Log($"DynamicList have {DynamicList.Count} skills, start coroutine");
            if (DynamicList.Count == 0) yield break; // safety check

            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(DynamicList,
                    SkillButton,ScriptLocalization.System_SkillSelect.EffectSelect,true, false
                ) // start new coroutine
            );
        }


        public void SkillButton(SkillButton myButton)
        {
            if (myButton == null || myButton.Myskill == null || myButton.Myskill.MySkill == null) return;

            var yourFirstSkill = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice;
            var yourSecondSkill = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0;
            var yourThirdSkill = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1;

            var key = myButton.Myskill.MySkill.KeyID; // skill preview

            if (key == yourFirstSkill) // if you click on 'skill preview' and it matches the key -> do something { }
            {
                BattleSystem.instance.AllyTeam.AP = 10;
            }
            else if (key == yourSecondSkill)
            {
                BattleSystem.instance.AllyTeam.Draw(10);
            }
            else if (key == yourThirdSkill)
            {
                SelfDestroy();
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var yourChar = ModItemKeys.Character_SuperHero;

            if (BuffTaker.Info.KeyData != yourChar) // prevents other characters to have this unique buff
            {
                SelfDestroy();
            }
        }

        //public void Awake()
        //{
        //    if (BChar.Info.Passive is P_SuperHero anyName) // storage the bool
        //    {
        //        anyName.MyUniqueBuffWasDestroyed = true;
        //    }
        //}
    }
}