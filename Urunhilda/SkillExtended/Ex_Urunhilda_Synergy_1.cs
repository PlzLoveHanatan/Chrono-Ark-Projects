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
using EItem;
using NLog.Targets;
namespace Urunhilda
{
    public class Ex_Urunhilda_Synergy_1 : Skill_Extended
    {
        //public bool OncePerFight;
        BattleChar Target;
        public override void Init()
        {
            OnePassive = true;
        }

        private List<Skill> DynamicList = new List<Skill>();

        private readonly List<string> SkillSelection = new List<string>
        {
            ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_0,
            ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_1,
            ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_2,
        };

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];
            Target = target;
            BattleSystem.DelayInput(Del());
        }

        private IEnumerator Del()
        {
            yield return new WaitForFixedUpdate();

            DynamicList.Clear();

            foreach (var key in SkillSelection)
            {
                var skill = Skill.TempSkill(key, BChar, BChar.MyTeam);
                if (skill == null || skill.MySkill == null) continue;
                DynamicList.Add(skill);
            }

            if (PlayData.TSavedata._Gold < 500)
            {
                DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_2);

                if (PlayData.TSavedata._Gold < 250)
                {
                    DynamicList.RemoveAll(s => s.MySkill.KeyID == ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_0);
                }
            }
            BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(DynamicList, Selection, ScriptLocalization.System_SkillSelect.EffectSelect, true, false));
        }

        public void Selection(SkillButton myButton)
        {
            if (myButton == null || myButton.Myskill == null || myButton.Myskill.MySkill == null) return;

            string key = myButton.Myskill.MySkill.KeyID;


            if (key == ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_0)
            {
                PlayData.TSavedata._Gold -= 250;
                Skill cloneSkill = MySkill.CloneSkill(true, null, null, true);
                BattleSystem.DelayInputAfter(AdditionalCast(cloneSkill, Target));
            }
            else if (key == ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_1)
            {
                PlayData.TSavedata._Gold += 250;
            }
            else if (key == ModItemKeys.Skill_S_Ex_Urunhilda_Synergy_2)
            {
                PlayData.TSavedata._Gold -= 500;
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_ArtifactPouch, 1));
            }
        }

        public IEnumerator AdditionalCast(Skill Skill, BattleChar Target)
        {
            yield return new WaitForSeconds(0.2f);
            bool AdditionalHit = true;
            if (Target.IsDead || !AdditionalHit) yield break;

            BChar.ParticleOut(Skill, Target);

            AdditionalHit = false;

            yield break;
        }
    }
}