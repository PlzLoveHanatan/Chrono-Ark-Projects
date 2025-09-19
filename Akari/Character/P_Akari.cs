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
using static CharacterDocument;
using NLog.Targets;
using System.Diagnostics.Eventing.Reader;
using Spine;
using System.Web.Compilation;
using Steamworks;
using System.Runtime.InteropServices.WindowsRuntime;
namespace Akari
{
    /// <summary>
    /// Passive:
    /// After every three successful attacks with Melee Combat Pages, add a random Ammunition to hand.
    /// </summary>
    public class P_Akari : Passive_Char, IP_PlayerTurn, IP_SkillUse_User, IP_Discard, IP_LevelUp, IP_BattleStart_Ones
    {
        public bool Mag;

        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void Turn()
        {
            var threeFold = ModItemKeys.Buff_B_ThreefoldTenacity_1;

            if (BattleSystem.instance.TurnNum >= 4)
            {
                if (BChar.BuffReturn(threeFold, false) == null)
                {
                    BChar.BuffAdd(threeFold, BChar, false, 0, false, -1, false);
                }

                if (BChar.BuffReturn(ModItemKeys.Buff_B_ThreefoldTenacity, false) != null)
                {
                    BChar.BuffRemove(ModItemKeys.Buff_B_ThreefoldTenacity);
                }
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.BasicSkill && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                BChar.BuffAdd(ModItemKeys.Buff_AmmoSupply, BChar, false, 0, false, -1, false);

                if (BattleSystem.instance.TurnNum <= 3)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_ThreefoldTenacity, BChar, false, 0, false, -1, false);
                }
            }
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (!Click && !HandFullWaste && Utils.Ammunition.Contains(skill.MySkill.KeyID))
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_TacticalReload, BChar, false, 0, false, -1, false);
            }
        }

        public void LevelUp()
        {
            if (!Mag)
            {
                FieldSystem.DelayInput(GainMag());
            }
        }

        public IEnumerator GainMag()
        {
            InventoryManager.Reward(ItemBase.GetItem(ModItemKeys.Item_Active_Standart_Mag));
            Mag = true;
            yield return null;
        }

        public void BattleStart(BattleSystem Ins)
        {
            Utils.ChargeMag(BChar);
        }
    }
}