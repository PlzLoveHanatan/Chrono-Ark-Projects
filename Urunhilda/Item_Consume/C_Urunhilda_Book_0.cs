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
namespace Urunhilda
{
    public class C_Urunhilda_Book_0 : UseitemBase
    {
        public override bool Use(Character CharInfo)
        {
            List<Skill> list = new List<Skill>();
            List<BattleAlly> battleallys = PlayData.Battleallys;
            BattleTeam tempBattleTeam = PlayData.TempBattleTeam;

            int index = PlayData.TSavedata.Party.IndexOf(CharInfo);
            if (index < 0)
            {
                return false;
            }

            var rareSkills = PlayData.ALLRARESKILLLIST;

            foreach (var data in rareSkills)
            {
                string keyID = data.KeyID ?? data.Key;
                if (!string.IsNullOrEmpty(keyID))
                {
                    Skill tempSkill = Skill.TempSkill(keyID, battleallys[index], tempBattleTeam);
                    if (tempSkill != null)
                    {
                        list.Add(tempSkill);
                    }
                }
            }

            if (list.Count == 0)
            {
                EffectView.SimpleTextout(FieldSystem.instance.TopWindow.transform, ScriptLocalization.System.CantRareSkill, 1f, false, 1f);
                return false;
            }

            foreach (Skill skill in list)
            {
                if (!SaveManager.IsUnlock(skill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
                {
                    SaveManager.NowData.unlockList.SkillPreView.Add(skill.MySkill.KeyID);
                }
            }

            FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.SkillAdd), ScriptLocalization.System_Item.SkillAdd, false, true, true, true, false));
            MasterAudio.PlaySound("BookFlip", 1f);

            return true;
        }

        public void SkillAdd(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.Info.UseSoulStone(Mybutton.Myskill);
            UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();
        }
    }
}