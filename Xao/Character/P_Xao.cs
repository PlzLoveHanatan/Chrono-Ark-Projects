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
using Spine.Unity.Examples;
namespace Xao
{
    /// <summary>
    /// Xao
    /// Passive:
    /// </summary>
    public class P_Xao : Passive_Char, IP_SkillUse_User, IP_DamageTake, IP_Healed, IP_SomeOneDead, IP_SkillUse_User_After, IP_Draw, IP_PlayerTurn
    {
        private GameObject chibi;
        private Vector3 size = new Vector3(235f, 235f);
        private string chibiName = "Chibi_Normal";
        private Utils.SpriteType chibiPath = Utils.SpriteType.Chibi_Idle; // Значение по умолчанию
        private Utils.SpriteType chibiPosition = Utils.SpriteType.Chibi_Idle;
        public bool SkillChange;

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (User != BChar && Dmg >= 1)
            {
                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
                switch (randomIndex)
                {
                    case 1:
                        SetChibi("Chibi_TakingDamage_0", Utils.SpriteType.Chibi_TakingDamage_0);
                        break;
                    case 2:
                        SetChibi("Chibi_TakingDamage_1", Utils.SpriteType.Chibi_TakingDamage_1);
                        break;
                }
            }
        }

        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar == BChar && Healer != BChar)
            {
                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
                switch (randomIndex)
                {
                    case 1:
                        SetChibi("Chibi_Normal", Utils.SpriteType.Chibi_Normal);
                        break;
                    case 2:
                        SetChibi("Chibi_NormalBlush", Utils.SpriteType.Chibi_NormalBlush);
                        break;
                }
            }
        }

        public override void Init()
        {
            OnePassive = true;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Xao_Combo.ComboChange(1);

                if (Utils.HentaiSkills.Contains(SkillD.MySkill.KeyID))
                {
                    SetChibi("Chibi_NormalBlush", Utils.SpriteType.Chibi_NormalBlush);
                }
                else if (SkillD.IsDamage)
                {
                    int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 4);

                    switch (randomIndex)
                    {
                        case 1:
                            SetChibi("Chibi_Attack", Utils.SpriteType.Chibi_Attack);
                            break;
                        case 2:
                            SetChibi("Chibi_AttackExtra_0", Utils.SpriteType.Chibi_AttackExtra_0);
                            break;
                        case 3:
                            SetChibi("Chibi_AttackExtra_1", Utils.SpriteType.Chibi_AttackExtra_1);
                            break;
                    }
                }
                else
                {
                    int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
                    switch (randomIndex)
                    {
                        case 1:
                            SetChibi("Chibi_Normal", Utils.SpriteType.Chibi_Normal);
                            break;
                        case 2:
                            SetChibi("Chibi_NormalBlush", Utils.SpriteType.Chibi_NormalBlush);
                            break;
                    }
                }
            }
        }

        public void SkillUseAfter(Skill SkillD)
        {
            if (BChar.Overload >= 2)
            {
                BChar.Overload = 0;
            }
        }

        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar == BChar)
            {
                Utils.DestroyObjects(Utils.ChibiNames);
                Xao_Hearts.DestroyAndNullifyAll();
            }
        }

        private void SetChibi(string name, Utils.SpriteType type)
        {
            chibiName = name;
            chibiPath = chibiPosition = type;
            chibi = Utils.ReplaceChibiIcon(Utils.ChibiNames, BChar, name, Utils.SpritePaths[type], Utils.ChibiPosition[type], size);

            if (chibi != null)
            {
                bool isBounceAnimation = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3) == 1;
                Utils.ChibiStartAnimation(chibi, isBounceAnimation);
            }
        }

        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            if (Drawskill?.MySkill == null && !SkillChange) yield break;

            if (Utils.XaoSkillList.TryGetValue(Drawskill.MySkill.KeyID, out var newSkillID))
            {
                var newSkill = Skill.TempSkill(newSkillID, Drawskill.Master, Drawskill.Master.MyTeam);
                Drawskill.SkillChange(newSkill);
            }
            yield break;
        }

        public void Turn()
        {
            var text = Utils.CreateIcon(BChar, "Text", Utils.GetRandomText(), Utils.GetRandomTextPosition(), new Vector3(100f, 100f), false, false);
            Utils.StartTextPopOut(text);
            Utils.AddBuff(BChar, ModItemKeys.Buff_B_Xao_Affection, 1);
            Xao_Hearts.HeartsCheck(BChar, 1);
        }
    }
}