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
		//private bool Removed;
        public bool OncePerFight;
        public int Cooldown;
        public int NowCooldown;

        public string EGO_Once => ModLocalization.EGO_Skill_Once ?? "";
        public string EGO_Cooldown => ModLocalization.EGO_Skill_Cooldown ?? "";

        public override void Init()
        {
            base.Init();
            Cooldown = 3;
            NowCooldown = 0;
        }

		public override string DescExtended(string desc)
		{
			return CooldownText + "" + base.DescExtended(desc);
		}

		public override bool Terms()
        {
            return NowCooldown <= 0;
        }

		public override void SkillUseHand(BattleChar Target)
		{
			UseEGO();
		}

		public void TurnUpdate()
        {
            if (NowCooldown > 0)
            {
                NowCooldown--;
                BattleSystem.DelayInput(UpdateExtended());
            }
        }

        public IEnumerator UpdateExtended()
        {
            if (NowCooldown > 0)
            {
				var exFind = MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_EmotionalSystem_CoolDown) as Ex_EmotionalSystem_CoolDown;
				if (exFind == null)
				{
					var ex = MySkill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_EmotionalSystem_CoolDown) as Ex_EmotionalSystem_CoolDown;
					ex.EGO_Extended = this;
				}
			}
            yield return null;
        }

        public string CooldownText
        {
            get
            {
                var text = "";

                if (OncePerFight)
                {
                    text = EGO_Once + "\n";
                }
                else if (NowCooldown > 0)
                {
                    text = EGO_Cooldown.Replace("&a", NowCooldown.ToString()) + "\n";
				}
                return text;
            }
        }

        public void UseEGO()
        {
            if (OncePerFight)
            {
				EmotionalSystem_EGO_Button.instance?.RemoveEGOSkill(MySkill);
            }
            else
            {
                NowCooldown = Cooldown;
                BattleSystem.DelayInput(UpdateExtended());
            }
            BattleSystem.DelayInput(ChangeHand());
        }

        public IEnumerator ChangeHand()
        {
			yield return null;
			EmotionalSystem_EGO_Button.instance.ChangeHand();
		}
    }
}