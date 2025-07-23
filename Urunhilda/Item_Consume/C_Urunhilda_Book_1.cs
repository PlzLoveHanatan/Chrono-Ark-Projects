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
	/// <summary>
	/// Secret Manual of Knowledge
	/// </summary>
    public class C_Urunhilda_Book_1 : UseitemBase
    {
        public override bool Use()
        {
            List<Skill> list2 = PlayData.GetLucySkill(false);

            if (list2 == null || list2.Count == 0)
            {
                EffectView.SimpleTextout(FieldSystem.instance.TopWindow.transform, ScriptLocalization.System.CantRareSkill, 1f, false, 1f);
                return false;
            }

            PlayData.TSavedata.UseItemKeys.Add(GDEItemKeys.Item_Consume_SkillBookLucy);

            FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(
                list2,
                new SkillButton.SkillClickDel(this.SkillAdd),
                ScriptLocalization.System_Item.SkillAdd,
                false, true, true, true, false));

            MasterAudio.PlaySound("BookFlip", 1f);

            return base.Use();
        }


        public void SkillAdd(SkillButton Mybutton)
        {
            PlayData.TSavedata.LucySkills.Add(Mybutton.Myskill.MySkill.KeyID);
            UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();
        }
    }
}