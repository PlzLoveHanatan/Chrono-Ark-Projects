using ChronoArkMod;
namespace SuperHero
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Barrier of Light ☆
		/// When attacked by <color=#FFA500>Super Hero</color> or <color=#FF00FF>Super Villain</color>, block <color=#FF4500>Mark of Justice</color>, reflect the half damage received onto a random enemy and reduce the damage taken to 0 and remove 1 stack.
		/// </summary>
        public static string Buff_B_Ex_SuperHero_BarrierofLight = "B_Ex_SuperHero_BarrierofLight";
		/// <summary>
		/// Light ☆ Armor
		/// Blocks all <color=#FF4500>Justice debuffs </color>.
		/// </summary>
        public static string Buff_B_E_SuperHero_LightArmor = "B_E_SuperHero_LightArmor";
		/// <summary>
		/// Blinding Glory
		/// </summary>
        public static string Buff_B_SuperHero_BlindingGlory = "B_SuperHero_BlindingGlory";
		/// <summary>
		/// Ego Shield
		/// </summary>
        public static string Buff_B_SuperHero_EgoShield = "B_SuperHero_EgoShield";
		/// <summary>
		/// EGO Surge ☆
		/// Heal 15% of all damage dealt.
		/// </summary>
        public static string Buff_B_SuperHero_EGOSurge = "B_SuperHero_EGOSurge";
		/// <summary>
		/// Glory of Justice ☆
		/// When attacked, counterattack for &a equal <color=#FF7C34>(50% Attack Power)</color>.
		/// </summary>
        public static string Buff_B_SuperHero_GloryofJustice = "B_SuperHero_GloryofJustice";
		/// <summary>
		/// Hero Complex ☆
		/// Increase stats by &a% for each buff stack.
		/// Description
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
		/// Justice ☆ Ascension
		/// <color=#FF00FF>Super Villain</color> cannot attack enemies while any allies remain.
		/// </summary>
        public static string Buff_B_SuperHero_JusticeAscension = "B_SuperHero_JusticeAscension";
		/// <summary>
		/// Justice ☆ Hero
		/// A <color=#1E90FF>Super Hero</color> cannot become a <color=#FF00FF>Super Villain</color> and cannot use skills to damage or apply negative effects to allies.
		/// </summary>
        public static string Buff_B_SuperHero_JusticeHero = "B_SuperHero_JusticeHero";
		/// <summary>
		/// Mark of Justice
		/// </summary>
        public static string Buff_B_SuperHero_MarkofJustice = "B_SuperHero_MarkofJustice";
		/// <summary>
		/// Overpowered Protagonist ☆
		/// HP cannot drop below 0.
		/// </summary>
        public static string Buff_B_SuperHero_OverpoweredProtagonist = "B_SuperHero_OverpoweredProtagonist";
		/// <summary>
		/// Plot Armor ☆
		/// At the start of each turn gain &a barrier <color=#FF7C34>(40% of Max Health)</color>.
		/// </summary>
        public static string Buff_B_SuperHero_PlotArmor = "B_SuperHero_PlotArmor";
		/// <summary>
		/// Relentless Recovery ☆
		/// At the start of each turn remove 2 random debuffs and restore &a HP <color=#FF7C34>(20% of Max Health)</color>.
		/// </summary>
        public static string Buff_B_SuperHero_RelentlessRecovery = "B_SuperHero_RelentlessRecovery";
		/// <summary>
		/// Scarlet Remnant
		/// </summary>
        public static string Buff_B_SuperHero_ScarletRemnant = "B_SuperHero_ScarletRemnant";
		/// <summary>
		/// Second Act ☆
		/// At the start of each turn draw 2 additional skill and Restore 2 Mana.
		/// </summary>
        public static string Buff_B_SuperHero_SecondAct = "B_SuperHero_SecondAct";
		/// <summary>
		/// When cast, <color=#FFA500>Super Hero</color> gains 4 <color=#FFD700>Hero Complex</color> and gains a buff that reflects damage received onto a random enemy and reduces the next two instances of damage taken from <color=#FFA500>Super Hero</color> to 0.
		/// Can only activate once per battle.
		/// </summary>
        public static string SkillExtended_Ex_SuperHero_BarrierofLight = "Ex_SuperHero_BarrierofLight";
		/// <summary>
		/// Justice ☆ Sword
		/// </summary>
        public static string Item_Equip_E_SuperHero_JusticeSword = "E_SuperHero_JusticeSword";
		/// <summary>
		/// Light ☆ Armor
		/// Blocks all <color=#FF4500>Justice debuffs </color>.
		/// When attacked by <color=#FFA500>Super Hero</color>, reduce the damage by 50%, unless <color=#FFA500>Super Hero</color> becomes a <color=#FF00FF>Super Villain</color>.
		/// This equipment cannot be cursed.
		/// <color=red>Cannot be equipped by</color> <color=#FFA500>Super Hero</color>.
		/// <color=#919191><color=#FF00FF>Justice ☆</color> always finds its villain.</color>
		/// </summary>
        public static string Item_Equip_E_SuperHero_LightArmor = "E_SuperHero_LightArmor";
		/// <summary>
		/// Blinding Glory
		/// <color=#919191>Accuracy -30%
		/// Evasion -30%
		/// Receiving Damage +15%
		/// Max 2 stack</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_BlindingGlory = "KeyWord_BlindingGlory";
		/// <summary>
		/// Hero Complex ☆
		/// <color=#919191>Increase all stats by 4% for each buff stack. Gain X chance <color=#FF7C34>(4% * StackNum)</color> to target an ally.
		/// Max 25 Stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HeroComplex = "KeyWord_HeroComplex";
		/// <summary>
		/// Hero Presence
		/// <color=#919191>Cannot take action.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HeroPresence = "KeyWord_HeroPresence";
		/// <summary>
		/// Hero's Spotlight
		/// <color=#919191>Can only target Super Hero.
		/// Removed when this character attacks Super Hero.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HerosSpotlight = "KeyWord_HerosSpotlight";
		/// <summary>
		/// Justice Hero
		/// <color=#919191><color=#1E90FF>Super Hero</color> cannot become a <color=#FF00FF>Super Villain</color> and attack allies.
		/// Attack Power +5
		/// Critical Damage +25%
		/// Max 1 Stack</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_JusticeHero = "KeyWord_JusticeHero";
		/// <summary>
		/// Mark Of Justice
		/// <color=#919191>Attack Power -5%
		/// Armor -5%
		/// Max 5 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_MarkOfJustice = "KeyWord_MarkOfJustice";
		/// <summary>
		/// Unique Buff ☆
		/// Only <color=#FFA500>Super Hero</color> can have these buffs.
		/// </summary>
        public static string SkillKeyword_KeyWord_UniqueBuff = "KeyWord_UniqueBuff";
		/// <summary>
		/// Unique Skill ☆
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// </summary>
        public static string SkillKeyword_KeyWord_UniquSkill = "KeyWord_UniquSkill";
        public static string SkillEffect_SE_S_S_SuperHero_ApotheosisofJustice = "SE_S_S_SuperHero_ApotheosisofJustice";
        public static string SkillEffect_SE_S_S_SuperHero_BloodstainedDress = "SE_S_S_SuperHero_BloodstainedDress";
        public static string SkillEffect_SE_S_S_SuperHero_ErasetheMobs = "SE_S_S_SuperHero_ErasetheMobs";
        public static string SkillEffect_SE_S_S_SuperHero_IntheNameofJustice = "SE_S_S_SuperHero_IntheNameofJustice";
        public static string SkillEffect_SE_S_S_SuperHero_IntheNameofJustice_0 = "SE_S_S_SuperHero_IntheNameofJustice_0";
        public static string SkillEffect_SE_S_S_SuperHero_IntheNameofJustice_1 = "SE_S_S_SuperHero_IntheNameofJustice_1";
        public static string SkillEffect_SE_S_S_SuperHero_JusticeFinale = "SE_S_S_SuperHero_JusticeFinale";
        public static string SkillEffect_SE_S_S_SuperHero_JusticePatience = "SE_S_S_SuperHero_JusticePatience";
        public static string SkillEffect_SE_S_S_SuperHero_JusticePatience_0 = "SE_S_S_SuperHero_JusticePatience_0";
        public static string SkillEffect_SE_S_S_SuperHero_LimitBreak = "SE_S_S_SuperHero_LimitBreak";
        public static string SkillEffect_SE_S_S_SuperHero_OverflowingwithLight = "SE_S_S_SuperHero_OverflowingwithLight";
        public static string SkillEffect_SE_S_S_SuperHero_Rare_ApotheosisofJustice = "SE_S_S_SuperHero_Rare_ApotheosisofJustice";
        public static string SkillEffect_SE_S_S_SuperHero_Rare_JusticeDarkestHour = "SE_S_S_SuperHero_Rare_JusticeDarkestHour";
        public static string SkillEffect_SE_S_S_SuperHero_Rare_JusticeHero = "SE_S_S_SuperHero_Rare_JusticeHero";
        public static string SkillEffect_SE_S_S_SuperHero_TheApplauseNeverEnds = "SE_S_S_SuperHero_TheApplauseNeverEnds";
        public static string SkillEffect_SE_S_S_SuperHero_UnwantedSuccessStory = "SE_S_S_SuperHero_UnwantedSuccessStory";
        public static string SkillEffect_SE_S_S_SuperHero_WorldIsMine = "SE_S_S_SuperHero_WorldIsMine";
        public static string SkillEffect_SE_Tick_B_SuperHero_ScarletRemnant = "SE_Tick_B_SuperHero_ScarletRemnant";
        public static string SkillEffect_SE_Tick_B_SuperHero_ScarletRemnant_0 = "SE_Tick_B_SuperHero_ScarletRemnant_0";
        public static string SkillEffect_SE_T_SuperHero_WorldIsMine = "SE_T_SuperHero_WorldIsMine";
        public static string SkillEffect_SE_T_S_SuperHero_BloodstainedDress = "SE_T_S_SuperHero_BloodstainedDress";
        public static string SkillEffect_SE_T_S_SuperHero_ErasetheMobs = "SE_T_S_SuperHero_ErasetheMobs";
        public static string SkillEffect_SE_T_S_SuperHero_IntheNameofJustice = "SE_T_S_SuperHero_IntheNameofJustice";
        public static string SkillEffect_SE_T_S_SuperHero_IntheNameofJustice_0 = "SE_T_S_SuperHero_IntheNameofJustice_0";
        public static string SkillEffect_SE_T_S_SuperHero_IntheNameofJustice_1 = "SE_T_S_SuperHero_IntheNameofJustice_1";
        public static string SkillEffect_SE_T_S_SuperHero_JusticeFinale = "SE_T_S_SuperHero_JusticeFinale";
        public static string SkillEffect_SE_T_S_SuperHero_JusticeGlory = "SE_T_S_SuperHero_JusticeGlory";
        public static string SkillEffect_SE_T_S_SuperHero_JusticeGlory_0 = "SE_T_S_SuperHero_JusticeGlory_0";
        public static string SkillEffect_SE_T_S_SuperHero_JusticePatience = "SE_T_S_SuperHero_JusticePatience";
        public static string SkillEffect_SE_T_S_SuperHero_JusticePatience_0 = "SE_T_S_SuperHero_JusticePatience_0";
        public static string SkillEffect_SE_T_S_SuperHero_UnwantedSuccessStory = "SE_T_S_SuperHero_UnwantedSuccessStory";
        public static string SkillEffect_SE_T_S_SuperHero_WorldIsMine = "SE_T_S_SuperHero_WorldIsMine";
		/// <summary>
		/// Super Hero
		/// Passive:
		/// Gain <color=#FFD700>Hero Complex</color> whenever you play non-Class attack skills.  
		/// At the start of each turn, gain <color=#FFD700>Hero Complex</color> (up to 25).  
		/// At max <color=#FFD700>Hero Complex</color>, <color=#FFA500>Super Hero</color> becomes a <color=#FF00FF>Super Villain</color>.  
		/// At the start of the 3rd turn, create <color=#FF4500>Justice ☆ Patience</color> in hand at the start of each turn.  
		/// Once per fight, <color=#FFA500>Super Hero</color> can become a <color=#1E90FF>Justice Hero</color> if you gain 3 unique <color=#FFA500>Super Hero</color> <color=#1E90FF>buffs</color> (Plot Armor, Relentless Recovery, Second Act).
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_SuperHero = "SuperHero";
        public static string Character_Skin_SuperHero_HeroOfJustice = "SuperHero_HeroOfJustice";
        public static string Character_Skin_SuperHero_LightOfHope = "SuperHero_LightOfHope";
		/// <summary>
		/// Bloodstained Dress
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_BloodstainedDress = "S_SuperHero_BloodstainedDress";
        public static string Skill_S_SuperHero_DummyHeal = "S_SuperHero_DummyHeal";
		/// <summary>
		/// Erase the Mobs
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_ErasetheMobs = "S_SuperHero_ErasetheMobs";
		/// <summary>
		/// In the Name of Justice ☆
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice = "S_SuperHero_IntheNameofJustice";
		/// <summary>
		/// <color=#FF00FF>In the Name of Justice ☆</color>
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice_0 = "S_SuperHero_IntheNameofJustice_0";
		/// <summary>
		/// <color=#FF4500>In the Name of Justice ☆</color>
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice_1 = "S_SuperHero_IntheNameofJustice_1";
		/// <summary>
		/// <color=#FF00FF>Justice ☆ Finale</color>
		/// This skill can be cast even if you are stunned.
		/// Only <color=#FF00FF>Super Villain</color> can use this skill.
		/// <b>Kill all allies and enemies.</b>
		/// <color=#FFD700>Justice ☆ demands sacrifice.</color>
		/// </summary>
        public static string Skill_S_SuperHero_JusticeFinale = "S_SuperHero_JusticeFinale";
		/// <summary>
		/// <color=#FFC000>Justice ☆ Glory</color>
		/// </summary>
        public static string Skill_S_SuperHero_JusticeGlory = "S_SuperHero_JusticeGlory";
		/// <summary>
		/// <color=#FF00FF>Justice ☆ Glory</color>
		/// </summary>
        public static string Skill_S_SuperHero_JusticeGlory_0 = "S_SuperHero_JusticeGlory_0";
		/// <summary>
		/// <color=#FF00FF>Justice ☆ Impatience</color>
		/// This skill can be cast even if you are stunned.
		/// This skill can be played repeatedly during this turn.
		/// Remove 1 random debuff from <color=#FF00FF>Self</color>.
		/// </summary>
        public static string Skill_S_SuperHero_JusticePatience = "S_SuperHero_JusticePatience";
		/// <summary>
		/// <color=#FF4500>Justice ☆ Patience</color>
		/// This skill can be cast even if you are stunned.
		/// This skill can be played repeatedly during this turn.
		/// Remove 1 random debuff from <color=#FFA500>Self</color>.
		/// </summary>
        public static string Skill_S_SuperHero_JusticePatience_0 = "S_SuperHero_JusticePatience_0";
		/// <summary>
		/// <color=#1E90FF>Limit Break ☆</color>
		/// Remove all debuffs.
		/// All buffs remain 2 extra turn.
		/// </summary>
        public static string Skill_S_SuperHero_LimitBreak = "S_SuperHero_LimitBreak";
		/// <summary>
		/// <color=#FFD700>Glorious ☆ Entrance</color>
		/// Move this skill to the top of the deck when a battle starts.
		/// Draw 3 skills. <color=#FFA500>Super Hero</color> gains 3 <color=#FFD700>Hero Complex</color>.
		/// If <color=#FFA500>Super Hero</color> is fainted, draw 1 skill, then exclude this skill from current fight.
		/// </summary>
        public static string Skill_S_SuperHero_LucyDraw = "S_SuperHero_LucyDraw";
		/// <summary>
		/// <color=#1E90FF>Overflowing with Light ☆</color>
		/// Remove Overload from all allies, restore 2 mana and draw 2 skills.
		/// </summary>
        public static string Skill_S_SuperHero_OverflowingwithLight = "S_SuperHero_OverflowingwithLight";
		/// <summary>
		/// <color=#FFD700>Apotheosis of Justice ☆</color>
		/// &a
		/// </summary>
        public static string Skill_S_SuperHero_Rare_ApotheosisofJustice = "S_SuperHero_Rare_ApotheosisofJustice";
		/// <summary>
		/// <color=#9400D3>Justice ☆ Darkest Hour</color>
		/// <color=#FFA500>Super Hero</color> gain Max <color=#FFD700>Hero Complex</color> and become a <color=#FF00FF>Super Villain</color>.
		/// Starting from 5th turn create <color=#FF00FF>Justice ☆ Finale</color> in hand.
		/// <color=#919191><color=#FF00FF>Justice ☆</color> always win.</color>
		/// </summary>
        public static string Skill_S_SuperHero_Rare_JusticeDarkestHour = "S_SuperHero_Rare_JusticeDarkestHour";
		/// <summary>
		/// <color=#1E90FF>Justice Hero ☆</color>
		/// Remove all <color=#FF4500>Justice debuffs </color> from all allies.
		/// <color=#919191>This world needs a <color=#1E90FF>Hero ☆</color></color>
		/// </summary>
        public static string Skill_S_SuperHero_Rare_JusticeHero = "S_SuperHero_Rare_JusticeHero";
		/// <summary>
		/// <color=#1E90FF>Applause Never Ends ☆</color>
		/// Gain &a Barrier <color=#FF7C34>(40% of Max Health)</color>.
		/// </summary>
        public static string Skill_S_SuperHero_TheApplauseNeverEnds = "S_SuperHero_TheApplauseNeverEnds";
		/// <summary>
		/// Unwanted Success Story
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_UnwantedSuccessStory = "S_SuperHero_UnwantedSuccessStory";
		/// <summary>
		/// World Is Mine
		/// Description
		/// </summary>
        public static string Skill_S_SuperHero_WorldIsMine = "S_SuperHero_WorldIsMine";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Apply <color=#50C878>Hero's Spotlight</color> to all enemies and allies.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Apotheosis_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("Apotheosis_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Apply <color=#50C878>Hero's Spotlight</color> to all enemies.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Apotheosis_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("Apotheosis_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Cost reduced by 2 if this is a fixed ability.
		/// Apply <color=#DC143C>Scarlet Remnant</color> to all allies.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string BloodStained_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("BloodStained_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Cost reduced by 2 if this is a fixed ability.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string BloodStained_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("BloodStained_1");
		/// <summary>
		/// Korean:
		/// English:
		/// All allies take damage equal 50% of their Max Health.
		/// If the target's health is below 60% (30% for bosses), <b>reduce the target's health to 0</b>.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EraseMobs_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("EraseMobs_0");
		/// <summary>
		/// Korean:
		/// English:
		/// If the target's health is below 60% (30% for bosses), <b>reduce the target's health to 0</b>.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EraseMobs_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("EraseMobs_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Gain 10% chance to target allies with attacks.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string HeroComplex_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("HeroComplex_0");
		/// <summary>
		/// Korean:
		/// English:
		/// This skill can be played repeatedly during this turn.
		/// If this skill defeat an enemy restore 2 Mana.
		/// Apply <color=#FF4500>Mark of Justice</color> to a random ally.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_0");
		/// <summary>
		/// Korean:
		/// English:
		/// This skill can be played repeatedly during this turn.
		/// If this skill defeat an enemy restore 2 Mana.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// Apply <color=#FF4500>Mark of Justice</color> to a random ally.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_2 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_2");
		/// <summary>
		/// Korean:
		/// English:
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_3 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_3");
		/// <summary>
		/// Korean:
		/// English:
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// Apply <color=#FF4500>Mark of Justice</color> to a random <color=#FF00FF>Villain</color>.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_4 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_4");
		/// <summary>
		/// Korean:
		/// English:
		/// At the start of each turn, create a 0-Cost <color=#FF4500>In the Name of Justice</color> in hand, apply <color=#FF4500>Mark of Justice</color> to all <color=#FF00FF>Enemies</color>, and gain <color=#FFD700>Hero Complex</color>.
		/// Attacks that inflict <color=#FF4500>Mark of Justice</color> apply 1 additional stack and increase its max stack by 1.
		/// This equipment cannot be cursed.
		/// <color=red>Can only be equipped by</color> <color=#FFA500>Super Hero</color>.
		/// <color=#919191>Justice ☆ is served!
		/// This item gains +1 Attack Power for every cleared stage.</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string JusticeSword_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("JusticeSword_0");
		/// <summary>
		/// Korean:
		/// English:
		/// At the start of each turn, create a 0-Cost <color=#FF00FF>In the Name of Justice</color> in hand, apply <color=#FF4500>Mark of Justice</color> to all <color=#FF00FF>Villains</color>, and gain <color=#FFD700>Hero Complex</color>.
		/// Attacks that inflict <color=#FF4500>Mark of Justice</color> apply 1 additional stack and increase its max stack by 1.
		/// This equipment cannot be cursed.
		/// <color=red>Can only be equipped by</color> <color=#FF00FF>Super Villain</color>.
		/// <color=#919191>Justice ☆ is served!
		/// This item gains +1 Attack Power for every cleared stage.</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string JusticeSword_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("JusticeSword_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Gain 50% chance to target allies.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string OverPowered => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("OverPowered");
		/// <summary>
		/// Korean:
		/// English:
		/// Cannot be disabled by discarding, exchanging, or overflow. Removed after &a turn(s).
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string SuperHero_Stun => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("SuperHero_Stun");
		/// <summary>
		/// Korean:
		/// English:
		/// Removed after &a turn.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string SuperHero_Stun_Enemy => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("SuperHero_Stun_Enemy");
		/// <summary>
		/// Korean:
		/// English:
		/// Apply <color=#FFA500>Hero Presence</color> to a random ally.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Unwanted_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("Unwanted_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Apply <color=#50C878>Hero's Spotlight</color> to all allies.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string World_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("World_0");

    }
}