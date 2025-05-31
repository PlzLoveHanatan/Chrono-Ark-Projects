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
	/// Instinct Tonic
	/// </summary>
    public class Mia_InstinctTonic : UseitemBase
    {
        public override bool CantTarget(Character CharInfo)
        {
            return CharInfo.KeyData == ModItemKeys.Character_Mia || Mia_Tonic.Instance.UsedList.Find((string a) => a == CharInfo.KeyData) != null || base.CantTarget(CharInfo);
        }
        public override bool Use(Character CharInfo)
        {
            Skill_Extended exToApply = Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Mia_InstinctSurge);

            foreach (BattleAlly battleAlly in PlayData.Battleallys)
            {
                if (battleAlly.Info == CharInfo)
                {
                    foreach (Skill enforceSkill in battleAlly.Skills)
                    {
                        if (exToApply.CanEnforce(enforceSkill))
                        {
                            UIManager.InstantiateActive(UIManager.inst.EnforceUI).GetComponent<UI_Enforce>().Init(new List<Skill_Extended> { exToApply }, CharInfo);

                            MasterAudio.PlaySound("Potion", 1f);
                            Mia_Tonic.Instance.UsedList.Add(CharInfo.KeyData);
                            return true;
                        }
                    }
                    break;
                }
            }

            EffectView.SimpleTextout(FieldSystem.instance.TopWindow.transform,ScriptLocalization.System_Item.CelestialCant,1f, false, 1f);
            return false;
        }
    }
}