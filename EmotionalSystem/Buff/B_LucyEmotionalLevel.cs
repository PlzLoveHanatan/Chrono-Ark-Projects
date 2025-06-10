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
using System.Security.Cryptography;
using UnityEngine.AI;
using Spine;
using static UnityEngine.UI.GridLayoutGroup;
using EmotionSystem;
using EmotionalSystem;
using ChronoArkMod.ModData.Settings;
using static EmotionalSystem.B_LucyEmotionalLevel;
using static EnemyCastingLineV2;

namespace EmotionalSystem
{
    public class B_LucyEmotionalLevel : Buff, IP_PlayerTurn, IP_Awake
    {
        private List<Abnormality> DynamicAbnormalityList = new List<Abnormality>();
        private List<string> DynamicEGOList = new List<string>();

        private bool FirstAwake = false;

        public int LastEmotionalLevel;
        public int NextEGONum;
        public int EmotionalLevel
        {
            get
            {
                int sumLevel = 0;
                int sumCharacter = 0;
                foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    var emotion = ally.MyEmotion();
                    if (emotion != null)
                    {
                        sumCharacter++;
                        sumLevel += emotion.Level;
                    }
                }
                if (sumLevel == 0) return 0;
                return sumLevel / sumCharacter;
            }
        }

        public int AllPosCoinNum
        {
            get
            {
                int num = 0;
                foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    var emotion = ally.MyEmotion();
                    if (emotion != null)
                    {
                        num += emotion.AccumPosCoin;
                    }
                }
                return num;
            }
        }

        public int AllNegCoinNum
        {
            get
            {
                int num = 0;
                foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    var emotion = ally.MyEmotion();
                    if (emotion != null)
                    {
                        num += emotion.AccumNegCoin;
                    }
                }
                return num;
            }
        }

        public void Awake()
        {
            if (!FirstAwake)
            {
                LastEmotionalLevel = 0;
                NextEGONum = 0;
                FirstAwake = true;

                var nowFloorInfo = AllLibraryFloors.NowFloorInfo;

                DynamicAbnormalityList.Clear();
                DynamicAbnormalityList.AddRange(nowFloorInfo.Abnomalities);

                DynamicEGOList.Clear();
                DynamicEGOList.AddRange(nowFloorInfo.EGOs);
            }
        }

        public override void Init()
        {
            OnePassive = true;
        }


        public void Turn()
        {
            EGO_System.instance?.TurnUpdate();

            if (EmotionalLevel > LastEmotionalLevel)
            {
                //int levelsGained = EmotionalLevel - LastEmotionalLevel;
                for (int i = LastEmotionalLevel + 1; i <= EmotionalLevel; i++)
                {
                    BattleSystem.DelayInputAfter(LucyEmotionLevelUp(i));

                    if (i >= 3)
                    {
                        BattleSystem.DelayInputAfter(SelectEGO(i));
                    }
                }

                LastEmotionalLevel = EmotionalLevel;
            }
        }

        private IEnumerator SelectEGO(int level)
        {
            List<Skill> list = new List<Skill>();

            var EGO = DynamicEGOList;

            var availableEGO = EGO.Random(BChar.GetRandomClass().SkillSelect, 2);
            if (availableEGO.Count == 0) yield break;

            list.AddRange(availableEGO.Select(x => Skill.TempSkill(x, BChar, BChar.MyTeam)));

            yield return BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(AddEGO),
                ModLocalization.EGOSelect, false, true, true, false, true);
            yield break;
        }

        private IEnumerator LucyEmotionLevelUp(int level)
        {
            // when you level up, add a new abno skill
            // this abno skill is based your next level
            List<Abnormality> selectionList = new List<Abnormality>();
            switch (level)
            {
                case 1:
                    if (AllPosCoinNum > AllNegCoinNum)
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);
                        //var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedPosAbnoLv1);
                        //selectionList.AddRange(selectedNegAbnoLv1);
                    }
                    else if (AllPosCoinNum < AllNegCoinNum)
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 3);
                        //var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedNegAbnoLv1);
                        //selectionList.AddRange(selectedPosAbnoLv1);
                    }
                    else
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var selectedRandomAbno = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

                        selectionList.AddRange(selectedRandomAbno);
                    }
                    break;

                case 2:
                    if (AllPosCoinNum > AllNegCoinNum)
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 2);
                        var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedPosAbnoLv1);
                        selectionList.AddRange(selectedNegAbnoLv1);
                    }
                    else if (AllPosCoinNum < AllNegCoinNum)
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 2);
                        var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedNegAbnoLv1);
                        selectionList.AddRange(selectedPosAbnoLv1);
                    }
                    else
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var selectedRandomAbno = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

                        selectionList.AddRange(selectedRandomAbno);
                    }
                    break;
                case 3:
                    if (AllPosCoinNum > AllNegCoinNum)
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
                        var selectedPosAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 2);
                        var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedPosAbnoLv2);
                        selectionList.AddRange(selectedPosAbnoLv1);
                    }
                    else if (AllPosCoinNum < AllNegCoinNum)
                    {
                        var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
                        var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
                        var selectedNegAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 2);
                        var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedNegAbnoLv2);
                        selectionList.AddRange(selectedNegAbnoLv1);
                    }
                    else
                    {
                        var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2 || abno.Level == 1);
                        var selectedRandomAbno = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

                        selectionList.AddRange(selectedRandomAbno);
                    }
                    break;
                case 4:
                    if (AllPosCoinNum > AllNegCoinNum)
                    {
                        var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
                        var selectedPosAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 2);
                        var selectedNegAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedPosAbnoLv2);
                        selectionList.AddRange(selectedNegAbnoLv2);
                    }
                    else if (AllPosCoinNum < AllNegCoinNum)
                    {
                        var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
                        var selectedNegAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 2);
                        var selectedPosAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

                        selectionList.AddRange(selectedNegAbnoLv2);
                        selectionList.AddRange(selectedPosAbnoLv2);
                    }
                    else
                    {
                        var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2 || abno.Level == 1);
                        var selectedRandomAbno = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

                        selectionList.AddRange(selectedRandomAbno);
                    }
                    break;
                case 5:
                    var availableAbnoLv3 = DynamicAbnormalityList.FindAll(abno => abno.Level == 3);
                    var abnoLv3 = availableAbnoLv3.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);
                    selectionList.AddRange(abnoLv3);
                    break;
                default:
                    break;
            }

            if (selectionList.Count == 0) yield break;

            var skillList = selectionList.Select(x => Skill.TempSkill(x.Name, BChar, BChar.MyTeam)).ToList();

            yield return BattleSystem.I_OtherSkillSelect(skillList, new SkillButton.SkillClickDel(ApplyAbnoSkill), ModLocalization.AbnoSelect, false, false, true, false, true);
        }

        public void AddEGO(SkillButton Mybutton)
        {
            if (Mybutton != null && Mybutton.Myskill != null)
            {
                string selectedSkill = Mybutton.Myskill.MySkill.KeyID;

                DynamicEGOList.RemoveAll(x => x == selectedSkill);

                var skill = Skill.TempSkill(selectedSkill, this.BChar);
                if (skill != null)
                {
                    EGO_System.instance?.AddEGOSkill(skill);
                    Utils.UnlockSkillPreview(selectedSkill);
                }
            }
        }

        public void ApplyAbnoSkill(SkillButton Mybutton)
        {
            if (Mybutton != null && Mybutton.Myskill != null)
            {
                string selectedSkill = Mybutton.Myskill.MySkill.KeyID;

                DynamicAbnormalityList.RemoveAll(x => x.Name == selectedSkill);

                if (selectedSkill == ModItemKeys.Skill_S_Abnormality_HistoryLv2_WorkerBee_Neg || selectedSkill == ModItemKeys.Skill_S_Abnormality_HistoryLv3_BarrierofThorns_Pos)
                {
                    Mybutton.Myskill.isExcept = true;
                    BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(Mybutton.Myskill, Mybutton.Myskill.Master, false, false, true, null));
                    Utils.UnlockSkillPreview(selectedSkill);
                }
                else if (selectedSkill == ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Music_Neg)
                {
                    Mybutton.Myskill.isExcept = true;
                    BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(Mybutton.Myskill, Mybutton.Myskill.Master, false, false, true, null));
                    Utils.UnlockSkillPreview(selectedSkill);
                }
                else
                {
                    List<Skill> AllyAbnormality = new List<Skill>();

                    foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
                    {
                        Skill AbnoSkill = Skill.TempSkill(selectedSkill, battleAlly, this.BChar.MyTeam);
                        AbnoSkill.isExcept = true;
                        AllyAbnormality.Add(AbnoSkill);
                    }

                    Utils.UnlockSkillPreview(selectedSkill);
                    BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(AllyAbnormality, new SkillButton.SkillClickDel(this.SelectTargetAbnoSkill),
                        ModLocalization.AbnoRecieve, false, false, true, false, true));
                }
            }
        }


        public void SelectTargetAbnoSkill(SkillButton Mybutton)
        {
            if (Mybutton != null && Mybutton.Myskill != null)
            {
                if (Mybutton.Myskill.Master != null && !Mybutton.Myskill.Master.Dummy && !Mybutton.Myskill.Master.IsDead)
                {
                    BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(Mybutton.Myskill, Mybutton.Myskill.Master, false, false, true, null));
                }
            }
        }
    }
}
