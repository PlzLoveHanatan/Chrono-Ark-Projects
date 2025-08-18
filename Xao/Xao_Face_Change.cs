using System;
using System.Diagnostics.Contracts;
using ChronoArkMod.ModData;
using ChronoArkMod;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Collections.Generic;
using static Xao.Utils;

namespace Xao
{
    public static class Xao_Face_Change
    {
        public static void ChooseFace(BattleChar xao, string skinKey)
        {
            if (SkinKeyToSpriteType.TryGetValue(skinKey, out SpriteType faceType))
            {
                if (Utils.SpritePaths.TryGetValue(faceType, out string facePath))
                {
                    BattleImageChange(xao, facePath);
                    return;
                }
            }

            Debug.LogWarning($"[Xao] ❌ Нет пути для скина {skinKey}, используем Xao.png по умолчанию.");
            BattleImageChange(xao, "Visual/Faces/Kaiju.png");
        }

        public static void BattleImageChange(BattleChar xao, string faceRelativePath)
        {
            try
            {
                ModInfo modInfo = ModManager.getModInfo("Xao");
                string facePath = modInfo.assetInfo.ImageFromFile(faceRelativePath);

                xao.Info.GetData.face_Path = facePath;

                //string faceOriginCharPath = "Assets/ModAssets/HeroFaceOriginChar.prefab";
                //string battleCharPath = "Assets/ModAssets/HeroBattleChar.prefab";
                //string faceSmallCharPath = "Assets/ModAssets/HeroFaceSmallChar.prefab";

                //superHero.Info.GetData.FaceOriginChar_Path = modInfo.assetInfo.ObjectFromAsset<GameObject>("superherounityassetbundle", faceOriginCharPath);
                //superHero.Info.GetData.BattleChar_Path = modInfo.assetInfo.ObjectFromAsset<GameObject>("superherounityassetbundle", battleCharPath);
                //superHero.Info.GetData.FaceSmallChar_Path = modInfo.assetInfo.ObjectFromAsset<GameObject>("superherounityassetbundle", faceSmallCharPath);

                var imageComponent = xao.UI.CharImage.GetComponent<UnityEngine.UI.Image>();
                if (imageComponent != null)
                {
                    AddressableLoadManager.LoadAsyncAction(facePath, AddressableLoadManager.ManageType.Character, imageComponent);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"[SuperHero] ❌ Ошибка в BattleImageChange: {e}");
            }
        }
    }
}
