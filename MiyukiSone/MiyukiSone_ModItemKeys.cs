using ChronoArkMod;
namespace MiyukiSone
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Miyuki Buff
		/// </summary>
        public static string Buff_B_Miyuki_Buff = "B_Miyuki_Buff";
		/// <summary>
		/// Buff Enemy
		/// Gain 1 action count. Removed at the start of the next turn.
		/// </summary>
        public static string Buff_B_Miyuki_Buff_Enemy = "B_Miyuki_Buff_Enemy";
        public static string Buff_B_Miyuki_CloseRangeShot = "B_Miyuki_CloseRangeShot";
		/// <summary>
		/// Debuff Ally
		/// </summary>
        public static string Buff_B_Miyuki_Debuff_Ally = "B_Miyuki_Debuff_Ally";
		/// <summary>
		/// Miyuki's Might
		/// </summary>
        public static string Buff_B_Miyuki_Might = "B_Miyuki_Might";
		/// <summary>
		/// =^_^=
		/// Click on buff for funny.
		/// </summary>
        public static string Buff_B_Miyuki_Passive = "B_Miyuki_Passive";
		/// <summary>
		/// Recovery
		/// </summary>
        public static string Buff_B_Miyuki_Recover = "B_Miyuki_Recover";
		/// <summary>
		/// Glitch
		/// </summary>
        public static string SkillExtended_Ex_Miyuki_Glitch = "Ex_Miyuki_Glitch";
		/// <summary>
		/// Miyuki
		/// Passive:
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_Miyuki = "Miyuki";
        public static string SkillEffect_SE_S_S_Special_EternalKiss = "SE_S_S_Special_EternalKiss";
        public static string SkillEffect_SE_T_S_EternalPromise = "SE_T_S_EternalPromise";
        public static string SkillEffect_SE_T_S_EternalVow = "SE_T_S_EternalVow";
        public static string SkillEffect_SE_T_S_GracefulSwing = "SE_T_S_GracefulSwing";
        public static string SkillEffect_SE_T_S_HappyBirthday = "SE_T_S_HappyBirthday";
        public static string SkillEffect_SE_T_S_Heal = "SE_T_S_Heal";
        public static string SkillEffect_SE_T_S_MeasuredLove = "SE_T_S_MeasuredLove";
        public static string SkillEffect_SE_T_S_Pandemonium = "SE_T_S_Pandemonium";
        public static string SkillEffect_SE_T_S_QueenBee = "SE_T_S_QueenBee";
        public static string SkillEffect_SE_T_S_Rare_JustforYOU = "SE_T_S_Rare_JustforYOU";
        public static string SkillEffect_SE_T_S_Special_EternalKiss = "SE_T_S_Special_EternalKiss";
        public static string SkillEffect_SE_T_S_StepToward = "SE_T_S_StepToward";
        public static string SkillEffect_SE_T_S_SweetRestraint = "SE_T_S_SweetRestraint";
        public static string SkillEffect_SE_T_S_WarningStrike = "SE_T_S_WarningStrike";
		/// <summary>
		/// Fractured Illusion
		/// Draw 2 skills and apply Illusion Sword buff onto them.
		/// </summary>
        public static string Skill_S_Miyuki_Draw_FracturedIllusion = "S_Miyuki_Draw_FracturedIllusion";
		/// <summary>
		/// Miyuki, Help
		/// Choose One -
		/// Reduce 'Inner Desire' damage by 15 and draw 2 skills.
		/// Or increase 'Inner Desire' damage by 12 and draw 3 skills.
		/// </summary>
        public static string Skill_S_Miyuki_Draw_MiyukiHelp = "S_Miyuki_Draw_MiyukiHelp";
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
		/// Helping Hand
		/// View allies' unique draw skills and select one to create in hand. The created skill gains Exclude.
		/// </summary>
        public static string Skill_S_Miyuki_LucyDraw = "S_Miyuki_LucyDraw";
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
		/// Game Update
		/// Cost is increased by 1 per stage cleared.
		/// Restart current stage.
		/// </summary>
        public static string Skill_S_Miyuki_Rare_GameUpdate = "S_Miyuki_Rare_GameUpdate";
		/// <summary>
		/// Just for YOU
		/// Remove one random debuff.
		/// </summary>
        public static string Skill_S_Miyuki_Rare_JustforYOU = "S_Miyuki_Rare_JustforYOU";
		/// <summary>
		/// Eternal Kiss
		/// Heal all characters except yourself and the target for &a <color=#FF7C34>(99% Healing Power)</color>.
		/// Convert all ally barriers into party barrier.
		/// Draw 1 skill.
		/// </summary>
        public static string Skill_S_Miyuki_Special_EternalKiss = "S_Miyuki_Special_EternalKiss";
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
		/// A Step Toward YOU
		/// Healing is increased by &a <color=#FF7C34>(18% Attack Power)</color> for each skill in hand not including this skill.
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

    }
}