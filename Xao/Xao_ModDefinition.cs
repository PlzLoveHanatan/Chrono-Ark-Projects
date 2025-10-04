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
using ChronoArkMod.ModData;
using static Mono.Security.X509.X520;
using System.Drawing;
namespace Xao
{
    public class Xao_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);
        public override List<object> BattleSystem_ModIReturn()
        {
            var list = base.BattleSystem_ModIReturn();

            list.Add(new ModIReturn());

            return list;
        }
    }

    public class ModIReturn : IP_BattleStart_Ones, IP_BattleEnd, IP_PlayerTurn
    {
        private static readonly Dictionary<Utils.SpriteType, Sprite> Sprites = new Dictionary<Utils.SpriteType, Sprite>();

        public bool FirstAwake;

        public void Awake()
        {
            if (!FirstAwake && Xao_Plugin.XaoInParty())
            {
                LoadAllSprites();
                FirstAwake = true;
            }
        }

        public void BattleStart(BattleSystem Ins)
        {
            if (Utils.Xao)
            {
                Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Combo);
                Utils.CreateIdleChibi();
                Utils.CreateNewCombo(Xao_Combo.Combo_0, "Combo_0", Utils.SpritePaths[Utils.SpriteType.Combo_0]);
                Utils.XaoHornyModOff();
                Xao_Hearts.CreateHearts(Utils.Xao);

                ResetCombo();
                ResetHeartsNum();
                ResetRewards();
                ResetRare();
            }
        }

        public void Turn()
        {
            if (Utils.Xao)
            {
                ResetRare();
            }
        }

        public void BattleEnd()
        {
            if (Utils.Xao)
            {
                Utils.XaoHornyModOff();
            }
        }

        public static void LoadAllSprites()
        {
            foreach (var kvp in Utils.SpritePaths)
            {
                Utils.LoadSpriteAsync(kvp.Value, sprite => Sprites[kvp.Key] = sprite);
            }
        }

        public static Sprite GetSprite(Utils.SpriteType type)
        {
            Sprites.TryGetValue(type, out var sprite);
            return sprite;
        }

        public void ResetRare()
        {
            Utils.RareNum = 0;
        }

        public void ResetHeartsNum()
        {
            Xao_Hearts.SavedStackNum = 0;
            Xao_Hearts_Ally.DestroyAll();
            Xao_Hearts_Ally_Synergy.DestroyAll();
        }

        public void ResetCombo()
        {
            Xao_Combo.CurrentCombo = 0;
        }

        public void ResetRewards()
        {
            Xao_Combo_Rewards.AttackPowerOncePerFight = false;
            Xao_Combo_Rewards.RelicPouchOncePerFight = false;
            Xao_Combo_Rewards.Legendary_OncePerFight = false;
        }
    }
}