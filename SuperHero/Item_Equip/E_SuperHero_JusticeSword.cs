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
using Spine;
using System.Security.Cryptography;
namespace SuperHero
{
    public class E_SuperHero_JusticeSword : EquipBase, IP_SkillUse_User, IP_PlayerTurn, IP_BattleStart_Ones
    {
        public bool FirstMarkCheck = true;


        public override string DescInit()
        {
            string text = ModLocalization.JusticeSword_0;

            if (BChar != null && BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) is B_SuperHero_HeroComplex complex)
            {
                if (complex.StackNum >= 25 && Utils.SuperVillainMod(BChar))
                {
                    text = ModLocalization.JusticeSword_1;
                }
            }
            return base.DescInit() + "\n" + text;
        }

        public void BattleStart(BattleSystem Ins)
        {
            FirstMarkCheck = true;
        }

        public override void FixedUpdate()
        {
            var justiceDamage = PlayData.TSavedata.GetCustomValue<JusticeSword>();
            if (justiceDamage == null)
            {
                justiceDamage = new JusticeSword();
                PlayData.TSavedata.AddCustomValue(justiceDamage);
                justiceDamage.JusticeDamage = 0;
            }
            PlusStat.atk = justiceDamage.JusticeDamage;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            var markOfJustice = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            if (Utils.HeroAttacksWithMark.Contains(SkillD.MySkill.KeyID) && SkillD.Master == Utils.SuperHero)
            {
                foreach (var target in Targets)
                {
                    if (target.Info.Ally) continue;

                    if (target.BuffReturn(markOfJustice, false) is B_SuperHero_MarkofJustice mark)
                    {
                        Utils.AddDebuff(target, BChar, markOfJustice, 1);
                        mark.MarkStacks++;
                        mark.BuffStat();
                    }
                }
            }
        }

        public void Turn()
        {
            Skill skill;
            var heroComplex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            string skillKey = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1;

            Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, heroComplex);

            if (Utils.SuperHero && Utils.SuperHero.BuffReturn(heroComplex, false) is B_SuperHero_HeroComplex complex && !Utils.SuperHeroMod(BChar) /*&& (BattleSystem.instance.TurnNum < 3 || complex.StackNum < 25)*/)
            {
                if (complex.StackNum >= 20)
                {
                    skillKey = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0;
                }
            }
            skill = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);

            var markofJustice = ModItemKeys.Buff_B_SuperHero_MarkofJustice;

            foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
            {
                if (enemy != null)
                {
                    Utils.AddDebuff(enemy, BChar, markofJustice, 1, 999);

                    if (enemy.BuffReturn(markofJustice, false) is B_SuperHero_MarkofJustice mark)
                    {
                        if (mark?.BuffData.MaxStack != 5 && FirstMarkCheck)
                        {
                            mark.BuffData.MaxStack = 5;
                            FirstMarkCheck = false;
                        }
                    }
                }
            }

            if (Utils.SuperHeroMod(BChar))
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    if (ally != null && ally.BuffReturn(markofJustice, false) is B_SuperHero_MarkofJustice mark)
                    {
                        mark?.SelfDestroy();
                    }
                }
            }
            else if (Utils.SuperVillainMod(BChar))
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    Utils.AddDebuff(ally, BChar, markofJustice, 1, 999);
                }
            }
        }
    }
}