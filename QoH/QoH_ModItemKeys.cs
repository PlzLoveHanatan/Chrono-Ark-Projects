using ChronoArkMod;
namespace QoH
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Affection and Hatred ♡
		/// When this unit dies, add 'Sparkling Justice Shot ♡' to your hand. When attacked by <color=#FF7C34>&c</color>heal the ally with the lowest health for &a <color=#FF7C34>(&b% &cAttack Power)</color>. Only activates once per turn. Current status: &d.
		/// </summary>
        public static string Buff_B_QoH_Affection = "B_QoH_Affection";
		/// <summary>
		/// Arcana Slave ☆
		/// When attacking, apply 'Shattered Heartbeat ☆ (<sprite=1> &f%) to the target's, restore &a health <color=#FF7C34>(&b% &cHealing Power)</color> and remove 1 stack.
		/// </summary>
        public static string Buff_B_QoH_ArcanaSlave = "B_QoH_ArcanaSlave";
		/// <summary>
		/// Magical Girl's Chant ☆
		/// When this buff ends, heal an ally with lowest health by &a <color=#FF7C34>(&b% &cAttack Power)</color>.
		/// </summary>
        public static string Buff_B_QoH_Chant = "B_QoH_Chant";
		/// <summary>
		/// Love/Hate ☆
		/// </summary>
        public static string Buff_B_QoH_LoveHate = "B_QoH_LoveHate";
		/// <summary>
		/// Love/Hate ☆
		/// </summary>
        public static string Buff_B_QoH_LoveHate_0 = "B_QoH_LoveHate_0";
		/// <summary>
		/// Love Justice ☆
		/// Restore &a health <color=#FF7C34>(&b% &cHealing Power)</color> and remove this buff, when taking damage.
		/// </summary>
        public static string Buff_B_QoH_LoveJustice = "B_QoH_LoveJustice";
		/// <summary>
		/// Magical Candy
		/// </summary>
        public static string Buff_B_QoH_MagicalCandy = "B_QoH_MagicalCandy";
		/// <summary>
		/// Here Comes Magical Girl ☆
		/// At the start of the turn, apply <color=#BD2DC2>Mark of Villainy</color> to a random enemy.
		/// </summary>
        public static string Buff_B_QoH_MagicalGirl = "B_QoH_MagicalGirl";
		/// <summary>
		/// <color=#BD2DC2>Mark of Villainy</color>
		/// Removed at the start of the next turn.
		/// Can be targeted regardless Taunt status.
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
		/// When this unit dies, random unit on this character's side takes <color=purple>Pain damage</color> equal to the half total damage of the debuff over X turns.
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
		/// Pain debuffs applied onto enemies by allies are extended by 1 turn. Each stack of pain debuff deal additonal <color=purple>2 Pain Damage</color>.
		/// </summary>
        public static string Buff_B_QoH_WhatUse = "B_QoH_WhatUse";
        public static string Buff_B_QoH_WhatUse_0 = "B_QoH_WhatUse_0";
		/// <summary>
		/// XVIII - Sun and Moon
		/// Ignores Emotion Level cap.
		/// At the start of the turn gain 3 <color=red>Negative</color> Points.
		/// </summary>
        public static string Buff_B_R_QoH_SunMoon = "B_R_QoH_SunMoon";
		/// <summary>
		/// When played, heal the ally with the lowest health (75% of Queen of Hatred Healing Power).
		/// Queen of Hatred can change Sanity unlimited times this turn.
		/// <sprite name="비용1"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_QoH_0 = "Ex_QoH_0";
		/// <summary>
		/// All enemies take <color=purple>X Pain Damage</color> equal to the total damage of the Pain debuffs on them over X turns * 2, then destroy all enemies Pain debuffs.
		/// <sprite name="비용1"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_QoH_1 = "Ex_QoH_1";
		/// <summary>
		/// Hysteria
		/// </summary>
        public static string SkillExtended_Ex_QoH_Hysteria = "Ex_QoH_Hysteria";
		/// <summary>
		/// Spent Use, Forming Hate
		/// For every stack of <sprite=1> Pain debuff applied onto an enemy, gain 40% chance to deal <color=purple>X Pain damage</color> equal to the total damage of the debuff over X turns * 2, then destroy the debuff.
		/// </summary>
        public static string Item_Equip_E_QoH_FormingHate = "E_QoH_FormingHate";
		/// <summary>
		/// Magical Girl's Lovely Gift
		/// At the start of the turn apply Love/Hate ☆ to all allies.
		/// </summary>
        public static string Item_Equip_E_QoH_LovelyGift = "E_QoH_LovelyGift";
		/// <summary>
		/// Boobs
		/// </summary>
        public static string SkillKeyword_KeyWord_Boobs = "KeyWord_Boobs";
		/// <summary>
		/// Queen of Hatred
		/// Passive:
		/// At the start of the turn, apply <color=#BD2DC2>Mark of Villainy</color> to a random enemy and gain <color=#FF77FF>Pure Heart ☆</color>
		/// Can change Sanity once per turn (twice at Level 4).
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_QoH = "QoH";
		/// <summary>
		/// XVII — Star
		/// <color=#C067EF>Emotion Burst</color>: At the start of the 6th turn, view remaining Level III Abnormalities and select 1 to apply to an ally. This effect activates only once per battle.
		/// </summary>
        public static string Item_Passive_R_QoH_Star = "R_QoH_Star";
		/// <summary>
		/// XVIII - Sun and Moon
		/// Increase all enemies health by 25%. Enemies ignores Emotion Level cap.
		/// Gain 1 'Dummy Data', 1 'Transcendent Tome' and 1 'Golden Bread'. Expand your Relic inventory by 2.
		/// </summary>
        public static string Item_Passive_R_QoH_SunMoon = "R_QoH_SunMoon";
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
        public static string SkillEffect_SE_Tick_B_QoH_LoveHate = "SE_Tick_B_QoH_LoveHate";
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
        public static string SkillEffect_SE_T_S_QoH_Test = "SE_T_S_QoH_Test";
		/// <summary>
		/// Blessing of Pure Heart ♡
		/// Cast this skill on the target and the ally on their right-side.
		/// </summary>
        public static string Skill_S_QoH_Blessing = "S_QoH_Blessing";
		/// <summary>
		/// Pink Star Blessing ♡
		/// Repeatedly heal an ally with lowest health by &a <color=#FF7C34>(40% Healing Power)</color> for each stack of <sprite=1> debuff on a target's.
		/// </summary>
        public static string Skill_S_QoH_Embrace = "S_QoH_Embrace";
		/// <summary>
		/// Sweet Peach Shot ♡
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
		/// </summary>
        public static string Skill_S_QoH_Justice = "S_QoH_Justice";
		/// <summary>
		/// Love Attack ♡
		/// Heal all allies by &a <color=#FF7C34>(50% &b Healing Power)</color> and draw 2 skills.
		/// Queen of Hatred can change Sanity unlimited times this turn.
		/// If Queen of Hatred is fainted, do not heal and draw 2 skills.
		/// </summary>
        public static string Skill_S_QoH_Lucy = "S_QoH_Lucy";
		/// <summary>
		/// Pure Heart Resonance ♡
		/// Heal all allies and draw 2 skills.
		/// </summary>
        public static string Skill_S_QoH_Lucy_0 = "S_QoH_Lucy_0";
		/// <summary>
		/// Justice Rhythm ☆
		/// Extend all enemies Pain debuffs by 1 turn and draw 3 skills.
		/// </summary>
        public static string Skill_S_QoH_Lucy_1 = "S_QoH_Lucy_1";
		/// <summary>
		/// Harmony Burst ♡
		/// If the target has a Pain debuff, heal an ally with lowest health by &a <color=#FF7C34>(50% Attack Power)</color> and apply 1 stack of this skill's debuffs to all enemies.
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
		/// Promise of Justice ♡
		/// </summary>
        public static string Skill_S_QoH_Spiral = "S_QoH_Spiral";
		/// <summary>
		/// Test
		/// </summary>
        public static string Skill_S_QoH_Test = "S_QoH_Test";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// <color=#FF77FF>순수한 마음 ☆</color>을 획득합니다
		/// English:
		/// Gain <color=#FF77FF>Pure Heart ☆</color>
		/// Japanese:
		/// <color=#FF77FF>ピュアハート ☆</color>を獲得する
		/// Chinese:
		/// 取回了<color=#FF77FF>纯净之心☆</color>
		/// Chinese-TW:
		/// 取回了<color=#FF77FF>純淨之心☆</color>
		/// </summary>
        public static string QoH_Sanity_H => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_H");
		/// <summary>
		/// Korean:
		/// 지금 <color=#BD2DC2>히스테리아!</color>를 획득합니다!
		/// English:
		/// Gain <color=#BD2DC2>Hysteria!</color> now!
		/// Japanese:
		/// 今すぐ <color=#BD2DC2>ヒステリア！</color>を獲得する！
		/// Chinese:
		/// 变得<color=#BD2DC2>歇斯底里</color>了！
		/// Chinese-TW:
		/// 變得<color=#BD2DC2>歇斯底里</color>了！
		/// </summary>
        public static string QoH_Sanity_M => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_M");
		/// <summary>
		/// Korean:
		/// <color=#BD2DC2>히스테리아!</color>
		/// English:
		/// <color=#BD2DC2>Hysteria!</color>
		/// Japanese:
		/// <color=#BD2DC2>ヒステリア！</color>
		/// Chinese:
		/// <color=#BD2DC2>歇斯底里</color>
		/// Chinese-TW:
		/// <color=#BD2DC2>歇斯底里</color>
		/// </summary>
        public static string QoH_Sanity_Mod_H => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_H");
		/// <summary>
		/// Korean:
		/// 오직 공격 스킬만 사용할 수 있습니다.
		/// 오직 부정 포인트만 획득할 수 있습니다.
		/// 회복되면 <color=#FF77FF>순수한 마음 ☆</color>을 획득합니다.
		/// English:
		/// Can only use Attack skills.
		/// Can only gain <color=red>Negative</color> Points. Gain <color=#FF77FF>Pure Heart ☆</color> when healed.
		/// Japanese:
		/// 攻撃スキルしか使用できない。
		/// ネガティブポイントしか獲得できない。
		/// 回復されると <color=#FF77FF>ピュアハート ☆</color> を獲得する。
		/// Chinese:
		/// 只能使用攻击技能。
		/// 只能获得<color=red>负面的</color>情感点数。
		/// 受到治疗时，获得<color=#FF77FF>纯净之心☆</color>。
		/// Chinese-TW:
		/// 只能使用攻擊技能。
		/// 只能獲得<color=red>負面的</color>情感點數。
		/// 受到治療時，獲得<color=#FF77FF>純淨之心☆</color>。
		/// </summary>
        public static string QoH_Sanity_Mod_H_Desc => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_H_Desc");
		/// <summary>
		/// Korean:
		/// <color=#FF77FF>순수한 마음 ☆</color>
		/// English:
		/// <color=#FF77FF>Pure Heart ☆</color>
		/// Japanese:
		/// <color=#FF77FF>ピュアハート ☆</color>
		/// Chinese:
		/// <color=#FF77FF>纯净之心☆</color>
		/// Chinese-TW:
		/// <color=#FF77FF>純淨之心☆</color>
		/// </summary>
        public static string QoH_Sanity_Mod_M => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_M");
		/// <summary>
		/// Korean:
		/// 오직 회복 스킬만 사용할 수 있습니다.
		/// 오직 긍정 포인트만 획득할 수 있습니다.
		/// 피해를 받으면 <color=#BD2DC2>히스테리아!</color>를 획득합니다.
		/// English:
		/// Can only use Healing skills.
		/// Can only gain <color=green>Positive</color> Points. Gain <color=#BD2DC2>Hysteria!</color> when taking damage.
		/// Japanese:
		/// 回復スキルしか使用できない。
		/// ポジティブポイントしか獲得できない。
		/// ダメージを受けると <color=#BD2DC2>ヒステリア！</color> を獲得する。
		/// Chinese:
		/// 只能使用治疗技能。
		/// 只能获得<color=green>正面的</color>情感点数。
		/// 受到伤害时，会变得<color=#BD2DC2>歇斯底里</color>。
		/// Chinese-TW:
		/// 只能使用治療技能。
		/// 只能獲得<color=green>正面的</color>情感點數。
		/// 受到傷害時，會變得<color=#BD2DC2>歇斯底里</color>。
		/// </summary>
        public static string QoH_Sanity_Mod_M_Desc => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Mod_M_Desc");
		/// <summary>
		/// Korean:
		/// 사라져…! 아, 으응…
		/// English:
		/// Disappear..! Agh, nngh...
		/// Japanese:
		/// 消えて…！あっ、んんっ…
		/// Chinese:
		/// 消失吧……！哈、哈嗯……
		/// Chinese-TW:
		/// 消失吧……！哈、哈嗯……
		/// </summary>
        public static string QoH_Sanity_Text_H => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Text_H");
		/// <summary>
		/// Korean:
		/// …흠? 무슨 일이 있었나?
		/// English:
		/// ..Hoh? Hath aught transpired?
		/// Japanese:
		/// …ほう？何かあったのか？
		/// Chinese:
		/// ……咦？刚刚发生什么了？
		/// Chinese-TW:
		/// ……咦？剛剛發生什麼了？
		/// </summary>
        public static string QoH_Sanity_Text_M => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_Text_M");
		/// <summary>
		/// Korean:
		/// <color=#919191>매 턴마다 이성 상태를 한 번만 전환할 수 있습니다</color>
		/// English:
		/// <color=#919191>Can switch Sanity only once per turn</color>
		/// Japanese:
		/// <color=#919191>1ターンにつき1回のみ正気度を切り替えることができる</color>
		/// Chinese:
		/// <color=#919191>每回合仅能切换 1 次理智</color>
		/// Chinese-TW:
		/// <color=#919191>每回合僅能切換 1 次理智</color>
		/// </summary>
        public static string QoH_Sanity_TurnCap => ModManager.getModInfo("QoH").localizationInfo.SystemLocalizationUpdate("QoH_Sanity_TurnCap");

    }
}