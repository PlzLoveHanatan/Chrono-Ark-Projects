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
namespace Unica
{
    /// <summary>
    /// Shuffle Hands
    /// Discard a skill with the lowest Mana Cost and draw 1 Skill.
    /// </summary>
    public class ShuffleHands : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse) return;

            base.SkillUseSingle(SkillD, Targets);

            var discardList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill
            && skill.Master != BattleSystem.instance.AllyTeam.LucyAlly).ToList();

            if (discardList.Count == 0) return;

            var skillToDiscard = discardList.FirstOrDefault(skill => skill.ExtendedFind_DataName(ModItemKeys.SkillExtended_ExSheathe) != null)
                ?? discardList.Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Unica).OrderBy(skill => skill.AP).FirstOrDefault()
                ?? discardList.OrderBy(skill => skill.AP).FirstOrDefault();

            skillToDiscard?.Delete(false);

            if (this.BChar.Info.KeyData == ModItemKeys.Character_Unica)
            {
                new P_Unica().ApplyEffects(this.BChar, 1);
            }        
        }       
        public override void IlyaWaste()
        {
            BattleSystem.DelayInput(this.Waste());
        }
        public IEnumerator Waste()
        {
            List<Skill> list = new List<Skill>();

            foreach (Skill skill in this.BChar.MyTeam.Skills_UsedDeck)
            {
                if (skill.Master.Info.KeyData == ModItemKeys.Character_Unica)
                {
                    list.Add(skill);
                }
            }

            if (list.Count > 0)
            {
                foreach (Skill selectedSkill in list)
                {
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(selectedSkill);
                }

                foreach (Skill selectedSkill in list)
                {
                    BattleSystem.instance.AllyTeam.Skills_Deck.Add(selectedSkill);
                }

                BattleSystem.instance.AllyTeam.ShuffleDeck();
                BattleSystem.instance.AllyTeam.Draw();
            }

            yield return null;
            yield break;
        }
        public override void Init() 
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            {
                if (BattleSystem.instance.AllyTeam.Skills.FindAll((Skill s) => s != this.MySkill).Count >= 4)
                {
                    base.SkillParticleOn();
                    return;
                }
                base.SkillParticleOff();
            }
        }
    }
}
       