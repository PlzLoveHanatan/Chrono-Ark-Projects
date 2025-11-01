using ChronoArkMod;
namespace EmotionSystem
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Despair
		/// Critical is increased by &a <color=#FF7C34>(Emotional Level * 5)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv1_Despair = "B_Abnormality_GuestLv1_Despair";
		/// <summary>
		/// Giant Mushroom
		/// Debuff Resistance is increased by &a <color=#FF7C34>(Emotional Level * 5)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv1_GiantMushroom = "B_Abnormality_GuestLv1_GiantMushroom";
		/// <summary>
		/// Strengthen
		/// Attack Power is increased by &a% <color=#FF7C34>(Emotional Level * 5)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv1_Strengthen = "B_Abnormality_GuestLv1_Strengthen";
		/// <summary>
		/// Stress
		/// Defense is increased by &a <color=#FF7C34>(Emotional Level * 5)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv1_Stress = "B_Abnormality_GuestLv1_Stress";
		/// <summary>
		/// Unity
		/// Apply Taunt status to all allies except self.
		/// At the end of the turn heal all allies by &a <color=#FF7C34>(Emotional Level * 5)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv1_Unity = "B_Abnormality_GuestLv1_Unity";
		/// <summary>
		/// You Must be Happy
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv1_YouMustbeHappy = "B_Abnormality_GuestLv1_YouMustbeHappy";
		/// <summary>
		/// Behaviour Adjustment
		/// Every turn dodge the first skill (except Lucy skills) or block 1 debuff. Abnormality status: &a.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_BehaviourAdjustment = "B_Abnormality_GuestLv2_BehaviourAdjustment";
		/// <summary>
		/// Behaviour Adjustment
		/// Dodge the first skill (except Lucy skills) and block 1 debuff.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_BehaviourAdjustment_0 = "B_Abnormality_GuestLv2_BehaviourAdjustment_0";
		/// <summary>
		/// Energy Conversion
		/// When this character loses 50% of its Max HP (20% for bosses), lose 1 Mana for the turn.
		/// Remaining damage: &a.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_EnergyConversion = "B_Abnormality_GuestLv2_EnergyConversion";
		/// <summary>
		/// Mirror Adjustment
		/// Every turn reflect 1 attack (except Lucy and additonal attacks). Current Abnormality status: &a.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_MirrorAdjustment = "B_Abnormality_GuestLv2_MirrorAdjustment";
		/// <summary>
		/// Mirror
		/// Attacker takes non-lethal <color=purple>Pain damage</color> equal to <color=#FF7C34>(80% of damage dealt)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_MirrorAdjustment_0 = "B_Abnormality_GuestLv2_MirrorAdjustment_0";
		/// <summary>
		/// <color=red>Present</color>
		/// At the start of the turn shuffle 1 <color=red>Present</color> into your deck.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_Present = "B_Abnormality_GuestLv2_Present";
		/// <summary>
		/// Shelter
		/// Upon reaching 0 health, this character receives a buff that grants invincibility for 2 turns.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_Shelter = "B_Abnormality_GuestLv2_Shelter";
		/// <summary>
		/// Shelter
		/// This character dies while taking &a damage <color=#FF7C34>(Emotional Level * 15)</color> or more damage from a single hit this Scene. Removed after &b turn(s).
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_Shelter_0 = "B_Abnormality_GuestLv2_Shelter_0";
		/// <summary>
		/// Storytime
		/// Select a party member at the start of the turn. Apply <color=#5A6A8B>Solitude</color> (<sprite=2> 125%) to the character and make them unable to act. <color=#5A6A8B>Solitude</color> can be removed by healing or taking damage.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_Storytime = "B_Abnormality_GuestLv2_Storytime";
		/// <summary>
		/// <color=#5A6A8B>Solitude</color>
		/// Stunned. Removed when healed or taking damage.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv2_Storytime_0 = "B_Abnormality_GuestLv2_Storytime_0";
		/// <summary>
		/// Bait
		/// At the start of the turn remove 1 random skill from hand and shuffle it into draw pile and lose 1 Mana.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv3_Bait = "B_Abnormality_GuestLv3_Bait";
		/// <summary>
		/// Cycle of the Curse
		/// At the start of the turn select one skill from hand to exclude from current fight. Skill owner takes non-lethal <color=purple>&a Pain Damage</color> <color=#FF7C34>(Current Stage * 5 + 5)</color>. If skill owner is Lucy lose 2 Mana instead.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv3_CycleCurse = "B_Abnormality_GuestLv3_CycleCurse";
		/// <summary>
		/// Dimensional Refraction
		/// At the start of each Scene, lose 1 Mana. Random skill in hand becomes <color=#A6B0FF>Blurred</color> for the rest of the Scene.
		/// </summary>
        public static string Buff_B_Abnormality_GuestLv3_DimensionalRefraction = "B_Abnormality_GuestLv3_DimensionalRefraction";
		/// <summary>
		/// Ashes
		/// Attacks inflict 1 <color=#FC6178>Burn</color> (<sprite=1> &a%).
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_Ashes = "B_Abnormality_HistoryLv1_Ashes";
		/// <summary>
		/// Display Affection
		/// Gain Ignore Taunt on all skills.
		/// Deal 20% more damage if the target's Action Count is 1, 2 or 9+. Otherwise, deal 40% less damage.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_DisplayAffection = "B_Abnormality_HistoryLv1_DisplayAffection";
		/// <summary>
		/// The Fairies' Care
		/// At the end of the turn restore &a health <color=#FF7C34>(20% Max Health)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_FairiesCare = "B_Abnormality_HistoryLv1_FairiesCare";
		/// <summary>
		/// Happy Memories
		/// The first skill played from hand costs 1 less.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_HappyMemories = "B_Abnormality_HistoryLv1_HappyMemories";
        public static string Buff_B_Abnormality_HistoryLv1_HappyMemories_0 = "B_Abnormality_HistoryLv1_HappyMemories_0";
		/// <summary>
		/// Matchlight
		/// Attacks inflict 2 <color=#FC6178>Burn</color> (<sprite=1> &b%).
		/// Gain 20% chance to take non-lethal <color=purple>&a Pain damage</color> <color=#FF7C34>(15% Max Health)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_Matchlight = "B_Abnormality_HistoryLv1_Matchlight";
		/// <summary>
		/// Nostalgic Embrace of the Old Days
		/// Whenever you damage an enemy, gain 50% chance to apply stun (<sprite=2>&a%) to the Guest. Only activates once per turn.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_NostalgicEmbrace = "B_Abnormality_HistoryLv1_NostalgicEmbrace";
		/// <summary>
		/// Footfalls
		/// When you damage an enemy while your HP is 20% or lower, deal <color=purple>Pain damage</color> equal to 80% of the target's Max HP (up to 100), inflict 10 <color=#FC6178>Burn</color>, <b>die</b> and gain 1 Lucy's Neckless charge.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Footfalls = "B_Abnormality_HistoryLv2_Footfalls";
		/// <summary>
		/// Gluttony
		/// Heal 20% of all damage dealt.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Gluttony = "B_Abnormality_HistoryLv2_Gluttony";
		/// <summary>
		/// Predation
		/// At the end of the turn take non-lethal <color=purple>&a Pain Damage</color> equal <color=#FF7C34>(30% Max Health)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Predation = "B_Abnormality_HistoryLv2_Predation";
		/// <summary>
		/// Spores
		/// Inflict 4 <color=#FC6178>Burn</color> (<sprite=1> &a%) and 4 <color=red>Bleed</color> (<sprite=1> &a%) to the attacker.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Spores = "B_Abnormality_HistoryLv2_Spores";
		/// <summary>
		/// Vines
		/// At the start of the turn, apply <color=orange>Bind</color> (<sprite=2> &a%) to all enemies.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Vines = "B_Abnormality_HistoryLv2_Vines";
		/// <summary>
		/// Vines of Despair
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Vines_0 = "B_Abnormality_HistoryLv2_Vines_0";
		/// <summary>
		/// Worker Bee
		/// Inflict <color=red>Pollen</color> (<sprite=0> &a%) to the attacker.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_WorkerBee = "B_Abnormality_HistoryLv2_WorkerBee";
		/// <summary>
		/// <color=#DC143C>Pollen</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_WorkerBee_0 = "B_Abnormality_HistoryLv2_WorkerBee_0";
		/// <summary>
		/// Barrier of Thorns
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_BarrierThorns = "B_Abnormality_HistoryLv3_BarrierThorns";
		/// <summary>
		/// <color=red>Loyalty</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_Loyalty = "B_Abnormality_HistoryLv3_Loyalty";
		/// <summary>
		/// Malice
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_Malice = "B_Abnormality_HistoryLv3_Malice";
		/// <summary>
		/// Axe
		/// Attacks inflict 1 <color=red>Bleed</color> (<sprite=1> &a%).
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv1_Axe = "B_Abnormality_LiteratureLv1_Axe";
		/// <summary>
		/// Cocoon
		/// Attacks inflict <color=#F0FF64>Paralysis</color> (<sprite=0> &a%), <color=red>Fragile</color> (<sprite=0> &a%) and <color=orange>Bind</color> (<sprite=2> &b%) to the target's. Only activates once per turn.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv1_Cocoon = "B_Abnormality_LiteratureLv1_Cocoon";
		/// <summary>
		/// Glitter
		/// Attacks inflict 1 <color=red>Bleed</color> (<sprite=1> &a%).
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv1_Glitter = "B_Abnormality_LiteratureLv1_Glitter";
		/// <summary>
		/// Look of the Day
		/// Increase Attack Power depending on the curent face (Min -40%, Max +40%).
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv1_LookDay = "B_Abnormality_LiteratureLv1_LookDay";
		/// <summary>
		/// Social Distancing
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv1_SocialDistancing = "B_Abnormality_LiteratureLv1_SocialDistancing";
		/// <summary>
		/// Surprise Gift
		/// At the start of each Scene apply <color=green>Friend</color> (Damage/healing increased by 40%) status to 1 random page in hand, prioritizing the owner pages.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv1_SurpriseGift = "B_Abnormality_LiteratureLv1_SurpriseGift";
		/// <summary>
		/// Alertness
		/// Attacks inflict <color=#F0FF64>Paralysis</color> (<sprite=0> &a%), <color=red>Fragile</color> (<sprite=0> &a%) and  <color=orange>Bind</color> (<sprite=2> &b%) to the target's. Only activates twice per turn.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv2_Alertness = "B_Abnormality_LiteratureLv2_Alertness";
		/// <summary>
		/// Friend
		/// At the start of each Scene apply <color=red>Friend</color> (Discarded after 1 turn, damage/healing increased by 80%) status to 2 random pages in hand, prioritizing the owner pages.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv2_Friend = "B_Abnormality_LiteratureLv2_Friend";
		/// <summary>
		/// Funny Prank
		/// Attacks have 50% to gain guaranteed critical.
		/// Take non-lethal <color=purple>&a Pain damage</color> <color=#FF7C34>(40% Max Health)</color> when the attack is not critical.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv2_FunnyPrank = "B_Abnormality_LiteratureLv2_FunnyPrank";
		/// <summary>
		/// Meal
		/// Heal 20% of all damage dealt.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv2_Meal = "B_Abnormality_LiteratureLv2_Meal";
		/// <summary>
		/// Obsession
		/// Attack inflict 2 <color=red>Bleed</color> (<sprite=1> &a%).
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv2_Obsession = "B_Abnormality_LiteratureLv2_Obsession";
		/// <summary>
		/// Shyness
		/// Deal <color=purple>&a Pain Damage</color> <color=#FF7C34>(50% Armor)</color> to the attacker.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv2_Shyness = "B_Abnormality_LiteratureLv2_Shyness";
		/// <summary>
		/// Gooey Waste
		/// Attacks inflict <color=#F0FF64>Paralysis</color> (<sprite=0> &a%), <color=red>Fragile</color> (<sprite=0> &a%), <color=orange>Bind</color> (<sprite=2> &b%) and <color=red>Bleed</color> (<sprite=1> &c%) to the target's.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv3_GooeyWaste = "B_Abnormality_LiteratureLv3_GooeyWaste";
		/// <summary>
		/// Loving Family
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv3_LovingFamily = "B_Abnormality_LiteratureLv3_LovingFamily";
		/// <summary>
		/// Nettle Clothing
		/// Reduce the next received damage to 0 and remove 1 stack.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv3_LovingFamily_0 = "B_Abnormality_LiteratureLv3_LovingFamily_0";
		/// <summary>
		/// Well-worn Parasol
		/// Gain 50% chance to reduce the next received damage to 0 and reflect double damage to the attacker.
		/// </summary>
        public static string Buff_B_Abnormality_LiteratureLv3_WornParasol = "B_Abnormality_LiteratureLv3_WornParasol";
		/// <summary>
		/// Lament
		/// At the end of the turn take non-lethal <color=purple>&a Pain Damage</color> <color=#FF7C34>(15% Max Health)</color>.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Lament = "B_Abnormality_TechnologicalLv1_Lament";
		/// <summary>
		/// Metallic Ringing
		/// Attacks inflict <color=#F0FF64>Paralysis</color> (<sprite=0> &a%).
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_MetallicRinging = "B_Abnormality_TechnologicalLv1_MetallicRinging";
		/// <summary>
		/// Repetitive Pattern-Recognition
		/// If user plays an Attack this turn, gain 1 Mana at the start of the next turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_RepetitivePattern = "B_Abnormality_TechnologicalLv1_RepetitivePattern";
		/// <summary>
		/// Request
		/// At the start of the turn, apply <color=#6291EC>Requested Target</color> to a random enemy.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Request = "B_Abnormality_TechnologicalLv1_Request";
		/// <summary>
		/// Requested Target
		/// Removed at the start of the next turn. Can be targeted regardless Taunt status.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Request_0 = "B_Abnormality_TechnologicalLv1_Request_0";
		/// <summary>
		/// Rhythm
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Rhythm = "B_Abnormality_TechnologicalLv1_Rhythm";
		/// <summary>
		/// Violence
		/// Attack deal 0~300% of damage.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Violence = "B_Abnormality_TechnologicalLv1_Violence";
		/// <summary>
		/// Chained Wrath
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_ChainedWrath = "B_Abnormality_TechnologicalLv2_ChainedWrath";
		/// <summary>
		/// Clean
		/// Restore 1 Mana upon landing a critical hit. Only activates twice per turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_Clean = "B_Abnormality_TechnologicalLv2_Clean";
		/// <summary>
		/// Eternal Rest
		/// Whenever you deal more than 10% of the target's Max Health as damage, apply Stun (<sprite=2>&a%) to the target.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_EternalRest = "B_Abnormality_TechnologicalLv2_EternalRest";
		/// <summary>
		/// Musical Addiction
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_MusicalAddiction = "B_Abnormality_TechnologicalLv2_MusicalAddiction";
		/// <summary>
		/// Recharge
		/// Gain 1 Mana at the start of the turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_Recharge = "B_Abnormality_TechnologicalLv2_Recharge";
		/// <summary>
		/// The Seventh Bullet
		/// Every 7th Attack target a random character, except user. Attacks Played: &a.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_SeventhBullet = "B_Abnormality_TechnologicalLv2_SeventhBullet";
        public static string Buff_B_Abnormality_TechnologicalLv2_SeventhBullet_0 = "B_Abnormality_TechnologicalLv2_SeventhBullet_0";
        public static string Buff_B_Abnormality_TechnologicalLv2_SeventhBullet_1 = "B_Abnormality_TechnologicalLv2_SeventhBullet_1";
		/// <summary>
		/// Coffin
		/// Attack skills destroys target's Action Point. Only activates once per turn. Current Status: &a.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_Coffin = "B_Abnormality_TechnologicalLv3_Coffin";
		/// <summary>
		/// Dark Flame
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_DarkFlame = "B_Abnormality_TechnologicalLv3_DarkFlame";
		/// <summary>
		/// <color=red>Gebrochener Pakt</color>
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_DarkFlame_0 = "B_Abnormality_TechnologicalLv3_DarkFlame_0";
		/// <summary>
		/// Music
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_Music = "B_Abnormality_TechnologicalLv3_Music";
		/// <summary>
		/// Hornet Sting
		/// </summary>
        public static string Buff_B_EGO_History_HornetSting = "B_EGO_History_HornetSting";
		/// <summary>
		/// Sanguine Desire
		/// Attacks inflict 1 <color=red>Bleed</color> (<sprite=1> &a%).
		/// </summary>
        public static string Buff_B_EGO_Literature_SanguineDesire = "B_EGO_Literature_SanguineDesire";
		/// <summary>
		/// Harmony
		/// </summary>
        public static string Buff_B_EGO_Technological_Harmony = "B_EGO_Technological_Harmony";
		/// <summary>
		/// Magic Bullet
		/// Synchronized with Der Freischütz.
		/// </summary>
        public static string Buff_B_EGO_Technological_MagicBullet = "B_EGO_Technological_MagicBullet";
		/// <summary>
		/// <color=orange>Bind</color>
		/// Remove 1 stack at the start of the next turn.
		/// </summary>
        public static string Buff_B_EmotionSystem_Bind = "B_EmotionSystem_Bind";
		/// <summary>
		/// <color=red>Bleed</color>
		/// Description
		/// </summary>
        public static string Buff_B_EmotionSystem_Bleed = "B_EmotionSystem_Bleed";
		/// <summary>
		/// <color=#FC6178>Burn</color>
		/// Description
		/// </summary>
        public static string Buff_B_EmotionSystem_Burn = "B_EmotionSystem_Burn";
		/// <summary>
		/// Disarm
		/// </summary>
        public static string Buff_B_EmotionSystem_Disarm = "B_EmotionSystem_Disarm";
		/// <summary>
		/// Feeble
		/// </summary>
        public static string Buff_B_EmotionSystem_Feeble = "B_EmotionSystem_Feeble";
		/// <summary>
		/// <color=red>Fragile</color>
		/// Removed when receive Critical hit.
		/// </summary>
        public static string Buff_B_EmotionSystem_Fragile = "B_EmotionSystem_Fragile";
		/// <summary>
		/// <color=#F0FF64>Paralysis</color>
		/// The effect remains until next attack.
		/// </summary>
        public static string Buff_B_EmotionSystem_Paralysis = "B_EmotionSystem_Paralysis";
		/// <summary>
		/// Dice
		/// Gain 1 action count.
		/// </summary>
        public static string Buff_B_Guest_Dice = "B_Guest_Dice";
		/// <summary>
		/// <color=red>Distortion: Marauding</color>
		/// Gain 2 action points.
		/// </summary>
        public static string Buff_B_Guest_Distortion_0 = "B_Guest_Distortion_0";
        public static string Buff_B_Guest_Distortion_0_0 = "B_Guest_Distortion_0_0";
		/// <summary>
		/// <color=red>Distortion: Colossal</color>
		/// Gain 1 action point.
		/// </summary>
        public static string Buff_B_Guest_Distortion_1 = "B_Guest_Distortion_1";
		/// <summary>
		/// <color=red>Distortion: Robust</color>
		/// Gain 1 action point.
		/// Block one incoming attack every turn.
		/// </summary>
        public static string Buff_B_Guest_Distortion_2 = "B_Guest_Distortion_2";
		/// <summary>
		/// <color=red>Distortion: Horrifying</color>
		/// Gain 1 action point.
		/// Remove 1 Mana.
		/// </summary>
        public static string Buff_B_Guest_Distortion_3 = "B_Guest_Distortion_3";
		/// <summary>
		/// <color=red>Distortion: Executioner</color>
		/// Gain 1 action point.
		/// After ending your turn, all characters below 0 health will faint.
		/// </summary>
        public static string Buff_B_Guest_Distortion_4 = "B_Guest_Distortion_4";
		/// <summary>
		/// <color=red>Distortion: Unstoppable</color>
		/// Gain 1 action point.
		/// </summary>
        public static string Buff_B_Guest_Distortion_5 = "B_Guest_Distortion_5";
        public static string Buff_B_Guest_Emotional_Level = "B_Guest_Emotional_Level";
		/// <summary>
		/// Light
		/// Next attack damage is increased by &a% <color=#FF7C34>(Emotional Level * 5)</color>.
		/// The effect remains until next attack.
		/// </summary>
        public static string Buff_B_Guest_Light = "B_Guest_Light";
        public static string Buff_B_Guest_TeamLevel = "B_Guest_TeamLevel";
        public static string Buff_B_Investigator_Draw = "B_Investigator_Draw";
		/// <summary>
		/// Investigator
		/// </summary>
        public static string Buff_B_Investigator_Emotional_Level = "B_Investigator_Emotional_Level";
        public static string Buff_B_Investigator_ManaReduction = "B_Investigator_ManaReduction";
		/// <summary>
		/// Lucy
		/// </summary>
        public static string Buff_B_Lucy_Emotional_Level = "B_Lucy_Emotional_Level";
		/// <summary>
		/// Music Box
		/// Gain additional Emotion Points (except additional sources). Remove turn cap.
		/// </summary>
        public static string Buff_B_Lucy_MusicBox = "B_Lucy_MusicBox";
		/// <summary>
		/// <color=#8A2BE2>Dark Melody</color>
		/// Cannot gain Emotion Points.
		/// </summary>
        public static string Buff_B_Potion_DarkTune = "B_Potion_DarkTune";
		/// <summary>
		/// <color=#FFDF00>Golden Melody</color>
		/// Gain additional Emotion Points (except additional sources). Remove turn cap.
		/// </summary>
        public static string Buff_B_Potion_PureTune = "B_Potion_PureTune";
		/// <summary>
		/// Dreaming Current
		/// Learn 1 <color=#00FFFF>unique</color>  Lucy draw skill.
		/// </summary>
        public static string Item_Consume_C_EmotionSystem_DreamingCurrent = "C_EmotionSystem_DreamingCurrent";
		/// <summary>
		/// Passive:
		/// </summary>
        public static string Character_EmotionSystem_Floor_History = "EmotionSystem_Floor_History";
		/// <summary>
		/// Passive:
		/// </summary>
        public static string Character_EmotionSystem_Floor_Literature = "EmotionSystem_Floor_Literature";
		/// <summary>
		/// Passive:
		/// </summary>
        public static string Character_EmotionSystem_Floor_Technological = "EmotionSystem_Floor_Technological";
		/// <summary>
		/// Passive:
		/// </summary>
        public static string Character_EmotionSystem_Guest = "EmotionSystem_Guest";
		/// <summary>
		/// <color=green>Friend</color>
		/// This skill's damage/healing is increased by 40%.
		/// </summary>
        public static string SkillExtended_Ex_Abnormality_Friend = "Ex_Abnormality_Friend";
		/// <summary>
		/// <color=red>Friend</color>
		/// This skill's damage/healing is increased by 80%.
		/// </summary>
        public static string SkillExtended_Ex_Abnormality_Friend_0 = "Ex_Abnormality_Friend_0";
		/// <summary>
		/// <color=#FFDF00>Friend</color>
		/// This skill's damage/healing is increased by 80%.
		/// </summary>
        public static string SkillExtended_Ex_Abnormality_Friend_1 = "Ex_Abnormality_Friend_1";
		/// <summary>
		/// Happy Memories
		/// The first skill played from hand costs 1 less.
		/// </summary>
        public static string SkillExtended_Ex_Abnormality_HappyMemories = "Ex_Abnormality_HappyMemories";
		/// <summary>
		/// The Seventh Bullet
		/// This Attack target a random character, except user.
		/// </summary>
        public static string SkillExtended_Ex_Abnormality_SeventhBullet = "Ex_Abnormality_SeventhBullet";
        public static string SkillExtended_Ex_EGO = "Ex_EGO";
		/// <summary>
		/// E.G.O. Cooldown
		/// </summary>
        public static string SkillExtended_Ex_EGO_Cooldown = "Ex_EGO_Cooldown";
		/// <summary>
		/// Emotional Level 5
		/// At 2 stacks, draw 1 skill.
		/// </summary>
        public static string SkillExtended_Ex_Investigator_Draw = "Ex_Investigator_Draw";
		/// <summary>
		/// Emotional Level 4
		/// The first skill played from hand costs 1 less.
		/// </summary>
        public static string SkillExtended_Ex_Investigator_ManaReduction = "Ex_Investigator_ManaReduction";
		/// <summary>
		/// <color=red>Gebrochener Pakt</color>
		/// Debuff Resist -300%
		/// Cannot be disabled
		/// </summary>
        public static string SkillKeyword_KeyWord_Abnormality_DarkFlame = "KeyWord_Abnormality_DarkFlame";
		/// <summary>
		/// Blurred
		/// Cannot be used this Scene.
		/// </summary>
        public static string SkillKeyword_KeyWord_Abnormality_DimensionalRefraction = "KeyWord_Abnormality_DimensionalRefraction";
		/// <summary>
		/// <color=red>Loyality</color>
		/// Attack Power +40%
		/// Healing Power +40%
		/// Critical Damage +40%
		/// Critical Healing +40%
		/// </summary>
        public static string SkillKeyword_KeyWord_Abnormality_Loyality = "KeyWord_Abnormality_Loyality";
		/// <summary>
		/// Abnormality Level I
		/// </summary>
        public static string SkillKeyword_Keyword_Abnormality_Lv1 = "Keyword_Abnormality_Lv1";
		/// <summary>
		/// Abnormality Level II
		/// </summary>
        public static string SkillKeyword_Keyword_Abnormality_Lv2 = "Keyword_Abnormality_Lv2";
		/// <summary>
		/// Abnormality Level III
		/// </summary>
        public static string SkillKeyword_Keyword_Abnormality_Lv3 = "Keyword_Abnormality_Lv3";
		/// <summary>
		/// <color=#6291EC>Requested Target</color>
		/// Receiving Damage +10%
		/// Removed at the start of the next turn. Can be targeted regardless Taunt status.
		/// </summary>
        public static string SkillKeyword_KeyWord_Abnormality_Request = "KeyWord_Abnormality_Request";
		/// <summary>
		/// <color=#C3CBD8>Entangled</color>
		/// Evade -20%
		/// Removed at the start of the next turn. Can be targeted regardless Taunt status.
		/// </summary>
        public static string SkillKeyword_KeyWord_Abnormality_Vines = "KeyWord_Abnormality_Vines";
		/// <summary>
		/// <color=red>Pollen</color>
		/// Receiving Damage +40%
		/// Max 2 stacks
		/// </summary>
        public static string SkillKeyword_KeyWord_Abnormality_WorkerBee = "KeyWord_Abnormality_WorkerBee";
		/// <summary>
		/// <color=orange>Bind</color>
		/// Speed -1 (Max 1)
		/// Evade -10%
		/// Max 5 stacks.
		/// </summary>
        public static string SkillKeyword_KeyWord_Bind = "KeyWord_Bind";
		/// <summary>
		/// <color=red>Bleed</color>
		/// Base<sprite=1> 100%
		/// Take <color=purple>X Pain damage</color> <color=#FF7C34>(Bleed * 3)</color>  and subtract 1/3rd of the <color=red>Bleed</color> stacks every time the character perform an action. (Rounds up)
		/// </summary>
        public static string SkillKeyword_KeyWord_Bleed = "KeyWord_Bleed";
		/// <summary>
		/// <color=#FC6178>Burn</color>
		/// Base<sprite=1> 100%
		/// At the end of the Scene, take <color=purple>X Pain damage</color> <color=#FF7C34>(Burn * 2)</color> and subtract 1/3rd of the <color=#FC6178>Burn</color> stack. (Rounds down)
		/// </summary>
        public static string SkillKeyword_KeyWord_Burn = "KeyWord_Burn";
		/// <summary>
		/// <color=#FFDF00>E.G.O.</color>
		/// Cannot be dodged.
		/// </summary>
        public static string SkillKeyword_KeyWord_EGO = "KeyWord_EGO";
		/// <summary>
		/// Nettle Clothing
		/// Reduce the next received damage to 0 and remove 1 stack.
		/// </summary>
        public static string SkillKeyword_KeyWord_EGO_BlackSwan = "KeyWord_EGO_BlackSwan";
		/// <summary>
		/// Desynchronizes
		/// Remove all unique user skills from the deck and replace them with the previous skills.
		/// </summary>
        public static string SkillKeyword_KeyWord_EGO_Desynchronizes = "KeyWord_EGO_Desynchronizes";
		/// <summary>
		/// Synchronize E.G.O.
		/// Remove all user skills from the deck, shuffle 8 unique pages to the deck, and change fixed ability to 'Desynchronize'.
		/// </summary>
        public static string SkillKeyword_KeyWord_EGO_Synchronize = "KeyWord_EGO_Synchronize";
		/// <summary>
		/// Unique Page
		/// Becomes available for use only while Synchronized.
		/// </summary>
        public static string SkillKeyword_KeyWord_EGO_Synchronize_Skill = "KeyWord_EGO_Synchronize_Skill";
		/// <summary>
		/// <color=#C067EF>Emotion Burst</color>
		/// Activates if all allies are at Emotion Level 5.
		/// </summary>
        public static string SkillKeyword_KeyWord_Emotion_Burst = "KeyWord_Emotion_Burst";
		/// <summary>
		/// <color=red>Fragile</color>
		/// Receiving Critical Chance +10%
		/// Removed when receive Critical hit.
		/// Max 5 stacks.
		/// </summary>
        public static string SkillKeyword_KeyWord_Fragile = "KeyWord_Fragile";
		/// <summary>
		/// <color=#F0FF64>Paralysis</color>
		/// Attack Power -10%
		/// The effect remains until next attack.
		/// Max 5 stacks.
		/// </summary>
        public static string SkillKeyword_KeyWord_Paralysis = "KeyWord_Paralysis";
		/// <summary>
		/// Dark Tune
		/// Apply <color=#8A2BE2>Dark Melody</color> to all enemies (cannot gain Emotion Points) for 1 turn.
		/// <color=#919191>Why is it that macabre can be felt in this melody?</color>
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_DarkTune = "P_EmotionSystem_DarkTune";
		/// <summary>
		/// Distilled Suffering
		/// Inflict 6 <color=red>Bleed</color> to the target.
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_DistilledSuffering = "P_EmotionSystem_DistilledSuffering";
		/// <summary>
		/// Distortion Fragment
		/// View 2 <color=#FFDF00>E.G.O.</color> skills and select 1 to add into <color=#FFDF00>E.G.O.</color> Hand.
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_DistortionFragment = "P_EmotionSystem_DistortionFragment";
		/// <summary>
		/// Essence of Tranquility
		/// View 3 <color=#32CD32>Positive</color> Level II Abnormalities and select 1 to apply to an ally.
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_EssenceTranquility = "P_EmotionSystem_EssenceTranquility";
		/// <summary>
		/// Essence of Wrath
		/// View 3 <color=red>Negative</color> Level II Abnormalities and select 1 to apply to an ally.
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_EssenceWrath = "P_EmotionSystem_EssenceWrath";
		/// <summary>
		/// Ignited Remorse
		/// Inflict 8 <color=#FC6178>Burn</color> to the target.
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_IgnitedRemorse = "P_EmotionSystem_IgnitedRemorse";
		/// <summary>
		/// Pure Tune
		/// Apply <color=#FFDF00>Golden Melody</color> to all allies (gain additional Emotion Points. Remove turn cap) for 1 turn.
		/// <color=#919191>Wind up the spring, and cure the ailing mind.</color>
		/// </summary>
        public static string Item_Potions_P_EmotionSystem_PureTune = "P_EmotionSystem_PureTune";
		/// <summary>
		/// Golden Sound
		/// At the start of the turn all allies gain 3 <color=green>Positive</color> Emotion Points.
		/// <color=#919191>Remember this melody? With love, Theresia.</color>
		/// </summary>
        public static string Item_Passive_R_EmotionSystem_GoldenSound = "R_EmotionSystem_GoldenSound";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_Despair = "SE_S_S_Abnormality_GuestLv1_Despair";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_GiantMushroom = "SE_S_S_Abnormality_GuestLv1_GiantMushroom";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_RainbowSea = "SE_S_S_Abnormality_GuestLv1_RainbowSea";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_Strengthen = "SE_S_S_Abnormality_GuestLv1_Strengthen";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_Stress = "SE_S_S_Abnormality_GuestLv1_Stress";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_Unity = "SE_S_S_Abnormality_GuestLv1_Unity";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv1_YouMustbeHappy = "SE_S_S_Abnormality_GuestLv1_YouMustbeHappy";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv2_BehaviourAdjustment = "SE_S_S_Abnormality_GuestLv2_BehaviourAdjustment";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv2_EnergyConversion = "SE_S_S_Abnormality_GuestLv2_EnergyConversion";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv2_MirrorAdjustment = "SE_S_S_Abnormality_GuestLv2_MirrorAdjustment";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv2_Present = "SE_S_S_Abnormality_GuestLv2_Present";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv2_Shelter = "SE_S_S_Abnormality_GuestLv2_Shelter";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv2_Storytime = "SE_S_S_Abnormality_GuestLv2_Storytime";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv3_Bait = "SE_S_S_Abnormality_GuestLv3_Bait";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv3_CycleCurse = "SE_S_S_Abnormality_GuestLv3_CycleCurse";
        public static string SkillEffect_SE_S_S_Abnormality_GuestLv3_DimensionalRefraction = "SE_S_S_Abnormality_GuestLv3_DimensionalRefraction";
        public static string SkillEffect_SE_S_S_Abnormality_Literature_Lv1_LookDay = "SE_S_S_Abnormality_Literature_Lv1_LookDay";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv1_MetallicRinging = "SE_S_S_Abnormality_TechnologicalLv1_MetallicRinging";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv1_RepetitivePattern = "SE_S_S_Abnormality_TechnologicalLv1_RepetitivePattern";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv1_Request = "SE_S_S_Abnormality_TechnologicalLv1_Request";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv1_Rhythm = "SE_S_S_Abnormality_TechnologicalLv1_Rhythm";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv1_Violence = "SE_S_S_Abnormality_TechnologicalLv1_Violence";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv2_ChainedWrath = "SE_S_S_Abnormality_TechnologicalLv2_ChainedWrath";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv2_Clean = "SE_S_S_Abnormality_TechnologicalLv2_Clean";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv2_EternalRest = "SE_S_S_Abnormality_TechnologicalLv2_EternalRest";
        public static string SkillEffect_SE_S_S_Abnormality_TechnologicalLv2_MusicalAddiction = "SE_S_S_Abnormality_TechnologicalLv2_MusicalAddiction";
        public static string SkillEffect_SE_S_S_Distortion_Selection_0 = "SE_S_S_Distortion_Selection_0";
        public static string SkillEffect_SE_S_S_Distortion_Selection_1 = "SE_S_S_Distortion_Selection_1";
        public static string SkillEffect_SE_S_S_Distortion_Selection_2 = "SE_S_S_Distortion_Selection_2";
        public static string SkillEffect_SE_S_S_Distortion_Selection_3 = "SE_S_S_Distortion_Selection_3";
        public static string SkillEffect_SE_S_S_Distortion_Selection_4 = "SE_S_S_Distortion_Selection_4";
        public static string SkillEffect_SE_S_S_EGO_Technological_MagicBullet = "SE_S_S_EGO_Technological_MagicBullet";
        public static string SkillEffect_SE_T_S_Abnormality_Guest_Storytime = "SE_T_S_Abnormality_Guest_Storytime";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_Ashes = "SE_T_S_Abnormality_HistoryLv1_Ashes";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_DisplayAffection = "SE_T_S_Abnormality_HistoryLv1_DisplayAffection";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_FairiesCare = "SE_T_S_Abnormality_HistoryLv1_FairiesCare";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_HappyMemories = "SE_T_S_Abnormality_HistoryLv1_HappyMemories";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_Matchlight = "SE_T_S_Abnormality_HistoryLv1_Matchlight";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_NostalgicEmbrace = "SE_T_S_Abnormality_HistoryLv1_NostalgicEmbrace";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Footfalls = "SE_T_S_Abnormality_HistoryLv2_Footfalls";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Gluttony = "SE_T_S_Abnormality_HistoryLv2_Gluttony";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Predation = "SE_T_S_Abnormality_HistoryLv2_Predation";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Spores = "SE_T_S_Abnormality_HistoryLv2_Spores";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Vines = "SE_T_S_Abnormality_HistoryLv2_Vines";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_WorkerBee = "SE_T_S_Abnormality_HistoryLv2_WorkerBee";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_BarrierThorns = "SE_T_S_Abnormality_HistoryLv3_BarrierThorns";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_Loyalty = "SE_T_S_Abnormality_HistoryLv3_Loyalty";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_Malice = "SE_T_S_Abnormality_HistoryLv3_Malice";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv1_Axe = "SE_T_S_Abnormality_Literature_Lv1_Axe";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv1_Cocoon = "SE_T_S_Abnormality_Literature_Lv1_Cocoon";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv1_Glitter = "SE_T_S_Abnormality_Literature_Lv1_Glitter";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv1_LookDay = "SE_T_S_Abnormality_Literature_Lv1_LookDay";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv1_SocialDistancing = "SE_T_S_Abnormality_Literature_Lv1_SocialDistancing";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv1_SurpriseGift = "SE_T_S_Abnormality_Literature_Lv1_SurpriseGift";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv2_Alertness = "SE_T_S_Abnormality_Literature_Lv2_Alertness";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv2_Friend = "SE_T_S_Abnormality_Literature_Lv2_Friend";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv2_FunnyPrank = "SE_T_S_Abnormality_Literature_Lv2_FunnyPrank";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv2_Meal = "SE_T_S_Abnormality_Literature_Lv2_Meal";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv2_Obsession = "SE_T_S_Abnormality_Literature_Lv2_Obsession";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv2_Shyness = "SE_T_S_Abnormality_Literature_Lv2_Shyness";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv3_GooeyWaste = "SE_T_S_Abnormality_Literature_Lv3_GooeyWaste";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv3_LovingFamily = "SE_T_S_Abnormality_Literature_Lv3_LovingFamily";
        public static string SkillEffect_SE_T_S_Abnormality_Literature_Lv3_WornParasol = "SE_T_S_Abnormality_Literature_Lv3_WornParasol";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Lament = "SE_T_S_Abnormality_TechnologicalLv1_Lament";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_MetallicRinging = "SE_T_S_Abnormality_TechnologicalLv1_MetallicRinging";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_RepetitivePattern = "SE_T_S_Abnormality_TechnologicalLv1_RepetitivePattern";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Request = "SE_T_S_Abnormality_TechnologicalLv1_Request";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Rhythm = "SE_T_S_Abnormality_TechnologicalLv1_Rhythm";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Violence = "SE_T_S_Abnormality_TechnologicalLv1_Violence";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_ChainedWrath = "SE_T_S_Abnormality_TechnologicalLv2_ChainedWrath";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_Clean = "SE_T_S_Abnormality_TechnologicalLv2_Clean";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_EternalRest = "SE_T_S_Abnormality_TechnologicalLv2_EternalRest";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_MusicalAddiction = "SE_T_S_Abnormality_TechnologicalLv2_MusicalAddiction";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_Recharge = "SE_T_S_Abnormality_TechnologicalLv2_Recharge";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_SeventhBullet = "SE_T_S_Abnormality_TechnologicalLv2_SeventhBullet";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv3_Coffin = "SE_T_S_Abnormality_TechnologicalLv3_Coffin";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv3_DarkFlame = "SE_T_S_Abnormality_TechnologicalLv3_DarkFlame";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv3_Music = "SE_T_S_Abnormality_TechnologicalLv3_Music";
        public static string SkillEffect_SE_T_S_Distortion_Selection_0 = "SE_T_S_Distortion_Selection_0";
        public static string SkillEffect_SE_T_S_Distortion_Selection_1 = "SE_T_S_Distortion_Selection_1";
        public static string SkillEffect_SE_T_S_Distortion_Selection_2 = "SE_T_S_Distortion_Selection_2";
        public static string SkillEffect_SE_T_S_Distortion_Selection_3 = "SE_T_S_Distortion_Selection_3";
        public static string SkillEffect_SE_T_S_Distortion_Selection_4 = "SE_T_S_Distortion_Selection_4";
        public static string SkillEffect_SE_T_S_Distortion_Selection_5 = "SE_T_S_Distortion_Selection_5";
        public static string SkillEffect_SE_T_S_EGO_History_FourthMatchFlame = "SE_T_S_EGO_History_FourthMatchFlame";
        public static string SkillEffect_SE_T_S_EGO_History_GreenStem = "SE_T_S_EGO_History_GreenStem";
        public static string SkillEffect_SE_T_S_EGO_History_Hornet = "SE_T_S_EGO_History_Hornet";
        public static string SkillEffect_SE_T_S_EGO_History_TheForgotten = "SE_T_S_EGO_History_TheForgotten";
        public static string SkillEffect_SE_T_S_EGO_History_Wingbeat = "SE_T_S_EGO_History_Wingbeat";
        public static string SkillEffect_SE_T_S_EGO_Literature_BlackSwan = "SE_T_S_EGO_Literature_BlackSwan";
        public static string SkillEffect_SE_T_S_EGO_Literature_Laetitia = "SE_T_S_EGO_Literature_Laetitia";
        public static string SkillEffect_SE_T_S_EGO_Literature_RedEyes = "SE_T_S_EGO_Literature_RedEyes";
        public static string SkillEffect_SE_T_S_EGO_Literature_SanguineDesire = "SE_T_S_EGO_Literature_SanguineDesire";
        public static string SkillEffect_SE_T_S_EGO_Literature_TodayExpression = "SE_T_S_EGO_Literature_TodayExpression";
        public static string SkillEffect_SE_T_S_EGO_Synchronize_MagicBullet_FloodingBullets = "SE_T_S_EGO_Synchronize_MagicBullet_FloodingBullets";
        public static string SkillEffect_SE_T_S_EGO_Synchronize_MagicBullet_InevitableBullet = "SE_T_S_EGO_Synchronize_MagicBullet_InevitableBullet";
        public static string SkillEffect_SE_T_S_EGO_Synchronize_MagicBullet_MagicBullet = "SE_T_S_EGO_Synchronize_MagicBullet_MagicBullet";
        public static string SkillEffect_SE_T_S_EGO_Synchronize_MagicBullet_SilentBullet = "SE_T_S_EGO_Synchronize_MagicBullet_SilentBullet";
        public static string SkillEffect_SE_T_S_EGO_Technological_GrinderMk = "SE_T_S_EGO_Technological_GrinderMk";
        public static string SkillEffect_SE_T_S_EGO_Technological_Harmony = "SE_T_S_EGO_Technological_Harmony";
        public static string SkillEffect_SE_T_S_EGO_Technological_Regret = "SE_T_S_EGO_Technological_Regret";
        public static string SkillEffect_SE_T_S_EGO_Technological_SolemnLament = "SE_T_S_EGO_Technological_SolemnLament";
        public static string SkillEffect_SE_T_S_EmotionSystem_Lucy_MusicBox = "SE_T_S_EmotionSystem_Lucy_MusicBox";
        public static string SkillEffect_SE_T_S_Potion_DarkTune = "SE_T_S_Potion_DarkTune";
        public static string SkillEffect_SE_T_S_Potion_DistilledSuffering = "SE_T_S_Potion_DistilledSuffering";
        public static string SkillEffect_SE_T_S_Potion_DistortionFragment = "SE_T_S_Potion_DistortionFragment";
        public static string SkillEffect_SE_T_S_Potion_EssenceTranquility = "SE_T_S_Potion_EssenceTranquility";
        public static string SkillEffect_SE_T_S_Potion_EssenceWrath = "SE_T_S_Potion_EssenceWrath";
        public static string SkillEffect_SE_T_S_Potion_IgnitedRemorse = "SE_T_S_Potion_IgnitedRemorse";
        public static string SkillEffect_SE_T_S_Potion_PureTune = "SE_T_S_Potion_PureTune";
        public static string SkillEffect_SE_T_S_Test = "SE_T_S_Test";
		/// <summary>
		/// <color=red>Despair</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv1_Despair = "S_Abnormality_GuestLv1_Despair";
		/// <summary>
		/// <color=green>Giant Mushroom</color>
		/// <color=#919191>If you can only perceive this abnormality as a giant mushroom, that's because you're seeing with clouded eyes.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv1_GiantMushroom = "S_Abnormality_GuestLv1_GiantMushroom";
		/// <summary>
		/// <color=green>Strengthen</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv1_Strengthen = "S_Abnormality_GuestLv1_Strengthen";
		/// <summary>
		/// <color=red>Stress</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv1_Stress = "S_Abnormality_GuestLv1_Stress";
		/// <summary>
		/// <color=green>Unity</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv1_Unity = "S_Abnormality_GuestLv1_Unity";
		/// <summary>
		/// <color=red>You Must be Happy</color>
		/// <color=#919191>Do you love your city?</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv1_YouMustbeHappy = "S_Abnormality_GuestLv1_YouMustbeHappy";
		/// <summary>
		/// <color=green>Behaviour Adjustment</color>
		/// <color=#919191>It readjusts everyone to become righteous, no matter how wicked, evil, and arrogant they may be.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv2_BehaviourAdjustment = "S_Abnormality_GuestLv2_BehaviourAdjustment";
		/// <summary>
		/// <color=red>Energy Conversion</color>
		/// <color=#919191>Just open up the machine, step inside, and press the button to make it shut. Now everything will be just fine.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv2_EnergyConversion = "S_Abnormality_GuestLv2_EnergyConversion";
		/// <summary>
		/// <color=red>Mirror Adjustment</color>
		/// <color=#919191>Those who face themselves in the mirror may appear the same, but in actuality, they have become completely different people.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv2_MirrorAdjustment = "S_Abnormality_GuestLv2_MirrorAdjustment";
		/// <summary>
		/// <color=green>Present</color>
		/// When drawn random ally takes non-lethal <color=purple>10 Pain Damage</color>.
		/// <color=#919191>With my infinite hatred, I give you this <color=red>gift</color>.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv2_Present = "S_Abnormality_GuestLv2_Present";
		/// <summary>
		/// <color=green>Shelter</color>
		/// <color=#919191>However, this shelter, while perfectly safe on the inside, alters the reality of the outside to be even more hopeless.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv2_Shelter = "S_Abnormality_GuestLv2_Shelter";
		/// <summary>
		/// <color=red>Storytime</color>
		/// <color=#919191>It's not a good idea to keep listening to her. She knows every story on Earth and even those that cannot possibly exist.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv2_Storytime = "S_Abnormality_GuestLv2_Storytime";
		/// <summary>
		/// <color=green>Bait</color>
		/// <color=#919191>The bud is glowing, it's amazing. It's so bright, other employees came over to look at the flower too. </color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv3_Bait = "S_Abnormality_GuestLv3_Bait";
		/// <summary>
		/// <color=red>Cycle of the Curse</color>
		/// <color=#919191>However, the curse continues eternally, never broken.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv3_CycleCurse = "S_Abnormality_GuestLv3_CycleCurse";
		/// <summary>
		/// <color=red>Dimensional Refraction</color>
		/// <color=#919191>The area is a vacuum. Strictly speaking, the Abnormality itself is the vacuum phenomenon.</color>
		/// </summary>
        public static string Skill_S_Abnormality_GuestLv3_DimensionalRefraction = "S_Abnormality_GuestLv3_DimensionalRefraction";
		/// <summary>
		/// Dimensional Refraction
		/// </summary>
        public static string Skill_S_Abnormality_Guest_DimensionalRefraction = "S_Abnormality_Guest_DimensionalRefraction";
        public static string Skill_S_Abnormality_Guest_Mirror = "S_Abnormality_Guest_Mirror";
		/// <summary>
		/// <color=red>Present</color>
		/// When drawn deal non-lethal <color=purple>10 Pain Damage</color> to a random ally and draw 1 skill.
		/// <color=#919191>With my infinite hatred, I give you this <color=red>gift</color>.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Guest_Present = "S_Abnormality_Guest_Present";
		/// <summary>
		/// <color=#5A6A8B>Solitude</color>
		/// </summary>
        public static string Skill_S_Abnormality_Guest_Storytime = "S_Abnormality_Guest_Storytime";
		/// <summary>
		/// <color=green>Ashes</color>
		/// <color=#919191>The charred body represents the child's crumbled hope, while the ever blazing flame represents the obsession for affection.
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_Ashes = "S_Abnormality_HistoryLv1_Ashes";
		/// <summary>
		/// <color=red>Display of Affection</color>
		/// <color=#919191>Its memories began with a warm hug.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_DisplayAffection = "S_Abnormality_HistoryLv1_DisplayAffection";
		/// <summary>
		/// <color=red>The Fairies' Care</color>
		/// <color=#919191>The fairies protect our employees. Everything will be peaceful while you are under the fairies' care.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_FairiesCare = "S_Abnormality_HistoryLv1_FairiesCare";
		/// <summary>
		/// <color=green>Happy Memories</color>
		/// <color=#919191>But, you see, Teddy never wanted to be separate from its owner ever again.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_HappyMemories = "S_Abnormality_HistoryLv1_HappyMemories";
		/// <summary>
		/// <color=red>Matchlight</color>
		/// <color=#919191>Well, she's like a ticking time bomb. No one can tell if she's in a good mood or not.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_Matchlight = "S_Abnormality_HistoryLv1_Matchlight";
		/// <summary>
		/// <color=green>Nostalgic Embrace of the Old Days</color>
		/// <color=#919191>Teddy was hugging someone tightly. Teddy loved hugs. But something was odd.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_NostalgicEmbrace = "S_Abnormality_HistoryLv1_NostalgicEmbrace";
		/// <summary>
		/// <color=red>Footfalls</color>
		/// <color=#919191>I am coming to you. You, who will be reduced to ash like me.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Footfalls = "S_Abnormality_HistoryLv2_Footfalls";
		/// <summary>
		/// <color=green>Gluttony</color>
		/// <color=#919191>The fairies were no more than carnivorous monsters, and their <b>protection</b> was their method to keep the meat fresh.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Gluttony = "S_Abnormality_HistoryLv2_Gluttony";
		/// <summary>
		/// <color=red>Predation</color>
		/// <color=#919191>His stomach and face were ripped off, and his eyeballs and organs were damaged as if they were eaten by something. Meanwhile, the fairies had someone's blood and flesh smeared all over their mouths.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Predation = "S_Abnormality_HistoryLv2_Predation";
		/// <summary>
		/// <color=green>Spores</color>
		/// <color=#919191>It has been confirmed that the spores carry drone eggs that hatch inside a living host.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Spores = "S_Abnormality_HistoryLv2_Spores";
		/// <summary>
		/// <color=green>Vines</color>
		/// <color=#919191>One day, a branch grew from it. The leaves and branches were already withered and dry, but it continued to grow.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Vines = "S_Abnormality_HistoryLv2_Vines";
		/// <summary>
		/// <color=red>Worker Bee</color>
		/// <color=#919191>They show only two forms of behavior: Delivering nutrients to the Queen, and proliferating.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_WorkerBee = "S_Abnormality_HistoryLv2_WorkerBee";
		/// <summary>
		/// <color=green>Barrier of Thorns</color>
		/// <color=#919191>The apple that dropped from Snow White's hand after a single bite could never be happy. The apple, full of loneliness and hatred towards the princess, waited for the day it would rot away and return to the earth.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv3_BarrierThorns = "S_Abnormality_HistoryLv3_BarrierThorns";
		/// <summary>
		/// <color=red>Loyalty</color>
		/// Kill the selected ally. Apply <color=red>Loyalty</color> to all allies.
		/// <color=#919191>The loyalty of bees is a naturally developed instinct. If we discover a way to draw forth that instinct, many things could change.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv3_Loyalty = "S_Abnormality_HistoryLv3_Loyalty";
		/// <summary>
		/// <color=green>Malice</color>
		/// <color=#919191>The inherent malice caused all life to crumble as soon as it bloomed.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv3_Malice = "S_Abnormality_HistoryLv3_Malice";
		/// <summary>
		/// <color=red>Axe</color>
		/// <color=#919191>At some point, <b>Redacted</b> was wearing the shoes, grinning from ear to ear. It was uh, well it was quite strange. That wasn't a smile. It was...</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv1_Axe = "S_Abnormality_Literature_Lv1_Axe";
		/// <summary>
		/// <color=green>Cocoon</color>
		/// <color=#919191>Unsurprisingly, not a single employee volunteered to retrieve the corpse of their cocooned colleague.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv1_Cocoon = "S_Abnormality_Literature_Lv1_Cocoon";
		/// <summary>
		/// <color=red>Glitter</color>
		/// <color=#919191>The shoes were colorless.
		/// They will soon turn red.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv1_Glitter = "S_Abnormality_Literature_Lv1_Glitter";
		/// <summary>
		/// <color=red>Look of the Day</color>
		/// <color=#919191>One sunny day, just like that day they sincerely dried the laundry, they dried their own skin.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv1_LookDay = "S_Abnormality_Literature_Lv1_LookDay";
		/// <summary>
		/// <color=green>Social Distancing</color>
		/// <color=#919191>If one tries to look at the face behind the skin, the result will not be pretty. The space behind their skin is the only personal space they have left. Leaving it uninvaded is the last bit of generosity the City can offer.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv1_SocialDistancing = "S_Abnormality_Literature_Lv1_SocialDistancing";
		/// <summary>
		/// <color=green>Surprise Gift</color>
		/// <color=#919191>And when the kid meets someone she likes, she will give them a gift made all by herself. The gift's content is a secret though!</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv1_SurpriseGift = "S_Abnormality_Literature_Lv1_SurpriseGift";
		/// <summary>
		/// <color=green>Alertness</color>
		/// <color=#919191>The eyes shone in the dark, searching for prey to feed to its spiderlings.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv2_Alertness = "S_Abnormality_Literature_Lv2_Alertness";
		/// <summary>
		/// <color=red>Friend</color>
		/// <color=#919191>She was so sad that she had to leave her dear friends behind, so she came up with a brilliant idea!</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv2_Friend = "S_Abnormality_Literature_Lv2_Friend";
		/// <summary>
		/// <color=red>Funny Prank</color>
		/// <color=#919191>So this little lady has made a decision! To stay here and never leave until laughter is restored to this place!</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv2_FunnyPrank = "S_Abnormality_Literature_Lv2_FunnyPrank";
		/// <summary>
		/// <color=green>Meal</color>
		/// <color=#919191>Peter was dragged to the ceiling in only a moment. The cocooned employee hanging off the ceiling of the Containment Unit will be a healthy meal for the spider's offspring.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv2_Meal = "S_Abnormality_Literature_Lv2_Meal";
		/// <summary>
		/// <color=red>Obsession</color>
		/// <color=#919191>I'll break your legs, then you won't be able to dance ever again.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv2_Obsession = "S_Abnormality_Literature_Lv2_Obsession";
		/// <summary>
		/// <color=green>Shyness</color>
		/// <color=#919191>The inability to show one's face is perhaps a form of shyness. When throbbing emotions surge up from time to time, it's best to simply cover the face.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv2_Shyness = "S_Abnormality_Literature_Lv2_Shyness";
		/// <summary>
		/// <color=green>Gooey Waste</color>
		/// <color=#919191>Elijah fell to her knees and finally started to puke up the thing everyone else was vomiting.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv3_GooeyWaste = "S_Abnormality_Literature_Lv3_GooeyWaste";
		/// <summary>
		/// <color=green>Loving Family</color>
		/// <color=#919191>She began to look for her brothers. Her family who needed to wear the nettle clothing to be free from the curse. Ones whom she shared happy dreams with.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv3_LovingFamily = "S_Abnormality_Literature_Lv3_LovingFamily";
		/// <summary>
		/// <color=red>Well-worn Parasol</color>
		/// <color=#919191>Believing that it would turn white, the black swan wanted to lift the curse by weaving together nettles. All that was left is a worn parasol it once treasured.</color>
		/// </summary>
        public static string Skill_S_Abnormality_Literature_Lv3_WornParasol = "S_Abnormality_Literature_Lv3_WornParasol";
		/// <summary>
		/// <color=red>Lament</color>
		/// <color=#919191>They say the mourner with a huge luggage on his back had come to be a savior to all.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Lament = "S_Abnormality_TechnologicalLv1_Lament";
		/// <summary>
		/// <color=green>Metallic Ringing</color>
		/// <color=#919191>My head... turning into metal... folds in my brain, being flattened...</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_MetallicRinging = "S_Abnormality_TechnologicalLv1_MetallicRinging";
		/// <summary>
		/// <color=green>Repetitive Pattern-Recognition</color>
		/// <color=#919191>The day I was sent to a new home for the first time, I gave them the gift they wanted so earnestly.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_RepetitivePattern = "S_Abnormality_TechnologicalLv1_RepetitivePattern";
		/// <summary>
		/// <color=green>Request</color>
		/// <color=#919191>Just as the Devil said, the bullets will puncture anything you please. Forever.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Request = "S_Abnormality_TechnologicalLv1_Request";
		/// <summary>
		/// <color=red>Rhythm</color>
		/// <color=#919191>It was creating a rhythm.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Rhythm = "S_Abnormality_TechnologicalLv1_Rhythm";
		/// <summary>
		/// <color=red>Violence</color>
		/// <color=#919191>What's really pitiful is people like you dying to the likes of me.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Violence = "S_Abnormality_TechnologicalLv1_Violence";
		/// <summary>
		/// <color=red>Chained Wrath</color>
		/// <color=#919191>He wears a straitjacket, but is as free as any man. No amount of chains and restraints is enough to prevent him from committing violence.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_ChainedWrath = "S_Abnormality_TechnologicalLv2_ChainedWrath";
		/// <summary>
		/// <color=green>Clean</color>
		/// <color=#919191>It recognizes a bad mood as a sign that the surroundings are dirty, and promptly enters cleaning mode.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_Clean = "S_Abnormality_TechnologicalLv2_Clean";
		/// <summary>
		/// <color=green>Eternal Rest</color>
		/// <color=#919191>People believed that they would become beautiful beings with small wings when they died. It's a silly story. Nonsensical too.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_EternalRest = "S_Abnormality_TechnologicalLv2_EternalRest";
		/// <summary>
		/// <color=red>Musical Addiction</color>
		/// <color=#919191>After all, art is a devil's gift, born from despair and suffering. Never stop performing until the body crumbles to dust.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_MusicalAddiction = "S_Abnormality_TechnologicalLv2_MusicalAddiction";
		/// <summary>
		/// <color=green>Recharge</color>
		/// <color=#919191>However, the limbs were equipped with sharp instruments instead of cleaning supplies.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_Recharge = "S_Abnormality_TechnologicalLv2_Recharge";
		/// <summary>
		/// <color=red>The Seventh Bullet</color>
		/// <color=#919191>The Devil proposed a childish contract: The last bullet would puncture the head of his beloved. The moment he heard that, he sought and shot all the people he loved.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_SeventhBullet = "S_Abnormality_TechnologicalLv2_SeventhBullet";
		/// <summary>
		/// <color=green>Coffin</color>
		/// <color=#919191>He's carrying a coffin. A large coffin to pay tribute to the employees who have nowhere else to go. It is still too small to comfort those innocent sacrifices.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv3_Coffin = "S_Abnormality_TechnologicalLv3_Coffin";
		/// <summary>
		/// <color=red>Dark Flame</color>
		/// Apply <color=red>Gebrochener Pakt</color> to all targets on a field.
		/// <color=#919191>One day, the marksman realized the Devil no longer followed him. He pondered why, then realized that his soul had already fallen to Hell from the beginning.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv3_DarkFlame = "S_Abnormality_TechnologicalLv3_DarkFlame";
		/// <summary>
		/// <color=red>Music</color>
		/// <color=#919191>But nothing could compare to the music it makes when it eats a human.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv3_Music = "S_Abnormality_TechnologicalLv3_Music";
		/// <summary>
		/// <color=red>Distortion: Marauding</color>
		/// </summary>
        public static string Skill_S_Distortion_Selection_0 = "S_Distortion_Selection_0";
		/// <summary>
		/// <color=red>Distortion: Colossal</color>
		/// </summary>
        public static string Skill_S_Distortion_Selection_1 = "S_Distortion_Selection_1";
		/// <summary>
		/// <color=red>Distortion: Robust</color>
		/// </summary>
        public static string Skill_S_Distortion_Selection_2 = "S_Distortion_Selection_2";
		/// <summary>
		/// <color=red>Distortion: Horrifying</color>
		/// </summary>
        public static string Skill_S_Distortion_Selection_3 = "S_Distortion_Selection_3";
		/// <summary>
		/// <color=red>Distortion: Executioner</color>
		/// </summary>
        public static string Skill_S_Distortion_Selection_4 = "S_Distortion_Selection_4";
		/// <summary>
		/// <color=red>Distortion: Unstoppable</color>
		/// </summary>
        public static string Skill_S_Distortion_Selection_5 = "S_Distortion_Selection_5";
		/// <summary>
		/// <color=#FFDF00>Fourth Match Flame</color>
		/// Inflict 10 <color=#FC6178>Burn</color>.
		/// If facing 1 enemy, inflict 10 additional <color=#FC6178>Burn</color>. 
		/// </summary>
        public static string Skill_S_EGO_History_FourthMatchFlame = "S_EGO_History_FourthMatchFlame";
		/// <summary>
		/// <color=#FFDF00>Green Stem</color>
		/// </summary>
        public static string Skill_S_EGO_History_GreenStem = "S_EGO_History_GreenStem";
		/// <summary>
		/// <color=#FFDF00>Hornet</color>
		/// </summary>
        public static string Skill_S_EGO_History_Hornet = "S_EGO_History_Hornet";
		/// <summary>
		/// <color=#FFDF00>The Forgotten</color>
		/// Destroy 3 target's Action Points.
		/// </summary>
        public static string Skill_S_EGO_History_TheForgotten = "S_EGO_History_TheForgotten";
		/// <summary>
		/// <color=#FFDF00>Wingbeat</color>
		/// Recast this skill 3 times.
		/// Heal the ally with the lowest health by 8.
		/// </summary>
        public static string Skill_S_EGO_History_Wingbeat = "S_EGO_History_Wingbeat";
		/// <summary>
		/// <color=#FFDF00>Black Swan</color>
		/// Inflict 5 <color=#F0FF64>Paralysis</color> (<sprite=0> &a%), 5 <color=red>Fragile</color> (<sprite=0> &a%), and 5 <color=red>Bleed</color> to the target's.
		/// All allies gain 1 stack of 'Nettle Clothing'.
		/// </summary>
        public static string Skill_S_EGO_Literature_BlackSwan = "S_EGO_Literature_BlackSwan";
		/// <summary>
		/// <color=#FFDF00>Laetitia</color>
		/// Apply <color=#FFDF00>Friend</color> (Damage/healing increased by 80%) status 3 times to a random pages in hand, prioritizing the owner pages.
		/// </summary>
        public static string Skill_S_EGO_Literature_Laetitia = "S_EGO_Literature_Laetitia";
		/// <summary>
		/// <color=#FFDF00>Red Eyes</color>
		/// </summary>
        public static string Skill_S_EGO_Literature_RedEyes = "S_EGO_Literature_RedEyes";
		/// <summary>
		/// <color=#FFDF00>Sanguine Desire</color>
		/// Inflict 10 <color=red>Bleed</color>.
		/// While this skill is counting, allies attacks inflict 1 <color=red>Bleed</color>.
		/// </summary>
        public static string Skill_S_EGO_Literature_SanguineDesire = "S_EGO_Literature_SanguineDesire";
		/// <summary>
		/// <color=#FFDF00>Today's Expression</color>
		/// </summary>
        public static string Skill_S_EGO_Literature_TodayExpression = "S_EGO_Literature_TodayExpression";
		/// <summary>
		/// <color=#FFDF00>Desynchronize</color>
		/// Desynchronizes with Der Freischütz.
		/// </summary>
        public static string Skill_S_EGO_Synchronize_MagicBullet_Desynchronize = "S_EGO_Synchronize_MagicBullet_Desynchronize";
		/// <summary>
		/// <color=#FFDF00>Flooding Bullets</color>
		/// Recast this skill 2 times.
		/// </summary>
        public static string Skill_S_EGO_Synchronize_MagicBullet_FloodingBullets = "S_EGO_Synchronize_MagicBullet_FloodingBullets";
		/// <summary>
		/// <color=#6291EC>Inevitable Bullet</color>
		/// This attack cannot be dodged.
		/// </summary>
        public static string Skill_S_EGO_Synchronize_MagicBullet_InevitableBullet = "S_EGO_Synchronize_MagicBullet_InevitableBullet";
		/// <summary>
		/// <color=#FFDF00>Magic Bullet</color>
		/// Restore 1 Mana.
		/// </summary>
        public static string Skill_S_EGO_Synchronize_MagicBullet_MagicBullet = "S_EGO_Synchronize_MagicBullet_MagicBullet";
		/// <summary>
		/// <color=#6291EC>Silent Bullet</color>
		/// </summary>
        public static string Skill_S_EGO_Synchronize_MagicBullet_SilentBullet = "S_EGO_Synchronize_MagicBullet_SilentBullet";
		/// <summary>
		/// <color=#FFDF00>Grinder Mk. 5-2</color>
		/// Inflict 10 <color=red>Bleed</color>.
		/// </summary>
        public static string Skill_S_EGO_Technological_GrinderMk = "S_EGO_Technological_GrinderMk";
		/// <summary>
		/// <color=#FFDF00>Harmony</color>
		/// </summary>
        public static string Skill_S_EGO_Technological_Harmony = "S_EGO_Technological_Harmony";
		/// <summary>
		/// <color=#FFDF00>Magic Bullet</color>
		/// Synchronize with Der Freischütz for the next 3 Scenes and draw 2 skills.
		/// </summary>
        public static string Skill_S_EGO_Technological_MagicBullet = "S_EGO_Technological_MagicBullet";
		/// <summary>
		/// <color=#FFDF00>Regret</color>
		/// Recast this skill 3 times.
		/// </summary>
        public static string Skill_S_EGO_Technological_Regret = "S_EGO_Technological_Regret";
		/// <summary>
		/// <color=#FFDF00>Solemn Lament</color>
		/// Recast this skill 8 times.
		/// </summary>
        public static string Skill_S_EGO_Technological_SolemnLament = "S_EGO_Technological_SolemnLament";
        public static string Skill_S_EmotionSystem_DummyHeal = "S_EmotionSystem_DummyHeal";
		/// <summary>
		/// <color=#00FFFF>Candy</color>
		/// Draw 2 skills.
		/// The ally with the lowest Emotion Level gains +1 Emotion Level (ignores turn cap).
		/// <color=#C067EF>Emotion Burst</color>: View remaining Level I Abnormalities and select 1 to apply to an ally.
		/// <color=#919191>Tell the kid today's treat is going to be grape-flavored candy. It's his favorite.</color>
		/// </summary>
        public static string Skill_S_EmotionSystem_Lucy_Candy = "S_EmotionSystem_Lucy_Candy";
		/// <summary>
		/// <color=#00FFFF>Hippity-Hop</color>
		/// Draw 2 skills.
		/// All allies gain 3 <color=green>Positive</color> Emotion Points.
		/// <color=#C067EF>Emotion Burst</color>: Draw 2 additional skills.
		/// <color=#919191>Though the only thing the child could do was run around inside a cramped lab instead of a grass field under the sun, his steps were shaky but filled with enthusiasm.</color>
		/// </summary>
        public static string Skill_S_EmotionSystem_Lucy_HippityHop = "S_EmotionSystem_Lucy_HippityHop";
		/// <summary>
		/// <color=#FFDF00>Music Box</color>
		/// Draw 1 skill.
		/// <color=#919191>Do you remember this melody? The professor used to play this song when the students were sleepy. Happy birthday.</color>
		/// </summary>
        public static string Skill_S_EmotionSystem_Lucy_MusicBox = "S_EmotionSystem_Lucy_MusicBox";
		/// <summary>
		/// <color=#FF00FF>R</color><color=#FF007F>a</color><color=#FF7F00>i</color><color=#FFFF00>n</color><color=#00FF00>b</color><color=#00FFFF>o</color><color=#007FFF>w</color> <color=#FF00AA>S</color><color=#FF5500>e</color><color=#AAFF00>a</color>
		/// Draw 3 skills and restore 2 Mana.
		/// All enemies gain 3 <color=red>Negative</color> Emotion Points.
		/// <color=#C067EF>Emotion Burst</color>: Restore 2 additional Mana.
		/// <color=#919191>Instead of visiting the ocean, the child was given candy which let him see the ocean. Many colorful nights and days passed.</color>
		/// </summary>
        public static string Skill_S_EmotionSystem_Lucy_RainbowSea = "S_EmotionSystem_Lucy_RainbowSea";
        public static string Skill_S_Guest_CursePain = "S_Guest_CursePain";
        public static string Skill_S_Guest_CurseWeak = "S_Guest_CurseWeak";
		/// <summary>
		/// Dark Tune
		/// </summary>
        public static string Skill_S_Potion_DarkTune = "S_Potion_DarkTune";
		/// <summary>
		/// Distilled Suffering
		/// </summary>
        public static string Skill_S_Potion_DistilledSuffering = "S_Potion_DistilledSuffering";
		/// <summary>
		/// Distortion Fragment
		/// </summary>
        public static string Skill_S_Potion_DistortionFragment = "S_Potion_DistortionFragment";
		/// <summary>
		/// Essence of Tranquility
		/// </summary>
        public static string Skill_S_Potion_EssenceTranquility = "S_Potion_EssenceTranquility";
		/// <summary>
		/// Essence of Wrath
		/// </summary>
        public static string Skill_S_Potion_EssenceWrath = "S_Potion_EssenceWrath";
		/// <summary>
		/// Ignited Remorse
		/// </summary>
        public static string Skill_S_Potion_IgnitedRemorse = "S_Potion_IgnitedRemorse";
		/// <summary>
		/// Pure Tune
		/// </summary>
        public static string Skill_S_Potion_PureTune = "S_Potion_PureTune";
		/// <summary>
		/// Test
		/// </summary>
        public static string Skill_S_Test = "S_Test";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// 현재 전투에서 제외할 스킬을 선택하십시오. 스킬 소유자는 치명적이지 않은 <color=purple>&a 고통 피해</color>를 받습니다. 스킬 소유자가 루시일 경우 대신 마나 2를 잃습니다.
		/// English:
		/// Select skill to exclude from current fight. Skill owner takes non-lethal <color=purple>&a Pain Damage</color>. If skill owner is Lucy lose 2 Mana instead.
		/// Japanese:
		/// 現在の戦闘から除外するスキルを選択してください。スキルの所有者は致命的ではない <color=purple>&a 痛みダメージ</color>を受けます。スキル所有者がルーシーの場合、代わりにマナを2失います。
		/// Chinese:
		/// 选择要从当前战斗中排除的技能。技能持有者受到非致命的 <color=purple>&a 疼痛伤害</color>。如果技能持有者是露西，则改为失去2点法力。
		/// Chinese-TW:
		/// 選擇要從當前戰鬥中排除的技能。技能持有者受到非致命的 <color=purple>&a 疼痛傷害</color>。如果技能持有者是露西，則改為失去2點法力。
		/// </summary>
        public static string Abnormality_Guest_CycleCurse => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("Abnormality_Guest_CycleCurse");
		/// <summary>
		/// Korean:
		/// 스킬을 <color=#FFDF00>E.G.O.</color> 손패로 변경합니다.
		/// English:
		/// Change skills to <color=#FFDF00>E.G.O.</color> Hand.
		/// Japanese:
		/// スキルを<color=#FFDF00>E.G.O.</color>ハンドに変更します。
		/// Chinese:
		/// 将技能变为<color=#FFDF00>E.G.O.</color>手牌。
		/// Chinese-TW:
		/// 將技能變為<color=#FFDF00>E.G.O.</color>手牌。
		/// </summary>
        public static string EGO_Button_ChangeToEGOHand => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EGO_Button_ChangeToEGOHand");
		/// <summary>
		/// Korean:
		/// <color=#FFDF00>E.G.O.</color> 손패를 스킬로 변경합니다.
		/// English:
		/// Change <color=#FFDF00>E.G.O.</color> Hand to skills.
		/// Japanese:
		/// <color=#FFDF00>E.G.O.</color>ハンドをスキルに変更します。
		/// Chinese:
		/// 将<color=#FFDF00>E.G.O.</color>手牌变回技能。
		/// Chinese-TW:
		/// 將<color=#FFDF00>E.G.O.</color>手牌變回技能。
		/// </summary>
        public static string EGO_Button_ChangeToHand => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EGO_Button_ChangeToHand");
		/// <summary>
		/// Korean:
		/// <color=#FFDF00>E.G.O.</color> 스킬은 사용할 수 없습니다.
		/// English:
		/// <color=#FFDF00>E.G.O.</color> skills are not available.
		/// Japanese:
		/// <color=#FFDF00>E.G.O.</color>スキルは使用できません。
		/// Chinese:
		/// <color=#FFDF00>E.G.O.</color>技能不可用。
		/// Chinese-TW:
		/// <color=#FFDF00>E.G.O.</color>技能無法使用。
		/// </summary>
        public static string EGO_Button_Empty => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EGO_Button_Empty");
		/// <summary>
		/// Korean:
		/// &a턴 후 다시 사용할 수 있습니다.
		/// English:
		/// Can be used again after &a turn(s).
		/// Japanese:
		/// &aターン後に再使用可能。
		/// Chinese:
		/// &a回合后可再次使用。
		/// Chinese-TW:
		/// &a回合後可再次使用。
		/// </summary>
        public static string EGO_Skill_Cooldown => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EGO_Skill_Cooldown");
		/// <summary>
		/// Korean:
		/// 전투당 한 번만 사용할 수 있습니다.
		/// English:
		/// Can be used once per battle.
		/// Japanese:
		/// 戦闘ごとに1回のみ使用可能。
		/// Chinese:
		/// 每场战斗只能使用一次。
		/// Chinese-TW:
		/// 每場戰鬥只能使用一次。
		/// </summary>
        public static string EGO_Skill_Once => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EGO_Skill_Once");
		/// <summary>
		/// Korean:
		/// 감정 시스템
		/// English:
		/// Emotion System
		/// Japanese:
		/// 感情システム
		/// Chinese:
		/// 情绪系统
		/// Chinese-TW:
		/// 情緒系統
		/// </summary>
        public static string EmotionSystem => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem");
		/// <summary>
		/// Korean:
		/// Library of Ruina의 감정 시스템을 도입합니다.
		/// English:
		/// Introduce the Emotion System from Library of Ruina.
		/// Japanese:
		/// Library of Ruinaの感情システムを導入します。
		/// Chinese:
		/// 引入来自《Library of Ruina》的情绪系统。
		/// Chinese-TW:
		/// 引入來自《Library of Ruina》的情緒系統。
		/// </summary>
        public static string EmotionSystemEmotionSystemTutorial_0 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem/EmotionSystemTutorial_0");
		/// <summary>
		/// Korean:
		/// 전투 중 두 가지 종류의 감정 포인트, <color=green>긍정적</color>과 <color-red>부정적</color>을 얻을 수 있습니다.
		/// English:
		/// During battles, you can obtain two types of Emotion Points: <color=green>Positive</color> and <color-red>Negative</color>.
		/// Japanese:
		/// 戦闘中、<color=green>ポジティブ</color>と<color-red>ネガティブ</color>の2種類の感情ポイントを獲得できます。
		/// Chinese:
		/// 在战斗中，你可以获得两种情绪点：<color=green>积极</color>和<color-red>消极</color>。
		/// Chinese-TW:
		/// 在戰鬥中，你可以獲得兩種情緒點：<color=green>正面</color>與<color-red>負面</color>。
		/// </summary>
        public static string EmotionSystemEmotionSystemTutorial_1 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem/EmotionSystemTutorial_1");
		/// <summary>
		/// Korean:
		/// 감정 포인트를 모아 감정 레벨을 올리면 이점을 얻을 수 있습니다.
		/// English:
		/// By collecting Emotion Points and raising your Emotion Level, you will gain benefits.
		/// Japanese:
		/// 感情ポイントを集め、感情レベルを上げることで恩恵を得ます。
		/// Chinese:
		/// 通过收集情绪点并提升情绪等级，你将获得增益。
		/// Chinese-TW:
		/// 透過收集情緒點並提升情緒等級，你將獲得增益。
		/// </summary>
        public static string EmotionSystemEmotionSystemTutorial_2 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem/EmotionSystemTutorial_2");
		/// <summary>
		/// Korean:
		/// <color=#FFDF00>E.G.O.</color> 스킬은 손 근처의 이 버튼을 통해 접근할 수 있습니다.
		/// English:
		/// <color=#FFDF00>E.G.O.</color> skill can be accessed through this button near your hand.
		/// Japanese:
		/// <color=#FFDF00>E.G.O.</color>スキルは手札付近のボタンからアクセスできます。
		/// Chinese:
		/// <color=#FFDF00>E.G.O.</color>技能可通过手牌旁的按钮访问。
		/// Chinese-TW:
		/// <color=#FFDF00>E.G.O.</color>技能可透過手牌旁的按鈕使用。
		/// </summary>
        public static string EmotionSystemEmotionSystemTutorial_3 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem/EmotionSystemTutorial_3");
		/// <summary>
		/// Korean:
		/// 평균 감정 레벨이 3 이상이 되면 <color=#FFDF00>E.G.O.</color> 스킬을 얻을 수 있습니다.
		/// English:
		/// You can obtain <color=#FFDF00>E.G.O.</color> skills when average Emotion Level reaches 3 and above.
		/// Japanese:
		/// 平均感情レベルが3以上になると、<color=#FFDF00>E.G.O.</color>スキルを獲得できます。
		/// Chinese:
		/// 当平均情绪等级达到3及以上时，可以获得<color=#FFDF00>E.G.O.</color>技能。
		/// Chinese-TW:
		/// 當平均情緒等級達到3及以上時，可以獲得<color=#FFDF00>E.G.O.</color>技能。
		/// </summary>
        public static string EmotionSystemEmotionSystemTutorial_4 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem/EmotionSystemTutorial_4");
		/// <summary>
		/// Korean:
		/// 현재 &a <color=red>출혈</color>.
		/// 행동 시마다 <color=purple>&b 고통 피해</color> <color=#FF7C34>(출혈 * 3)</color>를 받고 <color=red>출혈</color> 중첩의 1/3을 감소시킵니다. (올림)
		/// English:
		/// Current &a <color=red>Bleed</color>.
		/// Take <color=purple>&b Pain damage</color> <color=#FF7C34>(Bleed * 3)</color> and subtract 1/3rd of the <color=red>Bleed</color> stacks every time the character perform an action. (Rounds up)
		/// Japanese:
		/// 現在 &a <color=red>出血</color>。
		/// 行動ごとに <color=purple>&b 痛みダメージ</color> <color=#FF7C34>(出血 * 3)</color> を受け、<color=red>出血</color>スタックの1/3を減らします。（切り上げ）
		/// Chinese:
		/// 当前 &a <color=red>流血</color>。
		/// 每次行动时，受到 <color=purple>&b 疼痛伤害</color> <color=#FF7C34>(流血 * 3)</color>，并减少 <color=red>流血</color> 层数的1/3（向上取整）。
		/// Chinese-TW:
		/// 當前 &a <color=red>流血</color>。
		/// 每次行動時，受到 <color=purple>&b 疼痛傷害</color> <color=#FF7C34>(流血 * 3)</color>，並減少 <color=red>流血</color> 層數的1/3（向上取整）。
		/// </summary>
        public static string EmotionSystem_Bleed_0 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Bleed_0");
		/// <summary>
		/// Korean:
		/// 행동 시마다 <color=purple>&b 고통 피해</color> <color=#FF7C34>(출혈 * 3)</color>를 받고 <color=red>출혈</color> 중첩의 1/3을 감소시킵니다. (올림)
		/// English:
		/// Take <color=purple>&b Pain damage</color> <color=#FF7C34>(Bleed * 3)</color> and subtract 1/3rd of the <color=red>Bleed</color> stacks every time the character perform an action. (Rounds up)
		/// Japanese:
		/// 行動ごとに <color=purple>&b 痛みダメージ</color> <color=#FF7C34>(出血 * 3)</color> を受け、<color=red>出血</color>スタックの1/3を減らします。（切り上げ）
		/// Chinese:
		/// 每次行动时，受到 <color=purple>&b 疼痛伤害</color> <color=#FF7C34>(流血 * 3)</color>，并减少 <color=red>流血</color> 层数的1/3（向上取整）。
		/// Chinese-TW:
		/// 每次行動時，受到 <color=purple>&b 疼痛傷害</color> <color=#FF7C34>(流血 * 3)</color>，並減少 <color=red>流血</color> 層數的1/3（向上取整）。
		/// </summary>
        public static string EmotionSystem_Bleed_1 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Bleed_1");
		/// <summary>
		/// Korean:
		/// 보스 초대장
		/// English:
		/// Boss Invitations
		/// Japanese:
		/// ボス招待
		/// Chinese:
		/// Boss 邀请
		/// Chinese-TW:
		/// Boss 邀請
		/// </summary>
        public static string EmotionSystem_Boss_Invitations => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Boss_Invitations");
		/// <summary>
		/// Korean:
		/// 현재 &a <color=#FC6178>화상</color>.
		/// 장면 종료 시 <color=purple>&b 고통 피해</color> <color=#FF7C34>(화상 * 2)</color>를 받고 <color=#FC6178>화상</color> 중첩의 1/3을 감소시킵니다. (내림)
		/// English:
		/// Current &a <color=#FC6178>Burn</color>.
		/// At the end of the Scene, take <color=purple>&b Pain damage</color> <color=#FF7C34>(Burn * 2)</color> and subtract 1/3rd of the <color=#FC6178>Burn</color> stack. (Rounds down)
		/// Japanese:
		/// 現在 &a <color=#FC6178>火傷</color>。
		/// シーン終了時に <color=purple>&b 痛みダメージ</color> <color=#FF7C34>(火傷 * 2)</color> を受け、<color=#FC6178>火傷</color>スタックの1/3を減らします。（切り捨て）
		/// Chinese:
		/// 当前 &a <color=#FC6178>灼烧</color>。
		/// 在场景结束时，受到 <color=purple>&b 疼痛伤害</color> <color=#FF7C34>(灼烧 * 2)</color>，并减少 <color=#FC6178>灼烧</color> 层数的1/3（向下取整）。
		/// Chinese-TW:
		/// 當前 &a <color=#FC6178>灼燒</color>。
		/// 在場景結束時，受到 <color=purple>&b 疼痛傷害</color> <color=#FF7C34>(灼燒 * 2)</color>，並減少 <color=#FC6178>灼燒</color> 層數的1/3（向下取整）。
		/// </summary>
        public static string EmotionSystem_Burn_0 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Burn_0");
		/// <summary>
		/// Korean:
		/// 장면 종료 시 <color=purple>&b 고통 피해</color> <color=#FF7C34>(화상 * 2)</color>를 받고 <color=#FC6178>화상</color> 중첩의 1/3을 감소시킵니다. (내림)
		/// English:
		/// At the end of the Scene, take <color=purple>&b Pain damage</color> <color=#FF7C34>(Burn * 2)</color> and subtract 1/3rd of the <color=#FC6178>Burn</color> stack. (Rounds down)
		/// Japanese:
		/// シーン終了時に <color=purple>&b 痛みダメージ</color> <color=#FF7C34>(火傷 * 2)</color> を受け、<color=#FC6178>火傷</color>スタックの1/3を減らします。（切り捨て）
		/// Chinese:
		/// 在场景结束时，受到 <color=purple>&b 疼痛伤害</color> <color=#FF7C34>(灼烧 * 2)</color>，并减少 <color=#FC6178>灼烧</color> 层数的1/3（向下取整）。
		/// Chinese-TW:
		/// 在場景結束時，受到 <color=purple>&b 疼痛傷害</color> <color=#FF7C34>(灼燒 * 2)</color>，並減少 <color=#FC6178>灼燒</color> 層數的1/3（向下取整）。
		/// </summary>
        public static string EmotionSystem_Burn_1 => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Burn_1");
		/// <summary>
		/// Korean:
		/// <color=red>왜곡된</color> 보스
		/// English:
		/// <color=red>Distorted</color> Bosses
		/// Japanese:
		/// <color=red>歪んだ</color>ボス
		/// Chinese:
		/// <color=red>扭曲的</color>Boss
		/// Chinese-TW:
		/// <color=red>扭曲的</color>Boss
		/// </summary>
        public static string EmotionSystem_Distorted_Bosses => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Distorted_Bosses");
		/// <summary>
		/// Korean:
		/// 보스에게 적용할 <color=red>왜곡</color>을 선택하십시오.
		/// English:
		/// Select <color=red>distortion</color> to apply to the Boss.
		/// Japanese:
		/// ボスに適用する<color=red>歪曲</color>を選択してください。
		/// Chinese:
		/// 选择要应用于Boss的<color=red>扭曲</color>。
		/// Chinese-TW:
		/// 選擇要套用於Boss的<color=red>扭曲</color>。
		/// </summary>
        public static string EmotionSystem_Distortion_Selection => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Distortion_Selection");
		/// <summary>
		/// Korean:
		/// 게스트 감정
		/// English:
		/// Guest Emotions
		/// Japanese:
		/// ゲストの感情
		/// Chinese:
		/// 客人情绪
		/// Chinese-TW:
		/// 訪客情緒
		/// </summary>
        public static string EmotionSystem_EmotionsGuest => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_EmotionsGuest");
		/// <summary>
		/// Korean:
		/// 조사관 감정
		/// English:
		/// Investigator Emotions
		/// Japanese:
		/// 調査員の感情
		/// Chinese:
		/// 调查员情绪
		/// Chinese-TW:
		/// 調查員情緒
		/// </summary>
        public static string EmotionSystem_EmotionsInvestigator => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_EmotionsInvestigator");
		/// <summary>
		/// Korean:
		/// 역사의 층
		/// English:
		/// Floor of History
		/// Japanese:
		/// 歴史の階層
		/// Chinese:
		/// 历史层
		/// Chinese-TW:
		/// 歷史層
		/// </summary>
        public static string EmotionSystem_Floor_History => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Floor_History");
		/// <summary>
		/// Korean:
		/// 문학의 층
		/// English:
		/// Floor of Literature
		/// Japanese:
		/// 文学の階層
		/// Chinese:
		/// 文学层
		/// Chinese-TW:
		/// 文學層
		/// </summary>
        public static string EmotionSystem_Floor_Literature => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Floor_Literature");
		/// <summary>
		/// Korean:
		/// 기술 과학의 층
		/// English:
		/// Floor of Technological Sciences
		/// Japanese:
		/// 技術科学の階層
		/// Chinese:
		/// 技术科学层
		/// Chinese-TW:
		/// 技術科學層
		/// </summary>
        public static string EmotionSystem_Floor_Technological => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("EmotionSystem_Floor_Technological");
		/// <summary>
		/// Korean:
		/// 이상현상 페이지를 받을 조사관을 선택하십시오.
		/// English:
		/// Select investigator to receive Abnormality Page.
		/// Japanese:
		/// アブノーマリティページを受け取る調査員を選択してください。
		/// Chinese:
		/// 选择要获得异常页面的调查员。
		/// Chinese-TW:
		/// 選擇要獲得異常頁面的調查員。
		/// </summary>
        public static string SelectOwner_Abnormality => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("SelectOwner_Abnormality");
		/// <summary>
		/// Korean:
		/// <color=#FFDF00>E.G.O.</color>를 받을 조사관을 선택하십시오.
		/// English:
		/// Select investigator to receive <color=#FFDF00>E.G.O.</color>
		/// Japanese:
		/// <color=#FFDF00>E.G.O.</color>を受け取る調査員を選択してください。
		/// Chinese:
		/// 选择要获得<color=#FFDF00>E.G.O.</color>的调查员。
		/// Chinese-TW:
		/// 選擇要獲得<color=#FFDF00>E.G.O.</color>的調查員。
		/// </summary>
        public static string SelectOwner_EGO => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("SelectOwner_EGO");
		/// <summary>
		/// Korean:
		/// 이상현상 페이지를 선택하십시오.
		/// English:
		/// Select Abnormality Page.
		/// Japanese:
		/// アブノーマリティページを選択してください。
		/// Chinese:
		/// 选择异常页面。
		/// Chinese-TW:
		/// 選擇異常頁面。
		/// </summary>
        public static string Select_Abnormality => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("Select_Abnormality");
		/// <summary>
		/// Korean:
		/// <color=#FFDF00>E.G.O.</color> 페이지를 선택하십시오.
		/// English:
		/// Select <color=#FFDF00>E.G.O.</color> Page.
		/// Japanese:
		/// <color=#FFDF00>E.G.O.</color>ページを選択してください。
		/// Chinese:
		/// 选择<color=#FFDF00>E.G.O.</color>页面。
		/// Chinese-TW:
		/// 選擇<color=#FFDF00>E.G.O.</color>頁面。
		/// </summary>
        public static string Select_EGO => ModManager.getModInfo("EmotionSystem").localizationInfo.SystemLocalizationUpdate("Select_EGO");

    }
}