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
        private static Dictionary<Utils.SpriteType, Sprite> Sprites = new Dictionary<Utils.SpriteType, Sprite>();

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
                var aliveXao = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == xao);
                if (aliveXao != null)
                {
                    var chibiIdle = Utils.CreateIcon(aliveXao, "Chibi_Idle", Utils.SpritePaths[Utils.SpriteType.Chibi_Idle], Utils.ChibiPosition[Utils.SpriteType.Chibi_Idle], new Vector3(235f, 235f));
                    if (chibiIdle != null)
                    {
                        Debug.Log($"Chibi Created");
                    }
                    else
                    {
                        Debug.Log($"Chibi is not Created");
                    }

                    Xao_Visual_Hearts.CreateHearts(aliveXao);
                    Xao_Visual_Hearts.SavedStackNum = 0;
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