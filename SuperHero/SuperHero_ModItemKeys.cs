using ChronoArkMod;
namespace SuperHero
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Blinding Glory
		/// </summary>
        public static string Buff_B_SuperHero_BlindingGlory = "B_SuperHero_BlindingGlory";
		/// <summary>
		/// EGO Surge
		/// </summary>
        public static string Buff_B_SuperHero_EGOSurge = "B_SuperHero_EGOSurge";
		/// <summary>
		/// Hero Complex
		/// Increase all stats by 3% for each buff stack. Gain &a chance <color=#FF7C34>(5% * StackNum)</color> to target an ally.
		/// </summary>
        public static string Buff_B_SuperHero_HeroComplex = "B_SuperHero_HeroComplex";
		/// <summary>
		/// Hero Presence
		/// Cannot take action.
		/// </summary>
        public static string Buff_B_SuperHero_HeroPresence = "B_SuperHero_HeroPresence";
		/// <summary>
		/// Hero's Spotlight
		/// Can only target &target.
		/// Removed when this character attacks &target.
		/// </summary>
        public static string Buff_B_SuperHero_HerosSpotlight = "B_SuperHero_HerosSpotlight";
		/// <summary>
		/// Immortal Hero
		/// </summary>
        public static string Buff_B_SuperHero_ImmortalHero = "B_SuperHero_ImmortalHero";
		/// <summary>
		/// Mark of Justice
		/// </summary>
        public static string Buff_B_SuperHero_MarkofJustice = "B_SuperHero_MarkofJustice";
		/// <summary>
		/// Overpowered Protagonist
		/// HP cannot drop below 0.
		/// </summary>
        public static string Buff_B_SuperHero_OverpoweredProtagonist = "B_SuperHero_OverpoweredProtagonist";
		/// <summary>
		/// Plot Armor
		/// All incoming damage is reduced by 15%. Every turn restore &a HP <color=#FF7C34>(Hero Complex StackNum * 5%)</color>.
		/// </summary>
        public static string Buff_B_SuperHero_PlotArmor = "B_SuperHero_PlotArmor";
		/// <summary>
		/// Relentless Recovery
		/// At the start of each turn remove 2 random debuffs.
		/// </summary>
        public static string Buff_B_SuperHero_RelentlessRecovery = "B_SuperHero_RelentlessRecovery";
		/// <summary>
		/// Scarlet Remnant
		/// </summary>
        public static string Buff_B_SuperHero_ScarletRemnant = "B_SuperHero_ScarletRemnant";
		/// <summary>
		/// Blinding Glory
		/// <color=#919191>Accuracy -30%
		/// Evasion -30%
		/// Receiving Damage +15%
		/// Max 2 stack</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_BlindingGlory = "KeyWord_BlindingGlory";
		/// <summary>
		/// Hero Complex
		/// <color=#919191>Increase all stats by 3% for each buff stack. Gain &a chance <color=#FF7C34>(5% * StackNum)</color> to target an ally.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HeroComplex = "KeyWord_HeroComplex";
		/// <summary>
		/// Hero Presence
		/// <color=#919191>Cannot take action.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HeroPresence = "KeyWord_HeroPresence";
        public static string SkillEffect_SE_S_S_SuperHero_BloodstainedDress = "SE_S_S_SuperHero_BloodstainedDress";
        public static string SkillEffect_SE_S_S_SuperHero_TheApplauseNeverEnds = "SE_S_S_SuperHero_TheApplauseNeverEnds";
        public static string SkillEffect_SE_Tick_B_SuperHero_ScarletRemnant = "SE_Tick_B_SuperHero_ScarletRemnant";
        public static string SkillEffect_SE_T_SuperHero_WorldIsMine = "SE_T_SuperHero_WorldIsMine";
        public static string SkillEffect_SE_T_S_SuperHero_BloodstainedDress = "SE_T_S_SuperHero_BloodstainedDress";
        public static string SkillEffect_SE_T_S_SuperHero_ErasetheMobs = "SE_T_S_SuperHero_ErasetheMobs";
        public static string SkillEffect_SE_T_S_SuperHero_IntheNameofJustice = "SE_T_S_SuperHero_IntheNameofJustice";
        public static string SkillEffect_SE_T_S_SuperHero_UnwantedSuccessStory = "SE_T_S_SuperHero_UnwantedSuccessStory";
        public static string SkillEffect_SE_T_S_SuperHero_WorldIsMine = "SE_T_S_SuperHero_WorldIsMine";
		/// <summary>
		/// Super Hero
		/// Passive:
		/// </summary>
        public static string Character_SuperHero = "SuperHero";
		/// <summary>
		/// Bloodstained Dress
		/// </summary>
        public static string Skill_S_SuperHero_BloodstainedDress = "S_SuperHero_BloodstainedDress";
		/// <summary>
		/// Erase the Mobs
		/// If the target's health is below 40% (20% for bosses), <b>reduce the target's health to 0</b>.
		/// Otherwise gain 3 'Hero Complex'.
		/// </summary>
        public static string Skill_S_SuperHero_ErasetheMobs = "S_SuperHero_ErasetheMobs";
		/// <summary>
		/// In the Name of Justice
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice = "S_SuperHero_IntheNameofJustice";
		/// <summary>
		/// The Applause Never Ends
		/// Only Super Hero can use this skill.
		/// Apply 'Hero's Spotlight' to all enemies and gain &a Barrier <color=#FF7C34>(Max HP * 0.5)</color>.
		/// </summary>
        public static string Skill_S_SuperHero_TheApplauseNeverEnds = "S_SuperHero_TheApplauseNeverEnds";
		/// <summary>
		/// Unwanted Success Story
		/// Target a random ally.
		/// </summary>
        public static string Skill_S_SuperHero_UnwantedSuccessStory = "S_SuperHero_UnwantedSuccessStory";
		/// <summary>
		/// World Is Mine
		/// Apply 'Blinding Glory' and 'Hero Presence' to all enemies and 'Hero Presence' to all allies.
		/// </summary>
        public static string Skill_S_SuperHero_WorldIsMine = "S_SuperHero_WorldIsMine";
		/// <summary>
		/// Ego Shield
		/// </summary>
        public static string Buff_B_SuperHero_EgoShield = "B_SuperHero_EgoShield";
		/// <summary>
		/// Hero's Spotlight
		/// <color=#919191>Can only target Super Hero.
		/// Removed when this character attacks Super Hero.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HerosSpotlight = "KeyWord_HerosSpotlight";
        public static string Skill_S_SuperHero_DummyHeal = "S_SuperHero_DummyHeal";
		/// <summary>
		/// Limit Break
		/// Only Super Hero can use this skill.
		/// Remove all debbufs.
		/// </summary>
        public static string Skill_S_SuperHero_LimitBreak = "S_SuperHero_LimitBreak";
        public static string SkillEffect_SE_S_S_SuperHero_LimitBreak = "SE_S_S_SuperHero_LimitBreak";
		/// <summary>
		/// Second Act
		/// At the start of each turn draw 1 skill and Restore 1 Mana.
		/// </summary>
        public static string Buff_B_SuperHero_SecondAct = "B_SuperHero_SecondAct";
		/// <summary>
		/// Overflowing with Light 
		/// Only Super Hero can use this skill.
		/// Remove overload from Lucy, restore 2 mana and draw 2 skills
		/// </summary>
        public static string Skill_S_SuperHero_OverflowingwithLight = "S_SuperHero_OverflowingwithLight";
        public static string SkillEffect_SE_S_S_SuperHero_OverflowingwithLight = "SE_S_S_SuperHero_OverflowingwithLight";

    }

    public static class ModLocalization
    {

    }
}