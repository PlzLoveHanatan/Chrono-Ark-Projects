using ChronoArkMod;
namespace QoH
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Affection and Hatred ☆
		/// When this unit dies, add 'Sparkling Justice Shot ♡' to your hand. When attacked by <color=#FF7C34>&b</color>heal the ally with the lowest health for &a <color=#FF7C34>(50% &bHealing Power)</color>. Only activates once per turn. Current status: &c.
		/// </summary>
        public static string Buff_B_QoH_Affection = "B_QoH_Affection";
		/// <summary>
		/// Arcana Slave ☆
		/// Restore &a <color=#FF7C34>(50% &bHealing Power)</color> health and remove this buff, when taking damage.
		/// </summary>
        public static string Buff_B_QoH_ArcanaSlave = "B_QoH_ArcanaSlave";
		/// <summary>
		/// Magical Girl's Chant ☆
		/// When this unit dies or this buff ends, heal an ally with lowest health by &a <color=#FF7C34>(25% &bHealing Power)</color>.
		/// </summary>
        public static string Buff_B_QoH_Chant = "B_QoH_Chant";
		/// <summary>
		/// Love/Hate ☆
		/// Take <color=purple>&a Pain Damage</color> <color=#FF7C34>(50% &b Healing Power)</color>, every time this unit perform an action.
		/// </summary>
        public static string Buff_B_QoH_LoveHate = "B_QoH_LoveHate";
		/// <summary>
		/// Love/Hate ☆
		/// </summary>
        public static string Buff_B_QoH_LoveHate_0 = "B_QoH_LoveHate_0";
		/// <summary>
		/// Love Justice ☆
		/// Restore &a <color=#FF7C34>(100% &bHealing Power)</color> health and remove this buff, when playing attack skill.
		/// </summary>
        public static string Buff_B_QoH_LoveJustice = "B_QoH_LoveJustice";
		/// <summary>
		/// Magical Candy
		/// </summary>
        public static string Buff_B_QoH_MagicalCandy = "B_QoH_MagicalCandy";
		/// <summary>
		/// Here Comes Magical Girl ☆
		/// At the star of the turn apply <color=#BD2DC2>Mark of Villainy</color> to a random enemy.
		/// </summary>
        public static string Buff_B_QoH_MagicalGirl = "B_QoH_MagicalGirl";
		/// <summary>
		/// <color=#BD2DC2>Mark of Villainy</color>
		/// Removed at the start of the next turn.
		/// Can be targeted regardelss Taunt status.
		/// </summary>
        public static string Buff_B_QoH_Mark = "B_QoH_Mark";
		/// <summary>
		/// I used too much power...
		/// </summary>
        public static string Buff_B_QoH_Power = "B_QoH_Power";
		/// <summary>
		/// Description
		/// Description
		/// </summary>
        public static string Buff_B_QoH_Sanity = "B_QoH_Sanity";
		/// <summary>
		/// Shattered Heartbeat ☆
		/// When this unit dies, random unit on this character's side takes <color=purple>Pain damage</color> equal to the total damage of the debuff over X turns.
		/// </summary>
        public static string Buff_B_QoH_Shattered = "B_QoH_Shattered";
		/// <summary>
		/// Regressive Transformation - Reversed
		/// Can change Sanity unlimited times.
		/// Sanity loses their drawbacks.
		/// </summary>
        public static string Buff_B_QoH_Transformation = "B_QoH_Transformation";
		/// <summary>
		/// What use... am I...
		/// Pain debuffs applied onto enemies are extended by 2 turns.
		/// </summary>
        public static string Buff_B_QoH_WhatUse = "B_QoH_WhatUse";
		/// <summary>
		/// Hysteria
		/// </summary>
        public static string SkillExtended_Ex_QoH_Hysteria = "Ex_QoH_Hysteria";
		/// <summary>
		/// <color=#BD2DC2>Hysteria!</color>
		/// Attack Power +15%
		/// Healing Power -15%
		/// Aggro Increased
		/// Can play attacks this turn.
		/// Can only gain <color=red>Negative</color> Points.
		/// </summary>
        public static string SkillKeyword_KeyWord_Hysteria = "KeyWord_Hysteria";
		/// <summary>
		/// Here Comes Magical Girl ☆
		/// Remove <color=#BD2DC2>Hysteria!</color> when gain this buff.
		/// </summary>
        public static string SkillKeyword_KeyWord_MagicalGirl = "KeyWord_MagicalGirl";
		/// <summary>
		/// Queen of Hatred
		/// Passive:
		/// At the star of the turn apply <color=#BD2DC2>Mark of Villainy</color> to a random enemy and gain <color=#FF77FF>Pure Heart ☆</color>
		/// Can use attack skills only when having <color=#BD2DC2>Hysteria!</color>
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_QoH = "QoH";
        public static string SkillEffect_SE_S_S_QoH_Blessing = "SE_S_S_QoH_Blessing";
        public static string SkillEffect_SE_S_S_QoH_Heartwave = "SE_S_S_QoH_Heartwave";
        public static string SkillEffect_SE_S_S_QoH_Justice = "SE_S_S_QoH_Justice";
        public static string SkillEffect_SE_S_S_QoH_Promise = "SE_S_S_QoH_Promise";
        public static string SkillEffect_SE_S_S_QoH_Radiant = "SE_S_S_QoH_Radiant";
        public static string SkillEffect_SE_S_S_QoH_Rare_Beats = "SE_S_S_QoH_Rare_Beats";
        public static string SkillEffect_SE_S_S_QoH_Rare_Love = "SE_S_S_QoH_Rare_Love";
        public static string SkillEffect_SE_S_S_QoH_Spiral = "SE_S_S_QoH_Spiral";
        public static string SkillEffect_SE_Tick_B_QoH_Affection = "SE_Tick_B_QoH_Affection";
        public static string SkillEffect_SE_Tick_B_QoH_Chant = "SE_Tick_B_QoH_Chant";
        public static string SkillEffect_SE_Tick_B_QoH_LoveHate_0 = "SE_Tick_B_QoH_LoveHate_0";
        public static string SkillEffect_SE_Tick_B_QoH_LoveHate_1 = "SE_Tick_B_QoH_LoveHate_1";
        public static string SkillEffect_SE_Tick_B_QoH_MagicalCandy = "SE_Tick_B_QoH_MagicalCandy";
        public static string SkillEffect_SE_Tick_B_QoH_Shattered = "SE_Tick_B_QoH_Shattered";
        public static string SkillEffect_SE_T_S_QoH_Blessing = "SE_T_S_QoH_Blessing";
        public static string SkillEffect_SE_T_S_QoH_Embrace = "SE_T_S_QoH_Embrace";
        public static string SkillEffect_SE_T_S_QoH_Harmony = "SE_T_S_QoH_Harmony";
        public static string SkillEffect_SE_T_S_QoH_Heartwave = "SE_T_S_QoH_Heartwave";
        public static string SkillEffect_SE_T_S_QoH_Justice = "SE_T_S_QoH_Justice";
        public static string SkillEffect_SE_T_S_QoH_Miracle = "SE_T_S_QoH_Miracle";
        public static string SkillEffect_SE_T_S_QoH_Promise = "SE_T_S_QoH_Promise";
        public static string SkillEffect_SE_T_S_QoH_Radiant = "SE_T_S_QoH_Radiant";
        public static string SkillEffect_SE_T_S_QoH_Rare_Beats = "SE_T_S_QoH_Rare_Beats";
        public static string SkillEffect_SE_T_S_QoH_Rare_Love = "SE_T_S_QoH_Rare_Love";
        public static string SkillEffect_SE_T_S_QoH_Rare_Slave = "SE_T_S_QoH_Rare_Slave";
        public static string SkillEffect_SE_T_S_QoH_Shot = "SE_T_S_QoH_Shot";
        public static string SkillEffect_SE_T_S_QoH_Spiral = "SE_T_S_QoH_Spiral";
		/// <summary>
		/// Blessing of Pure Heart ♡
		/// Cast this skill on the target and the ally on their right-side.
		/// </summary>
        public static string Skill_S_QoH_Blessing = "S_QoH_Blessing";
		/// <summary>
		/// Starlight Embrace ♡
		/// Repeatedly heal an ally with lowest health by &a <color=#FF7C34>(20% Healing Power)</color> for each stack of <sprite=1> debuff on a target's.
		/// </summary>
        public static string Skill_S_QoH_Embrace = "S_QoH_Embrace";
		/// <summary>
		/// Harmony Burst ♡
		/// Recast this skill if the target have 3 different type of <sprite=1> debuffs.
		/// </summary>
        public static string Skill_S_QoH_Harmony = "S_QoH_Harmony";
		/// <summary>
		/// Glittering Heartwave ♡
		/// Increase healing amount by &a <color=#FF7C34>(25% Healing Power)</color> for each type of debuff on a target.
		/// Remove all target's <sprite=1> debuffs.
		/// </summary>
        public static string Skill_S_QoH_Heartwave = "S_QoH_Heartwave";
		/// <summary>
		/// In the Name of Justice ♡
		/// When facing 1 enemy apply additonal stack of 'Shattered Heartbeat ☆' to the target.
		/// </summary>
        public static string Skill_S_QoH_Justice = "S_QoH_Justice";
		/// <summary>
		/// Love Attack ♡
		/// Choose One -
		/// Heal all allies by &a <color=#FF7C34>(50% &b Healing Power)</color> and draw 2 skills.
		/// Or extend all enemies Pain debuffs by 2 turns and draw 2 skills.
		/// If Queen of Hatred is fainted, do not choose and draw 2 skills.
		/// </summary>
        public static string Skill_S_QoH_Lucy = "S_QoH_Lucy";
		/// <summary>
		/// Heal all allies and draw 2 skills.
		/// </summary>
        public static string Skill_S_QoH_Lucy_0 = "S_QoH_Lucy_0";
		/// <summary>
		/// Extend all enemies Pain debuffs by 2 turns and draw 2 skills.
		/// </summary>
        public static string Skill_S_QoH_Lucy_1 = "S_QoH_Lucy_1";
		/// <summary>
		/// Lovely Miracle Flash ♡
		/// If the target has a Pain debuff or is defeated by this skill, heal an ally with lowest health by &a <color=#FF7C34>(50% Healing Power)</color> and apply 1 stack of this skill's debuffs to all enemies.
		/// </summary>
        public static string Skill_S_QoH_Miracle = "S_QoH_Miracle";
		/// <summary>
		/// Shining Promise ♡
		/// </summary>
        public static string Skill_S_QoH_Promise = "S_QoH_Promise";
		/// <summary>
		/// Arcana of Radiant Hope ♡
		/// </summary>
        public static string Skill_S_QoH_Radiant = "S_QoH_Radiant";
		/// <summary>
		/// Arcana Beats ♡
		/// If the target has a Pain debuff, this skill gains 100% Critical Chance.
		/// </summary>
        public static string Skill_S_QoH_Rare_Beats = "S_QoH_Rare_Beats";
		/// <summary>
		/// With Love ♡
		/// </summary>
        public static string Skill_S_QoH_Rare_Love = "S_QoH_Rare_Love";
		/// <summary>
		/// Arcana Slave ♡
		/// Deal &a <color=#FF7C34>(50% Attack Power)</color> additional damage for each stack of <sprite=1> debuff on the target.
		/// </summary>
        public static string Skill_S_QoH_Rare_Slave = "S_QoH_Rare_Slave";
		/// <summary>
		/// Sparkling Justice Shot ♡
		/// If the target resists this debuff, move this skill to the discard pile.
		/// </summary>
        public static string Skill_S_QoH_Shot = "S_QoH_Shot";
		/// <summary>
		/// Love Spiral Ray ♡
		/// </summary>
        public static string Skill_S_QoH_Spiral = "S_QoH_Spiral";
        public static string Skill_S_QoH_Test = "S_QoH_Test";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Gain <color=#FF77FF>Pure Heart ☆</color>
		/// <color=#919191>Can switch Sanity only once per turn</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string QoH_Sanity_H => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_H");
		/// <summary>
		/// Korean:
		/// English:
		/// Gain <color=#BD2DC2>Hysteria!</color> now!
		/// <color=#919191>Can switch Sanity only once per turn</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string QoH_Sanity_M => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_M");
		/// <summary>
		/// Korean:
		/// English:
		/// <color=#BD2DC2>Hysteria!</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string QoH_Sanity_Mod_H => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_H");
		/// <summary>
		/// Korean:
		/// English:
		/// Can use attacks.
		/// Can only gain <color=red>Negative</color> Points.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string QoH_Sanity_Mod_H_Desc => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_H_Desc");
		/// <summary>
		/// Korean:
		/// English:
		/// <color=#FF77FF>Pure Heart ☆</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string QoH_Sanity_Mod_M => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_M");
		/// <summary>
		/// Korean:
		/// English:
		/// Can only gain <color=green>Positive</color> Points. Gain <color=#BD2DC2>Hysteria!</color> when taking damage.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string QoH_Sanity_Mod_M_Desc => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_M_Desc");

    }
}