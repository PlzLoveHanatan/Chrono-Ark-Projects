using System.Collections.Generic;
using System.Linq;
using Dialogical;
using EmotionalSystem;
using GameDataEditor;
using Spine;
using UnityEngine;

namespace EmotionalSystem
{
    public class EGO_System : MonoBehaviour
    {
        public static EGO_System instance;

        public List<Skill> dynamicAvailableEGOskills = new List<Skill>();

        private List<Skill> Hand = new List<Skill>();
        private List<Skill> DrawPile = new List<Skill>();
        private List<Skill> DiscardPile = new List<Skill>();
        private int ExchangeNum = 0;

        public bool egoActive = false;

        public bool hasEGOSkill => dynamicAvailableEGOskills.Count > 0;

        public void Awake()
        {
            instance = this;
        }
        public void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        public void TurnUpdate()
        {
            foreach (var skill in dynamicAvailableEGOskills)
            {
                var ex = skill.ExtendedFind<Ex_EmotionalSystem_EGO>();
                if (ex != null && ex.NowCountdown > 0)
                {
                    ex.NowCountdown--;
                }
            }
        }

        public void AddEGOSkill(Skill skill)
        {
            //skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_EmotionalSystem_EGO);
            dynamicAvailableEGOskills.Add(skill);
        }
        public void RemoveEGOSkill(Skill skill)
        {
            dynamicAvailableEGOskills.Remove(skill);
        }

        public void SwitchToEGO()
        {
            var team = BattleSystem.instance.AllyTeam;

            Hand.Clear();
            DrawPile.Clear();
            DiscardPile.Clear();

            Hand.AddRange(team.Skills);
            DrawPile.AddRange(team.Skills_Deck);
            DiscardPile.AddRange(team.Skills_UsedDeck);

            for (int i = team.Skills.Count - 1; i >= 0; i--)
            {
                Skill skill = team.Skills[i];
                skill.Remove();
                if (skill.MySkill.KeyID == GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard)
                {
                    dynamicAvailableEGOskills.Add(skill);
                }
            }

            team.Skills_Deck.Clear();
            team.Skills_UsedDeck.Clear();

            foreach (Skill skill in dynamicAvailableEGOskills.Where(sk => !sk.Master.IsDead))
            {
                BattleSystem.instance.StartCoroutine(team.AddSkillNoDrawEffect(skill, -1));
            }

            egoActive = true;

            // don't allow exchange in EGO skills
            ExchangeNum = team.DiscardCount;
            team.DiscardCount = 0;
        }

        public void SwitchToNormal()
        {
            var team = BattleSystem.instance.AllyTeam;

            for (int j = team.Skills.Count - 1; j >= 0; j--)
            {
                Skill skill = team.Skills[j];
                if (!dynamicAvailableEGOskills.Contains(skill))
                {
                    Hand.Add(skill);
                }

                if (skill.MySkill.KeyID == GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard)
                {
                    dynamicAvailableEGOskills.Remove(skill);
                }

                skill.Remove();
            }

            team.Skills.Clear();
            team.Skills_Deck.Clear();
            team.Skills_UsedDeck.Clear();

            team.Skills_Deck.AddRange(DrawPile);
            team.Skills_UsedDeck.AddRange(DiscardPile);

            foreach (var skill in Hand)
            {
                BattleSystem.instance.StartCoroutine(team.AddSkillNoDrawEffect(skill, -1));
            }

            egoActive = false;

            // return back the exchange count
            team.DiscardCount = ExchangeNum;
        }
    }
}
