using ChronoArkMod;
namespace Urunhilda
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Beastkin Instinct
		/// Allows <b>single-target</b> healing skills to be cast on an enemy.
		/// Upon casting, deal damage equal to healing amount.
		/// Gain Ignore Taunt on all healing skills.
		/// </summary>
        public static string Buff_B_Urunhilda_BeastkinInstinct = "B_Urunhilda_BeastkinInstinct";
		/// <summary>
		/// Ecstasy Rush
		/// Only Male characters can have this buff.
		/// </summary>
        public static string Buff_B_Urunhilda_EcstasyRush_0 = "B_Urunhilda_EcstasyRush_0";
		/// <summary>
		/// Ecxtasy Rush
		/// </summary>
        public static string Buff_B_Urunhilda_EcstasyRush_1 = "B_Urunhilda_EcstasyRush_1";
		/// <summary>
		/// Gentle Violence
		/// Allows <b>single-target</b> damage skills to be cast on an ally.
		/// Upon casting, heal ally equal to damage amount.
		/// Gain Ignore Taunt on all damage skills.
		/// </summary>
        public static string Buff_B_Urunhilda_GentleViolence = "B_Urunhilda_GentleViolence";
		/// <summary>
		/// Lustful Desire
		/// Only Male characters can have this buff.
		/// Hit Rate above 100% is converted into Critical Chance.
		/// </summary>
        public static string Buff_B_Urunhilda_LustfulDesire_0 = "B_Urunhilda_LustfulDesire_0";
		/// <summary>
		/// Lustful Desire
		/// </summary>
        public static string Buff_B_Urunhilda_LustfulDesire_1 = "B_Urunhilda_LustfulDesire_1";
		/// <summary>
		/// Milked Dry
		/// Only Male characters can have this buff.
		/// </summary>
        public static string Buff_B_Urunhilda_MilkedDry_0 = "B_Urunhilda_MilkedDry_0";
		/// <summary>
		/// Milked Dry
		/// </summary>
        public static string Buff_B_Urunhilda_MilkedDry_1 = "B_Urunhilda_MilkedDry_1";
		/// <summary>
		/// Rutting Instinct
		/// Only Urunhilda can have this buff.
		/// At 3 stacks obtain 'Beastkin Instinct'.
		/// At 5 stacks obtain 'Gentle Violence'.
		/// For every stack of buff or debuff applied onto a target, gain a &a% chance <color=#FF7C34>(StackNum * 5)</color> to apply an additional stack of the same buff or debuff.
		/// </summary>
        public static string Buff_B_Urunhilda_RuttingInstinct = "B_Urunhilda_RuttingInstinct";
		/// <summary>
		/// Succubus Mark
		/// Only Male characters can have this buff.
		/// </summary>
        public static string Buff_B_Urunhilda_SuccubusMark_0 = "B_Urunhilda_SuccubusMark_0";
		/// <summary>
		/// Succubus Mark
		/// </summary>
        public static string Buff_B_Urunhilda_SuccubusMark_1 = "B_Urunhilda_SuccubusMark_1";
		/// <summary>
		/// Secret Manual of Taming
		/// View all Investigators' Rare Skills in game and learn one.
		/// </summary>
        public static string Item_Consume_C_Urunhilda_Book_0 = "C_Urunhilda_Book_0";
		/// <summary>
		/// Secret Manual of Knowledge
		/// View all Lucy Rare Skills and learn one.
		/// </summary>
        public static string Item_Consume_C_Urunhilda_Book_1 = "C_Urunhilda_Book_1";
		/// <summary>
		/// Secret Manual of Restocking
		/// View all Lucy Draw Skills and learn one.
		/// </summary>
        public static string Item_Consume_C_Urunhilda_Book_2 = "C_Urunhilda_Book_2";
		/// <summary>
		/// Recoverin D
		/// When used, grant +1 Attack Power and increase Maximum Mana by 1 for <b>this playthrough.</b>
		/// </summary>
        public static string Item_Consume_C_Urunhilda_RecoverinD = "C_Urunhilda_RecoverinD";
		/// <summary>
		/// Red Recoverin
		/// When used, grant +1 Attack Power and increase Maximum Mana by 1 for <b>this playthrough.</b>
		/// </summary>
        public static string Item_Consume_C_Urunhilda_RecoverinRed = "C_Urunhilda_RecoverinRed";
		/// <summary>
		/// Beastkin Instinct
		/// When cast on an enemy, deal damage equal of healing amount.
		/// </summary>
        public static string SkillExtended_Ex_Urunhilda_RuttingInstinct_0 = "Ex_Urunhilda_RuttingInstinct_0";
		/// <summary>
		/// Gentle Violence
		/// When cast on an ally, heal ally equal to damage amount.
		/// </summary>
        public static string SkillExtended_Ex_Urunhilda_RuttingInstinct_1 = "Ex_Urunhilda_RuttingInstinct_1";
		/// <summary>
		/// If this skill defeat an enemy expand your Relic inventory by 1 and obtain 250 Gold, then exclude this skill from current fight.
		/// </summary>
        public static string SkillExtended_Ex_Urunhilda_Synergy_0 = "Ex_Urunhilda_Synergy_0";
		/// <summary>
		/// When played, select one:
		/// - Spend 250 Gold to recast this skill
		/// - Gain 250 Gold
		/// - Spend 500 Gold to obtain random relic
		/// </summary>
        public static string SkillExtended_Ex_Urunhilda_Synergy_1 = "Ex_Urunhilda_Synergy_1";
		/// <summary>
		/// Beastkin Brush
		/// Gain 7% Attack Power (Max 70%) for each stack of buff.
		/// </summary>
        public static string Item_Equip_E_Urunhilda_BeastkinBrush = "E_Urunhilda_BeastkinBrush";
		/// <summary>
		/// Golden Oath Ring
		/// For every stack of buff or debuff applied onto a target, apply an additional stack of the same buff or debuff.
		/// </summary>
        public static string Item_Equip_E_Urunhilda_GoldenOathRing = "E_Urunhilda_GoldenOathRing";
		/// <summary>
		/// Ecstasy Rush
		/// <color=#919191>Critical Damage +5%
		/// Receiving Critical Damage -5%
		/// Max 20 stacks
		/// </summary>
        public static string SkillKeyword_KeyWord_EcstasyRush = "KeyWord_EcstasyRush";
		/// <summary>
		/// Lustful Desire
		/// <color=#919191>Attack Power 5%
		/// Accuracy +5%
		/// Hit Rate above 100% is converted into Critical Chance.
		/// Only Male characters can have this buff.
		/// Max 20 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_LustfulDesire = "KeyWord_LustfulDesire";
		/// <summary>
		/// Rutting Instinct
		/// <color=#919191>Attack Power +1
		/// Healing Power +1
		/// For every stack of buff or debuff applied onto a target, gain a &a% chance <color=#FF7C34>(StackNum * 5)</color> to apply an additional stack of the same buff or debuff.
		/// Max 5 stacks </color>
		/// </summary>
        public static string SkillKeyword_KeyWord_RuttingInstinct = "KeyWord_RuttingInstinct";
        public static string CharRole_Role_GoldenBeastkin = "Role_GoldenBeastkin";
        public static string SkillEffect_SE_S_S_Urunhilda_BeatskinFluffyTease_1 = "SE_S_S_Urunhilda_BeatskinFluffyTease_1";
        public static string SkillEffect_SE_S_S_Urunhilda_BeatskinFluffyTease_2 = "SE_S_S_Urunhilda_BeatskinFluffyTease_2";
        public static string SkillEffect_SE_S_S_Urunhilda_BeatskinLustfulHand_1 = "SE_S_S_Urunhilda_BeatskinLustfulHand_1";
        public static string SkillEffect_SE_S_S_Urunhilda_GoldenRide_1 = "SE_S_S_Urunhilda_GoldenRide_1";
        public static string SkillEffect_SE_S_S_Urunhilda_GoldenStrokingFeet_1 = "SE_S_S_Urunhilda_GoldenStrokingFeet_1";
        public static string SkillEffect_SE_S_S_Urunhilda_GoldenTwistedPleasure_1 = "SE_S_S_Urunhilda_GoldenTwistedPleasure_1";
        public static string SkillEffect_SE_S_S_Urunhilda_GoldenTwistedPleasure_2 = "SE_S_S_Urunhilda_GoldenTwistedPleasure_2";
        public static string SkillEffect_SE_S_S_Urunhilda_Rare_GoldenBeastkingRapture = "SE_S_S_Urunhilda_Rare_GoldenBeastkingRapture";
        public static string SkillEffect_SE_S_S_Urunhilda_Rare_LustfulRush_0 = "SE_S_S_Urunhilda_Rare_LustfulRush_0";
        public static string SkillEffect_SE_S_S_Urunhilda_Rare_LustfulRush_1 = "SE_S_S_Urunhilda_Rare_LustfulRush_1";
        public static string SkillEffect_SE_S_S_Urunhilda_Rare_LustfulRush_2 = "SE_S_S_Urunhilda_Rare_LustfulRush_2";
        public static string SkillEffect_SE_S_S_Urunhilda_SelfExposing = "SE_S_S_Urunhilda_SelfExposing";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusDrain_2 = "SE_S_S_Urunhilda_SuccubusDrain_2";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusDrillingFeet_1 = "SE_S_S_Urunhilda_SuccubusDrillingFeet_1";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusDrillingFeet_2 = "SE_S_S_Urunhilda_SuccubusDrillingFeet_2";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusDrillingHand_1 = "SE_S_S_Urunhilda_SuccubusDrillingHand_1";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusDrillingHand_2 = "SE_S_S_Urunhilda_SuccubusDrillingHand_2";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusRidingPussy_1 = "SE_S_S_Urunhilda_SuccubusRidingPussy_1";
        public static string SkillEffect_SE_S_S_Urunhilda_SuccubusRidingPussy_2 = "SE_S_S_Urunhilda_SuccubusRidingPussy_2";
        public static string SkillEffect_SE_S_S_Urunhilda_Test = "SE_S_S_Urunhilda_Test";
        public static string SkillEffect_SE_S_S_Urunhilda_TwistedPleasure_2 = "SE_S_S_Urunhilda_TwistedPleasure_2";
        public static string SkillEffect_SE_S_S_Urunhilda_VelvetEmbrace_0 = "SE_S_S_Urunhilda_VelvetEmbrace_0";
        public static string SkillEffect_SE_S_S_Urunhilda_VelvetEmbrace_1 = "SE_S_S_Urunhilda_VelvetEmbrace_1";
        public static string SkillEffect_SE_S_S_Urunhilda_VelvetEmbrace_2 = "SE_S_S_Urunhilda_VelvetEmbrace_2";
        public static string SkillEffect_SE_Tick_B_Urunhilda_MilkedDry = "SE_Tick_B_Urunhilda_MilkedDry";
        public static string SkillEffect_SE_T_S_Urunhilda_BeatskinFluffyTease_0 = "SE_T_S_Urunhilda_BeatskinFluffyTease_0";
        public static string SkillEffect_SE_T_S_Urunhilda_BeatskinFluffyTease_1 = "SE_T_S_Urunhilda_BeatskinFluffyTease_1";
        public static string SkillEffect_SE_T_S_Urunhilda_BeatskinFluffyTease_2 = "SE_T_S_Urunhilda_BeatskinFluffyTease_2";
        public static string SkillEffect_SE_T_S_Urunhilda_BeatskinLustfulHand_0 = "SE_T_S_Urunhilda_BeatskinLustfulHand_0";
        public static string SkillEffect_SE_T_S_Urunhilda_BeatskinLustfulHand_1 = "SE_T_S_Urunhilda_BeatskinLustfulHand_1";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenRide_0 = "SE_T_S_Urunhilda_GoldenRide_0";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenRide_1 = "SE_T_S_Urunhilda_GoldenRide_1";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenStrokingFeet_0 = "SE_T_S_Urunhilda_GoldenStrokingFeet_0";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenStrokingFeet_1 = "SE_T_S_Urunhilda_GoldenStrokingFeet_1";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenTwistedPleasure_0 = "SE_T_S_Urunhilda_GoldenTwistedPleasure_0";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenTwistedPleasure_1 = "SE_T_S_Urunhilda_GoldenTwistedPleasure_1";
        public static string SkillEffect_SE_T_S_Urunhilda_GoldenTwistedPleasure_2 = "SE_T_S_Urunhilda_GoldenTwistedPleasure_2";
        public static string SkillEffect_SE_T_S_Urunhilda_Rare_GoldenBeastkingRapture = "SE_T_S_Urunhilda_Rare_GoldenBeastkingRapture";
        public static string SkillEffect_SE_T_S_Urunhilda_Rare_LustfulRush_0 = "SE_T_S_Urunhilda_Rare_LustfulRush_0";
        public static string SkillEffect_SE_T_S_Urunhilda_Rare_LustfulRush_1 = "SE_T_S_Urunhilda_Rare_LustfulRush_1";
        public static string SkillEffect_SE_T_S_Urunhilda_Rare_LustfulRush_2 = "SE_T_S_Urunhilda_Rare_LustfulRush_2";
        public static string SkillEffect_SE_T_S_Urunhilda_SelfExposing = "SE_T_S_Urunhilda_SelfExposing";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrain_0 = "SE_T_S_Urunhilda_SuccubusDrain_0";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrain_1 = "SE_T_S_Urunhilda_SuccubusDrain_1";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrain_2 = "SE_T_S_Urunhilda_SuccubusDrain_2";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrillingFeet_0 = "SE_T_S_Urunhilda_SuccubusDrillingFeet_0";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrillingFeet_1 = "SE_T_S_Urunhilda_SuccubusDrillingFeet_1";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrillingFeet_2 = "SE_T_S_Urunhilda_SuccubusDrillingFeet_2";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrillingHand_0 = "SE_T_S_Urunhilda_SuccubusDrillingHand_0";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrillingHand_1 = "SE_T_S_Urunhilda_SuccubusDrillingHand_1";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusDrillingHand_2 = "SE_T_S_Urunhilda_SuccubusDrillingHand_2";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusRidingPussy_0 = "SE_T_S_Urunhilda_SuccubusRidingPussy_0";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusRidingPussy_1 = "SE_T_S_Urunhilda_SuccubusRidingPussy_1";
        public static string SkillEffect_SE_T_S_Urunhilda_SuccubusRidingPussy_2 = "SE_T_S_Urunhilda_SuccubusRidingPussy_2";
        public static string SkillEffect_SE_T_S_Urunhilda_TwistedPleasure_0 = "SE_T_S_Urunhilda_TwistedPleasure_0";
        public static string SkillEffect_SE_T_S_Urunhilda_TwistedPleasure_1 = "SE_T_S_Urunhilda_TwistedPleasure_1";
        public static string SkillEffect_SE_T_S_Urunhilda_TwistedPleasure_2 = "SE_T_S_Urunhilda_TwistedPleasure_2";
        public static string SkillEffect_SE_T_S_Urunhilda_VelvetEmbrace_0 = "SE_T_S_Urunhilda_VelvetEmbrace_0";
        public static string SkillEffect_SE_T_S_Urunhilda_VelvetEmbrace_1 = "SE_T_S_Urunhilda_VelvetEmbrace_1";
        public static string SkillEffect_SE_T_S_Urunhilda_VelvetEmbrace_2 = "SE_T_S_Urunhilda_VelvetEmbrace_2";
		/// <summary>
		/// Hazuki Business
		/// Spend 250 Gold to recast this skill.
		/// </summary>
        public static string Skill_S_Ex_Urunhilda_Synergy_0 = "S_Ex_Urunhilda_Synergy_0";
		/// <summary>
		/// Hazuki Part-Time Job
		/// Gain 250 Gold.
		/// </summary>
        public static string Skill_S_Ex_Urunhilda_Synergy_1 = "S_Ex_Urunhilda_Synergy_1";
		/// <summary>
		/// Play with Urunhilda
		/// Spend 500 Gold to obtain random relic.
		/// </summary>
        public static string Skill_S_Ex_Urunhilda_Synergy_2 = "S_Ex_Urunhilda_Synergy_2";
		/// <summary>
		/// Beastkin Fluffy Tease
		/// Create a <color=#FFC300>Beastkin Fluffy Caress</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_BeatskinFluffyTease_0 = "S_Urunhilda_BeatskinFluffyTease_0";
		/// <summary>
		/// Beastkin Fluffy Caress
		/// Create a <color=#FFC300>Beastkin Fluffy Squeeze</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_BeatskinFluffyTease_1 = "S_Urunhilda_BeatskinFluffyTease_1";
		/// <summary>
		/// Beastkin Fluffy Squeeze
		/// OverHeal self by 4.
		/// </summary>
        public static string Skill_S_Urunhilda_BeatskinFluffyTease_2 = "S_Urunhilda_BeatskinFluffyTease_2";
		/// <summary>
		/// Beastkin Lustful Hand
		/// Create a <color=#FFC300>Beastkin Lustful Release</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_BeatskinLustfulHand_0 = "S_Urunhilda_BeatskinLustfulHand_0";
		/// <summary>
		/// Beastkin Lustful Release
		/// OverHeal self by 2.
		/// </summary>
        public static string Skill_S_Urunhilda_BeatskinLustfulHand_1 = "S_Urunhilda_BeatskinLustfulHand_1";
        public static string Skill_S_Urunhilda_DummyHeal = "S_Urunhilda_DummyHeal";
		/// <summary>
		/// Golden Ride
		/// Create a <color=#FFC300>Golden Climax</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenRide_0 = "S_Urunhilda_GoldenRide_0";
		/// <summary>
		/// Golden Climax
		/// OverHeal self by 2.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenRide_1 = "S_Urunhilda_GoldenRide_1";
		/// <summary>
		/// Golden Stroking Feet
		/// Create a <color=#FFC300>Golden Ecstasy Feet</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenStrokingFeet_0 = "S_Urunhilda_GoldenStrokingFeet_0";
		/// <summary>
		/// Golden Ecstasy Feet
		/// OverHeal self by 2.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenStrokingFeet_1 = "S_Urunhilda_GoldenStrokingFeet_1";
		/// <summary>
		/// Golden Twisted Pleasure
		/// Create a <color=#FFC300>Golden Twisted Embrace</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenTwistedPleasure_0 = "S_Urunhilda_GoldenTwistedPleasure_0";
		/// <summary>
		/// Golden Twisted Embrace
		/// Create a <color=#FFC300>Golden Twisted Climax</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenTwistedPleasure_1 = "S_Urunhilda_GoldenTwistedPleasure_1";
		/// <summary>
		/// Golden Twisted Climax
		/// OverHeal self by 4.
		/// </summary>
        public static string Skill_S_Urunhilda_GoldenTwistedPleasure_2 = "S_Urunhilda_GoldenTwistedPleasure_2";
		/// <summary>
		/// Golden Beastkin Fortune
		/// Randomly activate one of 4 effects below:
		/// 1. Draw 2 skills and create random 0-Cost <color=#FFC300>Golden Beastkin</color> skill in hand.
		/// 2. Draw 2 skills and create random 0-Cost <color=#d78fe9>Succubus</color> skill in hand.	
		/// 3. Draw 3 skills, OverHeal all allies by 3 and apply <color=#FF4081>Lustful Desire</color> and <color=#FF4081>Ecstasy Rush</color> to all Male allies.
		/// Urunhilda obtain <color=#FF1493>Rutting Instinct</color>.
		/// 4. Draw 3 skills, expand your Relic inventory by 1 and obtain 500 Gold.
		/// </summary>
        public static string Skill_S_Urunhilda_LucyDraw_0 = "S_Urunhilda_LucyDraw_0";
		/// <summary>
		/// <color=#FFC300>Fluffy Beastkin</color>
		/// Draw 2 skills and create random 0-Cost <color=#FFC300>Golden Beastkin</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_LucyDraw_1 = "S_Urunhilda_LucyDraw_1";
		/// <summary>
		/// <color=#d78fe9>Succubus Embrace</color>
		/// Draw 2 skills and create random 0-Cost <color=#d78fe9>Succubus</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_LucyDraw_2 = "S_Urunhilda_LucyDraw_2";
		/// <summary>
		/// <color=#FF1493>Rutting Instinct</color>
		/// Draw 3 skills, OverHeal all allies by 3 and apply <color=#FF4081>Lustful Desire</color> and <color=#FF4081>Ecstasy Rush</color> to all Male allies.
		/// Urunhilda obtain <color=#FF1493>Rutting Instinct</color>.
		/// </summary>
        public static string Skill_S_Urunhilda_LucyDraw_3 = "S_Urunhilda_LucyDraw_3";
		/// <summary>
		/// <color=#FFC300>Golden Luck</color>
		/// Draw 3 skills, expand your Relic inventory by 1 and obtain 500 Gold.
		/// </summary>
        public static string Skill_S_Urunhilda_LucyDraw_4 = "S_Urunhilda_LucyDraw_4";
		/// <summary>
		/// Golden Beastkin Rapture
		/// OverHeal self by 3. 
		/// If this skill defeat an enemy expand your Relic inventory by 1 and obtain 500 Gold, then exclude this skill from current fight.
		/// </summary>
        public static string Skill_S_Urunhilda_Rare_GoldenBeastkingRapture = "S_Urunhilda_Rare_GoldenBeastkingRapture";
		/// <summary>
		/// Lustful Rush
		/// Create a <color=#FFC300>Ecstasy Desire</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_Rare_LustfulRush_0 = "S_Urunhilda_Rare_LustfulRush_0";
		/// <summary>
		/// Ecstasy Desire
		/// Create a <color=#d78fe9>Gentle Violence</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_Rare_LustfulRush_1 = "S_Urunhilda_Rare_LustfulRush_1";
		/// <summary>
		/// Gentle Violence
		/// </summary>
        public static string Skill_S_Urunhilda_Rare_LustfulRush_2 = "S_Urunhilda_Rare_LustfulRush_2";
		/// <summary>
		/// Self Exposing
		/// Only Female characters can use this skill.
		/// At Max <color=#FF1493>Rutting Instinct</color> draw 2 skills prioritizing the target's skills.
		/// </summary>
        public static string Skill_S_Urunhilda_SelfExposing = "S_Urunhilda_SelfExposing";
		/// <summary>
		/// Succubus Drilling Feet
		/// Create a <color=#d78fe9>Succubus Caress Feet</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusDrillingFeet_0 = "S_Urunhilda_SuccubusDrillingFeet_0";
		/// <summary>
		/// Succubus Caress Feet
		/// Create a <color=#d78fe9>Succubus Squeeze Feet</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusDrillingFeet_1 = "S_Urunhilda_SuccubusDrillingFeet_1";
		/// <summary>
		/// Succubus Squeezing Feet
		/// OverHeal self by 4.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusDrillingFeet_2 = "S_Urunhilda_SuccubusDrillingFeet_2";
		/// <summary>
		/// Succubus Drilling Hand
		/// Create a <color=#d78fe9>Succubus Caress Hand</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusDrillingHand_0 = "S_Urunhilda_SuccubusDrillingHand_0";
		/// <summary>
		/// Succubus Caress Hand
		/// Create a <color=#d78fe9>Succubus Squeeze Hand</color> in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusDrillingHand_1 = "S_Urunhilda_SuccubusDrillingHand_1";
		/// <summary>
		/// Succubus Squeezing Hand
		/// OverHeal self by 4.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusDrillingHand_2 = "S_Urunhilda_SuccubusDrillingHand_2";
		/// <summary>
		/// Succubus Riding Pussy
		/// Create a <color=#d78fe9>Succubus Squeezing Pussy</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusRidingPussy_0 = "S_Urunhilda_SuccubusRidingPussy_0";
		/// <summary>
		/// Succubus Squeezing Pussy
		/// Create a <color=#d78fe9>Succubus Milking Pussy</color> skill in hand.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusRidingPussy_1 = "S_Urunhilda_SuccubusRidingPussy_1";
		/// <summary>
		/// Succubus Milking Pussy
		/// OverHeal self by 4.
		/// </summary>
        public static string Skill_S_Urunhilda_SuccubusRidingPussy_2 = "S_Urunhilda_SuccubusRidingPussy_2";
		/// <summary>
		/// Urunhilda
		/// Passive:
		/// Every level up gain 500 gold, relic pouch and expand your Relic inventory by 1.
		/// At the end of the battle, obtain Gold equal to 10% of the Gold you have.
		/// When attacked, a random Male ally takes the damage instead of Urunhilda and is OverHealed by 3.
		/// Urunhilda applies her buffs as debuffs to enemies and debuffs as buffs to allies.
		/// Obtain <color=#FF1493>Rutting Instinct</color> during fights.
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_Urunhilda = "Urunhilda";
        public static string Character_Skin_Urunhilda_Bondage = "Urunhilda_Bondage";
        public static string Character_Skin_Urunhilda_Engagement = "Urunhilda_Engagement";
        public static string Character_Skin_Urunhilda_Invisible = "Urunhilda_Invisible";
        public static string Character_Skin_Urunhilda_Maternity = "Urunhilda_Maternity";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Careful Lady Urunhilda.
		/// Japanese:
		/// Chinese:
		/// 小心！乌伦希尔妲小姐！
		/// Chinese-TW:
		/// </summary>
        public static string AllyTakesDamage => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("AllyTakesDamage");
		/// <summary>
		/// Korean:
		/// English:
		/// When you touch me, I gradually start to feel good.
		/// Japanese:
		/// Chinese:
		/// 每当你触碰我，我都会更加愉悦。
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BI_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BI_0");
		/// <summary>
		/// Korean:
		/// English:
		/// You can touch me more.
		/// Japanese:
		/// Chinese:
		/// 再多摸摸我......
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BI_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BI_1");
		/// <summary>
		/// Korean:
		/// English:
		/// You're so... naughty...
		/// Japanese:
		/// Chinese:
		/// 你真是...太调皮了...
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BI_2 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BI_2");
		/// <summary>
		/// Korean:
		/// English:
		/// Ah, n-no, don't rub my nipples like that! Hah, hah, hah, hah...
		/// Japanese:
		/// Chinese:
		/// 啊，不-不要，不要那樣揉我的乳頭！哈，哈，哈，哈....
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BI_3 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BI_3");
		/// <summary>
		/// Korean:
		/// English:
		/// Did you come to see me? Feeling lonely, were you?
		/// Japanese:
		/// Chinese:
		/// 你是来看我的吗？感到寂寞了吗？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BS_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BS_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Good morning. So, what's your plan for today?
		/// Japanese:
		/// Chinese:
		/// 早上好，今天有什么计划？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BS_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BS_1");
		/// <summary>
		/// Korean:
		/// English:
		/// What brings you here at this hour?
		/// Japanese:
		/// Chinese:
		/// 来找我做什么呢？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_BS_2 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_BS_2");
		/// <summary>
		/// Korean:
		/// English:
		/// Do you want to see the fruits of my madness? Geez, you're such a perv.
		/// Japanese:
		/// Chinese:
		/// 你想看我疯狂的成果吗？天哪，真是个变态。
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Chest_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Chest_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Huh? I'm going out of my way to do this for you, and that's how you talk to me?
		/// Japanese:
		/// Chinese:
		/// 哈？我特地为你做这个，你就这样跟我说话？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Chest_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Chest_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Who exactly do you think you're talking to?
		/// Japanese:
		/// Chinese:
		/// 你以为你在跟谁说话？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Cri_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Cri_0");
		/// <summary>
		/// Korean:
		/// English:
		/// I never said you could touch me this much, you know!
		/// Japanese:
		/// Chinese:
		/// 我可没说过你可以再碰我！
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Cri_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Cri_1");
		/// <summary>
		/// Korean:
		/// English:
		/// H-Hey, wait a second!
		/// Japanese:
		/// Chinese:
		/// 等-等等！
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Curse_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Curse_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Ugh, fine. I'll let you get away with this much, so just finish it quickly, okay?
		/// Japanese:
		/// Chinese:
		/// 好吧。我会让你做到这样的，所以快点结束，好吗？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Curse_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Curse_1");
		/// <summary>
		/// Korean:
		/// English:
		/// I might not really dislike your situation.
		/// Japanese:
		/// Chinese:
		/// 我可能并不是真的讨厌你。
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_DeathDoorAlly => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_DeathDoorAlly");
		/// <summary>
		/// Korean:
		/// English:
		/// What are you planning to do to me...
		/// Japanese:
		/// Chinese:
		/// 你打算对我做什么...
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_DeathDoor_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_DeathDoor_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Why are you touching me?
		/// Japanese:
		/// Chinese:
		/// 为什么摸我？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_DeathDoor_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_DeathDoor_1");
		/// <summary>
		/// Korean:
		/// English:
		/// I'll become your favorite partner, so teach me various things.
		/// Japanese:
		/// Chinese:
		/// 我会成为你最喜欢的搭档，所以教我各种事情吧。
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_FI_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_FI_0");
		/// <summary>
		/// Korean:
		/// English:
		/// What will you do for me today?
		/// Japanese:
		/// Chinese:
		/// 今天你会为我做什么？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_FI_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_FI_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Tch, fine. I guess we'll just do it, won't we?
		/// Japanese:
		/// Chinese:
		/// 啧，好吧。我想我们直接做吧！
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_FI_2 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_FI_2");
		/// <summary>
		/// Korean:
		/// English:
		/// Ugh... it feels good... I... already...
		/// Japanese:
		/// Chinese:
		/// 呃...感觉好舒服...我...已经...
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Healed_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Healed_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Nngh... H-Hey, touch me more gently, will you!
		/// Japanese:
		/// Chinese:
		/// 唔...嘿-嘿，再温柔地摸摸我，好吗。
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Healed_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Healed_1");
		/// <summary>
		/// Korean:
		/// English:
		/// I-I-I-I-I-I-I... I can't... I-I'm c-coming!
		/// Japanese:
		/// Chinese:
		/// 我-我-我...我不能...我-我去了！
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Kill_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Kill_0");
		/// <summary>
		/// Korean:
		/// English:
		/// W-why are you making me do something so embarrassing...?
		/// Japanese:
		/// Chinese:
		/// 为-为什么你要让我做这么尴尬的事情...？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Kill_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Kill_1");
		/// <summary>
		/// Korean:
		/// English:
		/// You're so noisy, just stop asking, idiot, go away already!
		/// Japanese:
		/// Chinese:
		/// 你好吵，别问了，白痴，快滚开！
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Master => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Master");
		/// <summary>
		/// Korean:
		/// English:
		/// Another trial, huh? Geez. So what are you trying to make me do this time?
		/// Japanese:
		/// Chinese:
		/// 又是试炼，嗯...那么这次你想让我做什么？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Pharos_0 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Pharos_0");
		/// <summary>
		/// Korean:
		/// English:
		/// I don't need you to tell me! Leave me alone, you perverted freak!
		/// Japanese:
		/// Chinese:
		/// 我不需要你告诉我！离我远点，你这个变态！
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Pharos_1 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Pharos_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Nngh... Do I... really... have to do this...?
		/// Japanese:
		/// Chinese:
		/// 唔...我真的...必须做这个...？
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Pharos_2 => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Pharos_2");
		/// <summary>
		/// Korean:
		/// English:
		/// More...
		/// Japanese:
		/// Chinese:
		/// 我还要更多...
		/// Chinese-TW:
		/// </summary>
        public static string Urunhilda_Potion => ModManager.getModInfo("Urunhilda").localizationInfo.SystemLocalizationUpdate("Urunhilda_Potion");

    }
}