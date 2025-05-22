using ChronoArkMod;
namespace Akari
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Akari
		/// Passive:
		/// Every 3rd attack Akari uses (excluding Ammunition) deals 15% increased damage.
		/// Starting from the 4th turn, all of Akari's attacks deal 15% increased damage for the rest of the battle.
		/// Each time Akari plays an attack (excluding Ranged Attacks and Ammunition), she gains 1 stack of "Ammo Supply".
		/// Each time Ammunition is discarded, Akari gains 1 stack of "Tactical Reload" for every unit of ammunition discarded.
		/// </summary>
        public static string Character_Akari = "Akari";
		/// <summary>
		/// Ammo Supply
		/// Create random Ammunition in hand at 3 stacks.
		/// </summary>
        public static string Buff_AmmoSupply = "AmmoSupply";
		/// <summary>
		/// Armor-piercing Ammunition
		/// If this skill is discarded by any Ranged Attack, apply the "Piercing Vulnerability"  to the target.
		/// </summary>
        public static string Skill_Armor_piercingAmmunition = "Armor_piercingAmmunition";
		/// <summary>
		/// Armor-piercing Ammunition
		/// If this skill is discarded by any Ranged Attack, apply the "Piercing Vulnerability" to the target.
		/// </summary>
        public static string Skill_Armor_piercingAmmunition_PlusView = "Armor_piercingAmmunition_PlusView";
		/// <summary>
		/// Bayonet Combat
		/// Create 2 random Ammunition in hand.
		/// </summary>
        public static string Skill_BayonetCombat = "BayonetCombat";
		/// <summary>
		/// Bayonet Sword
		/// Whenever the wearer plays an attack skill from hand (excluding Ranged Attacks and Ammunition), create a random Ammunition in hand.
		/// Only activates twice per turn.
		/// <color=#919191>Only those who value their ammo know how to strike with precision.</color> 
		/// </summary>
        public static string Item_Equip_BayonetSword = "BayonetSword";
		/// <summary>
		/// Piercing Vulnerability
		/// </summary>
        public static string Buff_B_Armor_piercingAmmunition = "B_Armor_piercingAmmunition";
        public static string Skill_B_Armor_piercingAmmunition_PlusView = "B_Armor_piercingAmmunition_PlusView";
		/// <summary>
		/// Bayonet Arsenal
		/// Whenever you play an Attack (excluding Ranged Attacks and Ammunition), create a random Ammunition in hand and remove 1 stack.
		/// </summary>
        public static string Buff_B_BayonetArsenal = "B_BayonetArsenal";
		/// <summary>
		/// Combat Readiness
		/// When attacked, create a random Ammunition in hand and remove 1 stack.
		/// </summary>
        public static string Buff_B_CombatReadiness = "B_CombatReadiness";
		/// <summary>
		/// Ember Wound
		/// </summary>
        public static string Buff_B_FlameAmmunition = "B_FlameAmmunition";
        public static string Skill_B_FlameAmmunition_PlusView = "B_FlameAmmunition_PlusView";
		/// <summary>
		/// Piercing Weakness
		/// </summary>
        public static string Buff_B_FrostAmmunition = "B_FrostAmmunition";
        public static string Skill_B_FrostAmmunition_PlusView = "B_FrostAmmunition_PlusView";
		/// <summary>
		/// Le Regole
		/// </summary>
        public static string Buff_B_LeRegole = "B_LeRegole";
        public static string Buff_B_LeRegole_NextTurn = "B_LeRegole_NextTurn";
		/// <summary>
		/// Punishing Precision
		/// </summary>
        public static string Buff_B_PunishingPrecision = "B_PunishingPrecision";
		/// <summary>
		/// Steel Reprisal
		/// When attacked, counterattack for 110% of Attack Power.
		/// This buff is removed if you take damage from the attacker.
		/// </summary>
        public static string Buff_B_SteelReprisal = "B_SteelReprisal";
		/// <summary>
		/// Suppressed Wound
		/// </summary>
        public static string Buff_B_SuppressedWound = "B_SuppressedWound";
		/// <summary>
		/// Tactical Reload
		/// Gain 1 stack for every discarded Ammunition.
		/// Draw 1 skill at 5 stacks.
		/// </summary>
        public static string Buff_B_TacticalReload = "B_TacticalReload";
		/// <summary>
		/// The Boss's Orders
		/// All Range Attacks now discard 1 additional Ammunition.
		/// </summary>
        public static string Buff_B_TheBossOrders = "B_TheBossOrders";
		/// <summary>
		/// Threefold Tenacity
		/// At 3 stacks increase next skill's damage by 15%.
		/// </summary>
        public static string Buff_B_ThreefoldTenacity = "B_ThreefoldTenacity";
		/// <summary>
		/// Threefold Tenacity
		/// Increase all skill's damage by 15%.
		/// </summary>
        public static string Buff_B_ThreefoldTenacity_0 = "B_ThreefoldTenacity_0";
		/// <summary>
		/// Class and Respect
		/// If this attack lands, restore 2 mana.
		/// Deal &a additional damage.
		/// Whenever this skill defeats an enemy, permanently increase this skill's damage by &b for the rest of <b>this run</b>.
		/// </summary>
        public static string Skill_ClassandRespect = "ClassandRespect";
		/// <summary>
		/// Discipline
		/// Destroy the target's Action Point.
		/// Recast this skill three times.
		/// </summary>
        public static string Skill_Discipline = "Discipline";
		/// <summary>
		/// Increase damage by 15% and create 2 random Ammunition in hand.
		/// </summary>
        public static string SkillExtended_Ex_AmmoSupply = "Ex_AmmoSupply";
		/// <summary>
		/// When played from hand, discard a random Ammunition. If an Ammunition was discarded increase this skill's Damage/Healing by 30%
		/// <sprite name="비용1"> or more
		/// </summary>
        public static string SkillExtended_Ex_AmmunitionDiscard = "Ex_AmmunitionDiscard";
        public static string SkillExtended_Ex_IncreaseHeal = "Ex_IncreaseHeal";
		/// <summary>
		/// Ferrous Guard
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// </summary>
        public static string Skill_FerrousGuard = "FerrousGuard";
		/// <summary>
		/// Flame Ammunition
		/// If this skill is discarded by any Ranged Attack, apply the "Ember Wound" to the target.
		/// </summary>
        public static string Skill_FlameAmmunition = "FlameAmmunition";
		/// <summary>
		/// Flame Ammunition
		/// If this skill is discarded by any Ranged Attack, apply the "Ember Wound" to the target.
		/// </summary>
        public static string Skill_FlameAmmunition_PlusView = "FlameAmmunition_PlusView";
		/// <summary>
		/// Focus Fire
		/// If facing 1 enemy, damage is increased by &a.
		/// Discard up to 3 Ammunitions.
		/// For each Ammunition discarded, increase this skill's damage by 15%.
		/// </summary>
        public static string Skill_FocusFire = "FocusFire";
		/// <summary>
		/// Frost Ammunition
		/// If this skill is discarded by any Ranged Attack, apply the "Piercing Weakness" to the target.
		/// </summary>
        public static string Skill_FrostAmmunition = "FrostAmmunition";
		/// <summary>
		/// Frost Ammunition
		/// If this skill is discarded by any Ranged Attack, apply the "Piercing Weakness" to the target.
		/// </summary>
        public static string Skill_FrostAmmunition_PlusView = "FrostAmmunition_PlusView";
		/// <summary>
		/// Judgment's Pistol
		/// At the start of each turn, gain 1 stack of "Ammo Supply".
		/// Whenever the wearer plays an skill (excluding Ammunition) from hand, discard a random Ammunition.
		/// If an Ammunition was discarded increase this skill's Damage/Healing by 30%.
		/// Only activates twice per turn.
		/// <color=#919191>Ammunition is precious. Every shot fired must count – Akari</color> 
		/// </summary>
        public static string Item_Equip_JudgmentsPistol = "JudgmentsPistol";
		/// <summary>
		/// Ammunition
		/// Gain an additional effect when discarded with Range Attack.
		/// </summary>
        public static string SkillKeyword_KeyWord_Ammunition = "KeyWord_Ammunition";
		/// <summary>
		/// Melee Attack
		/// After each successful Melee Attack, gain Ammo Supply.
		/// </summary>
        public static string SkillKeyword_KeyWord_MeleeAttack = "KeyWord_MeleeAttack";
		/// <summary>
		/// Range Attack
		/// Skills gain an additional effect when discarding Ammunition.
		/// </summary>
        public static string SkillKeyword_KeyWord_RangeAttack = "KeyWord_RangeAttack";
		/// <summary>
		/// Strategic Resupply
		/// Draw 2 skills.
		/// Choose - Draw 1 more.
		/// Or, create 2 random Ammunition in hand.
		/// If Akari is fainted, do not choose and draw 2 skills.
		/// </summary>
        public static string Skill_LDraw = "LDraw";
		/// <summary>
		/// Draw 1 skill.
		/// </summary>
        public static string Skill_LDraw_0 = "LDraw_0";
		/// <summary>
		/// Create 2 random Ammunition in hand
		/// </summary>
        public static string Skill_LDraw_1 = "LDraw_1";
		/// <summary>
		/// Le Regole
		/// If facing 1 enemy, damage is increased by &a.
		/// Create a random Ammunition in hand.
		/// At the start of the next turn, apply the "Le Regole" buff to all allies.
		/// </summary>
        public static string Skill_LeRegole = "LeRegole";
		/// <summary>
		/// Reload
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// Create a random Ammunition in hand.
		/// </summary>
        public static string Skill_Reload = "Reload";
        public static string SkillEffect_SE_S_BayonetCombat = "SE_S_BayonetCombat";
        public static string SkillEffect_SE_S_FerrousGuard = "SE_S_FerrousGuard";
        public static string SkillEffect_SE_S_LeRegole = "SE_S_LeRegole";
        public static string SkillEffect_SE_S_SteelKnuckles = "SE_S_SteelKnuckles";
        public static string SkillEffect_SE_Tick_B_Bleeding = "SE_Tick_B_Bleeding";
        public static string SkillEffect_SE_Tick_B_Bleeding_0 = "SE_Tick_B_Bleeding_0";
        public static string SkillEffect_SE_Tick_B_FlameAmmunition = "SE_Tick_B_FlameAmmunition";
        public static string SkillEffect_SE_Tick_B_PunishingPrecision = "SE_Tick_B_PunishingPrecision";
        public static string SkillEffect_SE_Tick_B_SuppressedWound = "SE_Tick_B_SuppressedWound";
        public static string SkillEffect_SE_T_Armor_piercingAmmunition = "SE_T_Armor_piercingAmmunition";
        public static string SkillEffect_SE_T_Armor_piercingAmmunition_0 = "SE_T_Armor_piercingAmmunition_0";
        public static string SkillEffect_SE_T_Armor_piercingAmmunition_PlusView = "SE_T_Armor_piercingAmmunition_PlusView";
        public static string SkillEffect_SE_T_BayonetCombat = "SE_T_BayonetCombat";
        public static string SkillEffect_SE_T_B_Armor_piercingAmmunition_PlusView = "SE_T_B_Armor_piercingAmmunition_PlusView";
        public static string SkillEffect_SE_T_B_FlameAmmunition_PlusView = "SE_T_B_FlameAmmunition_PlusView";
        public static string SkillEffect_SE_T_B_FrostAmmunition_PlusView = "SE_T_B_FrostAmmunition_PlusView";
        public static string SkillEffect_SE_T_B_LeRegole_ViewPlus = "SE_T_B_LeRegole_ViewPlus";
        public static string SkillEffect_SE_T_ClassandRespect = "SE_T_ClassandRespect";
        public static string SkillEffect_SE_T_Discipline = "SE_T_Discipline";
        public static string SkillEffect_SE_T_Discipline_0 = "SE_T_Discipline_0";
        public static string SkillEffect_SE_T_FerrousGuard = "SE_T_FerrousGuard";
        public static string SkillEffect_SE_T_FlameAmmunition = "SE_T_FlameAmmunition";
        public static string SkillEffect_SE_T_FlameAmmunition_PlusView = "SE_T_FlameAmmunition_PlusView";
        public static string SkillEffect_SE_T_FocusFire = "SE_T_FocusFire";
        public static string SkillEffect_SE_T_FrostAmmunition = "SE_T_FrostAmmunition";
        public static string SkillEffect_SE_T_FrostAmmunition_PlisView = "SE_T_FrostAmmunition_PlisView";
        public static string SkillEffect_SE_T_FrostAmmunition_PlusView = "SE_T_FrostAmmunition_PlusView";
        public static string SkillEffect_SE_T_LeRegole = "SE_T_LeRegole";
        public static string SkillEffect_SE_T_Reload = "SE_T_Reload";
        public static string SkillEffect_SE_T_ShockRound = "SE_T_ShockRound";
        public static string SkillEffect_SE_T_SteelKnuckles = "SE_T_SteelKnuckles";
        public static string SkillEffect_SE_T_SteelKnuckles_0 = "SE_T_SteelKnuckles_0";
        public static string SkillEffect_SE_T_SummaryJudgment = "SE_T_SummaryJudgment";
        public static string SkillEffect_SE_T_SuppressingShot = "SE_T_SuppressingShot";
		/// <summary>
		/// Shock Round
		/// Discard up to 2 Ammunitions.
		/// For each Ammunition discarded, increase this skill's damage by 15%.
		/// If 2 Ammunitions are discarded, apply (<sprite=2>&a%) Stun to the target.
		/// </summary>
        public static string Skill_ShockRound = "ShockRound";
		/// <summary>
		/// Steel Knuckles
		/// Draw 1 skill.
		/// </summary>
        public static string Skill_SteelKnuckles = "SteelKnuckles";
		/// <summary>
		/// Steel Reprisal
		/// </summary>
        public static string Skill_SteelKnuckles_0 = "SteelKnuckles_0";
		/// <summary>
		/// Summary Judgment
		/// Discard up to 4 Ammunitions.
		/// For each Ammunition discarded, increase this skill's damage by 30%.
		/// </summary>
        public static string Skill_SummaryJudgment = "SummaryJudgment";
		/// <summary>
		/// Suppressing Shot
		/// Discard a random Ammunition.
		/// If an Ammunition was discarded, increase this skill's damage by 15%.
		/// </summary>
        public static string Skill_SuppressingShot = "SuppressingShot";
		/// <summary>
		/// Threefold Tenacity
		/// Damage Increased by 15%
		/// </summary>
        public static string SkillExtended_Ex_ThreefoldTenacity = "Ex_ThreefoldTenacity";

    }

    public static class ModLocalization
    {

    }
}