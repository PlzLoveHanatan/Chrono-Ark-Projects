using ChronoArkMod;
namespace Raphi
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Angelic Armour
		/// Sheathe : Restore 1 Mana and draw 1 random user's skill from the discard pile, apply 'Discarded after 1 turn'.
		/// </summary>
        public static string Skill_AngelicArmour = "AngelicArmour";
        public static string VFXSkill_AngelicArmourSummer = "AngelicArmourSummer";
		/// <summary>
		/// Angelic Gamble
		/// When played from hand, if you have another <color=#FF69B4>Angelic Gamble</color> in hand, discard it, cast this skill on the target (Max 3).
		/// If 2 Angelic Gambles are discarded, gain <color=#7B68EE>Celestial Connection</color>, or <color=#C9A7F5>Celestial Blessing</color> if cast by an ally.
		/// Sheathe : Shuffle a random <color=#FF69B4>Angelic Gamble</color> from the discard pile back into your deck and draw 1 skill.
		/// </summary>
        public static string Skill_AngelicGamble = "AngelicGamble";
		/// <summary>
		/// Angelic Guidance
		/// Sheathe : View the draw pile and select one party member's class skill to add to your hand. Selected skill's cost is reduced by 1.
		/// </summary>
        public static string Skill_AngelicGuidance = "AngelicGuidance";
		/// <summary>
		/// Angelic Purification
		/// Remove 1 random debuff.
		/// Sheathe : Cast this skill on the ally with the lowest health.
		/// </summary>
        public static string Skill_AngelicPurification = "AngelicPurification";
        public static string VFXSkill_AngelicPurificationSummer = "AngelicPurificationSummer";
		/// <summary>
		/// Blessed Ascension
		/// Discard the top skill in hand and restore Mana per cost of the discarded skill (Max 2).
		/// </summary>
        public static string Skill_BlessedAscension = "BlessedAscension";
		/// <summary>
		/// Blessed Ascension
		/// Discard the top skill in hand and restore Mana per cost of the discarded skill (Max 2).
		/// </summary>
        public static string Skill_BlessedAscensionSummer = "BlessedAscensionSummer";
		/// <summary>
		/// Blessed Descent
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// When played from hand, discard the bottom skill and restore Mana equal to its cost (Max 2).
		/// Create 'Blessed Ascension' in hand.  
		/// Sheathe : Restore 1 mana and draw 1 skill, prioritizing Healing skills.
		/// </summary>
        public static string Skill_BlessedDescent = "BlessedDescent";
        public static string VFXSkill_BlessedDescentSummer = "BlessedDescentSummer";
		/// <summary>
		/// Aggro Increased
		/// </summary>
        public static string Buff_B_AggroIncreased = "B_AggroIncreased";
		/// <summary>
		/// Angel's Whisper
		/// At the start of next turn, draw &a additional skills.
		/// </summary>
        public static string Buff_B_AngelsWhisper = "B_AngelsWhisper";
		/// <summary>
		/// Armor Reduced!
		/// </summary>
        public static string Buff_B_ArmorReduced = "B_ArmorReduced";
		/// <summary>
		/// Celestial Connection
		/// Can be activated by left-clicking or pressing Hotkey 'C' (cannot be activated if stunned).
		/// Choose one of the following options:
		/// - Discard the top skill in hand and restore Mana per cost of the discarded skill's (Max 2).
		/// Or discard bottom skill in hand and draw skills per cost of the discarded skill's (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always draw instead.
		/// If you have 0 skills in hand, consume 1 stack to draw 1 skill.</color>
		/// </summary>
        public static string Buff_B_CelestialConnection = "B_CelestialConnection";
		/// <summary>
		/// Celestial Blessing
		/// Can be activated by left-clicking (cannot be activated if stunned).
		/// Choose one of the following options:
		/// - Discard the top skill in hand and restore Mana per cost of the discarded skill's (Max 2).
		/// Or discard bottom skill in hand and draw skills per cost of the discarded skill's (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always draw instead.
		/// If you have 0 skills in hand, consume 1 stack to draw 1 skill.</color>
		/// </summary>
        public static string Buff_B_CelestialConnection_0 = "B_CelestialConnection_0";
		/// <summary>
		/// Divine Daze
		/// </summary>
        public static string Buff_B_DivineDaze = "B_DivineDaze";
		/// <summary>
		/// Elysian Grace
		/// At 2 stacks, gain <color=#7B68EE>Celestial Connection</color> (if buff owner is Raphiel),
		/// or <color=#C9A7F5>Celestial Blessing</color>.
		/// </summary>
        public static string Buff_B_ElysianEdge = "B_ElysianEdge";
		/// <summary>
		/// Evasion Increased
		/// </summary>
        public static string Buff_B_EvasionIncreased = "B_EvasionIncreased";
		/// <summary>
		/// Heavenly Grace
		/// Apply 'Sheathe : Cast this skill on a random target' to all Healing skills.
		/// </summary>
        public static string Buff_B_HeavenlyGrace = "B_HeavenlyGrace";
		/// <summary>
		/// Heavenly Wrath
		/// Apply 'Sheathe : Cast this skill on a random target' to all Attack skills.
		/// <color=#919191>Excludes Ilya's skill 
		/// 'Thunder Burst - Ascension'.</color>
		/// </summary>
        public static string Buff_B_HeavenlyWrath = "B_HeavenlyWrath";
		/// <summary>
		/// Heaven's Embrace
		/// Whenever you cast your own skill, gain &a Barrier.
		/// <color=#919191>Based on the Healing Power of the owner of 'Heaven's Embrace'.</color>
		/// </summary>
        public static string Buff_B_HeavensEmbrace = "B_HeavensEmbrace";
		/// <summary>
		/// Heaven's Touch
		/// </summary>
        public static string Buff_B_HeavensTouch = "B_HeavensTouch";
		/// <summary>
		/// Capricious Blessing
		/// When played from hand, discard a random skill and draw 2 additional skills next turn.  
		/// Sheathe : Randomly activate one of the following 5 effects:  
		/// 1. Draw skills equal to the cost of this skill (Max 2).  
		/// 2. Restore Mana equal to the cost of this skill (Max 2).  
		/// 3. Heal all allies and gain <color=#7B68EE>Celestial Connection</color> (if skill owner is Raphiel), or <color=#C9A7F5>Celestial Blessing</color>.
		/// 4. Heal all allies and apply 1 stack of <color=#D2691E>Heaven's Touch</color> to all allies.  
		/// 5. Create a party barrier (&a) equal to 1.5x user's Healing Power. 
		/// </summary>
        public static string Skill_CapriciousBlessing = "CapriciousBlessing";
        public static string VFXSkill_CapriciousBlessingSummer = "CapriciousBlessingSummer";
		/// <summary>
		/// Draw skills per cost of this skill
		/// </summary>
        public static string Skill_CapriciousBlessingSummer_1 = "CapriciousBlessingSummer_1";
		/// <summary>
		/// Restore Mana per cost of this skill
		/// </summary>
        public static string Skill_CapriciousBlessingSummer_2 = "CapriciousBlessingSummer_2";
		/// <summary>
		/// Heal all allies and gain Celestial Connection or Celestial Blessing
		/// </summary>
        public static string Skill_CapriciousBlessingSummer_3 = "CapriciousBlessingSummer_3";
		/// <summary>
		/// Heal all allies and apply Heaven's Touch
		/// </summary>
        public static string Skill_CapriciousBlessingSummer_4 = "CapriciousBlessingSummer_4";
		/// <summary>
		/// Create a party barrier
		/// </summary>
        public static string Skill_CapriciousBlessingSummer_5 = "CapriciousBlessingSummer_5";
		/// <summary>
		/// Draw skills per cost of this skill
		/// </summary>
        public static string Skill_CapriciousBlessing_1 = "CapriciousBlessing_1";
		/// <summary>
		/// Restore Mana per cost of this skill
		/// </summary>
        public static string Skill_CapriciousBlessing_2 = "CapriciousBlessing_2";
		/// <summary>
		/// Heal all allies and gain Celestial Connection or Celestial Blessing
		/// </summary>
        public static string Skill_CapriciousBlessing_3 = "CapriciousBlessing_3";
		/// <summary>
		/// Heal all allies and apply Heaven's Touch
		/// </summary>
        public static string Skill_CapriciousBlessing_4 = "CapriciousBlessing_4";
		/// <summary>
		/// Create a party barrier
		/// </summary>
        public static string Skill_CapriciousBlessing_5 = "CapriciousBlessing_5";
		/// <summary>
		/// Elysian Edge
		/// Whenever the wearer plays a skill, gain 'Elysian Grace' buff (Max 4 per turn).
		/// <color=#919191>Elysian Grace - At 2 stacks, gain <color=#7B68EE>Celestial Connection</color> (if buff owner is Raphiel),
		/// or <color=#C9A7F5>Celestial Blessing</color>.</color>
		/// </summary>
        public static string Item_Equip_ElysianEdge = "ElysianEdge";
		/// <summary>
		/// Heavenly Grace
		/// Sheathe : Cast this skill on a random target.
		/// </summary>
        public static string SkillExtended_Ex_Raphi_1 = "Ex_Raphi_1";
		/// <summary>
		/// Heavenly Wrath
		/// Sheathe : Cast this skill on a random target.
		/// </summary>
        public static string SkillExtended_Ex_Raphi_2 = "Ex_Raphi_2";
		/// <summary>
		/// When played, gain <color=#C9A7F5>Celestial Blessing.</color>
		/// <sprite name="비용1"><sprite name="이상">
		/// <color=#919191>Some of Ilya's skills can't be enhanced.</color>
		/// </summary>
        public static string SkillExtended_Ex_Raphi_3 = "Ex_Raphi_3";
		/// <summary>
		/// Sheathe : Cast this skill, gain <color=#C9A7F5>Celestial Blessing.</color>
		/// <sprite name="비용2"><sprite name="이하">
		/// <color=#919191>Some of Ilya's skills can't be enhanced.</color>
		/// </summary>
        public static string SkillExtended_Ex_Raphi_4 = "Ex_Raphi_4";
		/// <summary>
		/// Sheathe : Draw this skill again.
		/// <sprite name="비용1"><sprite name="이상">
		/// <color=#919191>Some skills can't be enhanced.</color>
		/// </summary>
        public static string SkillExtended_Ex_Raphi_5 = "Ex_Raphi_5";
		/// <summary>
		/// Heavenly Renovation
		/// When played from hand, discard all skills in hand and increase healing by &a.
		/// For every 2 skills discarded, draw 1 additional skill next turn (Max 2).  
		/// If at least 6 skills are discarded, draw 1 skill.  
		/// If 7 skills are discarded, gain <color=#7B68EE>Celestial Connection</color>.
		/// Sheathe : Heal all allies by &b.
		/// </summary>
        public static string Skill_HeavenlyRenovation = "HeavenlyRenovation";
        public static string VFXSkill_HeavenlyRenovationSummer = "HeavenlyRenovationSummer";
        public static string Skill_HeavenlyRenovation_0 = "HeavenlyRenovation_0";
		/// <summary>
		/// Judgement Ascension
		/// Discard the top skill in hand and draw skills per cost of the discarded skill (Max 2).
		/// </summary>
        public static string Skill_JudgementAscension = "JudgementAscension";
		/// <summary>
		/// Judgement Descent
		/// When played from hand, discard the bottom skill and draw skills equal to its cost (Max 2).
		/// Create 'Judgement Ascension' in hand.  
		/// Sheathe : Restore 1 mana and draw 1 skill, prioritizing Attack skills.
		/// </summary>
        public static string Skill_JudgementDescent = "JudgementDescent";
		/// <summary>
		/// Celestial Blessing
		/// <color=#919191>- Discard the top skill in hand and restore Mana (Max 2).
		/// Or discard bottom skill in hand and draw skills (Max 2).</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_CelestialBlessing = "KeyWord_CelestialBlessing";
		/// <summary>
		/// Celestial Connection
		/// <color=#919191>- Discard the top skill in hand and restore Mana (Max 2).
		/// Or discard bottom skill in hand and draw skills (Max 2).</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_CelestialConnection = "KeyWord_CelestialConnection";
		/// <summary>
		/// Heaven's Touch
		/// <color=#919191>Critical Hit Chance +5%
		/// Critical Damage +10%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HeavensTouch = "KeyWord_HeavensTouch";
		/// <summary>
		/// Fateful Decision
		/// Draw 3 skills and select skill to discard.
		/// </summary>
        public static string Skill_L_FatefulDecision = "L_FatefulDecision";
		/// <summary>
		/// Overwhelming Prayer
		/// Cost is reduced by 1 whenever a skill is exchanged or discarded.
		/// When played from hand, draw 2 skills.
		/// Sheathe : Draw this skill again.
		/// </summary>
        public static string Skill_OverwhelmingPrayer = "OverwhelmingPrayer";
        public static string VFXSkill_OverwhelmingPrayerSummer = "OverwhelmingPrayerSummer";
		/// <summary>
		/// Heaven's Embrace
		/// Spend 3 <color=#7B68EE>Celestial Connection</color> to gain an additional 
		/// 'Heaven's Embrace' buff.
		/// </summary>
        public static string Skill_R1HeavensEmbrace = "R1HeavensEmbrace";
		/// <summary>
		/// Spend 3 "Celestial Connection"
		/// </summary>
        public static string Skill_R1HeavensEmbrace_0 = "R1HeavensEmbrace_0";
		/// <summary>
		/// Decline
		/// </summary>
        public static string Skill_R1HeavensEmbrace_1 = "R1HeavensEmbrace_1";
		/// <summary>
		/// Heavenly Grace
		/// </summary>
        public static string Skill_R2HeavenlyGrace = "R2HeavenlyGrace";
		/// <summary>
		/// Heavenly Wrath
		/// </summary>
        public static string Skill_R3HeavenlyWrath = "R3HeavenlyWrath";
		/// <summary>
		/// Raphiel
		/// Passive:
		/// Obtain 3 Celestial Potions.
		/// When used, select one of the party member's skills to apply a unique skill upgrade.
		/// At the start of each turn, gain <color=#7B68EE>Celestial Connection</color> (up to 3 stacks).
		/// </summary>
        public static string Character_Raphi = "Raphi";
		/// <summary>
		/// Celestial Potion
		/// This item can only be used once on each party member excluding Raphiel. 
		/// When used, select one of the party member's skills to apply a unique skill upgrade:
		/// When played, gain <color=#7B68EE>Celestial Connection</color>.
		/// <color=#919191>Requirement : Skill with a mana cost of 1 or more.
		/// Some of Ilya's skills can't be enhanced.</color>
		/// </summary>
        public static string Item_Consume_Raphi_Consume = "Raphi_Consume";
		/// <summary>
		/// Refreshing Renewal
		/// Draw 1 skill.
		/// Gain 2 stacks of <color=#7B68EE>Celestial Connection</color>,
		/// or 2 stacks of <color=#C9A7F5>Celestial Blessing</color> if cast by an ally.
		/// </summary>
        public static string Skill_RefreshingRenewal = "RefreshingRenewal";
        public static string VFXSkill_RefreshingRenewalSummer = "RefreshingRenewalSummer";
        public static string SkillEffect_SE_S_R2HeavenlyGrace = "SE_S_R2HeavenlyGrace";
        public static string SkillEffect_SE_S_R3HeavenlyWrath = "SE_S_R3HeavenlyWrath";
        public static string SkillEffect_SE_S_S_Raphi_9 = "SE_S_S_Raphi_9";
        public static string SkillEffect_SE_S_S_Raphi_R1 = "SE_S_S_Raphi_R1";
        public static string SkillEffect_SE_S_S_Raphi_R2 = "SE_S_S_Raphi_R2";
        public static string SkillEffect_SE_S_S_Raphi_R3 = "SE_S_S_Raphi_R3";
        public static string SkillEffect_SE_S_S_Raphi_test = "SE_S_S_Raphi_test";
        public static string SkillEffect_SE_T_AngelicArmour = "SE_T_AngelicArmour";
        public static string SkillEffect_SE_T_AngelicGamble = "SE_T_AngelicGamble";
        public static string SkillEffect_SE_T_AngelicGuidance = "SE_T_AngelicGuidance";
        public static string SkillEffect_SE_T_AngelicPurification = "SE_T_AngelicPurification";
        public static string SkillEffect_SE_T_BlessedAscension = "SE_T_BlessedAscension";
        public static string SkillEffect_SE_T_BlessedAscensionSummer = "SE_T_BlessedAscensionSummer";
        public static string SkillEffect_SE_T_BlessedDescent = "SE_T_BlessedDescent";
        public static string SkillEffect_SE_T_CapriciousBlessing = "SE_T_CapriciousBlessing";
        public static string SkillEffect_SE_T_HeavenlyRenovation = "SE_T_HeavenlyRenovation";
        public static string SkillEffect_SE_T_HeavenlyRenovation_0 = "SE_T_HeavenlyRenovation_0";
        public static string SkillEffect_SE_T_JudgementAscension = "SE_T_JudgementAscension";
        public static string SkillEffect_SE_T_JudgementDescent = "SE_T_JudgementDescent";
        public static string SkillEffect_SE_T_OverwhelmingPrayer = "SE_T_OverwhelmingPrayer";
        public static string SkillEffect_SE_T_R1HeavensEmbrace = "SE_T_R1HeavensEmbrace";
        public static string SkillEffect_SE_T_SolsticeStrike = "SE_T_SolsticeStrike";
        public static string SkillEffect_SE_T_S_Raphi_1 = "SE_T_S_Raphi_1";
        public static string SkillEffect_SE_T_S_Raphi_10 = "SE_T_S_Raphi_10";
        public static string SkillEffect_SE_T_S_Raphi_101 = "SE_T_S_Raphi_101";
        public static string SkillEffect_SE_T_S_Raphi_102 = "SE_T_S_Raphi_102";
        public static string SkillEffect_SE_T_S_Raphi_103 = "SE_T_S_Raphi_103";
        public static string SkillEffect_SE_T_S_Raphi_104 = "SE_T_S_Raphi_104";
        public static string SkillEffect_SE_T_S_Raphi_11 = "SE_T_S_Raphi_11";
        public static string SkillEffect_SE_T_S_Raphi_11_0 = "SE_T_S_Raphi_11_0";
        public static string SkillEffect_SE_T_S_Raphi_12 = "SE_T_S_Raphi_12";
        public static string SkillEffect_SE_T_S_Raphi_1a = "SE_T_S_Raphi_1a";
        public static string SkillEffect_SE_T_S_Raphi_2 = "SE_T_S_Raphi_2";
        public static string SkillEffect_SE_T_S_Raphi_2a = "SE_T_S_Raphi_2a";
        public static string SkillEffect_SE_T_S_Raphi_3 = "SE_T_S_Raphi_3";
        public static string SkillEffect_SE_T_S_Raphi_4 = "SE_T_S_Raphi_4";
        public static string SkillEffect_SE_T_S_Raphi_5 = "SE_T_S_Raphi_5";
        public static string SkillEffect_SE_T_S_Raphi_6 = "SE_T_S_Raphi_6";
        public static string SkillEffect_SE_T_S_Raphi_6a = "SE_T_S_Raphi_6a";
        public static string SkillEffect_SE_T_S_Raphi_7 = "SE_T_S_Raphi_7";
        public static string SkillEffect_SE_T_S_Raphi_7a = "SE_T_S_Raphi_7a";
        public static string SkillEffect_SE_T_S_Raphi_8 = "SE_T_S_Raphi_8";
        public static string SkillEffect_SE_T_S_Raphi_R1 = "SE_T_S_Raphi_R1";
        public static string SkillEffect_SE_T_S_Raphi_test = "SE_T_S_Raphi_test";
		/// <summary>
		/// Solstice Strike
		/// If facing 1 enemy, damage is increased by &a and apply additional <color=#DAA520>Divine Daze</color> to the target.
		/// Sheathe : Apply <color=#DAA520>Divine Daze</color> to all enemies.
		/// </summary>
        public static string Skill_SolsticeStrike = "SolsticeStrike";
        public static string Character_Skin_SummerRaphiel = "SummerRaphiel";
		/// <summary>
		/// Wings of Glory
		/// Upon landing a critical hit with attack gain <color=#D2691E>Heaven's Touch</color>.
		/// Only activates once per turn.
		/// <color=#919191>Empowered by the heavens, these wings bestow divine strength upon those who strike with righteous fury.</color>
		/// </summary>
        public static string Item_Equip_WingsofGlory = "WingsofGlory";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Select skill to discard. 
		/// Left is bottom skill, restore Mana equal skill cost (Max 2).
		/// Right is top skill, draw equal skill cost (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always draw instead.</color>
		/// Japanese:
		/// Chinese:
		/// 选择技能丢弃。
		/// 左侧是最下面的技能，抽取与费用等额的技能（最多2个）。
		/// 右侧是最上面的技能，恢复与其费用等额的法力值（最多2点）。
		/// Chinese-TW:
		/// 選擇技能丟棄。
		/// 左側是最下面的技能，抽取與費用等額的技能（最多2個）。
		/// 右側是最上面的技能，恢復與其費用等額的法力值（最多2點）。
		/// </summary>
        public static string Discard => ModManager.getModInfo("Raphi").localizationInfo.SystemLocalizationUpdate("Discard");
		/// <summary>
		/// Korean:
		/// English:
		/// Select a skill to add to your hand. Selected skill's cost is reduced by 1 (skills are shown in random order).
		/// Japanese:
		/// Chinese:
		/// 选择 1 个技能加入手中，费用减少 1 点。
		/// （技能以随机顺序展示）
		/// Chinese-TW:
		/// 選擇 1 個技能加入手中，費用減少 1 點。
		/// （技能以隨機順序展示）
		/// </summary>
        public static string DrawPile => ModManager.getModInfo("Raphi").localizationInfo.SystemLocalizationUpdate("DrawPile");

    }
}