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
using static System.Net.Mime.MediaTypeNames;
using Dialogical;
using Spine;
using static CharacterDocument;
using System.Text.RegularExpressions;
namespace ImaSuguRinne
{
    /// <summary>
    /// Fragment of Memory
    /// </summary>
    public class S_Rinne_FragmentofMemory : Skill_Extended
    {
        public override void Init()
        {
            if (MySkill.AllExtendeds != null && MySkill.AllExtendeds.Count > 0 && MySkill.MySkill.Name != ModLocalization.MemoryFragment)
            {
                MySkill.MySkill.Name = ModLocalization.MemoryFragment;
            }
        }

        public override IEnumerator DrawAction()
        {
            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_FragmentofMemory_0)
            {
                Utils.CastSkill(BChar, MySkill);
            }
            else
            {
                Utils.GlitchEffect(MySkill, 1);
                Utils.AllyTeam.Draw();
            }
            return base.DrawAction();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BChar.StartCoroutine(Utils.RetryNow(BChar));

            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_FragmentofMemory_0)
            {
                Utils.AllyTeam.Draw();
            }
            else
            {
                Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_FragmentofMemory_0, true, true, 0, 0, true, false);
                Utils.InsertSkillInDeck(BChar, skill);
            }
        }
    }
}