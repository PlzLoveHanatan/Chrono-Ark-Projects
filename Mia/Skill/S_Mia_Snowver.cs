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
namespace Mia
{
    /// <summary>
    /// Snowver Paw-er!
    /// </summary>
    public class S_Mia_Snowver : Skill_Extended
    {
        private Skill SkillSave;

        private List<Skill_Extended> MiaExList = new List<Skill_Extended>
        {
            Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Mia_HuntersInstinct),
            Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Mia_PersistentHunt),
            Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Mia_InstinctSurge),
            Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_SkillEn_Ilya_0),
            Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_SkillEn_Ilya_1),
            Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_SkillEn_ExchangeMana),
        };

        private static readonly Dictionary<string, List<string>> MiaBannedExlist = new Dictionary<string, List<string>>
        {
            {
                ModItemKeys.SkillExtended_Ex_Mia_InstinctSurge,
                new List<string>
                {
                    GDEItemKeys.Skill_S_Ilya_0,
                    GDEItemKeys.Skill_S_Ilya_9_Rare,
                    GDEItemKeys.Skill_S_Ilya_8_Rare,
                    GDEItemKeys.Skill_S_Ilya_5
                }
            },
            {
                ModItemKeys.SkillExtended_Ex_Mia_HuntersInstinct,
                new List<string>
                {
                    GDEItemKeys.Skill_S_Ilya_0,
                    GDEItemKeys.Skill_S_Ilya_9_Rare,
                    GDEItemKeys.Skill_S_Ilya_8_Rare
                }
            },
            {
                ModItemKeys.SkillExtended_Ex_Mia_PersistentHunt,
                new List<string>
                {
                    GDEItemKeys.Skill_S_Ilya_0,
                    GDEItemKeys.Skill_S_Ilya_9_Rare,
                    GDEItemKeys.Skill_S_Ilya_8_Rare,
                    GDEItemKeys.Skill_S_Ilya_2,
                    GDEItemKeys.Skill_S_Public_21,
                    GDEItemKeys.Skill_S_Mement_6,
                    GDEItemKeys.Skill_S_Mement_R1
                }
            }
        };



        public override bool SkillTargetSelectExcept(Skill ExceptSkill)
        {
            return /*ExceptSkill.Master.IsLucyNoC ||*/ ExceptSkill.Enforce || ExceptSkill.Master.Info.KeyData == ModItemKeys.Character_Mia;
        }

        public override void SkillTargetSingle(List<Skill> Targets)
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            SkillSave = Targets[0];
            List<Skill> selectionList = new List<Skill>();

            foreach (var pair in MiaBannedExlist)
            {
                string exKey = pair.Key;
                List<string> bannedSkills = pair.Value;

                if (bannedSkills.Contains(SkillSave.MySkill.KeyID))
                {
                    MiaExList.RemoveAll(ex => ex.Data.Key == exKey);
                }
            }

            List<Skill_Extended> randomSelectionList = MiaExList.Random(BChar.GetRandomClass().Main, 2);

            foreach (var extended in randomSelectionList)
            {
                Skill clone = SkillSave.CloneSkill(true, null, null, false);
                clone.ExtendedAdd(extended);
                selectionList.Add(clone);
            }

            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(
                selectionList,
                SkillSelect,
                ScriptLocalization.UI_Enforce.SelectEnforce,
                false, true, true, false, true));
        }



        public void SkillSelect(SkillButton Mybutton)
        {
            Skill_Extended extended = null;
            using (List<Skill_Extended>.Enumerator enumerator = Mybutton.Myskill.AllExtendeds.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Skill_Extended i = enumerator.Current;
                    if (i.Data != null)
                    {
                        i.ExtendedName();
                        if (MiaExList.Find((Skill_Extended a) => a.ExtendedName() == i.ExtendedName()) != null)
                        {
                            extended = Skill_Extended.DataToExtended(i.Data.Key);
                        }
                    }
                }
            }
            this.SkillSave.ExtendedAdd_Battle(extended);
            this.SkillSave.MyButton.InputData(this.SkillSave, null, false);
            BattleSystem.instance.AllyTeam.Draw();
        }
    }
}