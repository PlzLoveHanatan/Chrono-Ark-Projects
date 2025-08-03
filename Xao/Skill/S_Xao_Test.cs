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
namespace Xao
{
	/// <summary>
	/// Test
	/// </summary>
    public class S_Xao_Test : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //var chibi = Utils.ReplaceChibiIcon("Chibi_Idle", BChar, "Chibi_AttackH", Utils.ChibiPath["AttackH"], Utils.ChibiPosition["AttackH"], new Vector3(250f, 250f));
            //if (chibi != null)
            //{
            //    Utils.StartRotation(chibi);
            //}

            Xao_Visual_Hearts.HeartsCheck(BChar, 1);

            var icon = Utils.CreateIcon(BChar, "Text_0", Utils.GetRandomText(), Utils.GetRandomTextPosition(), new Vector3(100f, 100f), false);
            if (icon != null)
            {
                Debug.Log($"Icon Created");
                StartFlicking(icon);
            }
            else
            {
                Debug.Log($"Icon Not Created");
            }
            BattleSystem.DelayInput(NewText());
        }

        private IEnumerator NewText()
        {
            yield return new WaitForSeconds(0.5f);

            var icon = Utils.CreateIcon(BChar, "Text_1", Utils.GetRandomText(), Utils.GetRandomTextPosition(), new Vector3(100f, 100f), false);
            if (icon != null)
            {
                Debug.Log($"Icon Created");
                StartFlicking(icon);
            }
            else
            {
                Debug.Log($"Icon Not Created");
            }
        }

        private void StartFlicking(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Script flicker = obj.GetComponent<Xao_Script>();
                if (flicker == null)
                {
                    flicker = obj.AddComponent<Xao_Script>();
                }
                flicker.StartScaleUp();
            }
        }      
    }
}