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
namespace Akari
{
    public class B_BayonetArsenal : Buff, IP_SkillUse_User
    {
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.Ammunition.Concat(Utils.RangeAttacks).Contains(SkillD.MySkill.KeyID)) return;

            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse)
            {
                MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);

                Utils.CreateRandomAmmunition(BChar);

                SelfStackDestroy();
            }
        }
    }
}