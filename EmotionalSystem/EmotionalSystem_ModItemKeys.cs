using ChronoArkMod;
namespace EmotionalSystem
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Ashes
		/// Attacks inflict 2 <color=#f8181c>Burn</color>, remove 1 stack on attack.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_Ashes = "B_Abnormality_HistoryLv1_Ashes";
		/// <summary>
		/// Display of Affection
		/// Gain Ignore Taunt on all skills.
		/// Deal 15% more damage if the target's Action Count is 1, 2, or 9+.
		/// Otherwise, deal 15% less damage.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_DisplayofAffection = "B_Abnormality_HistoryLv1_DisplayofAffection";
		/// <summary>
		/// Happy Memories
		/// The first skill played from hand costs 1 less.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_HappyMemories = "B_Abnormality_HistoryLv1_HappyMemories";
        public static string Buff_B_Abnormality_HistoryLv1_HappyMemories_0 = "B_Abnormality_HistoryLv1_HappyMemories_0";
		/// <summary>
		/// Matchlight
		/// Gain <color=#B22222>Matchlight</color> (Max 5) and inflict 1 <color=#f8181c>Burn</color> on attack.
		/// Increase the next skill's damage by &a% <color=#FF7C34>(20% x Matchlight)</color>.
		/// Gain a <b>&b%</b> <color=#FF7C34>(10% x Matchlight)</color> chance to take non-lethal <color=purple>&c Pain damage</color> <color=#FF7C34>(20% Max HP x  Matchlight)</color>.
		/// Current <color=#B22222>Matchlight</color> : &d.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_Matchlight = "B_Abnormality_HistoryLv1_Matchlight";
		/// <summary>
		/// Nostalgic Embrace of the Old Days
		/// Whenever attaking enemy, gain (Base <sprite=2>100%) to apply "Embrace That Never Ends" <color=#FF7C34>Taunt</color> to the target.
		/// Adds the user's CC accuracy to <color=#FF7C34>Taunt</color> chance.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays = "B_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays";
		/// <summary>
		/// Embrace That Never Ends
		/// Can only target &target.
		/// Removed when this character attacks &target.
		/// <color=#919191>A suffocating embrace, gentle yet relentless. Like the arms of a forgotten teddy bear, it holds you in a false comfort, preventing escape and draining your will to fight.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_0 = "B_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_0";
		/// <summary>
		/// The Fairies' Care
		/// <color=#919191>The fairies mend your wounds, but their touch dulls your senses. You feel warm... and terribly vulnerable.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv1_TheFairiesCare = "B_Abnormality_HistoryLv1_TheFairiesCare";
		/// <summary>
		/// Footfalls
		/// If this character HP is 50% or lower, deal 70% of the target's Max HP as <color=purple>Pain damage</color> (up to 70), and inflict 5 <color=#f8181c>Burn</color> to the target's.
		/// Afterwards, take <color=purple>Pain damage</color> equal to 33% of the damage dealt, and remove this buff.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Footfalls = "B_Abnormality_HistoryLv2_Footfalls";
		/// <summary>
		/// Gluttony
		/// Applies "Glutton's Mark" on attack.
		/// Against targets with "Glutton's Mark", all user attacks deal 15% more damage and heal the user for 15% of the damage dealt.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Gluttony = "B_Abnormality_HistoryLv2_Gluttony";
		/// <summary>
		/// Glutton Mark
		/// Removed at the start of the next turn.
		/// <color=#919191>The Glutton's Mark is a symbol of the fairies' cruel claim. Once an enemy bears this mark, they become a feast to the insatiable hunger of the fae, drawing them closer to their inevitable consumption.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Gluttony_0 = "B_Abnormality_HistoryLv2_Gluttony_0";
		/// <summary>
		/// Fairy's Favor
		/// Applies "Glutton's Mark" on attack.
		/// <color=#919191>The Fae, in their twisted whim, bestow this favor upon their allies, marking their enemies with the Mark of the Glutton, ensuring that their prey remains fresh for the coming feast.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Gluttony_1 = "B_Abnormality_HistoryLv2_Gluttony_1";
		/// <summary>
		/// Predation
		/// <color=#919191>Beneath the delicate flutter of wings lies a predator's gaze—hungry, relentless, unyielding.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Predation = "B_Abnormality_HistoryLv2_Predation";
		/// <summary>
		/// Spores
		/// Inflict 4 <color=#f8181c>Burn</color> to the attacker.
		/// <color=#919191>The spores take root, wrapping the host in a shroud of defense, pulling the gaze of all toward them as the swarm prepares to strike.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Spores = "B_Abnormality_HistoryLv2_Spores";
		/// <summary>
		/// Vines
		/// At the start of each turn, apply "Entangled" to all enemies.
		/// <color=#919191>The vines are long dead, yet they still reach. Not for light, but for something—anything—to pull into the dark.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Vines = "B_Abnormality_HistoryLv2_Vines";
		/// <summary>
		/// Entangled
		/// Removed at the start of the next turn.
		/// Can be targeted regardless of Taunt status.
		/// <color=#919191>They brush against your skin—dry, hollow, searching.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Vines_0 = "B_Abnormality_HistoryLv2_Vines_0";
        public static string Buff_B_Abnormality_HistoryLv2_Vines_1 = "B_Abnormality_HistoryLv2_Vines_1";
		/// <summary>
		/// Vines of Despair
		/// <color=#919191>The withered vines found their hold—now they tighten, slow and suffocating.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_Vines_2 = "B_Abnormality_HistoryLv2_Vines_2";
		/// <summary>
		/// Worker Bee
		/// When attacked, apply "Pollen" to the attacker.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_WorkerBee = "B_Abnormality_HistoryLv2_WorkerBee";
		/// <summary>
		/// Pollen
		/// Removed at the start of the next turn.
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv2_WorkerBee_0 = "B_Abnormality_HistoryLv2_WorkerBee_0";
		/// <summary>
		/// Barrier of Thorns
		/// When attacked, apply "Thorned Grace" to the attacker.
		/// <color=#919191>A silent bloom surrounds her — unseen, untouched, unforgiving.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_BarrierofThorns = "B_Abnormality_HistoryLv3_BarrierofThorns";
		/// <summary>
		/// Thorned Grace
		/// <color=#919191>You reached out with violence—she offered you beauty. Now her thorns bloom inside you.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_BarrierofThorns_0 = "B_Abnormality_HistoryLv3_BarrierofThorns_0";
		/// <summary>
		/// Queen's Mandate
		/// <color=#919191>In the Queen's name, the hive stands stronger. Her will flows like nectar, feeding each ally's strength and restoring their vitality with every heartbeat.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_Loyalty = "B_Abnormality_HistoryLv3_Loyalty";
		/// <summary>
		/// Malice
		/// At the start of each turn, apply "Malicious Mark" to all enemies.
		/// <color=#919191>A whispered curse drips from her lips, leaving a mark of malice on all who oppose her, as silent and inevitable as death itself.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_Malice = "B_Abnormality_HistoryLv3_Malice";
		/// <summary>
		/// Malicious Mark
		/// Removed at the start of the next turn.
		/// All incoming damage is increased based on the missing HP of the character with "Malice".
		/// Removed when the character with Malice is killed.
		/// <color=#919191>The mark of her malice lingers like an unspoken vow, sealing their fate with a touch of cold inevitability.</color>
		/// </summary>
        public static string Buff_B_Abnormality_HistoryLv3_Malice_0 = "B_Abnormality_HistoryLv3_Malice_0";
		/// <summary>
		/// Lament
		/// -10% damage against enemies without debuffs.
		/// At the start of each turn if character have debuff, all allies gain 1 Attack Power for this turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Lament = "B_Abnormality_TechnologicalLv1_Lament";
		/// <summary>
		/// Chrysalis Requiem
		/// Removed at the start of the next turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Lament_0 = "B_Abnormality_TechnologicalLv1_Lament_0";
		/// <summary>
		/// Metallic Ringing
		/// Attacks inflict "Echo Distortion", remove 1 stack on attack.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_MetallicRinging = "B_Abnormality_TechnologicalLv1_MetallicRinging";
		/// <summary>
		/// Echo Distortion
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_MetallicRinging_0 = "B_Abnormality_TechnologicalLv1_MetallicRinging_0";
		/// <summary>
		/// Repetitive Pattern-Recognition
		/// After Playing 2 Attacks this turn, gain 1 additional mana next turn.
		/// Only activates once per turn.
		/// Attacks Played : &a.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition = "B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition";
        public static string Buff_B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_0 = "B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_0";
		/// <summary>
		/// Phantom Aim
		/// Gain Ignore Taunt on all skills.
		/// The first Attack inflict "Requested Target" to the target's.
		/// If this character defeat an enemy with "Requested Target" gain "Phantom Bounty" and remove this buff.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Request = "B_Abnormality_TechnologicalLv1_Request";
		/// <summary>
		/// Requested Target
		/// Removed at the start of the next turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Request_0 = "B_Abnormality_TechnologicalLv1_Request_0";
		/// <summary>
		/// Phantom Bounty
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Request_1 = "B_Abnormality_TechnologicalLv1_Request_1";
        public static string Buff_B_Abnormality_TechnologicalLv1_Request_2 = "B_Abnormality_TechnologicalLv1_Request_2";
		/// <summary>
		/// Rhythm 
		/// Attacks inflict "Destructive Harmony" to the target's (Max 5). 
		/// After Playing Attack, gain 1 "Rhythm" (Max 5).
		/// Current "Rhythm" : &a.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Rhythm = "B_Abnormality_TechnologicalLv1_Rhythm";
		/// <summary>
		/// Destructive Harmony
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Rhythm_0 = "B_Abnormality_TechnologicalLv1_Rhythm_0";
		/// <summary>
		/// Violence
		/// All attack skills deal -15 ~ +30% damage.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv1_Violence = "B_Abnormality_TechnologicalLv1_Violence";
		/// <summary>
		/// Chained Wrath
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_ChainedWrath = "B_Abnormality_TechnologicalLv2_ChainedWrath";
		/// <summary>
		/// Clean
		/// 25% increased damage against enemies with no Action Points or Actions Counts 9+.
		/// When landing Critical Hit this turn, restore 1 mana. Only activates once per turn.
		/// Current Crtical Hits : &a.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_Clean = "B_Abnormality_TechnologicalLv2_Clean";
		/// <summary>
		/// Eternal Rest
		/// 5% increased damage for each debuff on target's (Max 25%).
		/// If the damage dealt exceeds 20% of the target's Max HP, inflict Stun (Base <sprite=2>100%) to the target's.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_EternalRest = "B_Abnormality_TechnologicalLv2_EternalRest";
		/// <summary>
		/// Musical Addiction
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_MusicalAddiction = "B_Abnormality_TechnologicalLv2_MusicalAddiction";
		/// <summary>
		/// <color=#3CB371> Recharge</color>
		/// Gain 1 Attack Power for this turn, whenever this character is restores HP (Max 3).
		/// Restore 1 Mana if this character defeats an enemy. Only activates once per turn.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_Recharge = "B_Abnormality_TechnologicalLv2_Recharge";
		/// <summary>
		/// The Seventh Bullet
		/// Every 3rd Single Attack targets a random character.
		/// Single Attacks played: &a
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet = "B_Abnormality_TechnologicalLv2_TheSeventhBullet";
        public static string Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_0 = "B_Abnormality_TechnologicalLv2_TheSeventhBullet_0";
		/// <summary>
		/// Coffin
		/// If target's have less then 90% of their Max HP (45% for Bosses), destroy enemy Action Point. Only activates once per turn.
		/// Current Status : &a.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_Coffin = "B_Abnormality_TechnologicalLv3_Coffin";
		/// <summary>
		/// Tödlicher Akkord
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_DarkFlame = "B_Abnormality_TechnologicalLv3_DarkFlame";
		/// <summary>
		/// Gebrochener Pakt
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_DarkFlame_0 = "B_Abnormality_TechnologicalLv3_DarkFlame_0";
		/// <summary>
		/// Pulse of the Machine
		/// All incoming damage is increased by 35%.
		/// Ally Heal 20% of all damage dealt.
		/// </summary>
        public static string Buff_B_Abnormality_TechnologicalLv3_Music = "B_Abnormality_TechnologicalLv3_Music";
		/// <summary>
		/// Emotional Level
		/// </summary>
        public static string Buff_B_EmotionalLevel = "B_EmotionalLevel";
		/// <summary>
		/// Bleed
		/// Current &a <color=red>Bleed</color>.
		/// When this character performs an action, they take <color=purple>&b Pain damage</color> and subtract 1/3rd of the <color=red>Bleed</color> stacks. (Rounds down).
		/// </summary>
        public static string Buff_B_EmotionalSystem_Bleed = "B_EmotionalSystem_Bleed";
        public static string Buff_B_EmotionalSystem_Draw = "B_EmotionalSystem_Draw";
        public static string Buff_B_EmotionalSystem_ManaReduction = "B_EmotionalSystem_ManaReduction";
		/// <summary>
		/// Behavior Adjustment
		/// </summary>
        public static string Buff_B_EnemyAbnormality_BehaviorAdjustment = "B_EnemyAbnormality_BehaviorAdjustment";
		/// <summary>
		/// Despair
		/// </summary>
        public static string Buff_B_EnemyAbnormality_Despair = "B_EnemyAbnormality_Despair";
		/// <summary>
		/// Energy Conversion
		/// After this character takes 50% of its Max HP as damage (25% for bosses), сurrent Mana is reduced by 1 this turn.
		/// Damage remaining: &a.
		/// </summary>
        public static string Buff_B_EnemyAbnormality_EnergyConversion = "B_EnemyAbnormality_EnergyConversion";
		/// <summary>
		/// Mirror Adjustment
		/// Attacker takes non-lethal <color=purple>Pain damage</color>  equal to 40% of the damage dealt.
		/// Only activates once per turn.
		/// Retaliations: &a.
		/// </summary>
        public static string Buff_B_EnemyAbnormality_MirrorAdjustment = "B_EnemyAbnormality_MirrorAdjustment";
		/// <summary>
		/// Shelter from the 27th of March
		/// Upon reaching 0 health, this character receives a buff that grants invincibility for 1 turn.
		/// </summary>
        public static string Buff_B_EnemyAbnormality_Shelter = "B_EnemyAbnormality_Shelter";
		/// <summary>
		/// Shelter from the 27th of March
		/// Survive fatal damage at 1 health.
		/// </summary>
        public static string Buff_B_EnemyAbnormality_Shelter_0 = "B_EnemyAbnormality_Shelter_0";
		/// <summary>
		/// Strengthen
		/// </summary>
        public static string Buff_B_EnemyAbnormality_Strengthen = "B_EnemyAbnormality_Strengthen";
		/// <summary>
		/// Stress
		/// </summary>
        public static string Buff_B_EnemyAbnormality_Stress = "B_EnemyAbnormality_Stress";
		/// <summary>
		/// Unity
		/// At the start of each turn, heal all allies by 25% of their Max Health (up to 25). If there are no allies, heal yourself instead.
		/// </summary>
        public static string Buff_B_EnemyAbnormality_Unity = "B_EnemyAbnormality_Unity";
		/// <summary>
		/// You Must Be Happy
		/// All incoming damage is reduced by 15%.
		/// </summary>
        public static string Buff_B_EnemyAbnormality_YouMustBeHappy = "B_EnemyAbnormality_YouMustBeHappy";
        public static string Buff_B_EnemyEmotionalLevel = "B_EnemyEmotionalLevel";
		/// <summary>
		/// Additional Dice
		/// Gain one more action per turn.
		/// </summary>
        public static string Buff_B_EnemyEmotionalLevel_Dice = "B_EnemyEmotionalLevel_Dice";
		/// <summary>
		/// Light
		/// Attack damage increases by &a%.
		/// &b.
		/// </summary>
        public static string Buff_B_EnemyEmotionalLevel_Light = "B_EnemyEmotionalLevel_Light";
		/// <summary>
		/// Hornets Sting
		/// </summary>
        public static string Buff_B_LucyEGO_History_HornetsSting = "B_LucyEGO_History_HornetsSting";
		/// <summary>
		/// Shattered Harmony
		/// </summary>
        public static string Buff_B_LucyEGO_Technological_Harmony = "B_LucyEGO_Technological_Harmony";
		/// <summary>
		/// Magic Bullet
		/// Synchronized with Der Freischütz.
		/// </summary>
        public static string Buff_B_LucyEGO_Technological_MagicBullet = "B_LucyEGO_Technological_MagicBullet";
        public static string Buff_B_LucyEGO_Technological_MagicBullet_0 = "B_LucyEGO_Technological_MagicBullet_0";
        public static string Buff_B_LucyEmotionalLevel = "B_LucyEmotionalLevel";
		/// <summary>
		/// Burn
		/// Current &a <color=#f8181c>Burn</color>.
		/// At the end of the turn, take <color=purple>&b Pain damage</color> and subtract 1/3rd of the <color=#f8181c>Burn</color> stack. (Rounds down).
		/// </summary>
        public static string Buff_B_Xiao_Burn = "B_Xiao_Burn";
		/// <summary>
		/// Emotional Level 5
		/// At 2 stacks, draw 1 skill
		/// </summary>
        public static string SkillExtended_Ex_EmotionalSystem_Draw = "Ex_EmotionalSystem_Draw";
        public static string SkillExtended_Ex_EmotionalSystem_EGO = "Ex_EmotionalSystem_EGO";
		/// <summary>
		/// Happy Memories
		/// The first skill played from hand costs 1 less.
		/// </summary>
        public static string SkillExtended_Ex_EmotionalSystem_HappyMemories = "Ex_EmotionalSystem_HappyMemories";
		/// <summary>
		/// Emotional Level 4
		/// The first skill played from hand costs 1 less
		/// </summary>
        public static string SkillExtended_Ex_EmotionalSystem_ManaReduction = "Ex_EmotionalSystem_ManaReduction";
        public static string SkillExtended_Ex_EmotionalSystem_PainDamage = "Ex_EmotionalSystem_PainDamage";
		/// <summary>
		/// Repetitive Pattern-Recognition
		/// At 2 stack gain 1 additonal Mana next turn
		/// </summary>
        public static string SkillExtended_Ex_EmotionalSystem_Repetitive = "Ex_EmotionalSystem_Repetitive";
		/// <summary>
		/// Phantom Aim
		/// The first Attack inflict "Requested Target" to the target's
		/// </summary>
        public static string SkillExtended_Ex_EmotionalSystem_Request = "Ex_EmotionalSystem_Request";
		/// <summary>
		/// The Seventh Bullet
		/// Every 3rd Single Attack targets a random character.
		/// </summary>
        public static string SkillExtended_Ex_EmotionalSystem_TheSeventhBullet = "Ex_EmotionalSystem_TheSeventhBullet";
		/// <summary>
		/// Floor of History
		/// Passive:
		/// </summary>
        public static string Character_History_Floor = "History_Floor";
		/// <summary>
		/// Abnormality Level I
		/// </summary>
        public static string SkillKeyword_KeyWord_AbnormalityLevel_I = "KeyWord_AbnormalityLevel_I";
		/// <summary>
		/// Abnormality Level II
		/// </summary>
        public static string SkillKeyword_KeyWord_AbnormalityLevel_II = "KeyWord_AbnormalityLevel_II";
		/// <summary>
		/// Abnormality Level III
		/// </summary>
        public static string SkillKeyword_KeyWord_AbnormalityLevel_III = "KeyWord_AbnormalityLevel_III";
		/// <summary>
		/// Destructive Harmony
		/// <color=#919191>Attack Power +4%
		/// Receiving Damage +4%
		/// CC Resist -4%
		/// Max 5 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_DestructiveHarmony = "KeyWord_DestructiveHarmony";
		/// <summary>
		/// Desynchronizes
		/// <color=#919191>Remove all unique user skills from the deck and replace them with the previous skills.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Desynchronizes = "KeyWord_Desynchronizes";
		/// <summary>
		/// Echo Distortion
		/// <color=#919191>Attack Power -5%
		/// CC Resist -5%
		/// Max 5 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_EchoDistortion = "KeyWord_EchoDistortion";
		/// <summary>
		/// Embrace That Never Ends
		/// <color=#919191>Can only target user.
		/// Removed when this character attacks user.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_EmbraceThatNeverEnds = "KeyWord_EmbraceThatNeverEnds";
		/// <summary>
		/// Bleed
		/// When this character performs an action, they take <color=purple>X Pain damage</color> and subtract 1/3rd of the <color=red>Bleed</color> stacks. (Rounds down).
		/// <color=#919191>Base chance :<sprite=1> 100%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Emotional_Bleed = "KeyWord_Emotional_Bleed";
		/// <summary>
		/// Burn
		/// At the end of the turn, take <color=purple>X Pain damage</color> and subtract 1/3rd of the <color=#f8181c>Burn</color> stack. (Rounds down).
		/// <color=#919191>Base chance :<sprite=1> 100%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Emotional_Burn = "KeyWord_Emotional_Burn";
		/// <summary>
		/// Entangled
		/// <color=#919191>Receiving Critical Damage +15%
		/// Receiving Critical Chance +15%
		/// Evade -15%
		/// Can be targeted regardless of Taunt status.
		/// Removed at the start of the next turn.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Entangled = "KeyWord_Entangled";
		/// <summary>
		/// Fairy's Favor
		/// <color=#919191>Applies "Glutton's Mark" on attack.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_FairysFavor = "KeyWord_FairysFavor";
		/// <summary>
		/// Gebrochener Pakt
		/// <color=#919191>Debuff Resist -300%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_GebrochenerPakt = "KeyWord_GebrochenerPakt";
		/// <summary>
		/// Lucy E.G.O.
		/// </summary>
        public static string SkillKeyword_KeyWord_LucyEGO = "KeyWord_LucyEGO";
		/// <summary>
		/// Malicious Mark
		/// <color=#919191>All incoming damage is increased based on the missing HP of the character with "Malice"(Min 10%, Max 30%).
		/// Removed at the start of the next turn.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_MaliciousMark = "KeyWord_MaliciousMark";
		/// <summary>
		/// Phantom Bounty
		/// <color=#919191>Attack Power +1
		/// Active until the stage ends
		/// Max stacks 2</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_PhantomBounty = "KeyWord_PhantomBounty";
		/// <summary>
		/// Pollen
		/// <color=#919191>Receiving Damage +30%
		/// Removed at the start of the next turn.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Pollen = "KeyWord_Pollen";
		/// <summary>
		/// Queen's Mandate
		/// <color=#919191>Attack power +5
		/// Healing power +5</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_QueensMandate = "KeyWord_QueensMandate";
		/// <summary>
		/// Requested Target
		/// <color=#919191>Evade -10%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_RequestedTarget = "KeyWord_RequestedTarget";
		/// <summary>
		/// Rhythm
		/// <color=#919191>Attack Power +4%
		/// Receiving Damage +4%
		/// Max 5 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Rhythm = "KeyWord_Rhythm";
		/// <summary>
		/// Synchronize
		/// <color=#919191>Remove all user skills from the deck, add unique skills to hand, and change fixed ability to "Desynchronize".</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_Synchronize = "KeyWord_Synchronize";
		/// <summary>
		/// Thorned Grace
		/// <color=#919191>Attack power -5%
		/// Max 4 stacks</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_ThornedGrace = "KeyWord_ThornedGrace";
		/// <summary>
		/// Vines of Despair
		/// <color=#919191>Attack Power -20%
		/// Armor -20%</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_VinesofDespair = "KeyWord_VinesofDespair";
        public static string SkillEffect_SE_Tick_B_Abnormality_HistoryLv1_Matchlight = "SE_Tick_B_Abnormality_HistoryLv1_Matchlight";
        public static string SkillEffect_SE_Tick_B_EmotionalSystem_Bleed = "SE_Tick_B_EmotionalSystem_Bleed";
        public static string SkillEffect_SE_Tick_B_EnemyAbnormality_EnergyConversion_0 = "SE_Tick_B_EnemyAbnormality_EnergyConversion_0";
        public static string SkillEffect_SE_T_Abnormality_HistoryLv1_Ashes_Pos = "SE_T_Abnormality_HistoryLv1_Ashes_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_Ashes_Pos = "SE_T_S_Abnormality_HistoryLv1_Ashes_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_DisplayofAffection_Neg = "SE_T_S_Abnormality_HistoryLv1_DisplayofAffection_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_HappyMemories_Pos = "SE_T_S_Abnormality_HistoryLv1_HappyMemories_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_Matchlight_Neg = "SE_T_S_Abnormality_HistoryLv1_Matchlight_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_Pos = "SE_T_S_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv1_TheFairiesCare_Neg = "SE_T_S_Abnormality_HistoryLv1_TheFairiesCare_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Footfalls_Neg = "SE_T_S_Abnormality_HistoryLv2_Footfalls_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Gluttony_Pos = "SE_T_S_Abnormality_HistoryLv2_Gluttony_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Predation_Neg = "SE_T_S_Abnormality_HistoryLv2_Predation_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Spores_Pos = "SE_T_S_Abnormality_HistoryLv2_Spores_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_Vines_Pos = "SE_T_S_Abnormality_HistoryLv2_Vines_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv2_WorkerBee_Neg = "SE_T_S_Abnormality_HistoryLv2_WorkerBee_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_BarrierofThorns_Pos = "SE_T_S_Abnormality_HistoryLv3_BarrierofThorns_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_BarrierofThorns_Pos_0 = "SE_T_S_Abnormality_HistoryLv3_BarrierofThorns_Pos_0";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_Loyalty_Neg = "SE_T_S_Abnormality_HistoryLv3_Loyalty_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_Malice = "SE_T_S_Abnormality_HistoryLv3_Malice";
        public static string SkillEffect_SE_T_S_Abnormality_HistoryLv3_Malice_Pos = "SE_T_S_Abnormality_HistoryLv3_Malice_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Lament_Neg = "SE_T_S_Abnormality_TechnologicalLv1_Lament_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_MetallicRinging_Pos = "SE_T_S_Abnormality_TechnologicalLv1_MetallicRinging_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_Pos = "SE_T_S_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Request_Pos = "SE_T_S_Abnormality_TechnologicalLv1_Request_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Rhythm_Neg = "SE_T_S_Abnormality_TechnologicalLv1_Rhythm_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv1_Violence_Neg = "SE_T_S_Abnormality_TechnologicalLv1_Violence_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_ChainedWrath_Neg = "SE_T_S_Abnormality_TechnologicalLv2_ChainedWrath_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_Clean_Pos = "SE_T_S_Abnormality_TechnologicalLv2_Clean_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_EternalRest_Pos = "SE_T_S_Abnormality_TechnologicalLv2_EternalRest_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_MusicalAddiction_Neg = "SE_T_S_Abnormality_TechnologicalLv2_MusicalAddiction_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_Recharge_Pos = "SE_T_S_Abnormality_TechnologicalLv2_Recharge_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv2_TheSeventhBullet_Neg = "SE_T_S_Abnormality_TechnologicalLv2_TheSeventhBullet_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv3_Coffin_Pos = "SE_T_S_Abnormality_TechnologicalLv3_Coffin_Pos";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv3_DarkFlame_Neg = "SE_T_S_Abnormality_TechnologicalLv3_DarkFlame_Neg";
        public static string SkillEffect_SE_T_S_Abnormality_TechnologicalLv3_Music_Neg = "SE_T_S_Abnormality_TechnologicalLv3_Music_Neg";
        public static string SkillEffect_SE_T_S_Boss_Witch_Curse = "SE_T_S_Boss_Witch_Curse";
        public static string SkillEffect_SE_T_S_Boss_Witch_Curse_0 = "SE_T_S_Boss_Witch_Curse_0";
        public static string SkillEffect_SE_T_S_Buff_MirrorAdjustment = "SE_T_S_Buff_MirrorAdjustment";
        public static string SkillEffect_SE_T_S_Buff_Unity = "SE_T_S_Buff_Unity";
        public static string SkillEffect_SE_T_S_LucyEGO_History_FourthMatchFlame = "SE_T_S_LucyEGO_History_FourthMatchFlame";
        public static string SkillEffect_SE_T_S_LucyEGO_History_GreenStem = "SE_T_S_LucyEGO_History_GreenStem";
        public static string SkillEffect_SE_T_S_LucyEGO_History_Hornet = "SE_T_S_LucyEGO_History_Hornet";
        public static string SkillEffect_SE_T_S_LucyEGO_History_TheForgotten = "SE_T_S_LucyEGO_History_TheForgotten";
        public static string SkillEffect_SE_T_S_LucyEGO_History_Wingbeat = "SE_T_S_LucyEGO_History_Wingbeat";
        public static string SkillEffect_SE_T_S_LucyEGO_Technological_GrinderMk = "SE_T_S_LucyEGO_Technological_GrinderMk";
        public static string SkillEffect_SE_T_S_LucyEGO_Technological_Harmony = "SE_T_S_LucyEGO_Technological_Harmony";
        public static string SkillEffect_SE_T_S_LucyEGO_Technological_MagicBullet = "SE_T_S_LucyEGO_Technological_MagicBullet";
        public static string SkillEffect_SE_T_S_LucyEGO_Technological_Regret = "SE_T_S_LucyEGO_Technological_Regret";
        public static string SkillEffect_SE_T_S_LucyEGO_Technological_SolemnLament = "SE_T_S_LucyEGO_Technological_SolemnLament";
        public static string SkillEffect_SE_T_S_Synchronize_Technological_Desynchronize = "SE_T_S_Synchronize_Technological_Desynchronize";
        public static string SkillEffect_SE_T_S_Synchronize_Technological_FloodingBullets = "SE_T_S_Synchronize_Technological_FloodingBullets";
        public static string SkillEffect_SE_T_S_Synchronize_Technological_FloodingBullets_0 = "SE_T_S_Synchronize_Technological_FloodingBullets_0";
        public static string SkillEffect_SE_T_S_Synchronize_Technological_InevitableBullet = "SE_T_S_Synchronize_Technological_InevitableBullet";
        public static string SkillEffect_SE_T_S_Synchronize_Technological_MagicBullet = "SE_T_S_Synchronize_Technological_MagicBullet";
        public static string SkillEffect_SE_T_S_Synchronize_Technological_SilentBullet = "SE_T_S_Synchronize_Technological_SilentBullet";
        public static string SkillEffect_SE_T_S_WitchCurse = "SE_T_S_WitchCurse";
		/// <summary>
		/// <color=#3CB371>Ashes</color>
		/// <color=#919191>The charred body represents the child's crumbled hope, while the ever blazing flame represents the obsession for affection.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_Ashes_Pos = "S_Abnormality_HistoryLv1_Ashes_Pos";
		/// <summary>
		/// <color=#DC143C>Display of Affection</color>
		/// <color=#919191>Its memories began with a warm hug.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_DisplayofAffection_Neg = "S_Abnormality_HistoryLv1_DisplayofAffection_Neg";
		/// <summary>
		/// <color=#3CB371>Happy Memories</color>
		/// <color=#919191>But, you see, Teddy never wanted to be separate from its owner ever again.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_HappyMemories_Pos = "S_Abnormality_HistoryLv1_HappyMemories_Pos";
		/// <summary>
		/// <color=#DC143C>Matchlight</color>
		/// <color=#919191>Well, she's like a ticking time bomb. No one can tell if she's in a good mood or not.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_Matchlight_Neg = "S_Abnormality_HistoryLv1_Matchlight_Neg";
		/// <summary>
		/// <color=#3CB371>Nostalgic Embrace of the Old Days</color>
		/// <color=#919191>Teddy was hugging someone tightly. Teddy loved hugs. But something was odd.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_Pos = "S_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays_Pos";
		/// <summary>
		/// <color=#DC143C>The Fairies' Care</color>
		/// The target gains Healing Gauge equal to their max HP, then restores up to 10 HP.
		/// <color=#919191>The fairies protect our employees. Everything will be peaceful while you are under the fairies' care.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv1_TheFairiesCare_Neg = "S_Abnormality_HistoryLv1_TheFairiesCare_Neg";
		/// <summary>
		/// <color=#DC143C>Footfalls</color>
		/// <color=#919191>I am coming to you. You, who will be reduced to ash like me.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Footfalls_Neg = "S_Abnormality_HistoryLv2_Footfalls_Neg";
		/// <summary>
		/// <color=#3CB371>Gluttony</color>
		/// All allies except the selected gain "Fairy's Favor".
		/// <color=#919191>The fairies were no more than carnivorous monsters, and their "protection" was their method to keep the meat fresh.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Gluttony_Pos = "S_Abnormality_HistoryLv2_Gluttony_Pos";
		/// <summary>
		/// <color=#DC143C>Predation</color>
		/// Deal non-lethal <color=purple>15 Pain damage</color> to all living allies and heal the user for 66% of the total damage dealt.
		/// <color=#919191>His stomach and face were ripped off, and his eyeballs and organs were damaged as if they were eaten by something. Meanwhile, the fairies had someone's blood and flesh smeared all over their mouths.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Predation_Neg = "S_Abnormality_HistoryLv2_Predation_Neg";
		/// <summary>
		/// <color=#3CB371>Spores</color>
		/// <color=#919191>It has been confirmed that the spores carry drone eggs that hatch inside a living host.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Spores_Pos = "S_Abnormality_HistoryLv2_Spores_Pos";
		/// <summary>
		/// <color=#3CB371>Vines</color>
		/// <color=#919191>One day, a branch grew from it. The leaves and branches were already withered and dry, but it continued to grow.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_Vines_Pos = "S_Abnormality_HistoryLv2_Vines_Pos";
		/// <summary>
		/// <color=#DC143C>Worker Bee</color>
		/// <color=#919191>They show only two forms of behavior: Delivering nutrients to the Queen, and proliferating.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv2_WorkerBee_Neg = "S_Abnormality_HistoryLv2_WorkerBee_Neg";
		/// <summary>
		/// <color=#3CB371>Barrier of Thorns</color>
		/// <color=#919191>The apple that dropped from Snow White's hand after a single bite could never be happy. The apple, full of loneliness and hatred towards the princess, waited for the day it would rot away and return to the earth.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv3_BarrierofThorns_Pos = "S_Abnormality_HistoryLv3_BarrierofThorns_Pos";
		/// <summary>
		/// <color=#DC143C>Loyalty</color>
		/// Sacrifice the selected ally, all alive allies gain "Queen's Mandate" buff.
		/// <color=#919191>The loyalty of bees is a naturally developed instinct. If we discover a way to draw forth that instinct, many things could change.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv3_Loyalty_Neg = "S_Abnormality_HistoryLv3_Loyalty_Neg";
		/// <summary>
		/// <color=#3CB371>Malice</color>
		/// <color=#919191>The inherent malice caused all life to crumble as soon as it bloomed.</color>
		/// </summary>
        public static string Skill_S_Abnormality_HistoryLv3_Malice_Pos = "S_Abnormality_HistoryLv3_Malice_Pos";
		/// <summary>
		/// <color=#DC143C>Lament</color>
		/// <color=#919191>They say the mourner with a huge luggage on his back had come to be a savior to all.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Lament_Neg = "S_Abnormality_TechnologicalLv1_Lament_Neg";
		/// <summary>
		/// <color=#3CB371>Metallic Ringing</color>
		/// <color=#919191>My head... turning into metal... folds in my brain, being flattened...</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_MetallicRinging_Pos = "S_Abnormality_TechnologicalLv1_MetallicRinging_Pos";
		/// <summary>
		/// <color=#3CB371>Repetitive Pattern-Recognition</color>
		/// <color=#919191>The day I was sent to a new home for the first time, I gave them the gift they wanted so earnestly.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_Pos = "S_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_Pos";
		/// <summary>
		/// <color=#3CB371>Request</color>
		/// <color=#919191>Just as the Devil said, the bullets will puncture anything you please. Forever.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Request_Pos = "S_Abnormality_TechnologicalLv1_Request_Pos";
		/// <summary>
		/// <color=#DC143C>Rhythm</color>
		/// <color=#919191>It was creating a rhythm.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Rhythm_Neg = "S_Abnormality_TechnologicalLv1_Rhythm_Neg";
		/// <summary>
		/// <color=#DC143C>Violence</color>
		/// <color=#919191>What's really pitiful is people like you dying to the likes of me.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv1_Violence_Neg = "S_Abnormality_TechnologicalLv1_Violence_Neg";
		/// <summary>
		/// <color=#DC143C>Chained Wrath</color>
		/// <color=#919191>He wears a straitjacket, but is as free as any man. No amount of chains and restraints is enough to prevent him from committing violence.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_ChainedWrath_Neg = "S_Abnormality_TechnologicalLv2_ChainedWrath_Neg";
		/// <summary>
		/// <color=#3CB371>Clean</color>
		/// <color=#919191>It recognizes a bad mood as a sign that the surroundings are dirty, and promptly enters cleaning mode.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_Clean_Pos = "S_Abnormality_TechnologicalLv2_Clean_Pos";
		/// <summary>
		/// <color=#3CB371>Eternal Rest</color>
		/// <color=#919191>People believed that they would become beautiful beings with small wings when they died. It's a silly story. Nonsensical too.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_EternalRest_Pos = "S_Abnormality_TechnologicalLv2_EternalRest_Pos";
		/// <summary>
		/// <color=#DC143C>Musical Addiction</color>
		/// <color=#919191>After all, art is a devil's gift, born from despair and suffering. Never stop performing until the body crumbles to dust.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_MusicalAddiction_Neg = "S_Abnormality_TechnologicalLv2_MusicalAddiction_Neg";
		/// <summary>
		/// <color=#3CB371>Recharge</color>
		/// <color=#919191>However, the limbs were equipped with sharp instruments instead of cleaning supplies.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_Recharge_Pos = "S_Abnormality_TechnologicalLv2_Recharge_Pos";
		/// <summary>
		/// <color=#DC143C>The Seventh Bullet</color>
		/// <color=#919191>The Devil proposed a childish contract: The last bullet would puncture the head of his beloved. The moment he heard that, he sought and shot all the people he loved.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv2_TheSeventhBullet_Neg = "S_Abnormality_TechnologicalLv2_TheSeventhBullet_Neg";
		/// <summary>
		/// <color=#3CB371>Coffin</color>
		/// <color=#919191>He's carrying a coffin. A large coffin to pay tribute to the employees who have nowhere else to go. It is still too small to comfort those innocent sacrifices.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv3_Coffin_Pos = "S_Abnormality_TechnologicalLv3_Coffin_Pos";
		/// <summary>
		/// <color=#DC143C>Dark Flame</color>
		/// Apply "Gebrochener Pakt" to all targets except selected ally.
		/// <color=#919191>One day, the marksman realized the Devil no longer followed him. He pondered why, then realized that his soul had already fallen to Hell from the beginning.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv3_DarkFlame_Neg = "S_Abnormality_TechnologicalLv3_DarkFlame_Neg";
		/// <summary>
		/// <color=#DC143C>Music</color>
		/// <color=#919191>But nothing could compare to the music it makes when it eats a human.</color>
		/// </summary>
        public static string Skill_S_Abnormality_TechnologicalLv3_Music_Neg = "S_Abnormality_TechnologicalLv3_Music_Neg";
		/// <summary>
		/// Crucifying Curse
		/// </summary>
        public static string Skill_S_Boss_Witch_Curse = "S_Boss_Witch_Curse";
		/// <summary>
		/// Weakening Curse
		/// </summary>
        public static string Skill_S_Boss_Witch_Curse_0 = "S_Boss_Witch_Curse_0";
		/// <summary>
		/// Mirror Adjustment
		/// </summary>
        public static string Skill_S_Buff_MirrorAdjustment = "S_Buff_MirrorAdjustment";
		/// <summary>
		/// Unity
		/// </summary>
        public static string Skill_S_Buff_Unity = "S_Buff_Unity";
		/// <summary>
		/// <color=#ffc500>Fourth Match Flame</color>
		/// Inflict 10 <color=#f8181c>Burn</color>.
		/// If facing 1 enemy, inflict 5 additional <color=#f8181c>Burn</color>. 
		/// </summary>
        public static string Skill_S_LucyEGO_History_FourthMatchFlame = "S_LucyEGO_History_FourthMatchFlame";
		/// <summary>
		/// <color=#ffc500>Green Stem</color>
		/// Apply "Vines of Despair" if the target is affected by the "Entangled" debuff.
		/// Otherwise apply "Entagled".
		/// </summary>
        public static string Skill_S_LucyEGO_History_GreenStem = "S_LucyEGO_History_GreenStem";
		/// <summary>
		/// <color=#ffc500>Hornet</color>
		/// </summary>
        public static string Skill_S_LucyEGO_History_Hornet = "S_LucyEGO_History_Hornet";
		/// <summary>
		/// <color=#ffc500>The Forgotten</color>
		/// Destroy ALL target's Action Points.
		/// </summary>
        public static string Skill_S_LucyEGO_History_TheForgotten = "S_LucyEGO_History_TheForgotten";
		/// <summary>
		/// <color=#ffc500>Wingbeat</color>
		/// Recast this skill 2 times.
		/// Overheal the ally with the lowest health by 6.
		/// </summary>
        public static string Skill_S_LucyEGO_History_Wingbeat = "S_LucyEGO_History_Wingbeat";
		/// <summary>
		/// <color=#ffc500>Grinder Mk. 5-2</color>
		/// Inflict 10 <color=red>Bleed</color>.
		/// If this skill defeat an enemy, restore 1 Mana and Draw 1 skill.
		/// </summary>
        public static string Skill_S_LucyEGO_Technological_GrinderMk = "S_LucyEGO_Technological_GrinderMk";
		/// <summary>
		/// <color=#ffc500>Harmony</color>
		/// </summary>
        public static string Skill_S_LucyEGO_Technological_Harmony = "S_LucyEGO_Technological_Harmony";
		/// <summary>
		/// <color=#ffc500>Magic Bullet</color>
		/// The selected ally synchronizes with Der Freischütz for 3 Turns.
		/// </summary>
        public static string Skill_S_LucyEGO_Technological_MagicBullet = "S_LucyEGO_Technological_MagicBullet";
		/// <summary>
		/// <color=#ffc500>Regret</color>
		/// Recast this skill 2 times.
		/// Apply Stun (Base <sprite=2>100%) to the target.
		/// If the target is already Stunned, destroy their Action Points.
		/// </summary>
        public static string Skill_S_LucyEGO_Technological_Regret = "S_LucyEGO_Technological_Regret";
		/// <summary>
		/// <color=#ffc500>Solemn Lament</color>
		/// Recast this skill 6 times.
		/// </summary>
        public static string Skill_S_LucyEGO_Technological_SolemnLament = "S_LucyEGO_Technological_SolemnLament";
		/// <summary>
		/// <color=#ffc500>Desynchronize</color>
		/// Desynchronizes with Der Freischütz.
		/// </summary>
        public static string Skill_S_Synchronize_Technological_Desynchronize = "S_Synchronize_Technological_Desynchronize";
		/// <summary>
		/// <color=#ffc500>Flooding Bullets</color>
		/// Cast this skill again.
		/// </summary>
        public static string Skill_S_Synchronize_Technological_FloodingBullets = "S_Synchronize_Technological_FloodingBullets";
        public static string Skill_S_Synchronize_Technological_FloodingBullets_0 = "S_Synchronize_Technological_FloodingBullets_0";
		/// <summary>
		/// <color=#3c8dbc>Inevitable Bullet</color>
		/// This skill can be played repeatedly during this turn.
		/// </summary>
        public static string Skill_S_Synchronize_Technological_InevitableBullet = "S_Synchronize_Technological_InevitableBullet";
		/// <summary>
		/// <color=#ffc500>Magic Bullet</color>
		/// Restore 1 Mana.
		/// </summary>
        public static string Skill_S_Synchronize_Technological_MagicBullet = "S_Synchronize_Technological_MagicBullet";
		/// <summary>
		/// <color=#3c8dbc>Silent Bullet</color>
		/// Destroy target's Action Point.
		/// </summary>
        public static string Skill_S_Synchronize_Technological_SilentBullet = "S_Synchronize_Technological_SilentBullet";
		/// <summary>
		/// Floor of Technological Sciences
		/// Passive:
		/// </summary>
        public static string Character_Technological_Floor = "Technological_Floor";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// English:
		/// Select character to receive Abnormality Page.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string AbnoRecieve => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("AbnoRecieve");
		/// <summary>
		/// Korean:
		/// English:
		/// Select Abnormality Page.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string AbnoSelect => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("AbnoSelect");
		/// <summary>
		/// Korean:
		/// English:
		/// Lucy reaches her breakdown. Choose an E.G.O. to possess.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EGOSelect => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EGOSelect");
		/// <summary>
		/// Korean:
		/// English:
		/// No E.G.O. skills are available.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EGO_NoEGO => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EGO_NoEGO");
		/// <summary>
		/// Korean:
		/// English:
		/// Press to switch to E.G.O. skills.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EGO_SwitchEGO => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EGO_SwitchEGO");
		/// <summary>
		/// Korean:
		/// English:
		/// Press to switch to E.G.O. skills or press [S].
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EGO_SwitchEGOHotkey => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EGO_SwitchEGOHotkey");
		/// <summary>
		/// Korean:
		/// English:
		/// Press to switch back to your hand.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EGO_SwitchHand => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EGO_SwitchHand");
		/// <summary>
		/// Korean:
		/// English:
		/// Press to switch back to your hand or press [S].
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EGO_SwitchHandHotkey => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EGO_SwitchHandHotkey");
		/// <summary>
		/// Korean:
		/// English:
		/// Can be used after
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EgoCountdown => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EgoCountdown");
		/// <summary>
		/// Korean:
		/// English:
		/// turn(s).
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EgoCountdown_0 => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EgoCountdown_0");
		/// <summary>
		/// Korean:
		/// English:
		/// Can only be used once.
		/// Japanese:
		/// Chinese:
		/// Chinese-TW:
		/// </summary>
        public static string EgoOnce => ModManager.getModInfo("EmotionalSystem").localizationInfo.SystemLocalizationUpdate("EgoOnce");

    }
}