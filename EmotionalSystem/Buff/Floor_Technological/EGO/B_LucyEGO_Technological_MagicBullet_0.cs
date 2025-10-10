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
namespace EmotionalSystem
{
    public class B_LucyEGO_Technological_MagicBullet_0 : Buff, IP_Awake, IP_PlayerTurn_1
    {
        private int CountDown;
        public List<Skill> Hand = new List<Skill>();
        public List<Skill> DrawPile = new List<Skill>();
        public List<Skill> DiscardPile = new List<Skill>();
        public List<string> FixedAbility = new List<string>();

        public override void Init()
        {
            OnePassive = true;
        }

        public void Turn1()
        {
            CountDown++;

            if (CountDown >= 3)
            {
                var team = BattleSystem.instance.AllyTeam;

                for (int j = team.Skills.Count - 1; j >= 0; j--)
                {
                    Skill skill = team.Skills[j];
                    if (skill.Master == BChar)
                    {
                        skill.Remove();
                    }
                }

                team.Skills.RemoveAll(s => s.Master == BChar);
                team.Skills_Deck.RemoveAll(s => s.Master == BChar);
                team.Skills_UsedDeck.RemoveAll(s => s.Master == BChar);

                team.Skills_Deck.AddRange(DrawPile);
                team.Skills_UsedDeck.AddRange(DiscardPile);

                foreach (var skill in Hand)
                {
                    BattleSystem.instance.StartCoroutine(team.AddSkillNoDrawEffect(skill, -1));
                }

                foreach (var skill in FixedAbility)
                {
                    var skill2 = Skill.TempSkill(skill, this.BChar, this.BChar.MyTeam);

                    (this.BChar as BattleAlly).MyBasicSkill.SkillInput(skill2);
                }

                SelfDestroy();
            }
        }

        public void Awake()
        {
            CountDown = 0;
            var newSkills = new[]
                {
                    ModItemKeys.Skill_S_Synchronize_Technological_FloodingBullets,
                    ModItemKeys.Skill_S_Synchronize_Technological_InevitableBullet,
                    ModItemKeys.Skill_S_Synchronize_Technological_MagicBullet,
                    ModItemKeys.Skill_S_Synchronize_Technological_SilentBullet,

            };

            var fixedSkill = (this.BChar as BattleAlly)?.MyBasicSkill?.buttonData;

            if (fixedSkill != null)
            {
                FixedAbility.Add(fixedSkill.MySkill.KeyID);
            }

            Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_Synchronize_Technological_Desynchronize, this.BChar, this.BChar.MyTeam);

            (this.BChar as BattleAlly).MyBasicSkill.SkillInput(skill2);

            var team = BattleSystem.instance.AllyTeam;

            Hand.Clear();
            DrawPile.Clear();
            DiscardPile.Clear();

            Hand.AddRange(team.Skills.Where(s => s.Master == BChar));
            DrawPile.AddRange(team.Skills_Deck.Where(s => s.Master == BChar));
            DiscardPile.AddRange(team.Skills_UsedDeck.Where(s => s.Master == BChar));

            for (int i = team.Skills.Count - 1; i >= 0; i--)
            {
                Skill skill = team.Skills[i];
                if (skill.Master == BChar)
                {
                    skill.Remove();
                }
            }

            team.Skills_Deck.RemoveAll(s => s.Master == BChar);
            team.Skills_UsedDeck.RemoveAll(s => s.Master == BChar);

            foreach (var key in newSkills)
            {
                var skill = Skill.TempSkill(key, this.BChar);
                if (skill != null)
                {
                    BattleSystem.instance.StartCoroutine(team.AddSkillNoDrawEffect(skill, -1));
                    Utils.UnlockSkillPreview(key);
                }
            }
        }
    }
}