using ChronoArkMod;
namespace SuperHero
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Barrier of Light ☆
		/// When attacked by <color=#FFA500>Super Hero</color> or <color=#FF00FF>Super Villain</color>, block <color=#FF4500>Mark of Justice</color>, reflect the damage received onto a random enemy and reduce the damage taken to 0 and remove 1 stack.
		/// </summary>
        public static string Buff_B_Ex_SuperHero_BarrierofLight = "B_Ex_SuperHero_BarrierofLight";
		/// <summary>
		/// Light ☆ Armor
		/// Blocks <color=#FF4500>Mark of Justice</color>.
		/// </summary>
        public static string Buff_B_E_SuperHero_LightArmor = "B_E_SuperHero_LightArmor";
		/// <summary>
		/// Blinding Glory
		/// </summary>
        public static string Buff_B_SuperHero_BlindingGlory = "B_SuperHero_BlindingGlory";
		/// <summary>
		/// Ego Shield
		/// Only <color=#FFA500>Super Hero</color> can have this barrier.
		/// </summary>
        public static string Buff_B_SuperHero_EgoShield = "B_SuperHero_EgoShield";
		/// <summary>
		/// EGO Surge
		/// Only <color=#FFA500>Super Hero</color> can have this buff.
		/// Heal 15% of all damage dealt.
		/// </summary>
        public static string Buff_B_SuperHero_EGOSurge = "B_SuperHero_EGOSurge";
		/// <summary>
		/// Glory of Justice ☆
		/// When attacked, counterattack for &a equal <color=#FF7C34>(Attack Power * 0.4)</color>.
		/// Gain &b% chance <color=#FF7C34>(Hero Complex StackNum * 4%)</color> to counterattack ally instead of attacker.
		/// </summary>
        public static string Buff_B_SuperHero_GloryofJustice = "B_SuperHero_GloryofJustice";
		/// <summary>
		/// Hero Complex ☆
		/// Only <color=#FFA500>Super Hero</color> can have this buff.
		/// Increase stats by 4% for each buff stack. Gain &a% chance <color=#FF7C34>(4% * StackNum)</color> to target an ally.
		/// </summary>
        public static string Buff_B_SuperHero_HeroComplex = "B_SuperHero_HeroComplex";
        public static string Buff_B_SuperHero_HeroComplex_0 = "B_SuperHero_HeroComplex_0";
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
        public static string Buff_B_SuperHero_IntheNameofJustice = "B_SuperHero_IntheNameofJustice";
		/// <summary>
		/// Mark of Justice
		/// </summary>
        public static string Buff_B_SuperHero_MarkofJustice = "B_SuperHero_MarkofJustice";
		/// <summary>
		/// Overpowered Protagonist
		/// Only <color=#FFA500>Super Hero</color> can have this buff.
		/// HP cannot drop below 0.
		/// </summary>
        public static string Buff_B_SuperHero_OverpoweredProtagonist = "B_SuperHero_OverpoweredProtagonist";
		/// <summary>
		/// Plot Armor
		/// Only <color=#FFA500>Super Hero</color> can have this buff.
		/// All incoming damage is reduced by 15%. Every turn restore &a HP <color=#FF7C34>(Hero Complex StackNum * 4%)</color>.
		/// </summary>
        public static string Buff_B_SuperHero_PlotArmor = "B_SuperHero_PlotArmor";
		/// <summary>
		/// Relentless Recovery
		/// Only <color=#FFA500>Super Hero</color> can have this buff.
		/// At the start of each turn remove 3 random debuffs and gain &a barrier <color=#FF7C34>(Max HP * 0.4)</color>. 
		/// </summary>
        public static string Buff_B_SuperHero_RelentlessRecovery = "B_SuperHero_RelentlessRecovery";
		/// <summary>
		/// Scarlet Remnant
		/// </summary>
        public static string Buff_B_SuperHero_ScarletRemnant = "B_SuperHero_ScarletRemnant";
		/// <summary>
		/// Scarlet Remnant
		/// </summary>
        public static string Buff_B_SuperHero_ScarletRemnant_0 = "B_SuperHero_ScarletRemnant_0";
		/// <summary>
		/// Second Act
		/// Only <color=#FFA500>Super Hero</color> can have this buff.
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
		/// At the start of each turn, create a 0-Cost <color=#FF4500>In the Name of Justice</color> in hand, apply <color=#FF4500>Mark of Justice</color> to all <color=#FF00FF>Villains</color>, and gain <color=#FFD700>Hero Complex</color>.
		/// Attacks that inflict <color=#FF4500>Mark of Justice</color> apply 1 additional stack and increase its max stack by 1.
		/// This equipment cannot be cursed.
		/// <color=red>Can only be equipped by</color> <color=#FFA500>Super Hero</color>.
		/// <color=#919191>Justice ☆ is served!</color>
		/// </summary>
        public static string Item_Equip_E_SuperHero_JusticeSword = "E_SuperHero_JusticeSword";
		/// <summary>
		/// Light ☆ Armor
		/// When attacked by <color=#FFA500>Super Hero</color>, reflect the damage received onto a random enemy and reduce the damage taken to 0, unless <color=#FFA500>Super Hero</color> becomes a <color=#FF00FF>Super Villain</color>.
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
		/// Hero Complex
		/// <color=#919191>Increase all stats by 3% for each buff stack. Gain X chance <color=#FF7C34>(3% * StackNum)</color> to target an ally.
		/// Max 20 Stacks</color>
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
        public static string SkillEffect_SE_S_S_SuperHero_ApotheosisofJustice = "SE_S_S_SuperHero_ApotheosisofJustice";
        public static string SkillEffect_SE_S_S_SuperHero_BloodstainedDress = "SE_S_S_SuperHero_BloodstainedDress";
        public static string SkillEffect_SE_S_S_SuperHero_ErasetheMobs = "SE_S_S_SuperHero_ErasetheMobs";
        public static string SkillEffect_SE_S_S_SuperHero_IntheNameofJustice = "SE_S_S_SuperHero_IntheNameofJustice";
        public static string SkillEffect_SE_S_S_SuperHero_IntheNameofJustice_0 = "SE_S_S_SuperHero_IntheNameofJustice_0";
        public static string SkillEffect_SE_S_S_SuperHero_IntheNameofJustice_1 = "SE_S_S_SuperHero_IntheNameofJustice_1";
        public static string SkillEffect_SE_S_S_SuperHero_JusticeFinale = "SE_S_S_SuperHero_JusticeFinale";
        public static string SkillEffect_SE_S_S_SuperHero_JusticePatience = "SE_S_S_SuperHero_JusticePatience";
        public static string SkillEffect_SE_S_S_SuperHero_LimitBreak = "SE_S_S_SuperHero_LimitBreak";
        public static string SkillEffect_SE_S_S_SuperHero_OverflowingwithLight = "SE_S_S_SuperHero_OverflowingwithLight";
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
        public static string SkillEffect_SE_T_S_SuperHero_UnwantedSuccessStory = "SE_T_S_SuperHero_UnwantedSuccessStory";
        public static string SkillEffect_SE_T_S_SuperHero_WorldIsMine = "SE_T_S_SuperHero_WorldIsMine";
		/// <summary>
		/// Super Hero
		/// Passive:
		/// Gain 2 <color=#FFD700>Hero Complex</color> when killing an enemy.
		/// Gain <color=#FFD700>Hero Complex</color> whenever playing non-Сlass attack skills.
		/// At the start of each turn, gain <color=#FFD700>Hero Complex</color> (up to 25).
		/// At the start of 3rd turn if <color=#FFA500>Super Hero</color> have Max <color=#FFD700>Hero Complex</color> create <color=#FF00FF>Justice ☆ Patience</color> in hand at the start of each turn.
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_SuperHero = "SuperHero";
		/// <summary>
		/// Skin
		/// Passive:
		/// </summary>
        public static string Character_SuperHero_0 = "SuperHero_0";
		/// <summary>
		/// Passive:
		/// </summary>
        public static string Character_SuperHero_1 = "SuperHero_1";
        public static string Character_Skin_SuperHero_HeroOfJustice = "SuperHero_HeroOfJustice";
        public static string Character_Skin_SuperHero_LightOfHope = "SuperHero_LightOfHope";
		/// <summary>
		/// Apotheosis of Justice
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Apply <color=#50C878>Hero's Spotlight</color> to all enemies and allies.
		/// </summary>
        public static string Skill_S_SuperHero_ApotheosisofJustice = "S_SuperHero_ApotheosisofJustice";
		/// <summary>
		/// Bloodstained Dress
		/// Cost reduced by 1 if this a fixed ability.
		/// Apply 1 turn <color=#DC143C>Scarlet Remnant</color> to a random ally.
		/// </summary>
        public static string Skill_S_SuperHero_BloodstainedDress = "S_SuperHero_BloodstainedDress";
        public static string Skill_S_SuperHero_DummyHeal = "S_SuperHero_DummyHeal";
		/// <summary>
		/// Erase the Mobs
		/// If the target's health is below 40% (20% for bosses), <b>reduce the target's health to 0</b>.
		/// Otherwise gain 3 <color=#FFD700>Hero Complex</color>.
		/// </summary>
        public static string Skill_S_SuperHero_ErasetheMobs = "S_SuperHero_ErasetheMobs";
		/// <summary>
		/// In the Name of Justice ☆
		/// This skill can be played repeatedly during this turn.
		/// If this skill defeat an enemy restore 2 Mana.
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice = "S_SuperHero_IntheNameofJustice";
		/// <summary>
		/// <color=#FF00FF>In the Name of Justice ☆</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice_0 = "S_SuperHero_IntheNameofJustice_0";
		/// <summary>
		/// <color=#FF4500>In the Name of Justice ☆</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// </summary>
        public static string Skill_S_SuperHero_IntheNameofJustice_1 = "S_SuperHero_IntheNameofJustice_1";
		/// <summary>
		/// <color=#FF00FF>Justice ☆ Finale</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// This skill can be cast even if you are stunned.
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
		/// <color=#FF00FF>Justice ☆ Patience</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Remove all buffs/debuffs from the target, except <color=#FF4500>Mark of Justice</color>.
		/// This skill can be played repeatedly during this turn.
		/// This skill can be cast even if you are stunned.
		/// Remove 1 random debuff from <color=#FFA500>Super Hero</color>.
		/// </summary>
        public static string Skill_S_SuperHero_JusticePatience = "S_SuperHero_JusticePatience";
		/// <summary>
		/// <color=#00BFFF>Limit Break ☆</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Remove all debbufs.
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
		/// <color=#00BFFF>Overflowing with Light ☆</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Remove overload from Lucy and self, restore 2 mana and draw 2 skills.
		/// </summary>
        public static string Skill_S_SuperHero_OverflowingwithLight = "S_SuperHero_OverflowingwithLight";
		/// <summary>
		/// <color=#00BFFF>Applause Never Ends ☆</color>
		/// Only <color=#FFA500>Super Hero</color> can use this skill.
		/// Apply <color=#50C878>Hero's Spotlight</color> to all enemies and gain &a Barrier <color=#FF7C34>(Max HP * 0.4)</color>.
		/// </summary>
        public static string Skill_S_SuperHero_TheApplauseNeverEnds = "S_SuperHero_TheApplauseNeverEnds";
		/// <summary>
		/// Unwanted Success Story
		/// This skill always target an enemy and random ally.
		/// </summary>
        public static string Skill_S_SuperHero_UnwantedSuccessStory = "S_SuperHero_UnwantedSuccessStory";
		/// <summary>
		/// World Is Mine
		/// Attack all targets except <color=#FFA500>Super Hero</color>.
		/// </summary>
        public static string Skill_S_SuperHero_WorldIsMine = "S_SuperHero_WorldIsMine";

    }

    public static class ModLocalization
    {
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

    }
}