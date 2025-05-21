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

namespace Raphi
{
    public class Raphi_Consume : UseitemBase
    {

        public override bool CantTarget(Character CharInfo)
        {
            return CharInfo.KeyData == ModItemKeys.Character_Raphi || ItemRaphi.Instance.UsedList.Find((string a) => a == CharInfo.KeyData) != null || base.CantTarget(CharInfo);
        }
        public override bool Use(Character CharInfo)
        {
            
            List<Skill_Extended> list = new List<Skill_Extended>();
            List<Skill_Extended> list2 = new List<Skill_Extended>();
            new List<string>();
            list.Add(Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Raphi_3));
            List<Skill_Extended> list3 = new List<Skill_Extended>();
            foreach (Skill_Extended skill_Extended in list)
            {
                foreach (BattleAlly battleAlly in PlayData.Battleallys)
                {
                    if (battleAlly.Info == CharInfo)
                    {
                        bool flag = false;
                        foreach (Skill enforceSkill in battleAlly.Skills)
                        {
                            if (skill_Extended.CanEnforce(enforceSkill))
                            {
                                list3.Add(skill_Extended);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                    }
                }
            }
            if (list3.Count != 0)
            {
                list2.Add(list3.Random<Skill_Extended>());
            }
            if (list2.Count >= 1)
            {
                UIManager.InstantiateActive(UIManager.inst.EnforceUI).GetComponent<UI_Enforce>().Init(list, CharInfo);
                MasterAudio.PlaySound("Potion", 1f, null, 0f, null, null, false, false);
                ItemRaphi.Instance.UsedList.Add(CharInfo.KeyData);
                return true;

            }
            EffectView.SimpleTextout(FieldSystem.instance.TopWindow.transform, ScriptLocalization.System_Item.CelestialCant, 1f, false, 1f);
            return false;
        } 
    }
}