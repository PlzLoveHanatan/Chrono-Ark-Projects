using ChronoArkMod;
namespace Aqua
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Aqua
		/// Passive:
		/// Every time Aqua takes damage, heal all allies by 3 HP, and apply <color=#FF6B6B>Crying Shame</color> to all enemies.
		/// As a former Goddess, Aqua cannot be afflicted with any debuffs.
		/// <color=#919191>- This passive is applied from level 1.
		/// Her tears may be annoying, but surprisingly effective.
		/// Magical debuffs require mental stability to take effect. Sadly, that ship sank long ago.</color>
		/// </summary>
        public static string Character_Aqua = "Aqua";
		/// <summary>
		/// Aqua Veil
		/// </summary>
        public static string Buff_B_Aqua_AquaVeil = "B_Aqua_AquaVeil";
		/// <summary>
		/// Crying Shame
		/// </summary>
        public static string Buff_B_Aqua_CryingShame = "B_Aqua_CryingShame";
		/// <summary>
		/// Drenched 
		/// </summary>
        public static string Buff_B_Aqua_Drenched = "B_Aqua_Drenched";
		/// <summary>
		/// Dubious Blessing
		/// <color=#919191>You don't notice any changes, but you feel a slight discomfort.</color>
		/// </summary>
        public static string Buff_B_Aqua_DubiousBlessing = "B_Aqua_DubiousBlessing";
		/// <summary>
		/// Liquid Courage
		/// Single Attacks gain a 15% chance to target a random character.
		/// <color=#919191>Boldness in a bottle. Direction not included.</color>
		/// </summary>
        public static string Buff_B_Aqua_LiquidCourage = "B_Aqua_LiquidCourage";
		/// <summary>
		/// Type 100 Recovery Mode
		/// <color=#919191>Drenched but refreshed. Don't question it.</color>
		/// </summary>
        public static string Buff_B_Aqua_Type100RecoveryMode = "B_Aqua_Type100RecoveryMode";
		/// <summary>
		/// Unstable Posture
		/// </summary>
        public static string Buff_B_Aqua_UnstablePosture = "B_Aqua_UnstablePosture";
		/// <summary>
		/// When played cast 'Divine Lottery'
		/// <sprite name="비용1"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_Aqua_DivineLottery = "Ex_Aqua_DivineLottery";
		/// <summary>
		/// Damage Increased by 25%. When played gain 'Dubious Blessing'.
		/// </summary>
        public static string SkillExtended_Ex_Aqua_DubiousBlessing = "Ex_Aqua_DubiousBlessing";
		/// <summary>
		/// When played cast 'Nature's Beauty'.
		/// </summary>
        public static string SkillExtended_Ex_Aqua_NaturesBeauty = "Ex_Aqua_NaturesBeauty";
        public static string SkillExtended_Ex_Aqua_PainDamage = "Ex_Aqua_PainDamage";
		/// <summary>
		/// When played cast 'Party Trick'.
		/// <sprite name="비용1"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_Aqua_PartyTrick = "Ex_Aqua_PartyTrick";
		/// <summary>
		/// Aqua's Hagoromo
		/// Apply "Aqua Veil" to the attacker.
		/// At the start of each turn cast a random "Party Trick".
		/// <color=#919191>Makes you dodge better... or just confuse enemies with your fabulousness.</color>
		/// </summary>
        public static string Item_Equip_E_Aqua_Hagoromo = "E_Aqua_Hagoromo";
		/// <summary>
		/// Sacred Laundry Staff
		/// All attacks inflict "Drenched".
		/// At the start of each turn remove "Drenched" from all allies.
		/// <color=#919191>Not great for channeling magic, but fantastic for drying clothes.</color>
		/// </summary>
        public static string Item_Equip_E_Aqua_SacredLaundryStaff = "E_Aqua_SacredLaundryStaff";
		/// <summary>
		/// Aqua Veil
		/// <color=#919191>Accuracy -10%
		/// Max 3 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_AquaVeil = "KeyWord_AquaVeil";
		/// <summary>
		/// Crying Shame
		/// <color=#919191>Attack power -5%
		/// Max 4 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_CryingShame = "KeyWord_CryingShame";
		/// <summary>
		/// Drenched
		/// <color=#919191>Receive damage +10%
		/// Max 3 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Drenched = "KeyWord_Drenched";
		/// <summary>
		/// Dubious Blessing
		/// <color=#919191>Increase 1 random stat :
		/// Attack Power +20%
		/// Healing Power +20%
		/// Defense +20%
		/// Evade +20%
		/// Critical +20%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_DubiousBlessing = "KeyWord_DubiousBlessing";
		/// <summary>
		/// Nature's Beauty
		/// <color=#919191>Heal all allies by 5 and apply 5 barrier.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_NaturesBeauty = "KeyWord_NaturesBeauty";
		/// <summary>
		/// Type 100 Recovery Mode
		/// <color=#919191>Receive healing +30%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Type100 = "KeyWord_Type100";
		/// <summary>
		/// Unstable Posture
		/// <color=#919191>Armor -20%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_UnstablePosture = "KeyWord_UnstablePosture";
        public static string SkillEffect_SE_T_AxisCultRecruitment = "SE_T_AxisCultRecruitment";
        public static string SkillEffect_SE_T_BlessingoftheAxisCult = "SE_T_BlessingoftheAxisCult";
        public static string SkillEffect_SE_T_CreateWater = "SE_T_CreateWater";
        public static string SkillEffect_SE_T_GoddessPrayer = "SE_T_GoddessPrayer";
        public static string SkillEffect_SE_T_GodsBlow = "SE_T_GodsBlow";
        public static string SkillEffect_SE_T_HealingWater = "SE_T_HealingWater";
        public static string SkillEffect_SE_T_PartyDrunkard = "SE_T_PartyDrunkard";
        public static string SkillEffect_SE_T_Resurrection = "SE_T_Resurrection";
        public static string SkillEffect_SE_T_S_Aqua_AquaGradePurification = "SE_T_S_Aqua_AquaGradePurification";
        public static string SkillEffect_SE_T_S_Aqua_BlessingoftheAxisCult = "SE_T_S_Aqua_BlessingoftheAxisCult";
        public static string SkillEffect_SE_T_S_Aqua_FogofBlessings = "SE_T_S_Aqua_FogofBlessings";
        public static string SkillEffect_SE_T_S_Aqua_OverflowingGrace = "SE_T_S_Aqua_OverflowingGrace";
        public static string SkillEffect_SE_T_S_Aqua_PartyDrunkard = "SE_T_S_Aqua_PartyDrunkard";
        public static string SkillEffect_SE_T_S_Aqua_PartyTrick_TelekinesisTrick = "SE_T_S_Aqua_PartyTrick_TelekinesisTrick";
        public static string SkillEffect_SE_T_S_Aqua_Rare_AxisCultRecruitment = "SE_T_S_Aqua_Rare_AxisCultRecruitment";
        public static string SkillEffect_SE_T_S_Aqua_Rare_GodsBlow = "SE_T_S_Aqua_Rare_GodsBlow";
        public static string SkillEffect_SE_T_S_Aqua_SplashofJudgment = "SE_T_S_Aqua_SplashofJudgment";
        public static string SkillEffect_SE_T_S_Aqua_SplashofJudgment_0 = "SE_T_S_Aqua_SplashofJudgment_0";
        public static string SkillEffect_SE_T_S_Aqua_TorrentialTears = "SE_T_S_Aqua_TorrentialTears";
        public static string SkillEffect_SE_T_TorrentialTears = "SE_T_TorrentialTears";
        public static string SkillEffect_SE_T_TurnUndead = "SE_T_TurnUndead";
		/// <summary>
		/// Aqua-Grade Purification
		/// If the target is an ally, they will be healed instead.
		/// Remove all buffs/debuffs from target's.
		/// </summary>
        public static string Skill_S_Aqua_AquaGradePurification = "S_Aqua_AquaGradePurification";
		/// <summary>
		/// Blessing of the Axis Cult
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// </summary>
        public static string Skill_S_Aqua_BlessingoftheAxisCult = "S_Aqua_BlessingoftheAxisCult";
		/// <summary>
		/// Divine Lottery
		/// Cast a random skill.
		/// </summary>
        public static string Skill_S_Aqua_DivineLottery = "S_Aqua_DivineLottery";
        public static string Skill_S_Aqua_DummyHeal = "S_Aqua_DummyHeal";
		/// <summary>
		/// Fog of Blessings (and Mistakes)
		/// Apply <color=#5F9EA0>Aqua Veil</color> to all enemies.
		/// Gain 20% chance to apply <color=#5F9EA0>Aqua Veil</color> to allies.
		/// </summary>
        public static string Skill_S_Aqua_FogofBlessings = "S_Aqua_FogofBlessings";
		/// <summary>
		/// Goddess's Secret Weapon (Don't Tell Kazuma)
		/// Draw 2 skill's and cast 'Nature's Beauty' to all allies.
		/// </summary>
        public static string Skill_S_Aqua_LucyDraw = "S_Aqua_LucyDraw";
		/// <summary>
		/// Overflowing Grace of the Water Goddess
		/// Apply <color=#A3DFF7>Drenched</color> to all allies and enemies.
		/// Gain 20% chance to heal all enemies.
		/// </summary>
        public static string Skill_S_Aqua_OverflowingGrace = "S_Aqua_OverflowingGrace";
		/// <summary>
		/// Party Drunkard
		/// Cost is reduced by 1 if this skill is a fixed ability.
		/// </summary>
        public static string Skill_S_Aqua_PartyDrunkard = "S_Aqua_PartyDrunkard";
		/// <summary>
		/// Party Trick
		/// Randomly activate one of the following 7 effects:  
		/// 1. Cast Nature's Beauty.
		/// 2. Cast Phantasmal Beauty.
		/// 3. Cast Vanish Trick.
		/// 4. Cast Telekinesis Trick.
		/// 5. Cast Certain Kill Party Trick, The Type 100 Mist!
		/// 6. Cast Unusual Plant Growth Trick.
		/// 7. Cast Minor pocket dimension.
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick = "S_Aqua_PartyTrick";
		/// <summary>
		/// Certain kill party trick, the type 100 mist!
		/// Apply 3 stacks of <color=#A3DFF7>Drenched</color> and 2 stacks of <color=#5F9EA0>Aqua Veil</color> to all allies and enemies and apply <color=#48D1CC>Type 100 Recovery Mode</color> to all allies.
		/// <color=#919191>Once activated, it would last for at least half a day before fading.</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_Certainkill = "S_Aqua_PartyTrick_Certainkill";
		/// <summary>
		/// Minor pocket dimension
		/// Gain random book (Skill Book, Golden Skill Book, Mysterious Skill Book, Skill Book - Healing 101, Infinite Skill Book, Transcendent Tome).
		/// <color=#919191>She opened a rift to store snacks. Someone's forgotten tome fell out instead.</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_Minorpocket = "S_Aqua_PartyTrick_Minorpocket";
		/// <summary>
		/// Nature's Beauty
		/// Heal all allies by 5 and apply 5 barrier.
		/// <color=#919191>Trust Me, I'm a Goddess!</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_NaturesBeauty = "S_Aqua_PartyTrick_NaturesBeauty";
		/// <summary>
		/// Phantasmal Beauty
		/// Apply Stun (<sprite=2>150%) to all enemies.
		/// <color=#919191>Behold, My Illusion!</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_PhantasmalBeauty = "S_Aqua_PartyTrick_PhantasmalBeauty";
		/// <summary>
		/// Telekinesis Trick
		/// Deal <color=purple>20 Pain damage</color> and apply 'Unstable Posture' to a random target.
		/// <color=#919191>Totally Controlled!</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_TelekinesisTrick = "S_Aqua_PartyTrick_TelekinesisTrick";
		/// <summary>
		/// Unusual Plant
		/// Gain random potion.
		/// <color=#919191>Aqua tried to make flowers bloom. Something... drinkable sprouted instead.</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_UnusualPlant = "S_Aqua_PartyTrick_UnusualPlant";
		/// <summary>
		/// Vanish Trick
		/// Random enemy disappears (Deal <color=purple>40 Pain damage</color> to bosses instead).
		/// <color=#919191>Aqua knows not where it goes nor can she return the object.</color>
		/// </summary>
        public static string Skill_S_Aqua_PartyTrick_VanishTrick = "S_Aqua_PartyTrick_VanishTrick";
		/// <summary>
		/// Axis Cult Recruitment
		/// Gain a 30% chance to recruit the enemy into the Axis Cult (Deals <color=purple>60 Pain damage</color> to bosses instead, granting a special reward), then exclude this skill from current fight.
		/// New followers bring 400 gold and 4 soulstones.
		/// <color=#919191>Join us! We have hot springs... and emotional instability!</color>
		/// </summary>
        public static string Skill_S_Aqua_Rare_AxisCultRecruitment = "S_Aqua_Rare_AxisCultRecruitment";
		/// <summary>
		/// God's Blow
		/// Gain 50% chance to increase this skill damage by 400%.
		/// <color=#919191>Aqua gathers all her divine might... and misses spectacularly half the time.</color>
		/// </summary>
        public static string Skill_S_Aqua_Rare_GodsBlow = "S_Aqua_Rare_GodsBlow";
		/// <summary>
		/// Splash of Judgment
		/// Apply Taunt status on the target's.
		/// If the target has 1 or more <color=#A3DFF7>Drenched</color>, cast this skill again with Сountdown 1.
		/// </summary>
        public static string Skill_S_Aqua_SplashofJudgment = "S_Aqua_SplashofJudgment";
		/// <summary>
		/// Splash of Judgment
		/// </summary>
        public static string Skill_S_Aqua_SplashofJudgment_0 = "S_Aqua_SplashofJudgment_0";
		/// <summary>
		/// Torrential Tears
		/// Gain 50% chance to cast this skill again.
		/// Gain 20% chance to apply <color=#A3DFF7>Drenched</color> and <color=#FF6B6B>Crying Shame</color> to allies.
		/// <color=#919191>Kazumaaa, I wanna go hoooome!!</color>
		/// </summary>
        public static string Skill_S_Aqua_TorrentialTears = "S_Aqua_TorrentialTears";

    }

    public static class ModLocalization
    {

    }
}