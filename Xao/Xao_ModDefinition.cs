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
using static Xao.Utils;
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

    public class ModIReturn : IP_BattleStart_Ones
    {
        private static readonly Dictionary<Utils.SpriteType, Sprite> Sprites = new Dictionary<Utils.SpriteType, Sprite>();

        private bool FirstAwake;

        public void Awake()
        {

            if (!FirstAwake)
            {
                LoadAllSprites();
                FirstAwake = true;
            }
            Debug.Log($"[{FirstAwake} is ] Awake completed");
        }

        public void BattleStart(BattleSystem Ins)
        {
            try
            {
                string xao = ModItemKeys.Character_Xao;
                string combo = ModItemKeys.Buff_B_Xao_Combo;
                string affection = ModItemKeys.Buff_B_Xao_Affection;
                string normalMod = ModItemKeys.Buff_B_Xao_Mod_0;
                var aliveXao = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == xao);
                if (aliveXao != null)
                {
                    Utils.AddBuff(aliveXao, combo);
                    Utils.AddBuff(aliveXao, normalMod);
                    GameObject chibiIdle = Utils.CreateIcon(aliveXao, "Chibi_Idle", Utils.SpritePaths[Utils.SpriteType.Chibi_Idle], Utils.ChibiPosition[Utils.SpriteType.Chibi_Idle], new Vector3(235f, 235f));
                    Xao_Combo.Combo_0 = Utils.CreateComboButton("Combo_0", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_0], new Vector2(110f, 110f), new Vector2(-749.7802f, -438.8362f));
                    Utils.AddComponent<Xao_Combo_Tooltip>(Xao_Combo.Combo_0);
                    //GameObject randomHentaitext = Utils.CreateIcon(Utils.Xao, "RandomHentaiText", Utils.GetRandomText(), Utils.GetRandomTextPosition(), new Vector3(100f, 100f), false, false);
                    //Utils.StartTextPopOut(randomHentaitext);
                    //var combo2 = Utils.CreateIcon(aliveXao, "Combo_0",Utils.SpritePaths[Utils.SpriteType.Combo_0],Utils.ComboPosition[Utils.SpriteType.Combo_0],new Vector3(100f, 100f));

                    //Utils.AddBuff(aliveXao, affection);
                    Xao_Hearts.CreateHearts(aliveXao);
                    Xao_Hearts.SavedStackNum = 0;
                    Xao_Combo.CurrentCombo = 0;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Battle Start Error" + e.ToString());
            }
        }


        public static void LoadAllSprites()
        {
            foreach (var kvp in Utils.SpritePaths)
            {
                Utils.LoadSpriteAsync(kvp.Value, sprite => Sprites[kvp.Key] = sprite);
            }
        }

        public static Sprite GetSprite(SpriteType type)
        {
            Sprites.TryGetValue(type, out var sprite);
            return sprite;
        }
    }
}