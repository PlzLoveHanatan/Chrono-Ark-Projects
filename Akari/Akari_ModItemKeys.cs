using ChronoArkMod;
namespace Akari
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Akari
		/// Passive:
		/// At the start of each fight gain 1 'Standart Mag'.
		/// Every 3rd attack deals 20% increased damage.  
		/// From the 3rd turn, all attacks deal 20% increased damage for the rest of the battle.  
		/// Whenever play an attack (excluding Ranged Attacks and Ammunition), gain 1 stack of 'Ammo Supply'.
		/// Whenever Ammunition is discarded, gain 1 stack of 'Tactical Reload' for each unit of Ammunition discarded.
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
        public static string VFXSkill_BayonetCombat_H = "BayonetCombat_H";
		/// <summary>
		/// Bayonet Sword
		/// Whenever playing an attack skill from hand (excluding Ranged Attacks and Ammunition), create a random Ammunition in hand.
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
		/// <summary>
		/// Punishing Precision
		/// </summary>
        public static string Buff_B_PunishingPrecision = "B_PunishingPrecision";
		/// <summary>
		/// Steel Reprisal
		/// When attacked, counterattack for &a <color=#FF7C34>(90% Attack Power)</color>.
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
		/// All Range Attacks discard 1 additional Ammunition.
		/// </summary>
        public static string Buff_B_TheBossOrders = "B_TheBossOrders";
		/// <summary>
		/// Threefold Tenacity
		/// At 2 stacks increase next skill's damage by 20%.
		/// </summary>
        public static string Buff_B_ThreefoldTenacity = "B_ThreefoldTenacity";
        public static string Buff_B_ThreefoldTenacity_0 = "B_ThreefoldTenacity_0";
		/// <summary>
		/// Threefold Tenacity
		/// All skill's damage increased by 20%.
		/// </summary>
        public static string Buff_B_ThreefoldTenacity_1 = "B_ThreefoldTenacity_1";
		/// <summary>
		/// Class and Respect
		/// If this attack lands, restore 2 mana.
		/// Deal &a additional damage.
		/// Whenever this skill defeats an enemy, permanently increase this skill's damage by &b for the rest of <b>this run</b>.
		/// </summary>
        public static string Skill_ClassandRespect = "ClassandRespect";
        public static string VFXSkill_ClassandRespect_H = "ClassandRespect_H";
		/// <summary>
		/// Discipline
		/// Destroy the target's Action Point.
		/// Recast this skill three times.
		/// </summary>
        public static string Skill_Discipline = "Discipline";
        public static string VFXSkill_Discipline_H = "Discipline_H";
		/// <summary>
		/// Damage/Healing is increased by 30%.
		/// Draw 1 skill and create 2 random Ammunition in hand.
		/// <sprite name="비용2"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_AmmoSupply = "Ex_AmmoSupply";
		/// <summary>
		/// Cost is reduced by 1. When played from hand, discard a random Ammunition. If an Ammunition was discarded increase this skill's Damage/Healing by 30%
		/// <sprite name="비용2"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_AmmunitionDiscard = "Ex_AmmunitionDiscard";
        public static string SkillExtended_Ex_JudgmentsPistol = "Ex_JudgmentsPistol";
		/// <summary>
		/// Threefold Tenacity
		/// This skill's damage increased by 20%.
		/// </summary>
        public static string SkillExtended_Ex_ThreefoldTenacity = "Ex_ThreefoldTenacity";
		/// <summary>
		/// Ferrous Guard
		/// </summary>
        public static string Skill_FerrousGuard = "FerrousGuard";
        public static string VFXSkill_FerrousGuard_H = "FerrousGuard_H";
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
		/// If facing 1 enemy, damage is increased by &a <color=#FF7C34>(30% Attack Power)</color>.
		/// Discard up to 3 Ammunitions.
		/// For each Ammunition discarded, increase this skill's damage by 20%.
		/// </summary>
        public static string Skill_FocusFire = "FocusFire";
        public static string VFXSkill_FocusFire_H = "FocusFire_H";
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
		/// At the start of each turn, create random Ammunition in hand. Whenever playing a skill (excluding Ammunition) from hand, discard a random Ammunition.
		/// If an Ammunition was discarded increase this skill's Damage/Healing by 30%.
		/// Only activates once per turn.
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
		/// Choose one -
		/// Draw 3 skills.
		/// Or, draw 2 skills and gain 1 'Standart Mag'.
		/// If Akari is fainted, draw 1 skill and exclude this skill from current fight.
		/// </summary>
        public static string Skill_LDraw = "LDraw";
		/// <summary>
		/// Draw 3 skills.
		/// </summary>
        public static string Skill_LDraw_0 = "LDraw_0";
		/// <summary>
		/// Draw 3 skills.
		/// </summary>
        public static string Skill_LDraw_0_H = "LDraw_0_H";
		/// <summary>
		/// Draw 2 skills and gain 1 'Standart Mag'.
		/// </summary>
        public static string Skill_LDraw_1 = "LDraw_1";
		/// <summary>
		/// Draw 2 skills and gain 1 'Standart Mag'.
		/// </summary>
        public static string Skill_LDraw_1_H = "LDraw_1_H";
        public static string VFXSkill_LDraw_H = "LDraw_H";
		/// <summary>
		/// Le Regole
		/// If facing 1 enemy, damage is increased by &a <color=#FF7C34>(60% Attack Power)</color>.
		/// Gain 1 'Standart Mag'.
		/// </summary>
        public static string Skill_LeRegole = "LeRegole";
        public static string VFXSkill_LeRegole_H = "LeRegole_H";
		/// <summary>
		/// Reload
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// Create a random Ammunition in hand.
		/// </summary>
        public static string Skill_Reload = "Reload";
        public static string VFXSkill_Reload_H = "Reload_H";
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
		/// For each Ammunition discarded, increase this skill's damage by 20%.
		/// If 2 Ammunitions are discarded, apply (<sprite=2>&a%) Stun to the target.
		/// </summary>
        public static string Skill_ShockRound = "ShockRound";
        public static string VFXSkill_ShockRound_H = "ShockRound_H";
        public static string Character_Skin_Skin_Akari_Otome = "Skin_Akari_Otome";
        public static string Item_Active_Standart_Mag = "Standart_Mag";
		/// <summary>
		/// Steel Knuckles
		/// Draw 1 skill.
		/// </summary>
        public static string Skill_SteelKnuckles = "SteelKnuckles";
		/// <summary>
		/// Steel Reprisal
		/// </summary>
        public static string Skill_SteelKnuckles_0 = "SteelKnuckles_0";
        public static string VFXSkill_SteelKnuckles_H = "SteelKnuckles_H";
		/// <summary>
		/// Summary Judgment
		/// Discard up to 4 Ammunitions.
		/// For each Ammunition discarded, increase this skill's damage by 40%.
		/// </summary>
        public static string Skill_SummaryJudgment = "SummaryJudgment";
        public static string VFXSkill_SummaryJudgment_H = "SummaryJudgment_H";
		/// <summary>
		/// Suppressing Shot
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// Discard a random Ammunition.
		/// If an Ammunition was discarded, increase this skill's damage by 20%.
		/// </summary>
        public static string Skill_SuppressingShot = "SuppressingShot";
        public static string VFXSkill_SuppressingShot_H = "SuppressingShot_H";
        public static string Skill_S_Akari_StandartMag = "S_Akari_StandartMag";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// 탄창 장전 완료!
		/// English:
		/// Mag loaded!
		/// Japanese:
		/// マガジン装填完了！
		/// Chinese:
		/// 弹匣已装填！
		/// Chinese-TW:
		/// 弹匣已装填！
		/// </summary>
        public static string Reload => ModManager.getModInfo("Akari").localizationInfo.SystemLocalizationUpdate("Reload");

    }
}