using ChronoArkMod;
namespace MiyukiSone
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Miyuki's Suffocation
		/// </summary>
        public static string Buff_B_Miyuki_Buff_Ally = "B_Miyuki_Buff_Ally";
        public static string Buff_B_Miyuki_CloseRangeShot = "B_Miyuki_CloseRangeShot";
		/// <summary>
		/// Debuff Ally
		/// </summary>
        public static string Buff_B_Miyuki_Debuff_Ally = "B_Miyuki_Debuff_Ally";
		/// <summary>
		/// Miyuki's Extra Action
		/// Gain 1 action count.
		/// </summary>
        public static string Buff_B_Miyuki_Enemy_ExtraAction = "B_Miyuki_Enemy_ExtraAction";
		/// <summary>
		/// Miyuki's Might
		/// </summary>
        public static string Buff_B_Miyuki_Might = "B_Miyuki_Might";
		/// <summary>
		/// Miyuki's Affection
		/// Avaliable Lucy draw skills:
		/// You can click on this it to toggle draw.
		/// </summary>
        public static string Buff_B_Miyuki_Passive = "B_Miyuki_Passive";
		/// <summary>
		/// Recovery
		/// </summary>
        public static string Buff_B_Miyuki_Recover = "B_Miyuki_Recover";
		/// <summary>
		/// When played from hand, view 5 random <color=#F53172FF>Heroic</color> equipment and select 1 to obtain. Then replace this skill with <color=red>Yabeley's Tomato Juice</color>.
		/// <sprite name="비용1"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_Miyuki_Ex_0 = "Ex_Miyuki_Ex_0";
		/// <summary>
		/// Glitch
		/// </summary>
        public static string SkillExtended_Ex_Miyuki_Glitch = "Ex_Miyuki_Glitch";
		/// <summary>
		/// Fourth Wall Breaker
		/// Remove stats cap.
		/// At the start of the turn, remove all curses from the deck.
		/// If the wearer  is Miyuki, the 'Miyuki's Affection' Lucy draw skill cost 1 less and gain 'Swiftness'.
		/// </summary>
        public static string Item_Equip_E_Miyuki_WallBreaker = "E_Miyuki_WallBreaker";
		/// <summary>
		/// Reality Warping
		/// </summary>
        public static string SkillKeyword_KeyWord_RealityWarping = "KeyWord_RealityWarping";
		/// <summary>
		/// Miyuki
		/// Passive:
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_Miyuki = "Miyuki";
		/// <summary>
		/// Scenario Editor
		/// Expand your Relic inventory by 1.
		/// At the start of turn, draw 2 more skills.
		/// Investigators can only have up to &a of the same skills. &b minimum skills on allies.
		/// Obtain 2 'Infinite Skill Book'. Remove deck shuffle, rare skills limit and mana upgrade restrictions.
		/// </summary>
        public static string Item_Passive_Re_Miyuki_ScenarioEditor = "Re_Miyuki_ScenarioEditor";
        public static string SkillEffect_SE_S_S_Miyuki_Rare_FinalView = "SE_S_S_Miyuki_Rare_FinalView";
        public static string SkillEffect_SE_S_S_Miyuki_Rare_FinalView_Particle = "SE_S_S_Miyuki_Rare_FinalView_Particle";
        public static string SkillEffect_SE_S_S_Special_EternalKiss = "SE_S_S_Special_EternalKiss";
        public static string SkillEffect_SE_T_S_EternalPromise = "SE_T_S_EternalPromise";
        public static string SkillEffect_SE_T_S_EternalVow = "SE_T_S_EternalVow";
        public static string SkillEffect_SE_T_S_GracefulSwing = "SE_T_S_GracefulSwing";
        public static string SkillEffect_SE_T_S_HappyBirthday = "SE_T_S_HappyBirthday";
        public static string SkillEffect_SE_T_S_Heal = "SE_T_S_Heal";
        public static string SkillEffect_SE_T_S_MeasuredLove = "SE_T_S_MeasuredLove";
        public static string SkillEffect_SE_T_S_Miyuki_Rare_FinalView = "SE_T_S_Miyuki_Rare_FinalView";
        public static string SkillEffect_SE_T_S_Miyuki_Rare_FinalView_Particle = "SE_T_S_Miyuki_Rare_FinalView_Particle";
        public static string SkillEffect_SE_T_S_Miyuki_Special_Close = "SE_T_S_Miyuki_Special_Close";
        public static string SkillEffect_SE_T_S_Pandemonium = "SE_T_S_Pandemonium";
        public static string SkillEffect_SE_T_S_QueenBee = "SE_T_S_QueenBee";
        public static string SkillEffect_SE_T_S_Rare_JustforYOU = "SE_T_S_Rare_JustforYOU";
        public static string SkillEffect_SE_T_S_Special_EternalKiss = "SE_T_S_Special_EternalKiss";
        public static string SkillEffect_SE_T_S_StepToward = "SE_T_S_StepToward";
        public static string SkillEffect_SE_T_S_SweetRestraint = "SE_T_S_SweetRestraint";
        public static string SkillEffect_SE_T_S_WarningStrike = "SE_T_S_WarningStrike";
		/// <summary>
		/// Eternal Promise
		/// This skill costs 4 more while it is in the deck.
		/// If cast on an ally at Death's Door, do not apply Healing Circle and immediately heal for &a <color=#FF7C34>(180% Healing Power)</color>.
		/// If this skill is adjacent to 'Eternal Vow', exclude this skill and 'Eternal Vow', then create 'Eternal Kiss' in hand.
		/// </summary>
        public static string Skill_S_Miyuki_EternalPromise = "S_Miyuki_EternalPromise";
		/// <summary>
		/// Eternal Vow
		/// <color=#FFA636FF>Burnnn!: All enemy Pain and Weakening debuffs remain an extra turn.</color>
		/// This skill costs 3 more while it is in the deck or discard pile.
		/// If this skill is adjacent to 'Eternal Promise', exclude this skill and 'Eternal Promise', then create 'Eternal Kiss' in hand.
		/// </summary>
        public static string Skill_S_Miyuki_EternalVow = "S_Miyuki_EternalVow";
		/// <summary>
		/// Glitching Phone
		/// Refresh and gain a random fixed ability.
		/// </summary>
        public static string Skill_S_Miyuki_GlitchingPhone = "S_Miyuki_GlitchingPhone";
		/// <summary>
		/// Graceful Swing
		/// Gain Ignore Taunt and guaranteed Critical against targets who have a Weakening debuff or is at 50% HP or below.
		/// <b>Prophecy : Deal &a </b><color=#FF7C34>(150% Attack Power)</color><b> additional damage.</b>
		/// </summary>
        public static string Skill_S_Miyuki_GracefulSwing = "S_Miyuki_GracefulSwing";
		/// <summary>
		/// Happy Birthday to YOU
		/// This skill always leaves a Mark of Silverstein if played from hand.
		/// Deal &a <color=#FF7C34>(140% Attack Power)</color> additional damage if an ally is <b>stunned</b> or the target is <sprite=2>debuffed.
		/// Sheathe: Cast this skill.
		/// </summary>
        public static string Skill_S_Miyuki_HappyBirthday = "S_Miyuki_HappyBirthday";
		/// <summary>
		/// Fractured Illusion
		/// Draw 2 skills and apply Illusion Sword buff onto them.
		/// </summary>
        public static string Skill_S_Miyuki_LucyDraw_FracturedIllusion = "S_Miyuki_LucyDraw_FracturedIllusion";
		/// <summary>
		/// Helping Hand
		/// View allies' unique draw skills and select one to create in hand. The created skill gains Exclude.
		/// </summary>
        public static string Skill_S_Miyuki_LucyDraw_HelpingHand = "S_Miyuki_LucyDraw_HelpingHand";
		/// <summary>
		/// Miyuki, Help
		/// Choose One -
		/// Reduce 'Inner Desire' damage by 15 and draw 2 skills.
		/// Or increase 'Inner Desire' damage by 12 and draw 3 skills.
		/// </summary>
        public static string Skill_S_Miyuki_LucyDraw_MiyukiHelp = "S_Miyuki_LucyDraw_MiyukiHelp";
		/// <summary>
		/// Miyuki's Phone
		/// Create random Lucy rare skills until your hand is full. These skills gain Exclude.
		/// Cost is reduced by 1 at the end of the turn.
		/// </summary>
        public static string Skill_S_Miyuki_LucyDraw_MiyukiPhone = "S_Miyuki_LucyDraw_MiyukiPhone";
		/// <summary>
		/// Measured Love
		/// Select an ally.
		/// Deal <color=purple>Pain damage</color> equal 50% of the ally max hp.
		/// View 3 random usable skills of that ally and select one to create in hand. The skill gains Countdown 2 and Exclude.
		/// The selected ally is allowed to Parry once.
		/// </summary>
        public static string Skill_S_Miyuki_MeasuredLove = "S_Miyuki_MeasuredLove";
		/// <summary>
		/// Pandemonium
		/// Can only target characters with a Weakening debuff.
		/// This skill always lands against targets with a Mark of Silverstein.
		/// This skill casts twice if it has an Illusion Sword buff.
		/// </summary>
        public static string Skill_S_Miyuki_Pandemonium = "S_Miyuki_Pandemonium";
		/// <summary>
		/// Normal Affection
		/// At the start of the turn, draw the normal amount of skills.
		/// </summary>
        public static string Skill_S_Miyuki_Passive_0 = "S_Miyuki_Passive_0";
		/// <summary>
		/// Overflowing Affection
		/// At the start of turn, draw 1 less skill and create a random Lucy draw skill in hand, based on Miyuki's current skills in the deck. The created skill gains Exclude and costs 1 mana.
		/// </summary>
        public static string Skill_S_Miyuki_Passive_1 = "S_Miyuki_Passive_1";
		/// <summary>
		/// Queen Bee
		/// If Eve has 2 stacks, consume 1 stack to restore &a <color=#FF7C34>(40% Healing Power)</color> more health.
		/// </summary>
        public static string Skill_S_Miyuki_QueenBee = "S_Miyuki_QueenBee";
		/// <summary>
		/// Final View
		/// Ignores armor.
		/// Recast this skill 2 times.
		/// Current Striking Ability is &a.
		/// Whenever this skill kills an ally or brings an ally to Death's Door, permanently increase skill's damage by &b for <b>this run</b>.
		/// Can be used outside of battle.
		/// This effect cannot be reactivated until you clear 1 stage.
		/// Current Status: &c.
		/// </summary>
        public static string Skill_S_Miyuki_Rare_FinalView = "S_Miyuki_Rare_FinalView";
        public static string Skill_S_Miyuki_Rare_FinalView_0 = "S_Miyuki_Rare_FinalView_0";
		/// <summary>
		/// Final View
		/// Ignores Armor.
		/// </summary>
        public static string Skill_S_Miyuki_Rare_FinalView_Particle = "S_Miyuki_Rare_FinalView_Particle";
		/// <summary>
		/// Game Update
		/// Can only be used once per run.
		/// Can be used outside of battle.
		/// Cost is decreased by 1 per stage cleared.
		/// Restart current stage.
		/// </summary>
        public static string Skill_S_Miyuki_Rare_GameUpdate = "S_Miyuki_Rare_GameUpdate";
        public static string Skill_S_Miyuki_Rare_GameUpdate_0 = "S_Miyuki_Rare_GameUpdate_0";
        public static string Skill_S_Miyuki_Rare_GameUpdate_1 = "S_Miyuki_Rare_GameUpdate_1";
		/// <summary>
		/// Just for YOU
		/// Remove one random debuff.
		/// </summary>
        public static string Skill_S_Miyuki_Rare_JustforYOU = "S_Miyuki_Rare_JustforYOU";
		/// <summary>
		/// Miyuki's Close-Range Shot
		/// You can recast this skill whenever you play a skill that costs 1 or more.
		/// <b>(Except skills created by other characters)</b>
		/// This skill does not stack Supply Arrows buff if it is used for free.
		/// </summary>
        public static string Skill_S_Miyuki_Special_Close = "S_Miyuki_Special_Close";
		/// <summary>
		/// Eternal Kiss
		/// Heal all characters except yourself and the target for &a <color=#FF7C34>(99% Healing Power)</color>.
		/// Convert all ally barriers into party barrier.
		/// Draw 1 skill.
		/// </summary>
        public static string Skill_S_Miyuki_Special_EternalKiss = "S_Miyuki_Special_EternalKiss";
		/// <summary>
		/// Miyuki's Joker
		/// Draw a skill from the deck.
		/// When this skill is cast or discarded, all allies with <b>Bleeding or Stunned</b> take 15 damage.
		/// <color=#919191>How did this get here...</color>
		/// </summary>
        public static string Skill_S_Miyuki_Special_Joker = "S_Miyuki_Special_Joker";
        public static string Skill_S_Miyuki_Special_KillAlly = "S_Miyuki_Special_KillAlly";
        public static string Skill_S_Miyuki_Special_KillEnemy = "S_Miyuki_Special_KillEnemy";
		/// <summary>
		/// Miyuki's Might
		/// Choose one of Phoenix's abilities to cast.
		/// List of usable abilities a&
		/// </summary>
        public static string Skill_S_Miyuki_Special_Might = "S_Miyuki_Special_Might";
		/// <summary>
		/// Sacrificed Knowledge
		/// Cannot be used.
		/// </summary>
        public static string Skill_S_Miyuki_Special_SacrificedKnowledge = "S_Miyuki_Special_SacrificedKnowledge";
		/// <summary>
		/// Yabeley's Tomato Juice
		/// Cannot be used.
		/// </summary>
        public static string Skill_S_Miyuki_Special_Yabeley = "S_Miyuki_Special_Yabeley";
		/// <summary>
		/// A Step Toward YOU
		/// Healing is increased by &a <color=#FF7C34>(18% Healing Power)</color> for each skill in hand not including this skill.
		/// Skills above and below this skill gain an Illusion Sword buff.
		/// </summary>
        public static string Skill_S_Miyuki_StepToward = "S_Miyuki_StepToward";
		/// <summary>
		/// Sweet Restraint
		/// Deal <color=purple>&a Pain damage</color> <color=#FF7C34>(50% Healing Power)</color> to the target.
		/// Heal all other allies for &b <color=#FF7C34>(80% Healing Power)</color>.
		/// </summary>
        public static string Skill_S_Miyuki_SweetRestraint = "S_Miyuki_SweetRestraint";
		/// <summary>
		/// Warning Strike
		/// Reduce damage of one 'Inner Desire' by &a <color=#FF7C34>(100% Healing Power)</color>.
		/// If the target has 'Identified!' debuff, apply 'Receive 40% increased damage on the next attack' (130%<sprite=0>) debuff on the target.
		/// </summary>
        public static string Skill_S_Miyuki_WarningStrike = "S_Miyuki_WarningStrike";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// 턴 시작 시, 스킬 1장 덜 드로우하고 무작위 루시 드로우 스킬을 손에 생성합니다.
		/// English:
		/// At the start of turn, draw 1 less skill and create a random Lucy draw skill in hand.
		/// Japanese:
		/// ターン開始時、スキルを1枚少なくドローし、ランダムなルーシードロースキルを手札に生成します。
		/// Chinese:
		/// 回合开始时，少抽1张技能，并将1张随机的「露西抽牌技能」置入手牌。
		/// Chinese-TW:
		/// 回合開始時，少抽1張技能，並將1張隨機的「露西抽牌技能」置入手牌。
		/// </summary>
        public static string AffectionOverflow_0 => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("AffectionOverflow_0");
		/// <summary>
		/// Korean:
		/// 턴 시작 시, 일반적인 양의 스킬을 드로우합니다.
		/// English:
		/// At the start of the turn, draw the normal amount of skills.
		/// Japanese:
		/// ターン開始時、通常の枚数のスキルをドローします。
		/// Chinese:
		/// 回合开始时，正常抽取技能。
		/// Chinese-TW:
		/// 回合開始時，正常抽取技能。
		/// </summary>
        public static string AffectionOverflow_1 => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("AffectionOverflow_1");
		/// <summary>
		/// Korean:
		/// 둘이만 있었으면 좋겠어.
		/// English:
		/// I want it to be just the two of us.
		/// Japanese:
		/// 二人きりになりたいな。
		/// Chinese:
		/// 我只想我们两个人独处。
		/// Chinese-TW:
		/// 我只想我們兩個人獨處。
		/// </summary>
        public static string FinalView => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("FinalView");
		/// <summary>
		/// Korean:
		/// 이 스테이지를 다시 시작할래?
		/// English:
		/// Do YOU want restart the stage ?
		/// Japanese:
		/// このステージを再開する？
		/// Chinese:
		/// 你想重新开始这一关吗？
		/// Chinese-TW:
		/// 你想重新開始這一關嗎？
		/// </summary>
        public static string GameUpdate => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("GameUpdate");
		/// <summary>
		/// Korean:
		/// 현재 애정 상태: &a.
		/// English:
		/// Current Affection: &a.
		/// Japanese:
		/// 現在の愛情状態: &a。
		/// Chinese:
		/// 当前好感度：&a
		/// Chinese-TW:
		/// 目前好感度：&a
		/// </summary>
        public static string MiyukiAffection => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("MiyukiAffection");
		/// <summary>
		/// Korean:
		/// 약화 효과를 선택하세요.
		/// English:
		/// Select a downgrade effect.
		/// Japanese:
		/// 弱体化効果を選択してください。
		/// Chinese:
		/// 选择一个负面效果。
		/// Chinese-TW:
		/// 選擇一個負面效果。
		/// </summary>
        public static string MiyukiDowngrade => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("MiyukiDowngrade");
		/// <summary>
		/// Korean:
		/// 미유키의
		/// English:
		/// Miyuki's
		/// Japanese:
		/// ミユキの
		/// Chinese:
		/// 美雪的
		/// Chinese-TW:
		/// 美雪的
		/// </summary>
        public static string MiyukiName => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("MiyukiName");
		/// <summary>
		/// Korean:
		/// 강화 효과를 선택하세요.
		/// English:
		/// Select an upgrade effect.
		/// Japanese:
		/// 強化効果を選択してください。
		/// Chinese:
		/// 选择一个升级效果。
		/// Chinese-TW:
		/// 選擇一個升級效果。
		/// </summary>
        public static string MiyukiUpgrade => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("MiyukiUpgrade");
		/// <summary>
		/// Korean:
		/// 왜 나를 그렇게 싫어하는 거야?
		/// English:
		/// Why YOU hate Me so much?
		/// Japanese:
		/// どうしてそんなに私を嫌うの？
		/// Chinese:
		/// 你为什么就这么讨厌我呢？
		/// Chinese-TW:
		/// 你為什麼就這麼討厭我呢？
		/// </summary>
        public static string RandomAllyKill => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("RandomAllyKill");
		/// <summary>
		/// Korean:
		/// 내가 당신을 그렇게 사랑한단 말이야!
		/// English:
		/// I love YOU that much!
		/// Japanese:
		/// 私はあなたをこんなに愛しているのに！
		/// Chinese:
		/// 我就有这么爱你！
		/// Chinese-TW:
		/// 我就有這麼愛你！
		/// </summary>
        public static string RandomEnemyKill => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("RandomEnemyKill");
		/// <summary>
		/// Korean:
		/// 이 스킬이 덱에 있을 때, 미유키의 애정에 &a개의 루시 드로우 스킬을 추가합니다.
		/// English:
		/// When this skill in the deck, add &a Lucy draw skill to Miyuki's Affection.
		/// Japanese:
		/// このスキルがデッキにある時、ミユキの愛情に&a個のルーシードロースキルを追加します。
		/// Chinese:
		/// 当此技能在牌库中时，为美雪的好感度增加 &a 张「露西抽牌技能」。
		/// Chinese-TW:
		/// 當此技能在牌組中時，為美雪的好感度增加 &a 張「露西抽牌技能」。
		/// </summary>
        public static string RealityWarpingDesc => ModManager.getModInfo("MiyukiSone").localizationInfo.SystemLocalizationUpdate("RealityWarpingDesc");

    }
}