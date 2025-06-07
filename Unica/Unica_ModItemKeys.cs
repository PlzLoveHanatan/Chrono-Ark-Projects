using ChronoArkMod;
namespace Unica
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Bottom Deal
		/// Draw 1 additional skills next turn.
		/// </summary>
        public static string Buff_BottomDeal = "BottomDeal";
		/// <summary>
		/// Brace Up
		/// Discard a skill with the lowest Mana Cost.
		/// Sheathe : Draw 2 additional skills next turn and increase the damage of your next skill by 15%.
		/// </summary>
        public static string Skill_BraceUp = "BraceUp";
		/// <summary>
		/// Daring Decision
		/// Discard all skills in hand and increase this skill's damage by &a for each skill discarded.
		/// For every 2 skills discarded, draw 1 additional skill next turn (Max 2).
		/// For every 4 skills discarded, immediately draw 1 skill (Max 2).
		/// </summary>
        public static string Skill_DaringDecision = "DaringDecision";
		/// <summary>
		/// Double Down
		/// Next skill will cost 1 less.
		/// </summary>
        public static string Buff_DoubleDown = "DoubleDown";
		/// <summary>
		/// Draw Next Turn
		/// At the start of next turn, draw &a more skills.
		/// </summary>
        public static string Buff_DrawNextTurn = "DrawNextTurn";
		/// <summary>
		/// Equivalent Exchange
		/// Discard a skill with the lowest Mana Cost and restore 1 mana.
		/// Sheathe : Increase the damage of your next skill by 30%.
		/// </summary>
        public static string Skill_Equivalent_Exchange = "Equivalent_Exchange";
		/// <summary>
		/// Sheathe : Cast this skill. Draw 1 additional skill next turn.
		/// <sprite name="비용2"><sprite name="이하">
		/// </summary>
        public static string SkillExtended_ExSheathe = "ExSheathe";
		/// <summary>
		/// Favorable Hand
		/// Increases the damage of your next skill by 15%.
		/// </summary>
        public static string Buff_FavorableHand = "FavorableHand";
		/// <summary>
		/// Fierce Onslaught
		/// Discard a skill with the lowest Mana Cost.
		/// Sheathe : Cast this skill.
		/// </summary>
        public static string Skill_FierceOnslaught = "FierceOnslaught";
		/// <summary>
		/// Impugnatio Ultima
		/// Discard all skills in hand and increase this skill's damage by &a for each skill discarded.
		/// For every 2 skills discarded, draw 1 additional skill next turn (Max 2).
		/// For every 4 skills discarded, immediately draw 1 skill (Max 2).
		/// Sheathe : Draw this skill again.
		/// </summary>
        public static string Skill_ImpugnatioUltima = "ImpugnatioUltima";
		/// <summary>
		/// Insurance Coverage
		/// </summary>
        public static string Buff_InsuranceCoverage = "InsuranceCoverage";
		/// <summary>
		/// Insurance Plan
		/// </summary>
        public static string Buff_InsurancePlan = "InsurancePlan";
		/// <summary>
		/// In Times Like These!
		/// </summary>
        public static string Buff_InTimesLikeThese = "InTimesLikeThese";
		/// <summary>
		/// In Times Like These!
		/// </summary>
        public static string Buff_InTimesLikeThese_0 = "InTimesLikeThese_0";
		/// <summary>
		/// Flip The Table
		/// Draw 3 skills and select skill to discard.
		/// </summary>
        public static string Skill_LDraw_FlipTheTable = "LDraw_FlipTheTable";
		/// <summary>
		/// Low at Night, High at Day
		/// Sheathe : Draw 1 additional skill next turn. Increase the damage of your next skill by 15%.
		/// </summary>
        public static string Skill_LowatNight_HighatDay = "LowatNight_HighatDay";
		/// <summary>
		/// Margin
		/// At the end of each turn, gain 1 additional Mana next turn and draw 1 page if the hand is empty.
		/// </summary>
        public static string Buff_Margin = "Margin";
		/// <summary>
		/// Multi-Party Compensation
		/// If facing 1 enemy, damage is increased by &a.
		/// When played from hand, discard a skill with the lowest Mana Cost, remove one random debuff from all allies, and apply 'Insurance Plan' to all allies.
		/// Sheathe :  Cast this skill, draw 1 skill, restore 1 Mana, and apply 'Insurance Coverage' to all allies.
		/// </summary>
        public static string Skill_Multi_Party_Compensation = "Multi_Party_Compensation";
		/// <summary>
		/// Outburst
		/// Discard a skill with the lowest Mana Cost and draw 1 skill.
		/// </summary>
        public static string Skill_Outburst = "Outburst";
		/// <summary>
		/// Sakura
		/// If you have 4 or more skills in hand (excluding this one), randomly discard 3 skills.
		/// For each skill discarded, increase this skill's damage by &a.
		/// Draw 2 additional skills next turn.
		/// </summary>
        public static string Skill_Sakura = "Sakura";
        public static string SkillEffect_SE_T_BraceUp = "SE_T_BraceUp";
        public static string SkillEffect_SE_T_DaringDecision = "SE_T_DaringDecision";
        public static string SkillEffect_SE_T_Equivalent_Exchange = "SE_T_Equivalent_Exchange";
        public static string SkillEffect_SE_T_FierceOnslaught = "SE_T_FierceOnslaught";
        public static string SkillEffect_SE_T_ImpugnatioUltima = "SE_T_ImpugnatioUltima";
        public static string SkillEffect_SE_T_LowatNight_HighatDay = "SE_T_LowatNight_HighatDay";
        public static string SkillEffect_SE_T_Multi_Party_Compensation = "SE_T_Multi_Party_Compensation";
        public static string SkillEffect_SE_T_Outburst = "SE_T_Outburst";
        public static string SkillEffect_SE_T_Relay = "SE_T_Relay";
        public static string SkillEffect_SE_T_Sakura = "SE_T_Sakura";
        public static string SkillEffect_SE_T_ShuffleHands = "SE_T_ShuffleHands";
        public static string SkillEffect_SE_T_StayCalm = "SE_T_StayCalm";
        public static string SkillEffect_SE_T_Switcheroo = "SE_T_Switcheroo";
        public static string SkillEffect_SE_T_WaterSplash = "SE_T_WaterSplash";
        public static string SkillEffect_SE_T_WaterSplash_0 = "SE_T_WaterSplash_0";
		/// <summary>
		/// Shuffle Hands
		/// Discard the skill with the lowest Mana Cost. Draw 1 additional skill next turn.
		/// Sheathe: Take all of the user's skills from the discard pile and shuffle them back into your deck and draw 1 skill.
		/// </summary>
        public static string Skill_ShuffleHands = "ShuffleHands";
		/// <summary>
		/// Stacking the Deck
		/// Restore 1 additional mana next turn.
		/// </summary>
        public static string Buff_StackingtheDeck = "StackingtheDeck";
		/// <summary>
		/// Stay Calm
		/// Sheathe : Restore 1 Mana and draw 1 random user's skill from the discard pile.
		/// </summary>
        public static string Skill_StayCalm = "StayCalm";
		/// <summary>
		/// Switcheroo
		/// Sheathe : Draw 1 skill. Next skill will cost 1 less.
		/// </summary>
        public static string Skill_Switcheroo = "Switcheroo";
		/// <summary>
		/// Unica
		/// Passive:
		/// Whenever this character discards a skill, restore 2 HP.
		/// If this character discarded at least one skill this turn, draw 1 additional skill and restore 1 additional mana on the next turn.
		/// At the end of each turn, if the hand is empty, draw 1 additional skill and restore 1 additional mana mana on the next turn.
		/// If there are 3 or fewer skills in hand at the end of the turn, gain 15% bonus Attack Power for the next turn.
		/// </summary>
        public static string Character_Unica = "Unica";
		/// <summary>
		/// Water Splash
		/// Discard the skill with the lowest Mana Cost.
		/// If you have another 'Water Splash' in hand, discard it instead. Then, cast this skill on the target and draw 1 skill. (Max 3 times)
		/// </summary>
        public static string Skill_WaterSplash = "WaterSplash";
		/// <summary>
		/// Water Splash
		/// </summary>
        public static string Skill_WaterSplash_0 = "WaterSplash_0";
		/// <summary>
		/// Weakening Impact
		/// </summary>
        public static string Buff_WeakeningImpact = "WeakeningImpact";
		/// <summary>
		/// Winning Hand
		/// Increases the damage of your next skill by 30%.
		/// </summary>
        public static string Buff_WinningHand = "WinningHand";

    }

    public static class ModLocalization
    {

    }
}