using System.Collections.Generic;
using Dialogical;
using GameDataEditor;
using Spine;
using UnityEngine;

namespace projectEGO
{
    public class EGO_System : MonoBehaviour, IP_Awake
    {
        private bool FirstAwake = false;

        public List<string> dynamicAvailableEGOskills = new List<string>();

        private List<Skill> Hand = new List<Skill>();
        private List<Skill> DrawPile = new List<Skill>();
        private List<Skill> DiscardPile = new List<Skill>();

        private List<string> LucySkills = new List<string>
        {
            GDEItemKeys.Skill_S_Lucy_17,
            GDEItemKeys.Skill_S_Lucy_18,
            GDEItemKeys.Skill_S_Lucy_19,
            GDEItemKeys.Skill_S_Lucy_20,
        };

        private bool egoActive = false;

        public void Awake()
        {
            if (!FirstAwake)
            {
                dynamicAvailableEGOskills.Clear();
                FirstAwake = true;
            }
        }
        public void Call()
        {
            if (!egoActive)
                SwitchToEGO();
            else
                SwitchToNormal();
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
            }

            team.Skills_Deck.Clear();
            team.Skills_UsedDeck.Clear();

            foreach (var key in LucySkills)
            {
                Skill skill = Skill.TempSkill(key, team.LucyAlly);
                if (skill != null)
                {
                    BattleSystem.instance.StartCoroutine(team.AddSkillNoDrawEffect(skill, -1));
                }
            }

            egoActive = true;
            Debug.Log("EGO activated: original skills stored, EGO skills loaded.");
        }

        public void SwitchToNormal()
        {
            var team = BattleSystem.instance.AllyTeam;

            for (int j = team.Skills.Count - 1; j >= 0; j--)
            {
                Skill skill = team.Skills[j];
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
            Debug.Log($"EGO deactivated: original skills restored. " +
                      $"Restored {Hand.Count} hand, {DrawPile.Count} deck, {DiscardPile.Count} discard.");
        }
    }
}
