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
                if (NowCountdown > 0)
                {
                    text = ModLocalization.EgoCountdown + " &a ".Replace("&a", NowCountdown.ToString()) + ModLocalization.EgoCountdown_0 + "\n";

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