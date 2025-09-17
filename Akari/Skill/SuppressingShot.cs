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
using static CharacterDocument;
namespace Akari
{
    /// <summary>
    /// Suppressing Shot
    /// </summary>
    public class SuppressingShot : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Gun_Normal1", 100f, null, 0f, null, null, false, false);

            int discardedAmo = Utils.DiscardAndApplyAmmunition(BChar, 1, Targets[0]);

            if (discardedAmo > 0)
            {
                PlusSkillPerFinal.Damage = 20 * discardedAmo;
            }
        }
    }
}
