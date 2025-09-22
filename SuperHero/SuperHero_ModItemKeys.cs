using ChronoArkMod;
namespace SuperHero
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Barrier of Light ☆
		/// When attacked by <color=#FFA500>Super Hero</color> or <color=#FF00FF>Super Villain</color>, reduce the incoming damage to 0, reflect it onto a random enemy, and remove 1 stack.
		/// </summary>
        public static string Buff_B_Ex_SuperHero_BarrierofLight = "B_Ex_SuperHero_BarrierofLight";
		/// <summary>
		/// Light ☆ Armor
		/// Blocks all <color=#FF4500>Justice debuffs</color>.
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
        public static string Buff_B_SuperHero_EnemyResist = "B_SuperHero_EnemyResist";
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
		/// </summary>
        public static string Buff_B_SuperHero_OverpoweredProtagonist = "B_SuperHero_OverpoweredProtagonist";
		/// <summary>
		/// Plot Armor ☆
		/// At the start of each turn gain &a barrier <color=#FF7C34>(20% of Max Health)</color>.
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
		/// At the start of each turn draw 1 additional skill and Restore 1 Mana.
		/// </summary>
        public static string Buff_B_SuperHero_SecondAct = "B_SuperHero_SecondAct";
		/// <summary>
		/// When cast, <color=#FFA500>Super Hero</color> gains 4 <color=#FFD700>Hero Complex</color>.
		/// Gain a buff that reduces the next damage received from <color=#FFA500>Super Hero</color> to 0 and reflects it onto a random enemy.
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
		/// Remove Overload from all allies, restore 1 mana and draw 1 skill.
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
		/// Gain &a Barrier <color=#FF7C34>(20% of Max Health)</color>.
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
		/// 모든 적과 아군에게 <color=#50C878>히어로의 스포트라이트</color>를 적용합니다.
		/// English:
		/// Apply <color=#50C878>Hero's Spotlight</color> to all enemies and allies.
		/// Japanese:
		/// 全ての敵と味方に<color=#50C878>ヒーローのスポットライト</color>を適用する。
		/// Chinese:
		/// 对所有敌人和友方施加<color=#50C878>英雄的聚光灯</color>。
		/// Chinese-TW:
		/// 對所有敵人和友方施加<color=#50C878>英雄的聚光燈</color>。
		/// </summary>
        public static string Apotheosis_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("Apotheosis_0");
		/// <summary>
		/// Korean:
		/// 모든 적에게 <color=#50C878>히어로의 스포트라이트</color>를 적용합니다.
		/// English:
		/// Apply <color=#50C878>Hero's Spotlight</color> to all enemies.
		/// Japanese:
		/// 全ての敵に<color=#50C878>ヒーローのスポットライト</color>を適用する。
		/// Chinese:
		/// 对所有敌人施加<color=#50C878>英雄的聚光灯</color>。
		/// Chinese-TW:
		/// 對所有敵人施加<color=#50C878>英雄的聚光燈</color>。
		/// </summary>
        public static string Apotheosis_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("Apotheosis_1");
		/// <summary>
		/// Korean:
		/// 고정 능력일 경우 비용이 1减少됩니다.
		/// 모든 아군에게 <color=#DC143C>주홍색 잔재</color>를 적용합니다.
		/// English:
		/// Cost reduced by 1 if this is a fixed ability.
		/// Apply <color=#DC143C>Scarlet Remnant</color> to all allies.
		/// Japanese:
		/// 固定アビリティの場合、コストが1減少する。
		/// 味方全体に<color=#DC143C>真紅の残滓</color>を適用する。
		/// Chinese:
		/// 若是固定能力，则费用减少1。
		/// 对所有友方施加<color=#DC143C>猩红残迹</color>。
		/// Chinese-TW:
		/// 若是固定能力，則費用減少1。
		/// 對所有友方施加<color=#DC143C>猩紅殘跡</color>。
		/// </summary>
        public static string BloodStained_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("BloodStained_0");
		/// <summary>
		/// Korean:
		/// 고정 능력일 경우 비용이 1减少됩니다.
		/// English:
		/// Cost reduced by 1 if this is a fixed ability.
		/// Japanese:
		/// 固定アビリティの場合、コストが1減少する。
		/// Chinese:
		/// 若是固定能力，则费用减少1。
		/// Chinese-TW:
		/// 若是固定能力，則費用減少1。
		/// </summary>
        public static string BloodStained_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("BloodStained_1");
		/// <summary>
		/// Korean:
		/// 모든 아군이 최대 체력의 50%에 해당하는 피해를 받습니다.
		/// 대상의 체력이 60% (보스의 경우 30%) 미만일 경우, <b>대상의 체력을 0으로 만듭니다</b>.
		/// English:
		/// All allies take damage equal 50% of their Max Health.
		/// If the target's health is below 60% (30% for bosses), <b>reduce the target's health to 0</b>.
		/// Japanese:
		/// 味方全体が最大HPの50%に等しいダメージを受ける。
		/// 対象のHPが60%（ボスの場合は30%）未満の場合、<b>対象のHPを0にする</b>。
		/// Chinese:
		/// 所有友方受到等于其最大生命值50%的伤害。
		/// 若目标生命值低于60%（首领为30%），<b>则目标生命值降为0</b>。
		/// Chinese-TW:
		/// 所有友方受到等於其最大生命值50%的傷害。
		/// 若目標生命值低於60%（首領為30%），<b>則目標生命值降為0</b>。
		/// </summary>
        public static string EraseMobs_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("EraseMobs_0");
		/// <summary>
		/// Korean:
		/// 대상의 체력이 60% (보스의 경우 30%) 미만일 경우, <b>대상의 체력을 0으로 만듭니다</b>.
		/// English:
		/// If the target's health is below 60% (30% for bosses), <b>reduce the target's health to 0</b>.
		/// Japanese:
		/// 対象のHPが60%（ボスの場合は30%）未満の場合、<b>対象のHPを0にする</b>。
		/// Chinese:
		/// 若目标生命值低于60%（首领为30%），<b>则目标生命值降为0</b>。
		/// Chinese-TW:
		/// 若目標生命值低於60%（首領為30%），<b>則目標生命值降為0</b>。
		/// </summary>
        public static string EraseMobs_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("EraseMobs_1");
		/// <summary>
		/// Korean:
		/// 공격으로 아군을 대상으로 지정할 확률 &b%를 얻습니다.
		/// English:
		/// Gain &b% chance to target allies with attacks.
		/// Japanese:
		/// 攻撃で味方を対象にする確率&b%を獲得する
		/// Chinese:
		/// 获得&b%几率以友方为攻击目标。
		/// Chinese-TW:
		/// 獲得&b%機率以友方為攻擊目標。
		/// </summary>
        public static string HeroComplex_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("HeroComplex_0");
		/// <summary>
		/// Korean:
		/// 이 턴 동안 이 스킬을 반복해서 사용할 수 있습니다.
		/// 이 스킬로 적을 처치하면 마나 2를 회복합니다.
		/// 무작위 아군에게 <color=#FF4500>정의의 표식</color>을 적용합니다.
		/// English:
		/// This skill can be played repeatedly during this turn.
		/// If this skill defeat an enemy restore 2 Mana.
		/// Apply <color=#FF4500>Mark of Justice</color> to a random ally.
		/// Japanese:
		/// このターン中、このスキルを繰り返し使用できる。
		/// このスキルで敵を倒した場合、マナを2回復する。
		/// ランダムな味方に<color=#FF4500>正義の印</color>を適用する。
		/// Chinese:
		/// 此回合内可重复使用此技能。
		/// 若此技能击败敌人，则恢复2点法力值。
		/// 对随机友方施加<color=#FF4500>正义标记</color>。
		/// Chinese-TW:
		/// 此回合內可重複使用此技能。
		/// 若此技能擊敗敵人，則恢復2點法力值。
		/// 對隨機友方施加<color=#FF4500>正義標記</color>。
		/// </summary>
        public static string InTheNameOfJustice_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_0");
		/// <summary>
		/// Korean:
		/// 이 턴 동안 이 스킬을 반복해서 사용할 수 있습니다.
		/// 이 스킬로 적을 처치하면 마나 2를 회복합니다
		/// English:
		/// This skill can be played repeatedly during this turn.
		/// If this skill defeat an enemy restore 2 Mana.
		/// Japanese:
		/// このターン中、このスキルを繰り返し使用できる。
		/// このスキルで敵を倒した場合、マナを2回復する。
		/// Chinese:
		/// 此回合内可重复使用此技能。
		/// 若此技能击败敌人，则恢复2点法力值。
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_1");
		/// <summary>
		/// Korean:
		/// 이 스킬이 교체되거나 버려지면 스킬 1장을 뽑습니다.
		/// 무작위 아군에게 <color=#FF4500>정의의 표식</color>을 적용합니다.
		/// English:
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// Apply <color=#FF4500>Mark of Justice</color> to a random ally.
		/// Japanese:
		/// このスキルが交換または破棄された場合、スキルを1枚引く。
		/// ランダムな味方に<color=#FF4500>正義の印</color>を適用する。
		/// Chinese:
		/// 若此技能被交换或丢弃，则抽取1张技能牌。
		/// 对随机友方施加<color=#FF4500>正义标记</color>。
		/// Chinese-TW:
		/// 若此技能被交換或丟棄，則抽取1張技能牌。
		/// 對隨機友方施加<color=#FF4500>正義標記</color>。
		/// </summary>
        public static string InTheNameOfJustice_2 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_2");
		/// <summary>
		/// Korean:
		/// 이 스킬이 교체되거나 버려지면 스킬 1장을 뽑습니다.
		/// English:
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// Japanese:
		/// このスキルが交換または破棄された場合、スキルを1枚引く。
		/// Chinese:
		/// 若此技能被交换或丢弃，则抽取1张技能牌。
		/// Chinese-TW:
		/// 若此技能被交換或丟棄，則抽取1張技能牌。
		/// </summary>
        public static string InTheNameOfJustice_3 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_3");
		/// <summary>
		/// Korean:
		/// 이 스킬이 교체되거나 버려지면 스킬 1장을 뽑습니다.
		/// 무작위 <color=#FF00FF>악당</color>에게 <color=#FF4500>정의의 표식</color>을 적용합니다.
		/// English:
		/// Draw 1 skill if this skill Exchanged or Discarded.
		/// Apply <color=#FF4500>Mark of Justice</color> to a random <color=#FF00FF>Villain</color>.
		/// Japanese:
		/// このスキルが交換または破棄された場合、スキルを1枚引く。
		/// ランダムな<color=#FF00FF>悪党</color>に<color=#FF4500>正義の印</color>を適用する。
		/// Chinese:
		/// 若此技能被交换或丢弃，则抽取1张技能牌。
		/// 对随机<color=#FF00FF>反派</color>施加<color=#FF4500>正义标记</color>。
		/// Chinese-TW:
		/// </summary>
        public static string InTheNameOfJustice_4 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("InTheNameOfJustice_4");
		/// <summary>
		/// Korean:
		/// 매 턴 시작 시, 손에 비용 0인 <color=#FF4500>정의의 이름으로</color>을 생성하고, 모든 <color=#FF00FF>적</color>에게 <color=#FF4500>정의의 표식</color>을 적용하며, <color=#FFD700>히어로 콤플렉스</color>를 얻습니다.
		/// <color=#FF4500>정의의 표식</color>을 부여하는 공격은 추가로 1스택을 적용하고 최대 스택을 1增加시킵니다.
		/// 이 장비는 저주받을 수 없습니다.
		/// <color=red>장비 가능:</color> <color=#FFA500>슈퍼 히어로</color>のみ.
		/// <color=#919191>정의 ☆가 실현되었다!
		/// 이 아이템은 클리어한 스테이지마다 공격력 +1을 얻습니다.</color>
		/// English:
		/// At the start of each turn, create a 0-Cost <color=#FF4500>In the Name of Justice</color> in hand, apply <color=#FF4500>Mark of Justice</color> to all <color=#FF00FF>Enemies</color>, and gain <color=#FFD700>Hero Complex</color>.
		/// Attacks that inflict <color=#FF4500>Mark of Justice</color> apply 1 additional stack and increase its max stack by 1.
		/// This equipment cannot be cursed.
		/// <color=red>Can only be equipped by</color> <color=#FFA500>Super Hero</color>.
		/// <color=#919191>Justice ☆ is served!
		/// This item gains +1 Attack Power for every cleared stage.</color>
		/// Japanese:
		/// 各ターン開始時、手札にコスト0の<color=#FF4500>正義の名において</color>を作成し、全ての<color=#FF00FF>敵</color>に<color=#FF4500>正義の印</color>を適用し、<color=#FFD700>ヒーローコンプレックス</color>を獲得する。
		/// <color=#FF4500>正義の印</color>を付与する攻撃は追加で1スタック適用し、最大スタックを1増加させる。
		/// この装備は呪われない。
		/// <color=red>装備可能:</color> <color=#FFA500>スーパーヒーロー</color>のみ.
		/// <color=#919191>正義☆は果たされた！
		/// このアイテムはクリアしたステージ毎に攻撃力+1を獲得する。</color>
		/// Chinese:
		/// 每回合开始时，在手牌中生成一张消耗为0的<color=#FF4500>以正义之名</color>，对所有<color=#FF00FF>敌人</color>施加<color=#FF4500>正义标记</color>，并获得<color=#FFD700>英雄情结</color>。
		/// 造成<color=#FF4500>正义标记</color>的攻击会额外施加1层并使其最大层数+1。
		/// 此装备无法被诅咒。
		/// <color=red>可装备者仅限：</color> <color=#FFA500>超级英雄</color>。
		/// <color=#919191>正义☆得到伸张！
		/// 此物品每通过一个关卡便获得+1攻击力。</color>
		/// Chinese-TW:
		/// 每回合開始時，在手牌中生成一張消耗為0的<color=#FF4500>以正義之名</color>，對所有<color=#FF00FF>敵人</color>施加<color=#FF4500>正義標記</color>，並獲得<color=#FFD700>英雄情結</color>。
		/// 造成<color=#FF4500>正義標記</color>的攻擊會額外施加1層並使其最大層數+1。
		/// 此裝備無法被詛咒。
		/// <color=red>可裝備者僅限：</color> <color=#FFA500>超級英雄</color>。
		/// <color=#919191>正義☆得到伸張！
		/// 此物品每通過一個關卡便獲得+1攻擊力。</color>
		/// </summary>
        public static string JusticeSword_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("JusticeSword_0");
		/// <summary>
		/// Korean:
		/// 매 턴 시작 시, 손에 비용 0인 <color=#FF00FF>정의의 이름으로</color>을 생성하고, 모든 <color=#FF00FF>악당</color>에게 <color=#FF4500>정의의 표식</color>을 적용하며, <color=#FFD700>히어로 콤플렉스</color>를 얻습니다.
		/// <color=#FF4500>정의의 표식</color>을 부여하는 공격은 추가로 1스택을 적용하고 최대 스택을 1增加시킵니다.
		/// 이 장비는 저주받을 수 없습니다.
		/// <color=red>장비 가능:</color> <color=#FF00FF>슈퍼 빌런</color>のみ.
		/// <color=#919191>정의 ☆가 실현되었다!
		/// 이 아이템은 클리어한 스테이지마다 공격력 +1을 얻습니다.</color>
		/// English:
		/// At the start of each turn, create a 0-Cost <color=#FF00FF>In the Name of Justice</color> in hand, apply <color=#FF4500>Mark of Justice</color> to all <color=#FF00FF>Villains</color>, and gain <color=#FFD700>Hero Complex</color>.
		/// Attacks that inflict <color=#FF4500>Mark of Justice</color> apply 1 additional stack and increase its max stack by 1.
		/// This equipment cannot be cursed.
		/// <color=red>Can only be equipped by</color> <color=#FF00FF>Super Villain</color>.
		/// <color=#919191>Justice ☆ is served!
		/// This item gains +1 Attack Power for every cleared stage.</color>
		/// Japanese:
		/// 各ターン開始時、手札にコスト0の<color=#FF00FF>正義の名において</color>を作成し、全ての<color=#FF00FF>悪党</color>に<color=#FF4500>正義の印</color>を適用し、<color=#FFD700>ヒーローコンプレックス</color>を獲得する。
		/// <color=#FF4500>正義の印</color>を付与する攻撃は追加で1スタック適用し、最大スタックを1増加させる。
		/// この装備は呪われない。
		/// <color=red>装備可能:</color> <color=#FF00FF>スーパーヴィラン</color>のみ.
		/// <color=#919191>正義☆は果たされた！
		/// このアイテムはクリアしたステージ毎に攻撃力+1を獲得する。</color>
		/// Chinese:
		/// 每回合开始时，在手牌中生成一张消耗为0的<color=#FF00FF>以正义之名</color>，对所有<color=#FF00FF>反派</color>施加<color=#FF4500>正义标记</color>，并获得<color=#FFD700>英雄情结</color>。
		/// 造成<color=#FF4500>正义标记</color>的攻击会额外施加1层并使其最大层数+1。
		/// 此装备无法被诅咒。
		/// <color=red>可装备者仅限：</color> <color=#FF00FF>超级反派</color>。
		/// <color=#919191>正义☆得到伸张！
		/// 此物品每通过一个关卡便获得+1攻击力。</color>
		/// Chinese-TW:
		/// 每回合開始時，在手牌中生成一張消耗為0的<color=#FF00FF>以正義之名</color>，對所有<color=#FF00FF>反派</color>施加<color=#FF4500>正義標記</color>，並獲得<color=#FFD700>英雄情結</color>。
		/// 造成<color=#FF4500>正義標記</color>的攻擊會額外施加1層並使其最大層數+1。
		/// 此裝備無法被詛咒。
		/// <color=red>可裝備者僅限：</color> <color=#FF00FF>超級反派</color>。
		/// <color=#919191>正義☆得到伸張！
		/// 此物品每通過一個關卡便獲得+1攻擊力。</color>
		/// </summary>
        public static string JusticeSword_1 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("JusticeSword_1");
		/// <summary>
		/// Korean:
		/// 아군을 대상으로 지정할 확률 50%를 얻습니다.
		/// English:
		/// Gain 50% chance to target allies.
		/// Japanese:
		/// 味方を対象にする確率50%を獲得する。
		/// Chinese:
		/// 获得50%几率以友方为目标。
		/// Chinese-TW:
		/// 獲得50%機率以友方為目標。
		/// </summary>
        public static string OverPowered => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("OverPowered");
		/// <summary>
		/// Korean:
		/// 버리기, 교체, 또는 오버플로로 비활성화될 수 없습니다. &a턴 후 제거됩니다.
		/// English:
		/// Cannot be disabled by discarding, exchanging, or overflow. Removed after &a turn(s).
		/// Japanese:
		/// 破棄、交換、またはオーバーフローでは無効化できない。&aターン後に解除される。
		/// Chinese:
		/// 无法通过丢弃、交换或溢出被禁用。&a回合后移除。
		/// Chinese-TW:
		/// 無法通過丟棄、交換或溢出被禁用。&a回合後移除。
		/// </summary>
        public static string SuperHero_Stun => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("SuperHero_Stun");
		/// <summary>
		/// Korean:
		/// &a턴 후 제거됩니다.
		/// English:
		/// Removed after &a turn.
		/// Japanese:
		/// &aターン後に解除される。
		/// Chinese:
		/// &a回合后移除。
		/// Chinese-TW:
		/// &a回合後移除。
		/// </summary>
        public static string SuperHero_Stun_Enemy => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("SuperHero_Stun_Enemy");
		/// <summary>
		/// Korean:
		/// 무작위 아군에게 <color=#FFA500>히어로의 존재감</color>를 적용합니다.
		/// English:
		/// Apply <color=#FFA500>Hero Presence</color> to a random ally.
		/// Japanese:
		/// ランダムな味方に<color=#FFA500>ヒーローの存在感</color>を適用する。
		/// Chinese:
		/// 对随机友方施加<color=#FFA500>英雄气场</color>。
		/// Chinese-TW:
		/// 對隨機友方施加<color=#FFA500>英雄氣場</color>。
		/// </summary>
        public static string Unwanted_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("Unwanted_0");
		/// <summary>
		/// Korean:
		/// 모든 아군에게 <color=#50C878>히어로의 스포트라이트</color>를 적용합니다.
		/// English:
		/// Apply <color=#50C878>Hero's Spotlight</color> to all allies.
		/// Japanese:
		/// 味方全体に<color=#50C878>ヒーローのスポットライト</color>を適用する。
		/// Chinese:
		/// 对所有友方施加<color=#50C878>英雄的聚光灯</color>。
		/// Chinese-TW:
		/// 對所有友方施加<color=#50C878>英雄的聚光燈</color>。
		/// </summary>
        public static string World_0 => ModManager.getModInfo("SuperHero").localizationInfo.SystemLocalizationUpdate("World_0");

    }
}