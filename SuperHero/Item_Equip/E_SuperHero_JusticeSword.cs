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
namespace SuperHero
{
    public class E_SuperHero_JusticeSword : EquipBase, IP_SkillUse_User, IP_PlayerTurn
    {

        public override void FixedUpdate()
        {
            PlusStat.atk = Utils.JusticeSword;
        }

        public void Enchent()
        {
            MyItem.Enchant.CurseEnchant = false;
        }

        public override void Init()
        {
            if (MyItem != null)
            {
                MyItem.Curse = new EquipCurse();
                MyItem._Isidentify = true;
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var buff = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            if (Utils.HeroAttacksWithMark.Contains(SkillD.MySkill.KeyID) && SkillD.Master.Info.KeyData == superHero)
            {
                foreach (var t in Targets)
                {
                    var buff2 = t.BuffReturn(buff, false) as B_SuperHero_MarkofJustice;

                    if (!t.Info.Ally && t.BuffReturn(buff, false) != null)
                    {
                        t.BuffAdd(buff, BChar, false, 0, false, -1, false);
                        buff2.MarkStacks++;
                        buff2.BuffStat();
                    }
                }
            }
        }

        public void Turn()
        {
            Skill newSkill;
            var superHero = ModItemKeys.Character_SuperHero;
            var justice = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0;
            var justice1 = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1;
            var hero = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == superHero);
            var buff1 = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var buff3 = hero.BuffReturn(buff1, false) as B_SuperHero_HeroComplex;

            if (hero != null && (BattleSystem.instance.TurnNum < 3 || buff3.StackNum < 25))
            {
                if (buff3 != null && buff3.StackNum >= 20)
                {
                    newSkill = Skill.TempSkill(justice, BChar, BChar.MyTeam);
                }
                else
                {
                    newSkill = Skill.TempSkill(justice1, BChar, BChar.MyTeam);
                }
                BattleSystem.instance.AllyTeam.Add(newSkill, true);
            }

            bool firstCheck = true;

            var buff = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            var allyTeam = BattleSystem.instance.AllyTeam.AliveChars;
            var enemyTeam = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Concat(allyTeam.Where(x => x != null && x.Info.KeyData != superHero));

            if (hero != null)
            {
                hero.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
            }

            foreach (var enemy in enemyTeam)
            {
                var buff2 = enemy.BuffReturn(buff, false) as B_SuperHero_MarkofJustice;

                if (enemy != null)
                {
                    enemy.BuffAdd(buff, BChar, false, 999, false, -1, false);

                    if (buff2 != null && buff2.BuffData != null)
                    {
                        if (buff2.BuffData.MaxStack != 5 && firstCheck)
                        {
                            buff2.BuffData.MaxStack = 5;
                            firstCheck = false;
                        }
                    }
                }
            }
        }
    }
}