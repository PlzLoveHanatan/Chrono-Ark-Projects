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
using EmotionalSystem;
using UnityEngine.Video;
namespace EmotionalSystem
{
    public class Ex_EmotionalSystem_EGO : Skill_Extended
    {
        private bool Removed;
        public bool Once;
        public int Countdown;
        public int NowCountdown;
        public override void Init()
        {
            base.Init();
            Removed = false;
            Countdown = 3;
            NowCountdown = 0;
        }

        public override bool Terms()
        {
            if (NowCountdown > 0)
            {
                return false;
            }
            return true;
        }

        public void TurnUpdate()
        {
            if (NowCountdown > 0)
            {
                NowCountdown--;
                BattleSystem.DelayInput(ExUpdate());
            }
        }

        public IEnumerator ExUpdate()
        {
            if (NowCountdown == 0) yield break;

            var exFind = MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_EmotionalSystem_CoolDown) as Ex_EmotionalSystem_CoolDown;
            if (exFind == null)
            {
                var ex = MySkill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_EmotionalSystem_CoolDown) as Ex_EmotionalSystem_CoolDown;
                ex.MainEx = this;
                //ex.CoolDownUpdate();
            }

            yield return null;
            //else
            //{
            //    exFind.CoolDownUpdate();
            //}
        }

        public override string DescExtended(string desc)
        {
            return CountdownText + "" + base.DescExtended(desc);
        }

        public string CountdownText
        {
            get
            {
                var text = "";

                if (Once)
                {
                    return ModLocalization.EgoOnce + "\n";
                }
                else if (NowCountdown > 0)
                {
                    text = ModLocalization.EgoCountdown.Replace("&a", NowCountdown.ToString()) + "\n";
                    //text = ModLocalization.EgoCountdown + " &a ".Replace("&a", NowCountdown.ToString()) + ModLocalization.EgoCountdown_0 + "\n";
                    return text;
                }
                return text;
            }
        }

        //public override string ExtendedDes()
        //{
        //    if (Once)
        //    {
        //        return "Can only be used once"; // change this to localized text later
        //    }
        //    var text = base.ExtendedDes();
        //    text.Replace("&a", Countdown.ToString());
        //    if (NowCountdown > 0)
        //    {
        //        text += "\n" + "Can be used after &b turn(s).".Replace("&b", NowCountdown.ToString()); // change this to localized text later
        //    }
        //    return text;
        //}

        public void UseEGO()
        {
            if (Once)
            {
                if (!Removed)
                {
                    EGO_System.instance?.RemoveEGOSkill(MySkill);
                    Removed = true;
                }
            }
            else
            {
                NowCountdown = Countdown;

                BattleSystem.DelayInput(ExUpdate());
            }
        }

        public override void SkillUseHand(BattleChar Target)
        {
            UseEGO();
        }

        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        //{
        //    UseEGO();
        //    if (!Removed)
        //    {
        //        EGO_System.instance?.RemoveEGOSkill(SkillD);
        //        Removed = true;
        //    }
        //}

        //public void SkillCasting(CastingSkill ThisSkill)
        //{
        //    UseEGO();
        //    if (!Removed)
        //    {
        //        EGO_System.instance?.RemoveEGOSkill(ThisSkill.skill);
        //        Removed = true;
        //    }
        //}
    }
}