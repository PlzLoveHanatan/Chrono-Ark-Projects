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
using NLog.Targets;
namespace EmotionalSystem
{
    /// <summary>
    /// Footfalls
    /// </summary>
    public class B_Abnormality_HistoryLv2_Footfalls : Buff, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            bool lowHp = BChar.HP <= BChar.GetStat.maxhp * 0.2f;

            if (SP.SkillData.Master == BChar && DMG >= 1 && !hit.Info.Ally && lowHp)
            {
                BattleSystem.DelayInput(StartCrying(hit));
            }
        }

        private IEnumerator StartCrying(BattleChar hit)
        {
			Utils.PlaySound("Floor_History_Cry");
			BattleSystem.instance.BlackBackground.SetBackgroundDirect(0.5f, 1.5f);
			yield return new WaitForSecondsRealtime(3f);
			yield return new WaitForSecondsRealtime(2f);
			BattleSystem.DelayInput(Explode(hit));
		}

        private IEnumerator Explode(BattleChar hit)
        {
			int damage = Mathf.Min(80, (int)(hit.GetStat.maxhp * 0.8f));
			hit.Damage(BChar, damage, false, true, false, 0, false, false, false);
			Utils.ApplyBurn(hit, BChar, 10);
			Utils.PlaySound("Floor_History_Explode");
			BChar.Dead();
			EmotionalSystem_Scripts.ChargeLucyNeck(BChar);
			yield return BackgroundOff();
		}

		public IEnumerator BackgroundOff()
		{
			BattleSystem.instance.BlackBackground.SetBackgroundDirectOff();
			yield return null;
			yield break;
		}
	}
}