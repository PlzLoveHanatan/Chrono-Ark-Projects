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
using Spine;
using Newtonsoft.Json.Linq;
using static UnityEngine.GridBrushBase;
using NLog.Targets;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using EmotionSystem;
using Object = UnityEngine.Object;
using EmotionalSystem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ChronoArkMod.ModData.Settings;

namespace EmotionalSystem
{
    /// <summary>
    /// Emotional Level
    /// </summary>
    public class B_EmotionalLevel : Buff, IP_SkillUse_User, IP_PlayerTurn, IP_Dodge, IP_DamageTake, IP_Healed, IP_Awake, IP_DealDamage, IP_Kill, IP_SomeOneDead, IP_EmotionLvUpBefore
    {
        public CharEmotion Emotion;

        private int EmotionsGainThisTurn;

        public static GameObject EmotionPrefab;
        public int EmotionalLevel => Emotion.Level;

        public static List<int> CoinsToLevelUp = new List<int> { 3, 3, 5, 7, 9 };

        public void Awake()
        {
            EmotionsGainThisTurn = 0;

            Emotion = BChar.UI.transform.GetChild(0)?.GetComponentInChildren<CharEmotion>();
            if (Emotion != null) return;

            if (EmotionPrefab == null)
            {
                EmotionPrefab = Utils.GetAssets<GameObject>("Assets/ModAssets/EmotionUI.prefab", "emotionsystemunityassetbundle");
                if (EmotionPrefab == null)
                {
                    Debug.Log("Assets cannot be loaded");
                    return;
                }
            }

            var emotionUI = Object.Instantiate(EmotionPrefab, BChar.UI.transform.GetChild(0));
            emotionUI.transform.localPosition = new Vector3(40f, -25f, 0f);
            Emotion = emotionUI.GetComponent<CharEmotion>();
            Emotion.Init(BChar, 0, CoinsToLevelUp);
            emotionUI.SetActive(true);

            Debug.Log("UI created");
        }

        public void Turn()
        {
            EmotionsGainThisTurn = 0;
            Emotion.CanGetCoin = true;

            if (EmotionalLevel >= 4)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_EmotionalSystem_ManaReduction, BChar, false, 0, false, -1, false);
            }

            if (EmotionalLevel >= 5)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_EmotionalSystem_Draw, BChar, false, 0, false, -1, false);
            }
        }

        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void SomeOneDead(BattleChar DeadChar)
        {
            if (BChar.Info.Ally == DeadChar.Info.Ally && DeadChar != BChar)
            {
                var pos = DeadChar.GetPosUI();
                for (int i = 0; i < 3; i++)
                {
                    BChar.GetNegEmotion(pos);
                }
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsHeal || SkillD.IsDamage && SkillD.Master == BChar)
            {
                BChar.GetPosEmotion(SkillD.GetPosUI());
            }

            else if (SkillD.Master == BChar)
            {
                BChar.GetNegEmotion(SkillD.GetPosUI());
            }
        }

        public void KillEffect(SkillParticle SP)
        {
            if (SP.UseStatus == BChar)
            {
                var target = SP.TargetChar.First(bc => bc.IsDead);
                var pos = target?.GetPosUI();
                for (int i = 0; i < 3; i++)
                {
                    BChar.GetPosEmotion(pos);
                }
            }
        }

        public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
        {
            if (IsCri)
            {
                BChar.GetPosEmotion(BChar.GetPosUI());
            }
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg >= 1)
            {
                BChar.GetNegEmotion(User.GetPosUI());

                if (Cri)
                {
                    BChar.GetNegEmotion(User.GetPosUI());
                }
            }
        }

        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar == BChar)
            {
                BChar.GetPosEmotion(Healer.GetPosUI());
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == BChar)
            {
                BChar.GetPosEmotion(SP.UseStatus.GetPosUI());
            }
        }

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            string addedBuffKey = addedbuff.BuffData.Key;

            //if (ModBuffs.Contains(addedBuffKey)) return;

            if (BuffTaker == BChar && addedbuff.BuffData.Debuff)
            {
                BChar.GetNegEmotion(BuffUser.GetPos());
            }
            if (BuffTaker == BChar && !addedbuff.BuffData.Debuff)
            {
                BChar.GetPosEmotion(BuffUser.GetPos());
            }
        }

        public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
        {
            if (charEmotion.BChar == BChar)
            {
                EmotionsGainThisTurn++;

                if (EmotionsGainThisTurn >= 2)
                {
                    charEmotion.CanGetCoin = false;
                }
            }
        }
    }
}