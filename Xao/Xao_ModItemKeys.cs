using ChronoArkMod;
namespace Xao
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Affection <sprite name="Xao_Heart">
		/// Spend 1 <sprite name="Xao_Heart"> to remove Overload.
		/// <color=#919191>You can activate this buff by left-clicking. Cannot be activated if user is stunned.</color>
		/// </summary>
        public static string Buff_B_Xao_Affection = "B_Xao_Affection";
		/// <summary>
		/// Affection <sprite name="Xao_Heart">
		/// Spend 1 <sprite name="Xao_Heart"> to remove Overload.
		/// <color=#919191>You can activate this buff by left-clicking. Cannot be activated if user is stunned.</color>
		/// </summary>
        public static string Buff_B_Xao_Affection_Ally = "B_Xao_Affection_Ally";
		/// <summary>
		/// Affection <sprite name="Xao_Heart">
		/// Spend 1 <sprite name="Xao_Heart"> to remove Overload.
		/// <color=#919191>You can activate this buff by left-clicking. Cannot be activated if user is stunned.</color>
		/// </summary>
        public static string Buff_B_Xao_Affection_Ally_Synergy = "B_Xao_Affection_Ally_Synergy";
		/// <summary>
		/// Aroused Bliss
		/// </summary>
        public static string Buff_B_Xao_ArousedBliss = "B_Xao_ArousedBliss";
		/// <summary>
		/// Combo
		/// </summary>
        public static string Buff_B_Xao_Combo = "B_Xao_Combo";
		/// <summary>
		/// Forbidden Desire
		/// Increase <color=#87CEFA>Combo</color> rewards for this battle.
		/// </summary>
        public static string Buff_B_Xao_ForbiddenDesire = "B_Xao_ForbiddenDesire";
		/// <summary>
		/// Starry Tease
		/// 50% chance to deal &a additional damage <color=#FF7C34>(Attack * 0.2)</color> to the target.
		/// </summary>
        public static string Buff_B_Xao_MagicalDay_0 = "B_Xao_MagicalDay_0";
		/// <summary>
		/// Magical Ecstasy
		/// </summary>
        public static string Buff_B_Xao_MagicalDay_1 = "B_Xao_MagicalDay_1";
		/// <summary>
		/// Magical Climax
		/// Keep your current <color=#87CEFA>Combo</color> for the next turn.
		/// At the start of next turn restore 1 Mana and remove this buff.
		/// </summary>
        public static string Buff_B_Xao_MagicalDay_2 = "B_Xao_MagicalDay_2";
		/// <summary>
		/// Mistress' Dominance
		/// Next skill will cost 1 less.
		/// </summary>
        public static string Buff_B_Xao_MistressDominance = "B_Xao_MistressDominance";
		/// <summary>
		/// Mistress' Touch
		/// </summary>
        public static string Buff_B_Xao_MistressTouch = "B_Xao_MistressTouch";
		/// <summary>
		/// Normal Mod
		/// Just Visual Buff
		/// </summary>
        public static string Buff_B_Xao_Mod_0 = "B_Xao_Mod_0";
		/// <summary>
		/// Horny
		/// At the start of turn, if Xao already has 3 <sprite name="Xao_Heart">, create a random swiftness 0-Cost Xao skill. Otherwise, she gains 1 <sprite name="Xao_Heart"> (up to 3).
		/// </summary>
        public static string Buff_B_Xao_Mod_1 = "B_Xao_Mod_1";
		/// <summary>
		/// Pleasure Lock
		/// Cannot take action.
		/// </summary>
        public static string Buff_B_Xao_PleasureLock = "B_Xao_PleasureLock";
		/// <summary>
		/// Soaked Lust
		/// </summary>
        public static string Buff_B_Xao_SoakedLust = "B_Xao_SoakedLust";
        public static string Buff_B_Xao_S_SimpleExchange = "B_Xao_S_SimpleExchange";
		/// <summary>
		/// Wet Bliss
		/// Whenever playing skill increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color> and remove 1 stack.
		/// </summary>
        public static string Buff_B_Xao_WetBliss = "B_Xao_WetBliss";
		/// <summary>
		/// Wet Dream
		/// Cannot gain Overload.
		/// </summary>
        public static string Buff_B_Xao_WetDream = "B_Xao_WetDream";
		/// <summary>
		/// Love Egg
		/// At the start of each turn gain 1 <sprite name="Xao_Heart"> (up to 3).
		/// <color=#919191>Can be equipped on any ally.</color>
		/// </summary>
        public static string Item_Equip_Equip_Xao_LoveEgg = "Equip_Xao_LoveEgg";
		/// <summary>
		/// Magic Wand
		/// Whenever playing skill increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color>.
		/// </summary>
        public static string Item_Equip_Equip_Xao_MagicWand = "Equip_Xao_MagicWand";
		/// <summary>
		/// When played, increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color> and, if <color=#87CEFA>Combo</color> is <color=#d78fe9>4</color> or more, <b>permanently</b> increase one stat of the ally based on their Role.
		/// Attack Role : Attack Power +1
		/// Tank Role : Armor +3%
		/// Support Role : Healing Power +1</color>
		/// </summary>
        public static string SkillExtended_Ex_Xao_0 = "Ex_Xao_0";
		/// <summary>
		/// When played increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color> and gain 1 <sprite name="Xao_Heart"> (Max 1).
		/// Keep your current <color=#87CEFA>Combo</color> for the next turn.
		/// </summary>
        public static string SkillExtended_Ex_Xao_1 = "Ex_Xao_1";
		/// <summary>
		/// Mistress' Dominance
		/// Next skill will cost 1 less.
		/// </summary>
        public static string SkillExtended_Ex_Xao_MistressDominance = "Ex_Xao_MistressDominance";
        public static string SkillExtended_Ex_Xao_Rare = "Ex_Xao_Rare";
		/// <summary>
		/// <color=#87CEFA>Combo</color>
		/// <color=#919191>When you gain <color=#87CEFA>Combo</color> in a turn, receive rewards based on your <color=#87CEFA>Combo</color> count.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Combo = "KeyWord_Combo";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_0 = "SE_S_S_Xao_BikiniTime_0";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_1 = "SE_S_S_Xao_BikiniTime_1";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_2 = "SE_S_S_Xao_BikiniTime_2";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_3 = "SE_S_S_Xao_BikiniTime_3";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_Love_0 = "SE_S_S_Xao_BikiniTime_Love_0";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_Love_1 = "SE_S_S_Xao_BikiniTime_Love_1";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_Love_2 = "SE_S_S_Xao_BikiniTime_Love_2";
        public static string SkillEffect_SE_S_S_Xao_BikiniTime_Love_3 = "SE_S_S_Xao_BikiniTime_Love_3";
        public static string SkillEffect_SE_S_S_Xao_CowGirl_2 = "SE_S_S_Xao_CowGirl_2";
        public static string SkillEffect_SE_S_S_Xao_CowGirl_Love_2 = "SE_S_S_Xao_CowGirl_Love_2";
        public static string SkillEffect_SE_S_S_Xao_ExperienceMaidFootjob_2 = "SE_S_S_Xao_ExperienceMaidFootjob_2";
        public static string SkillEffect_SE_S_S_Xao_ExperienceMaidFootjob_Love_2 = "SE_S_S_Xao_ExperienceMaidFootjob_Love_2";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_0 = "SE_S_S_Xao_MagicalGirlPussy_0";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_1 = "SE_S_S_Xao_MagicalGirlPussy_1";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_2 = "SE_S_S_Xao_MagicalGirlPussy_2";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_3 = "SE_S_S_Xao_MagicalGirlPussy_3";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_Love_0 = "SE_S_S_Xao_MagicalGirlPussy_Love_0";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_Love_1 = "SE_S_S_Xao_MagicalGirlPussy_Love_1";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_Love_2 = "SE_S_S_Xao_MagicalGirlPussy_Love_2";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlPussy_Love_3 = "SE_S_S_Xao_MagicalGirlPussy_Love_3";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_0 = "SE_S_S_Xao_MagicalGirlThighjob_0";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_1 = "SE_S_S_Xao_MagicalGirlThighjob_1";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_2 = "SE_S_S_Xao_MagicalGirlThighjob_2";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_3 = "SE_S_S_Xao_MagicalGirlThighjob_3";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_Love_0 = "SE_S_S_Xao_MagicalGirlThighjob_Love_0";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_Love_1 = "SE_S_S_Xao_MagicalGirlThighjob_Love_1";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_Love_2 = "SE_S_S_Xao_MagicalGirlThighjob_Love_2";
        public static string SkillEffect_SE_S_S_Xao_MagicalGirlThighjob_Love_3 = "SE_S_S_Xao_MagicalGirlThighjob_Love_3";
        public static string SkillEffect_SE_S_S_Xao_MaidPanties_0 = "SE_S_S_Xao_MaidPanties_0";
        public static string SkillEffect_SE_S_S_Xao_MaidPanties_1 = "SE_S_S_Xao_MaidPanties_1";
        public static string SkillEffect_SE_S_S_Xao_MaidPanties_2 = "SE_S_S_Xao_MaidPanties_2";
        public static string SkillEffect_SE_S_S_Xao_MaidPanties_Love_0 = "SE_S_S_Xao_MaidPanties_Love_0";
        public static string SkillEffect_SE_S_S_Xao_MaidPanties_Love_1 = "SE_S_S_Xao_MaidPanties_Love_1";
        public static string SkillEffect_SE_S_S_Xao_MaidPanties_Love_2 = "SE_S_S_Xao_MaidPanties_Love_2";
        public static string SkillEffect_SE_S_S_Xao_MikoExperienceAnal_2 = "SE_S_S_Xao_MikoExperienceAnal_2";
        public static string SkillEffect_SE_S_S_Xao_MikoExperienceAnal_Love_2 = "SE_S_S_Xao_MikoExperienceAnal_Love_2";
        public static string SkillEffect_SE_S_S_Xao_MikoExperiencePussy_3 = "SE_S_S_Xao_MikoExperiencePussy_3";
        public static string SkillEffect_SE_S_S_Xao_MikoExperiencePussy_Love_3 = "SE_S_S_Xao_MikoExperiencePussy_Love_3";
        public static string SkillEffect_SE_S_S_Xao_Rare_SleepSex_0 = "SE_S_S_Xao_Rare_SleepSex_0";
        public static string SkillEffect_SE_S_S_Xao_Rare_SleepSex_01 = "SE_S_S_Xao_Rare_SleepSex_01";
        public static string SkillEffect_SE_S_S_Xao_Rare_SleepSex_1 = "SE_S_S_Xao_Rare_SleepSex_1";
        public static string SkillEffect_SE_S_S_Xao_Rare_SleepSex_2 = "SE_S_S_Xao_Rare_SleepSex_2";
        public static string SkillEffect_SE_S_S_Xao_SwimsuitDay_2 = "SE_S_S_Xao_SwimsuitDay_2";
        public static string SkillEffect_SE_S_S_Xao_SwimsuitDay_Love_2 = "SE_S_S_Xao_SwimsuitDay_Love_2";
        public static string SkillEffect_SE_Tick_B_Xao_MistressTouch = "SE_Tick_B_Xao_MistressTouch";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_0 = "SE_T_S_Xao_BikiniTime_0";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_1 = "SE_T_S_Xao_BikiniTime_1";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_2 = "SE_T_S_Xao_BikiniTime_2";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_3 = "SE_T_S_Xao_BikiniTime_3";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_Love_0 = "SE_T_S_Xao_BikiniTime_Love_0";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_Love_1 = "SE_T_S_Xao_BikiniTime_Love_1";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_Love_2 = "SE_T_S_Xao_BikiniTime_Love_2";
        public static string SkillEffect_SE_T_S_Xao_BikiniTime_Love_3 = "SE_T_S_Xao_BikiniTime_Love_3";
        public static string SkillEffect_SE_T_S_Xao_B_MagicalDay = "SE_T_S_Xao_B_MagicalDay";
        public static string SkillEffect_SE_T_S_Xao_CowGirl_0 = "SE_T_S_Xao_CowGirl_0";
        public static string SkillEffect_SE_T_S_Xao_CowGirl_1 = "SE_T_S_Xao_CowGirl_1";
        public static string SkillEffect_SE_T_S_Xao_CowGirl_2 = "SE_T_S_Xao_CowGirl_2";
        public static string SkillEffect_SE_T_S_Xao_CowGirl_Love_0 = "SE_T_S_Xao_CowGirl_Love_0";
        public static string SkillEffect_SE_T_S_Xao_CowGirl_Love_1 = "SE_T_S_Xao_CowGirl_Love_1";
        public static string SkillEffect_SE_T_S_Xao_CowGirl_Love_2 = "SE_T_S_Xao_CowGirl_Love_2";
        public static string SkillEffect_SE_T_S_Xao_ExperienceMaidFootjob_0 = "SE_T_S_Xao_ExperienceMaidFootjob_0";
        public static string SkillEffect_SE_T_S_Xao_ExperienceMaidFootjob_1 = "SE_T_S_Xao_ExperienceMaidFootjob_1";
        public static string SkillEffect_SE_T_S_Xao_ExperienceMaidFootjob_2 = "SE_T_S_Xao_ExperienceMaidFootjob_2";
        public static string SkillEffect_SE_T_S_Xao_ExperienceMaidFootjob_Love_0 = "SE_T_S_Xao_ExperienceMaidFootjob_Love_0";
        public static string SkillEffect_SE_T_S_Xao_ExperienceMaidFootjob_Love_1 = "SE_T_S_Xao_ExperienceMaidFootjob_Love_1";
        public static string SkillEffect_SE_T_S_Xao_ExperienceMaidFootjob_Love_2 = "SE_T_S_Xao_ExperienceMaidFootjob_Love_2";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_0 = "SE_T_S_Xao_MagicalGirlPussy_0";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_1 = "SE_T_S_Xao_MagicalGirlPussy_1";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_2 = "SE_T_S_Xao_MagicalGirlPussy_2";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_3 = "SE_T_S_Xao_MagicalGirlPussy_3";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_Love_0 = "SE_T_S_Xao_MagicalGirlPussy_Love_0";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_Love_1 = "SE_T_S_Xao_MagicalGirlPussy_Love_1";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_Love_2 = "SE_T_S_Xao_MagicalGirlPussy_Love_2";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlPussy_Love_3 = "SE_T_S_Xao_MagicalGirlPussy_Love_3";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_0 = "SE_T_S_Xao_MagicalGirlThighjob_0";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_1 = "SE_T_S_Xao_MagicalGirlThighjob_1";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_2 = "SE_T_S_Xao_MagicalGirlThighjob_2";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_3 = "SE_T_S_Xao_MagicalGirlThighjob_3";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_Love_0 = "SE_T_S_Xao_MagicalGirlThighjob_Love_0";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_Love_1 = "SE_T_S_Xao_MagicalGirlThighjob_Love_1";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_Love_2 = "SE_T_S_Xao_MagicalGirlThighjob_Love_2";
        public static string SkillEffect_SE_T_S_Xao_MagicalGirlThighjob_Love_3 = "SE_T_S_Xao_MagicalGirlThighjob_Love_3";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_0 = "SE_T_S_Xao_MaidPanties_0";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_01 = "SE_T_S_Xao_MaidPanties_01";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_1 = "SE_T_S_Xao_MaidPanties_1";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_2 = "SE_T_S_Xao_MaidPanties_2";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_Love_0 = "SE_T_S_Xao_MaidPanties_Love_0";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_Love_1 = "SE_T_S_Xao_MaidPanties_Love_1";
        public static string SkillEffect_SE_T_S_Xao_MaidPanties_Love_2 = "SE_T_S_Xao_MaidPanties_Love_2";
        public static string SkillEffect_SE_T_S_Xao_MikoExperienceAnal_0 = "SE_T_S_Xao_MikoExperienceAnal_0";
        public static string SkillEffect_SE_T_S_Xao_MikoExperienceAnal_1 = "SE_T_S_Xao_MikoExperienceAnal_1";
        public static string SkillEffect_SE_T_S_Xao_MikoExperienceAnal_2 = "SE_T_S_Xao_MikoExperienceAnal_2";
        public static string SkillEffect_SE_T_S_Xao_MikoExperienceAnal_Love_0 = "SE_T_S_Xao_MikoExperienceAnal_Love_0";
        public static string SkillEffect_SE_T_S_Xao_MikoExperienceAnal_Love_1 = "SE_T_S_Xao_MikoExperienceAnal_Love_1";
        public static string SkillEffect_SE_T_S_Xao_MikoExperienceAnal_Love_2 = "SE_T_S_Xao_MikoExperienceAnal_Love_2";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_0 = "SE_T_S_Xao_MikoExperiencePussy_0";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_1 = "SE_T_S_Xao_MikoExperiencePussy_1";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_2 = "SE_T_S_Xao_MikoExperiencePussy_2";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_3 = "SE_T_S_Xao_MikoExperiencePussy_3";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_Love_0 = "SE_T_S_Xao_MikoExperiencePussy_Love_0";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_Love_1 = "SE_T_S_Xao_MikoExperiencePussy_Love_1";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_Love_2 = "SE_T_S_Xao_MikoExperiencePussy_Love_2";
        public static string SkillEffect_SE_T_S_Xao_MikoExperiencePussy_Love_3 = "SE_T_S_Xao_MikoExperiencePussy_Love_3";
        public static string SkillEffect_SE_T_S_Xao_Rare_SimpleExchange_0 = "SE_T_S_Xao_Rare_SimpleExchange_0";
        public static string SkillEffect_SE_T_S_Xao_Rare_SimpleExchange_01 = "SE_T_S_Xao_Rare_SimpleExchange_01";
        public static string SkillEffect_SE_T_S_Xao_Rare_SimpleExchange_1 = "SE_T_S_Xao_Rare_SimpleExchange_1";
        public static string SkillEffect_SE_T_S_Xao_Rare_SimpleExchange_2 = "SE_T_S_Xao_Rare_SimpleExchange_2";
        public static string SkillEffect_SE_T_S_Xao_Rare_SimpleExchange_3 = "SE_T_S_Xao_Rare_SimpleExchange_3";
        public static string SkillEffect_SE_T_S_Xao_Rare_SimpleExchange_4 = "SE_T_S_Xao_Rare_SimpleExchange_4";
        public static string SkillEffect_SE_T_S_Xao_Rare_SleepSex = "SE_T_S_Xao_Rare_SleepSex";
        public static string SkillEffect_SE_T_S_Xao_Rare_SleepSex_0 = "SE_T_S_Xao_Rare_SleepSex_0";
        public static string SkillEffect_SE_T_S_Xao_Rare_SleepSex_01 = "SE_T_S_Xao_Rare_SleepSex_01";
        public static string SkillEffect_SE_T_S_Xao_Rare_SleepSex_0_Horny = "SE_T_S_Xao_Rare_SleepSex_0_Horny";
        public static string SkillEffect_SE_T_S_Xao_Rare_SleepSex_1 = "SE_T_S_Xao_Rare_SleepSex_1";
        public static string SkillEffect_SE_T_S_Xao_Rare_SleepSex_2 = "SE_T_S_Xao_Rare_SleepSex_2";
        public static string SkillEffect_SE_T_S_Xao_SwimsuitDay_0 = "SE_T_S_Xao_SwimsuitDay_0";
        public static string SkillEffect_SE_T_S_Xao_SwimsuitDay_1 = "SE_T_S_Xao_SwimsuitDay_1";
        public static string SkillEffect_SE_T_S_Xao_SwimsuitDay_2 = "SE_T_S_Xao_SwimsuitDay_2";
        public static string SkillEffect_SE_T_S_Xao_SwimsuitDay_Love_0 = "SE_T_S_Xao_SwimsuitDay_Love_0";
        public static string SkillEffect_SE_T_S_Xao_SwimsuitDay_Love_1 = "SE_T_S_Xao_SwimsuitDay_Love_1";
        public static string SkillEffect_SE_T_S_Xao_SwimsuitDay_Love_2 = "SE_T_S_Xao_SwimsuitDay_Love_2";
		/// <summary>
		/// Bikini Time ♡
		/// Create a <color=#d78fe9>Bikini Time ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_0 = "S_Xao_BikiniTime_0";
		/// <summary>
		/// Bikini Time ♡♡
		/// Create a <color=#d78fe9>Bikini Time ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_1 = "S_Xao_BikiniTime_1";
		/// <summary>
		/// Bikini Time ♡♡♡
		/// Create a <color=#d78fe9>Bikini Time ♡♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_2 = "S_Xao_BikiniTime_2";
		/// <summary>
		/// Bikini Time ♡♡♡♡
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_3 = "S_Xao_BikiniTime_3";
		/// <summary>
		/// Bikini Time Pleasure ♥
		/// Create a <color=#d78fe9>Bikini Time ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_Love_0 = "S_Xao_BikiniTime_Love_0";
		/// <summary>
		/// Bikini Time Pleasure ♥♥
		/// Create a <color=#d78fe9>Bikini Time ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_Love_1 = "S_Xao_BikiniTime_Love_1";
		/// <summary>
		/// Bikini Time Pleasure ♥♥♥
		/// Create a <color=#d78fe9>Bikini Time ♥♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_Love_2 = "S_Xao_BikiniTime_Love_2";
		/// <summary>
		/// Bikini Time Pleasure ♥♥♥♥
		/// </summary>
        public static string Skill_S_Xao_BikiniTime_Love_3 = "S_Xao_BikiniTime_Love_3";
		/// <summary>
		/// Spend 1 <sprite name="Xao_Heart"> to remove Overload
		/// </summary>
        public static string Skill_S_Xao_B_Affection_0 = "S_Xao_B_Affection_0";
		/// <summary>
		/// Magical Starfall
		/// </summary>
        public static string Skill_S_Xao_B_MagicalDay = "S_Xao_B_MagicalDay";
		/// <summary>
		/// Cowgirl ♡
		/// Create a <color=#d78fe9>Cowgirl ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_CowGirl_0 = "S_Xao_CowGirl_0";
		/// <summary>
		/// Cowgirl ♡♡
		/// Create a <color=#d78fe9>Cowgirl ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_CowGirl_1 = "S_Xao_CowGirl_1";
		/// <summary>
		/// Cowgirl ♡♡♡
		/// </summary>
        public static string Skill_S_Xao_CowGirl_2 = "S_Xao_CowGirl_2";
		/// <summary>
		/// Cowgirl Pleasure ♥
		/// Create a <color=#d78fe9>Cowgirl Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_CowGirl_Love_0 = "S_Xao_CowGirl_Love_0";
		/// <summary>
		/// Cowgirl Pleasure ♥♥
		/// Create a <color=#d78fe9>Cowgirl Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_CowGirl_Love_1 = "S_Xao_CowGirl_Love_1";
		/// <summary>
		/// Cowgirl Pleasure ♥♥♥
		/// </summary>
        public static string Skill_S_Xao_CowGirl_Love_2 = "S_Xao_CowGirl_Love_2";
		/// <summary>
		/// Experience Maid Footjob ♡
		/// Create a <color=#d78fe9>Experience Maid Footjob ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_ExperienceMaidFootjob_0 = "S_Xao_ExperienceMaidFootjob_0";
		/// <summary>
		/// Experience Maid Footjob ♡♡
		/// Create a <color=#d78fe9>Experience Maid Footjob ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_ExperienceMaidFootjob_1 = "S_Xao_ExperienceMaidFootjob_1";
		/// <summary>
		/// Experience Maid Footjob ♡♡♡
		/// </summary>
        public static string Skill_S_Xao_ExperienceMaidFootjob_2 = "S_Xao_ExperienceMaidFootjob_2";
		/// <summary>
		/// Experience Maid Footjob Pleasure ♥
		/// Create a <color=#d78fe9>Experience Maid Footjob Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_ExperienceMaidFootjob_Love_0 = "S_Xao_ExperienceMaidFootjob_Love_0";
		/// <summary>
		/// Experience Maid Footjob Pleasure ♥♥
		/// Create a <color=#d78fe9>Experience Maid Footjob Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_ExperienceMaidFootjob_Love_1 = "S_Xao_ExperienceMaidFootjob_Love_1";
		/// <summary>
		/// Experience Maid Footjob Pleasure ♥♥♥
		/// </summary>
        public static string Skill_S_Xao_ExperienceMaidFootjob_Love_2 = "S_Xao_ExperienceMaidFootjob_Love_2";
		/// <summary>
		/// <color=#87CEFA>Kaiju</color> <color=#d78fe9>Embrace</color>
		/// Draw 2 skills and choose one:
		/// - Increase <color=#87CEFA>Combo</color> by <color=#d78fe9>2</color> and Xao gains 1 <sprite name="Xao_Heart">.
		/// - Or restore 1 Mana and keep your current <color=#87CEFA>Combo</color> for the next turn.
		/// If Xao is fainted, draw 1 skill, then exclude this skill from the current fight.
		/// </summary>
        public static string Skill_S_Xao_LucyDraw_0 = "S_Xao_LucyDraw_0";
		/// <summary>
		/// <color=#87CEFA>Extra</color> <color=#d78fe9>Effort</color>
		/// Increase <color=#87CEFA>Combo</color> by <color=#d78fe9>2</color> and Xao gain 1 <sprite name="Xao_Heart">.
		/// </summary>
        public static string Skill_S_Xao_LucyDraw_1 = "S_Xao_LucyDraw_1";
		/// <summary>
		/// <color=#87CEFA>Take It</color> <color=#d78fe9>Easy</color>
		/// Restore 1 Mana and keep your current <color=#87CEFA>Combo</color> for the next turn.
		/// </summary>
        public static string Skill_S_Xao_LucyDraw_2 = "S_Xao_LucyDraw_2";
		/// <summary>
		/// Magical Girl Pussy ♡
		/// Create a <color=#d78fe9>Magical Girl Pussy ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_0 = "S_Xao_MagicalGirlPussy_0";
		/// <summary>
		/// Magical Girl Pussy ♡♡
		/// Create a <color=#d78fe9>Magical Girl Pussy ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_1 = "S_Xao_MagicalGirlPussy_1";
		/// <summary>
		/// Magical Girl Pussy ♡♡♡
		/// Create a <color=#d78fe9>Magical Girl Pussy ♡♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_2 = "S_Xao_MagicalGirlPussy_2";
		/// <summary>
		/// Magical Girl Pussy ♡♡♡♡
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_3 = "S_Xao_MagicalGirlPussy_3";
		/// <summary>
		/// Magical Girl Pussy Pleasure ♥
		/// Create a <color=#d78fe9>Magical Girl Pussy Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_Love_0 = "S_Xao_MagicalGirlPussy_Love_0";
		/// <summary>
		/// Magical Girl Pussy Pleasure ♥♥
		/// Create a <color=#d78fe9>Magical Girl Pussy Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_Love_1 = "S_Xao_MagicalGirlPussy_Love_1";
		/// <summary>
		/// Magical Girl Pussy Pleasure ♥♥♥
		/// Create a <color=#d78fe9>Magical Girl Pussy Pleasure ♥♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_Love_2 = "S_Xao_MagicalGirlPussy_Love_2";
		/// <summary>
		/// Magical Girl Pussy Pleasure ♥♥♥♥
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlPussy_Love_3 = "S_Xao_MagicalGirlPussy_Love_3";
		/// <summary>
		/// Magical Girl Thighjob ♡
		/// Create a <color=#d78fe9>Magical Girl Thighjob ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_0 = "S_Xao_MagicalGirlThighjob_0";
		/// <summary>
		/// Magical Girl Thighjob ♡♡
		/// Create a <color=#d78fe9>Magical Girl Thighjob ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_1 = "S_Xao_MagicalGirlThighjob_1";
		/// <summary>
		/// Magical Girl Thighjob ♡♡♡
		/// Create a <color=#d78fe9>Magical Girl Thighjob ♡♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_2 = "S_Xao_MagicalGirlThighjob_2";
		/// <summary>
		/// Magical Girl Thighjob ♡♡♡♡
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_3 = "S_Xao_MagicalGirlThighjob_3";
		/// <summary>
		/// Magical Girl Thighjob Pleasure ♥
		/// Create a <color=#d78fe9>Magical Girl Thighjob Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_Love_0 = "S_Xao_MagicalGirlThighjob_Love_0";
		/// <summary>
		/// Magical Girl Thighjob Pleasure ♥♥
		/// Create a <color=#d78fe9>Magical Girl Thighjob Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_Love_1 = "S_Xao_MagicalGirlThighjob_Love_1";
		/// <summary>
		/// Magical Girl Thighjob Pleasure ♥♥♥
		/// Create a <color=#d78fe9>Magical Girl Thighjob Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_Love_2 = "S_Xao_MagicalGirlThighjob_Love_2";
		/// <summary>
		/// Magical Girl Thighjob Pleasure ♥♥♥♥
		/// </summary>
        public static string Skill_S_Xao_MagicalGirlThighjob_Love_3 = "S_Xao_MagicalGirlThighjob_Love_3";
		/// <summary>
		/// Maid Panties ♡
		/// Create a <color=#d78fe9>Maid Panties ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MaidPanties_0 = "S_Xao_MaidPanties_0";
		/// <summary>
		/// Maid Panties ♡♡
		/// Create a <color=#d78fe9>Maid Panties ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MaidPanties_1 = "S_Xao_MaidPanties_1";
		/// <summary>
		/// Maid Panties ♡♡♡
		/// </summary>
        public static string Skill_S_Xao_MaidPanties_2 = "S_Xao_MaidPanties_2";
		/// <summary>
		/// Maid Panties Pleasure ♥
		/// Create a <color=#d78fe9>Maid Panties Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MaidPanties_Love_0 = "S_Xao_MaidPanties_Love_0";
		/// <summary>
		/// Maid Panties Pleasure ♥♥
		/// Create a <color=#d78fe9>Maid Panties Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MaidPanties_Love_1 = "S_Xao_MaidPanties_Love_1";
		/// <summary>
		/// Maid Panties Pleasure ♥♥♥
		/// </summary>
        public static string Skill_S_Xao_MaidPanties_Love_2 = "S_Xao_MaidPanties_Love_2";
		/// <summary>
		/// Miko Experience Anal ♡
		/// Create a <color=#d78fe9>Miko Experience Anal ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperienceAnal_0 = "S_Xao_MikoExperienceAnal_0";
		/// <summary>
		/// Miko Experience Anal ♡♡
		/// Create a <color=#d78fe9>Miko Experience Anal ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperienceAnal_1 = "S_Xao_MikoExperienceAnal_1";
		/// <summary>
		/// Miko Experience Anal ♡♡♡
		/// </summary>
        public static string Skill_S_Xao_MikoExperienceAnal_2 = "S_Xao_MikoExperienceAnal_2";
		/// <summary>
		/// Miko Experience Anal Pleasure ♥
		/// Create a <color=#d78fe9>Miko Experience Anal Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperienceAnal_Love_0 = "S_Xao_MikoExperienceAnal_Love_0";
		/// <summary>
		/// Miko Experience Anal Pleasure ♥♥
		/// Create a <color=#d78fe9>Miko Experience Anal Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperienceAnal_Love_1 = "S_Xao_MikoExperienceAnal_Love_1";
		/// <summary>
		/// Miko Experience Anal Pleasure ♥♥♥
		/// </summary>
        public static string Skill_S_Xao_MikoExperienceAnal_Love_2 = "S_Xao_MikoExperienceAnal_Love_2";
		/// <summary>
		/// Miko Experience Pussy ♡
		/// Create a <color=#d78fe9>Miko Experience Pussy ♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_0 = "S_Xao_MikoExperiencePussy_0";
		/// <summary>
		/// Miko Experience Pussy ♡♡
		/// Create a <color=#d78fe9>Miko Experience Pussy ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_1 = "S_Xao_MikoExperiencePussy_1";
		/// <summary>
		/// Miko Experience Pussy ♡♡♡
		/// Create a <color=#d78fe9>Miko Experience Pussy ♡♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_2 = "S_Xao_MikoExperiencePussy_2";
		/// <summary>
		/// Miko Experience Pussy ♡♡♡♡
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_3 = "S_Xao_MikoExperiencePussy_3";
		/// <summary>
		/// Miko Experience Pussy Pleasure ♥
		/// Create a <color=#d78fe9>Miko Experience Pussy Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_Love_0 = "S_Xao_MikoExperiencePussy_Love_0";
		/// <summary>
		/// Miko Experience Pussy Pleasure ♥♥
		/// Create a <color=#d78fe9>Miko Experience Pussy Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_Love_1 = "S_Xao_MikoExperiencePussy_Love_1";
		/// <summary>
		/// Miko Experience Pussy Pleasure ♥♥♥
		/// Create a <color=#d78fe9>Miko Experience Pussy Pleasure ♥♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_Love_2 = "S_Xao_MikoExperiencePussy_Love_2";
		/// <summary>
		/// Miko Experience Pussy Pleasure ♥♥♥♥
		/// </summary>
        public static string Skill_S_Xao_MikoExperiencePussy_Love_3 = "S_Xao_MikoExperiencePussy_Love_3";
		/// <summary>
		/// Simple Exchange ♡
		/// This skill can be played repeatedly during this turn.
		/// Even-numbered skills cost 1 Mana. Odd-numbered skills cost 0 Mana, gain Swiftness and grant 1 <sprite name="Xao_Heart">.
		/// </summary>
        public static string Skill_S_Xao_Rare_SimpleExchange_0 = "S_Xao_Rare_SimpleExchange_0";
		/// <summary>
		/// Simple Exchange ♥
		/// This skill can be played repeatedly during this turn.
		/// Even-numbered skills cost 1 Mana. Odd-numbered skills cost 0 Mana, gain Swiftness and grant 1 <sprite name="Xao_Heart">.
		/// </summary>
        public static string Skill_S_Xao_Rare_SimpleExchange_01 = "S_Xao_Rare_SimpleExchange_01";
		/// <summary>
		/// Simple Exchange
		/// </summary>
        public static string Skill_S_Xao_Rare_SimpleExchange_1 = "S_Xao_Rare_SimpleExchange_1";
		/// <summary>
		/// Simple Exchange
		/// </summary>
        public static string Skill_S_Xao_Rare_SimpleExchange_2 = "S_Xao_Rare_SimpleExchange_2";
		/// <summary>
		/// Simple Exchange
		/// </summary>
        public static string Skill_S_Xao_Rare_SimpleExchange_3 = "S_Xao_Rare_SimpleExchange_3";
		/// <summary>
		/// Simple Exchange
		/// </summary>
        public static string Skill_S_Xao_Rare_SimpleExchange_4 = "S_Xao_Rare_SimpleExchange_4";
		/// <summary>
		/// Sleep Sex ♡
		/// Create a <color=#d78fe9>Sleep Sex &a</color> in hand.
		/// Increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color>.
		/// </summary>
        public static string Skill_S_Xao_Rare_SleepSex_0 = "S_Xao_Rare_SleepSex_0";
		/// <summary>
		/// Sleep Sex ♥
		/// Create a <color=#d78fe9>Sleep Sex &a</color> in hand.
		/// Increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color>.
		/// </summary>
        public static string Skill_S_Xao_Rare_SleepSex_01 = "S_Xao_Rare_SleepSex_01";
		/// <summary>
		/// Sleep Sex
		/// Create a <color=#d78fe9>Sleep Sex &a</color> in hand.
		/// Increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color>.
		/// </summary>
        public static string Skill_S_Xao_Rare_SleepSex_1 = "S_Xao_Rare_SleepSex_1";
		/// <summary>
		/// Sleep Sex
		/// Keep your current <color=#87CEFA>Combo</color> for the next turn.
		/// Increase <color=#87CEFA>Combo</color> by <color=#d78fe9>1</color>.
		/// </summary>
        public static string Skill_S_Xao_Rare_SleepSex_2 = "S_Xao_Rare_SleepSex_2";
		/// <summary>
		/// Swimsuit Day ♡
		/// Create a <color=#d78fe9>Swimsuit Day ♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_SwimsuitDay_0 = "S_Xao_SwimsuitDay_0";
		/// <summary>
		/// Swimsuit Day ♡♡
		/// Create a <color=#d78fe9>Swimsuit Day ♡♡♡</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_SwimsuitDay_1 = "S_Xao_SwimsuitDay_1";
		/// <summary>
		/// Swimsuit Day ♡♡♡
		/// </summary>
        public static string Skill_S_Xao_SwimsuitDay_2 = "S_Xao_SwimsuitDay_2";
		/// <summary>
		/// Swimsuit Day Pleasure ♥
		/// Create a <color=#d78fe9>Swimsuit Day Pleasure ♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_SwimsuitDay_Love_0 = "S_Xao_SwimsuitDay_Love_0";
		/// <summary>
		/// Swimsuit Day Pleasure ♥♥
		/// Create a <color=#d78fe9>Swimsuit Day Pleasure ♥♥♥</color> in hand.
		/// </summary>
        public static string Skill_S_Xao_SwimsuitDay_Love_1 = "S_Xao_SwimsuitDay_Love_1";
		/// <summary>
		/// Swimsuit Day Pleasure ♥♥♥
		/// </summary>
        public static string Skill_S_Xao_SwimsuitDay_Love_2 = "S_Xao_SwimsuitDay_Love_2";
		/// <summary>
		/// Xao
		/// Passive:
		/// Xao can obtain <color=#87CEFA>Combo</color> in fights and she cannot exceed 2 Overload.
		/// Each time she uses a skill, she alternates between gaining +1 Overload and losing -1 Overload.
		/// The first time Xao gains 3 <sprite name="Xao_Heart"> in a battle, she removes all Overload and becomes <color=#d78fe9>Horny</color>.
		/// At the start of each turn, if Xao already has 3 <sprite name="Xao_Heart">, create a random 0-Cost swiftness Xao skill. Otherwise, she gains 1 <sprite name="Xao_Heart"> (up to 3).
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_Xao = "Xao";
        public static string Character_Skin_Xao_Bandage_Panties = "Xao_Bandage_Panties";
        public static string Character_Skin_Xao_Bandage_Panties_W = "Xao_Bandage_Panties_W";
        public static string Character_Skin_Xao_Band_Aids = "Xao_Band_Aids";
        public static string Character_Skin_Xao_Bikini = "Xao_Bikini";
        public static string Character_Skin_Xao_Black_Maid_Pantyhose = "Xao_Black_Maid_Pantyhose";
        public static string Character_Skin_Xao_Cute_Lace_Bow = "Xao_Cute_Lace_Bow";
        public static string Character_Skin_Xao_Magical_Teen = "Xao_Magical_Teen";
        public static string Character_Skin_Xao_Maid = "Xao_Maid";
        public static string Character_Skin_Xao_Miko = "Xao_Miko";
        public static string Character_Skin_Xao_Naked = "Xao_Naked";
        public static string Character_Skin_Xao_Swimsuit = "Xao_Swimsuit";
        public static string Character_Skin_Xao_White_Maid_Pantyhose = "Xao_White_Maid_Pantyhose";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Spend 1 <sprite name="Xao_Heart"> to remove Overload.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Affection_0 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Affection_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Current Combo is &a.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_0 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_0");
		/// <summary>
		/// Korean:
		/// English:
		/// We did it !
		/// Current Combo is &a.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_1 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Current Combo is &a.
		/// Combo will not reset at the start of the next turn.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_2 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_2");
		/// <summary>
		/// Korean:
		/// English:
		/// We did it !
		/// Current Combo is &a.
		/// Combo will not reset at the start of the next turn.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_3 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_3");
		/// <summary>
		/// Korean:
		/// English:
		/// Current <color=#87CEFA>Combo</color>: <color=#d78fe9>&a</color> 
		/// At the start of each turn, reset <color=#87CEFA>Combo</color> to 0.
		/// <color=#87CEFA>Combo</color> Rewards:  
		/// <color=#d78fe9>2</color>: Gain 1 <sprite name="Xao_Heart">  
		/// <color=#d78fe9>4</color>: Draw 1 skill  
		/// <color=#d78fe9>6</color>: Restore 1 Mana  
		/// <color=#d78fe9>8</color>: Permanently increase Attack Power by 1 for this <b>playthrough</b>
		/// <color=#919191>(Saving <color=#87CEFA>Combo</color> between turns activates all rewards on the next turn.)</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_Description_0 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_Description_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Current <color=#87CEFA>Combo</color>: <color=#d78fe9>&a</color> 
		/// At the start of each turn, reset <color=#87CEFA>Combo</color> to 0.
		/// <color=#87CEFA>Combo</color> Rewards:  
		/// <color=#d78fe9>2</color>: Gain 1 <sprite name="Xao_Heart">  
		/// <color=#d78fe9>4</color>: Draw 1 skill  
		/// <color=#d78fe9>6</color>: Restore 1 Mana  
		/// <color=#d78fe9>8</color>: Permanently increase Attack Power by 1 for this <b>playthrough</b>
		/// <color=#d78fe9>15</color>: Gain 1 Key (Once per battle)
		/// <color=#d78fe9>20</color>: Reveal the Black Fog and the Secret Tile on the current stage
		/// <color=#d78fe9>30</color>: Gain 1 random <color=#F9D62FFF>Legendary Equipment</color> (Once per battle)
		/// <color=#919191>(Saving <color=#87CEFA>Combo</color> between turns activates all rewards on the next turn.)</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_Description_1 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_Description_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Current <color=#87CEFA>Combo</color>: <color=#d78fe9>&a</color> 
		/// At the start of each turn, reset <color=#87CEFA>Combo</color> to 0.
		/// <color=#87CEFA>Combo</color> Rewards:  
		/// <color=#d78fe9>2</color>: Gain 1 <sprite name="Xao_Heart">  
		/// <color=#d78fe9>4</color>: Draw 1 skill  
		/// <color=#d78fe9>6</color>: Restore 1 Mana  
		/// <color=#d78fe9>8</color>: Permanently increase Attack Power by 1 for this <b>playthrough</b>
		/// <color=#d78fe9>15</color>: Gain 1 Key (Once per battle)
		/// <color=#d78fe9>20</color>: Reveal the Black Fog and the Secret Tile on the current stage
		/// <color=#d78fe9>30</color>: Gain 1 unique <color=#87CEFA>Xao's</color> <color=#d78fe9>Equipment</color> (Once per this <b>playthrough</b>)
		/// <color=#d78fe9>50</color>: Gain 1 random <color=#F9D62FFF>Legendary Equipment</color> (Once per battle)
		/// <color=#919191>(Saving <color=#87CEFA>Combo</color> between turns activates all rewards on the next turn.)</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Combo_Description_2 => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("Combo_Description_2");
		/// <summary>
		/// Korean:
		/// English:
		/// This skill can be played repeatedly during this turn.
		/// Even-numbered skills cost 1 Mana. Odd-numbered skills cost 0 Mana, gain Swiftness and grant 1 <sprite name="Xao_Heart">.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string RareDescription => ModManager.getModInfo("Xao").localizationInfo.SystemLocalizationUpdate("RareDescription");

    }
}