﻿using System;
using System.Diagnostics.Contracts;
using ChronoArkMod.ModData;
using ChronoArkMod;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Collections.Generic;
namespace SuperHero
{
    public static class SuperHero_FaceChange
    {
        public static void ChooseFace(BattleChar superHero, bool isVillain)
        {
            string faceFileName;
            if (isVillain)
            {
                faceFileName = "SuperVillainBattleFace.png";
            }
            else
            {
                faceFileName = "SuperHeroBattleFace.png";
            }

            string facePath = "BattleFace/" + faceFileName;

            BattleImageChange(superHero, facePath);
        }

        public static void BattleImageChange(BattleChar superHero, string faceRelativePath)
        {
            try
            {
                ModInfo modInfo = ModManager.getModInfo("SuperHero");
                string facePath = modInfo.assetInfo.ImageFromFile(faceRelativePath);

                superHero.Info.GetData.face_Path = facePath;

                //string faceOriginCharPath = "Assets/ModAssets/HeroFaceOriginChar.prefab";
                //string battleCharPath = "Assets/ModAssets/HeroBattleChar.prefab";
                //string faceSmallCharPath = "Assets/ModAssets/HeroFaceSmallChar.prefab";

                //superHero.Info.GetData.FaceOriginChar_Path = modInfo.assetInfo.ObjectFromAsset<GameObject>("superherounityassetbundle", faceOriginCharPath);
                //superHero.Info.GetData.BattleChar_Path = modInfo.assetInfo.ObjectFromAsset<GameObject>("superherounityassetbundle", battleCharPath);
                //superHero.Info.GetData.FaceSmallChar_Path = modInfo.assetInfo.ObjectFromAsset<GameObject>("superherounityassetbundle", faceSmallCharPath);

                var imageComponent = superHero.UI.CharImage.GetComponent<UnityEngine.UI.Image>();
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
