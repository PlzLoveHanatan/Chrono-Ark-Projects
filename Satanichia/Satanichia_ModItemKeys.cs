using ChronoArkMod;
namespace Satanichia
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Hein
		/// </summary>
        public static string Buff_B_Hein = "B_Hein";
        public static string Buff_B_Satanichia_DrawNextTurn = "B_Satanichia_DrawNextTurn";
        public static string SkillExtended_Ex_Satanichia_Hein = "Ex_Satanichia_Hein";
        public static string SkillExtended_Ex_Satanichia_Sheathe = "Ex_Satanichia_Sheathe";
		/// <summary>
		/// Passive:
		/// </summary>
        public static string Character_Satanichia = "Satanichia";
        public static string SkillEffect_SE_S_S_Satanichia_NetherRequiem = "SE_S_S_Satanichia_NetherRequiem";
        public static string SkillEffect_SE_T_S_Satanichia_DemonicJest = "SE_T_S_Satanichia_DemonicJest";
        public static string SkillEffect_SE_T_S_Satanichia_DevilsJape = "SE_T_S_Satanichia_DevilsJape";
        public static string SkillEffect_SE_T_S_Satanichia_DiabolicCharade = "SE_T_S_Satanichia_DiabolicCharade";
        public static string SkillEffect_SE_T_S_Satanichia_TrickstersGambit = "SE_T_S_Satanichia_TrickstersGambit";
        public static string SkillEffect_SE_T_S_Satanichia_TwilightTrick = "SE_T_S_Satanichia_TwilightTrick";
		/// <summary>
		/// Demonic Jest
		/// Discard the top skill in hand. If the discarded skill is Heal, cast this skill on an ally with lowest HP.
		/// Sheathe : Restore 1 Mana and draw 1 skill, prioritizing Healing skills.
		/// </summary>
        public static string Skill_S_Satanichia_DemonicJest = "S_Satanichia_DemonicJest";
		/// <summary>
		/// Devil's Jape
		/// Discard the bottom skill in hand. If the discarded skill is Attack, cast this skill on an enemy with lowest HP.
		/// Sheathe : Restore 1 Mana and draw 1 skill, prioritizing Attack skills.
		/// </summary>
        public static string Skill_S_Satanichia_DevilsJape = "S_Satanichia_DevilsJape";
		/// <summary>
		/// Diabolic Charade
		/// Cost reduced by 1 for each skill in hand.
		/// Discard up to 4 skills, for each skill discarded increase this skill's damage by 15%. Draw 2 additional skills next turn.
		/// Sheathe : Draw this skill again.
		/// </summary>
        public static string Skill_S_Satanichia_DiabolicCharade = "S_Satanichia_DiabolicCharade";
		/// <summary>
		/// Nether Requiem
		/// </summary>
        public static string Skill_S_Satanichia_NetherRequiem = "S_Satanichia_NetherRequiem";
		/// <summary>
		/// Trickster's Gambit
		/// When played from hand, discard highest Mana cost skill in hand. Increase this skill's damage by discarded skill Mana cost * 15%.
		/// Sheathe : Draw skills equal to the cost of this skill (Max 2). 
		/// </summary>
        public static string Skill_S_Satanichia_TrickstersGambit = "S_Satanichia_TrickstersGambit";
		/// <summary>
		/// Twilight Trick
		/// Deal &a additional damage.
		/// Sheathe : Permanently increase this skill's damage by &b for the rest of <b>this run</b>.
		/// </summary>
        public static string Skill_S_Satanichia_TwilightTrick = "S_Satanichia_TwilightTrick";

    }

    public static class ModLocalization
    {

    }
}