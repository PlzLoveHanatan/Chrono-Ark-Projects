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
namespace CommonBook
{
	/// <summary>
	/// CommonSkillBook
	/// </summary>
    public class Item_book1 : UseitemBase
    {
        public static bool IsHeal(GDESkillData skillData)
        {
            if (skillData.HealSkill) return true;
            return skillData.Effect_Target.HEAL_Per >= 1 || skillData.Effect_Target.HEAL_Base >= 1;
        }

        public static bool IsDamage(GDESkillData skillData)
        {
            return skillData.Effect_Target.DMG_Per >= 1 || skillData.Effect_Target.DMG_Base >= 1;
        }

        public static List<GDESkillData> GetAllHealingPublicSkills()
        {
            var skillList = PlayData.ALLSKILLLIST;
            skillList = skillList.FindAll(data => data.Category.Key == GDEItemKeys.SkillCategory_PublicSkill && IsHeal(data));
            return skillList;
        }

        public static List<GDESkillData> GetAllDamagePublicSkills()
        {
            var skillList = PlayData.ALLSKILLLIST;
            skillList = skillList.FindAll(data => data.Category.Key == GDEItemKeys.SkillCategory_PublicSkill && IsDamage(data));
            return skillList;
        }

        public static List<GDESkillData> GetAllBuffPublicSkills()
        {
            var skillList = PlayData.ALLSKILLLIST;
            skillList = skillList.FindAll(data => data.Category.Key == GDEItemKeys.SkillCategory_PublicSkill && !IsHeal(data) && !IsDamage(data));
            return skillList;
        }

        public override bool Use(Character CharInfo)
        {
            List<Skill> list = new List<Skill>();
            List<BattleAlly> battleallys = PlayData.Battleallys;
            BattleTeam tempBattleTeam = PlayData.TempBattleTeam;
            for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
            {
                if (CharInfo == PlayData.TSavedata.Party[i])
                {

                    foreach (var data in GetAllHealingPublicSkills())
                    {
                        list.Add(Skill.TempSkill(data.KeyID ?? data.Key, battleallys[i], tempBattleTeam));
                    }
                }
            }
            foreach (Skill skill in list)
            {
                if (!SaveManager.IsUnlock(skill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
                {
                    SaveManager.NowData.unlockList.SkillPreView.Add(skill.MySkill.KeyID);
                }
            }
            FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.SkillAdd), ScriptLocalization.System_Item.SkillAdd, false, true, true, true, false));
            MasterAudio.PlaySound("BookFlip", 1f, null, 0f, null, null, false, false);
            return true;
        }
        public void SkillAdd(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.Info.UseSoulStone(Mybutton.Myskill);
            UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();
        }
    }
}