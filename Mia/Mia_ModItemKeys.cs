using ChronoArkMod;
namespace Mia
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Burst of Flavor
		/// The next Sheathe effect occurs twice.
		/// </summary>
        public static string Buff_B_Mia_BurstofFlavor = "B_Mia_BurstofFlavor";
        public static string Buff_B_Mia_BurstofFlavor_0 = "B_Mia_BurstofFlavor_0";
		/// <summary>
		/// Additional Draw
		/// At the start of next turn, draw &a additional skill(s).
		/// </summary>
        public static string Buff_B_Mia_DrawNextTurn = "B_Mia_DrawNextTurn";
		/// <summary>
		/// Pawtience
		/// At 2 stacks, gain <color=#FF0070>Savage Impulse</color> (if buff owner is Mia),
		/// or <color=#FF4E00>Instinct Surge</color>.
		/// </summary>
        public static string Buff_B_Mia_E_Meowpiercer = "B_Mia_E_Meowpiercer";
		/// <summary>
		/// Flur
		/// </summary>
        public static string Buff_B_Mia_Flur = "B_Mia_Flur";
		/// <summary>
		/// Instinctive Precision
		/// </summary>
        public static string Buff_B_Mia_InstinctivePrecision = "B_Mia_InstinctivePrecision";
		/// <summary>
		/// <color=#FF4E00>Instinct Surge</color>
		/// Can be activated by left-clicking (cannot be activated if stunned).
		/// Choose one of the following options:
		/// - Discard the top skill in your hand and draw skills equal to the discarded skill’s cost (Max 2).
		/// - Discard the bottom skill in your hand and restore Mana equal to the discarded skill’s cost (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always restore Mana.
		/// If you have 0 skills in hand, consume 1 stack to draw 1 skill.</color>
		/// </summary>
        public static string Buff_B_Mia_InstinctSurge = "B_Mia_InstinctSurge";
		/// <summary>
		/// Pawcut 
		/// </summary>
        public static string Buff_B_Mia_Pawcut = "B_Mia_Pawcut";
		/// <summary>
		/// Predatory Drive
		/// </summary>
        public static string Buff_B_Mia_PredatoryDrive = "B_Mia_PredatoryDrive";
		/// <summary>
		/// <color=#FF0070>Savage Impulse</color>
		/// Can be activated by left-clicking or pressing Hotkey 'V' (cannot be activated if stunned).
		/// Choose one of the following options:
		/// - Discard the top skill in your hand and draw skills equal to the discarded skill's cost (Max 2).
		/// - Discard the bottom skill in your hand and restore Mana equal to the discarded skill's cost (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always restore Mana.
		/// If you have 0 skills in hand, consume 1 stack to draw 1 skill.</color>
		/// </summary>
        public static string Buff_B_Mia_SavageImpulse = "B_Mia_SavageImpulse";
		/// <summary>
		/// Savage Rhythm
		/// Current Discard: &a
		/// At 2 discards, if Mia is level 3 or higher, restore 1 Mana once per turn. If Mia is level 4 or higher, draw 1 skill once per turn.
		/// If Mia is level 6, the first Sheathe effect triggers twice once per turn.
		/// </summary>
        public static string Buff_B_Mia_SheatheTriggers = "B_Mia_SheatheTriggers";
		/// <summary>
		/// Sheathe : Cast this skill, gain <color=#FF4E00>Instinct Surge.</color>
		/// <sprite name="비용2"><sprite name="이하">
		/// <color=#919191>Some of Ilya's skills can't be enhanced.</color>
		/// </summary>
        public static string SkillExtended_Ex_Mia_HuntersInstinct = "Ex_Mia_HuntersInstinct";
		/// <summary>
		/// When played, gain <color=#FF4E00>Instinct Surge.</color>
		/// <sprite name="비용1"><sprite name="이상">
		/// <color=#919191>Some of Ilya's skills can't be enhanced.</color>
		/// </summary>
        public static string SkillExtended_Ex_Mia_InstinctSurge = "Ex_Mia_InstinctSurge";
		/// <summary>
		/// Sheathe : Draw this skill again.
		/// <sprite name="비용1"><sprite name="이상">
		/// <color=#919191>Some skills can't be enhanced.</color>
		/// </summary>
        public static string SkillExtended_Ex_Mia_PersistentHunt = "Ex_Mia_PersistentHunt";
		/// <summary>
		/// Meowpiercer
		/// All attacks inflict 'Pawcut'.
		/// Whenever a discard occurs, gain 'Pawtience' (Max 4 per turn).
		/// <color=#919191>Pawcut – Base chance: <sprite=1> 105%
		/// Pawtience - At 2 stacks, gain <color=#FF0070>Savage Impulse</color> (if buff owner is Mia),
		/// or <color=#FF4E00>Instinct Surge</color>. </color>
		/// </summary>
        public static string Item_Equip_E_Mia_Meowpiercer = "E_Mia_Meowpiercer";
		/// <summary>
		/// Instinct Surge
		/// <color=#737373>- Discard the top skill in your hand and draw skills (Max 2).
		/// - Discard the bottom skill in your hand and restore Mana (Max 2).</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_InstinctSurge = "KeyWord_InstinctSurge";
		/// <summary>
		/// Savage Impulse
		/// <color=#737373>- Discard the top skill in your hand and draw skills (Max 2).
		/// - Discard the bottom skill in your hand and restore Mana (Max 2).</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_SavageImpulse = "KeyWord_SavageImpulse";
		/// <summary>
		/// Mia
		/// Passive:
		/// Obtain 3 Instinct Tonic.
		/// When used, select one of the party members skills to apply a unique skill upgrade.
		/// At the start of each turn, gain <color=#FF0070>Savage Impulse</color> (up to 3 stacks).
		/// Level 2: Gain 3% Attack Power per discard (up to 15%).
		/// Level 3: Restore 1 Mana once per turn when 2 discards occur.
		/// Level 4: Draw 1 skill once per turn when 2 discards occur.
		/// Level 5: Gain 5% Critical Chance per discard (up to 25%).
		/// Level 6: The first Sheathe effect occurs twice once per turn.
		/// </summary>
        public static string Character_Mia = "Mia";
		/// <summary>
		/// Instinct Tonic
		/// This item can only be used once on each party member excluding Mia.
		/// When used, select one of the party member's skills to apply a unique skill upgrade:
		/// When played, gain <color=#FF4E00>Instinct Surge</color>.
		/// <color=#919191>Requirement : Skill with a mana cost of 1 or more.
		/// Some of Ilya's skills can't be enhanced.</color>
		/// </summary>
        public static string Item_Consume_Mia_InstinctTonic = "Mia_InstinctTonic";
        public static string SkillEffect_SE_S_S_Mia_BurstofFlavor = "SE_S_S_Mia_BurstofFlavor";
        public static string SkillEffect_SE_Tick_B_Mia_Pawcut = "SE_Tick_B_Mia_Pawcut";
        public static string SkillEffect_SE_T_S_Mia_BeastsPunchline = "SE_T_S_Mia_BeastsPunchline";
        public static string SkillEffect_SE_T_S_Mia_FeralPrank = "SE_T_S_Mia_FeralPrank";
        public static string SkillEffect_SE_T_S_Mia_FestivalFang = "SE_T_S_Mia_FestivalFang";
        public static string SkillEffect_SE_T_S_Mia_FluffyStrike = "SE_T_S_Mia_FluffyStrike";
        public static string SkillEffect_SE_T_S_Mia_ImpulsiveHarvest = "SE_T_S_Mia_ImpulsiveHarvest";
        public static string SkillEffect_SE_T_S_Mia_MeowsteryMomentum = "SE_T_S_Mia_MeowsteryMomentum";
        public static string SkillEffect_SE_T_S_Mia_PlayfulMasquerade = "SE_T_S_Mia_PlayfulMasquerade";
        public static string SkillEffect_SE_T_S_Mia_Rare_HarvestDance = "SE_T_S_Mia_Rare_HarvestDance";
        public static string SkillEffect_SE_T_S_Mia_RogueClaw = "SE_T_S_Mia_RogueClaw";
        public static string SkillEffect_SE_T_S_Mia_Scrollfang = "SE_T_S_Mia_Scrollfang";
        public static string SkillEffect_SE_T_S_Mia_VortexChores = "SE_T_S_Mia_VortexChores";
		/// <summary>
		/// Beast's Punchline
		/// When played from hand, discard the top skill in hand. If the discarded skill is Heal, cast this skill on a ally with lowest HP.
		/// Sheathe : Restore 1 Mana and draw 1 skill, prioritizing Healing skills.
		/// </summary>
        public static string Skill_S_Mia_BeastsPunchline = "S_Mia_BeastsPunchline";
		/// <summary>
		/// Burst of Flavor
		/// Create a random attack skill in hand.
		/// </summary>
        public static string Skill_S_Mia_BurstofFlavor = "S_Mia_BurstofFlavor";
		/// <summary>
		/// Camel Sprint
		/// Draw 1 skill.
		/// Gain 2 stacks of <color=#FF0070>Savage Impulse</color>,
		/// or 2 stacks of <color=#FF4E00>Instinct Surge</color> if cast by an ally.
		/// </summary>
        public static string Skill_S_Mia_CamelSprint = "S_Mia_CamelSprint";
		/// <summary>
		/// Festival Fang
		/// Deal &a additional damage.
		/// Sheathe : Permanently increase this skill's damage by &b for the rest of <b>this run</b>.
		/// </summary>
        public static string Skill_S_Mia_FestivalFang = "S_Mia_FestivalFang";
		/// <summary>
		/// Fluffy Strike
		/// When played from hand, discard the bottom skill in hand. If the discarded skill is Attack, cast this skill on a enemy with lowest HP.
		/// Sheathe : Restore 1 Mana and draw 1 skill, prioritizing Attack skills.
		/// </summary>
        public static string Skill_S_Mia_FluffyStrike = "S_Mia_FluffyStrike";
		/// <summary>
		/// Impulsive Harvest
		/// When played from hand, create random attack skill in hand.
		/// Sheathe : Shuffle all your skills from the discard pile back into your deck, then draw 1 skill.
		/// </summary>
        public static string Skill_S_Mia_ImpulsiveHarvest = "S_Mia_ImpulsiveHarvest";
		/// <summary>
		/// Messy Notes
		/// Discard the top skill in your hand and draw 3 skills.
		/// </summary>
        public static string Skill_S_Mia_LucyDraw_0 = "S_Mia_LucyDraw_0";
		/// <summary>
		/// Mia's Dreamland
		/// Draw 4 skills.
		/// Apply 'Discarded after 1 turn' to these skills.
		/// </summary>
        public static string Skill_S_Mia_LucyDraw_1 = "S_Mia_LucyDraw_1";
		/// <summary>
		/// Meowstery Momentum
		/// Cost reduced by 1 for each skill in hand. When played from hand, discard all skills in hand and increase damage by &a.
		/// For every 2 skills discarded, draw 1 additional skill next turn (Max 2).  
		/// If at least 6 skills are discarded, draw 1 skill.  
		/// If 7 skills are discarded, gain <color=#FF0070>Savage Impulse</color>, or <color=#FF4E00>Instinct Surge</color> if cast by an ally.
		/// Sheathe : Draw this skill again.
		/// </summary>
        public static string Skill_S_Mia_MeowsteryMomentum = "S_Mia_MeowsteryMomentum";
		/// <summary>
		/// Playful Masquerade
		/// Sheathe : Cast this skill.
		/// </summary>
        public static string Skill_S_Mia_PlayfulMasquerade = "S_Mia_PlayfulMasquerade";
		/// <summary>
		/// Chaotic Harvest
		/// Cast all skills in hand on a random targets and draw (discarded сount) skills.
		/// </summary>
        public static string Skill_S_Mia_Rare_ChaoticHarvest = "S_Mia_Rare_ChaoticHarvest";
		/// <summary>
		/// Harvest Dance
		/// Sheathe : Cast this skill on a random enemy, then draw this skill again and restore 1 Mana.
		/// </summary>
        public static string Skill_S_Mia_Rare_HarvestDance = "S_Mia_Rare_HarvestDance";
		/// <summary>
		/// Scrollfang: Mia's Cut
		/// When played from hand, discard the skill in your hand with the highest Mana cost and increase damage by &a * that skill's cost.
		/// Sheathe : Draw skills equal to this skill's cost (Max 2).
		/// </summary>
        public static string Skill_S_Mia_Scrollfang = "S_Mia_Scrollfang";
		/// <summary>
		/// Snowver Paw-er!
		/// Select one skill in your hand that is not upgraded (except Mia skills).
		/// Choose one of two random Sheathe upgrade effects and apply it for this battle.
		/// Draw 1 skill.
		/// </summary>
        public static string Skill_S_Mia_Snowver = "S_Mia_Snowver";
		/// <summary>
		/// Vortex Chores
		/// When played from hand, if you have another <color=#FF69B4>Vortex Chores</color> in hand, discard it, cast this skill on the target (Max 3).
		/// If 2 Vortex Chores are discarded, gain <color=#FF0070>Savage Impulse</color>, or <color=#FF4E00>Instinct Surge</color> if cast by an ally.
		/// Sheathe : Shuffle a random <color=#FF69B4>Vortex Chores</color> from the discard pile back into your deck and draw 1 skill.
		/// </summary>
        public static string Skill_S_Mia_VortexChores = "S_Mia_VortexChores";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Select skill to discard. 
		/// Left is bottom skill, restore Mana equal skill cost (Max 2).
		/// Right is top skill, draw equal skill cost (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always restore Mana.</color>
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string Discard => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("Discard");
		/// <summary>
		/// Korean:
		/// English:
		/// Amy, Miya, and Lord Squall are all amazing with instruments! Miya's in charge of dancing and cheering!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaBattleStart_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaBattleStart_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Meat is great, but Amy said you've gotta eat your veggies too.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaBattleStart_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaBattleStart_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Click to activate <color=#FF0070>Savage Impulse</color>.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaButton_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaButton_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Click or press [V] to activate <color=#FF0070>Savage Impulse</color>.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaButton_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaButton_1");
		/// <summary>
		/// Korean:
		/// English:
		/// <color=#FF0070>Savage Impulse</color> is not available.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaButton_2 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaButton_2");
		/// <summary>
		/// Korean:
		/// English:
		/// Let's go treasure hunting with Miya! Mount the camel, and off we go!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaChest => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaChest");
		/// <summary>
		/// Korean:
		/// English:
		/// It's good manners to enjoy any kind of food deliciously!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaCri => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaCri");
		/// <summary>
		/// Korean:
		/// English:
		/// Time to clean up! Let's do this, Keppal!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaCurse => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaCurse");
		/// <summary>
		/// Korean:
		/// English:
		/// Miya's not sleepy yet. Tonight, no one's getting to sleep until morning...
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaDD => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaDD");
		/// <summary>
		/// Korean:
		/// English:
		/// There are so many games I've never seen before! The outside world is so much fun!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaDDAlly => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaDDAlly");
		/// <summary>
		/// Korean:
		/// English:
		/// Eating delicious food is its own kind of exercise!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaHealed => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaHealed");
		/// <summary>
		/// Korean:
		/// English:
		/// Miya is a lancer, you know!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaIdleB_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaIdleB_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Miya is really strong! But using all that strength sure makes me hungry...
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaIdleB_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaIdleB_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Compared to where Mia was born, Axel city is full of wonders!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaIdleF => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaIdleF");
		/// <summary>
		/// Korean:
		/// English:
		/// I can think on my feet, defeat monsters, and this spear is just perfect for everything!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaKill => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaKill");
		/// <summary>
		/// Korean:
		/// English:
		/// The city has all kinds of delicious food.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaMaster => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaMaster");
		/// <summary>
		/// Korean:
		/// English:
		/// Grilled squid by the sea is the best! I want Amy to try some too!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaOther_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaOther_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Haah! Phew! There's so much delicious food! Christmas really is an amazing day!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaOther_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaOther_1");
		/// <summary>
		/// Korean:
		/// English:
		/// I decorated for Halloween with Amy! What do you think Cute, right
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaOther_2 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaOther_2");
		/// <summary>
		/// Korean:
		/// English:
		/// Sleepovers mean pillow fights! Try to catch Miya's speedy pillow!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaPharos_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPharos_0");
		/// <summary>
		/// Korean:
		/// English:
		/// When you're full, you get sleepy... Hope you have really sweet dreams!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaPharos_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPharos_1");
		/// <summary>
		/// Korean:
		/// English:
		/// Nikuman, Nikuman, I'm so happy! I'm going to eat it all myself!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaPharos_2 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPharos_2");
		/// <summary>
		/// Korean:
		/// English:
		/// That fruit looks delicious... but Miya prefers meat!
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string MiaPotion => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPotion");

    }
}