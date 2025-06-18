using ChronoArkMod;
namespace Darkness
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Armorgasm ♡
		/// When taking damage, gain +5% Armor and +5% Faint Resist (up to 25).
		/// </summary>
        public static string Buff_B_Darkness_Armorgasm = "B_Darkness_Armorgasm";
		/// <summary>
		/// Busty ♡ Buffer
		/// Gain &a barrier when you cast your own skill.
		/// Consumes 1 stack when played.
		/// <color=#919191>Barrier amount equals <color=#FF7C34>(Max HP * 0.2)</color> of the owner of 'Knight's Resolve'.</color>
		/// </summary>
        public static string Buff_B_Darkness_BustyBuffer = "B_Darkness_BustyBuffer";
		/// <summary>
		/// Busty ♡ Taunt
		/// Can only target &target.
		/// Removed when this character attacks &target.
		/// </summary>
        public static string Buff_B_Darkness_BustyTaunt = "B_Darkness_BustyTaunt";
		/// <summary>
		/// Darkness ♡ Ecstasy
		/// All incoming damage is reduced by 15%.
		/// </summary>
        public static string Buff_B_Darkness_DarknessEcstasy = "B_Darkness_DarknessEcstasy";
		/// <summary>
		/// Darkness Protection ♡
		/// Darkness will receive attacks for this character.
		/// This character also receives Darkness' Armor and healing gauge protection effects.
		/// </summary>
        public static string Buff_B_Darkness_DarknessProtection = "B_Darkness_DarknessProtection";
		/// <summary>
		/// Delightful ♡ Defense
		/// At the start of each turn create a party barrier equal &a <color=#FF7C34>(Max HP * 0.5)</color>.
		/// </summary>
        public static string Buff_B_Darkness_DelightfulDefense = "B_Darkness_DelightfulDefense";
		/// <summary>
		/// Ecstatic ♡ Endurance
		/// Health cannot fall below current 0.
		/// </summary>
        public static string Buff_B_Darkness_EcstaticEndurance = "B_Darkness_EcstaticEndurance";
		/// <summary>
		/// Hit Me Harder ♡
		/// Taunted by &target
		/// Removed when you attack the target.
		/// </summary>
        public static string Buff_B_Darkness_HitMeHarder = "B_Darkness_HitMeHarder";
		/// <summary>
		/// Hurt Me More ♡ Please ♡
		/// When attacked, counterattack for &a equal <color=#FF7C34>(Attack Power * 0.6)</color>.
		/// <color=#919191>Only Darkness can have this buff.</color>
		/// </summary>
        public static string Buff_B_Darkness_HurtMeMorePlease = "B_Darkness_HurtMeMorePlease";
		/// <summary>
		/// Iron Maiden Mode ♡
		/// All incoming damage reduced by 50%.
		/// At the start of the turn apply <color=#FF1493>Busty ♡ Taunt</color> to all enemies.
		/// <color=#919191>You can activate this buff by left-clicking to reveal options that help you better control the fight. Especially handy during the Reaper fight.</color>
		/// </summary>
        public static string Buff_B_Darkness_IronMaidenMode = "B_Darkness_IronMaidenMode";
		/// <summary>
		/// Trial of Weakness
		/// </summary>
        public static string Buff_B_Darkness_TrialofWeakness = "B_Darkness_TrialofWeakness";
		/// <summary>
		/// Darkness
		/// Passive:
		/// Increase Aggro and Faint Resist by 10 for each Darkness Level ♡
		/// Level 1 : When taking damage Gain +5% Defense and +5% Faint Resist.
		/// Level 2 : All incoming damage is reduced by 15%.
		/// Level 3 : At the start of each turn gain barrier <color=#FF7C34>(Max HP * 0.2)</color>.
		/// Level 4 : At the start of each turn remove 1 random debuff and apply it to all enemies.
		/// Level 5 : All incoming damage reduced by 30%.
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_Darkness = "Darkness";
		/// <summary>
		/// Cost reduced by 1 if you have 15 or more barrier remaining. Create a party barrier
		/// equal <color=#FF7C34>(Max HP * 0.5)</color>.
		/// <sprite name="비용2"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_Darkness_0 = "Ex_Darkness_0";
		/// <summary>
		/// If you have 15 or more barrier remaining recast this skill.
		/// <sprite name="비용2"><sprite name="이상">
		/// </summary>
        public static string SkillExtended_Ex_Darkness_1 = "Ex_Darkness_1";
		/// <summary>
		/// Busty ♡ Taunt
		/// <color=#919191>Can only target Darkness.
		/// Removed when this character attacks Darkness.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_BustyTaunt = "KeyWord_BustyTaunt";
		/// <summary>
		/// Darkness Protection
		/// <color=#919191>Darkness will receive attacks for this character.
		/// This character also receives Darkness' Armor and healing gauge protection effects.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_DarknessProtection = "KeyWord_DarknessProtection";
		/// <summary>
		/// Hit Me Harder ♡
		/// <color=#919191>Taunted by user
		/// Removed when you attack the user.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HitMeHarder = "KeyWord_HitMeHarder";
        public static string SkillEffect_SE_S_S_Darkness_Decoy = "SE_S_S_Darkness_Decoy";
        public static string SkillEffect_SE_S_S_Darkness_HerosParry = "SE_S_S_Darkness_HerosParry";
        public static string SkillEffect_SE_S_S_Darkness_MasochistsCourage = "SE_S_S_Darkness_MasochistsCourage";
        public static string SkillEffect_SE_S_S_Darkness_PartyKnight = "SE_S_S_Darkness_PartyKnight";
        public static string SkillEffect_SE_S_S_Darkness_Rare_IronMaidensEmbrace = "SE_S_S_Darkness_Rare_IronMaidensEmbrace";
        public static string SkillEffect_SE_S_S_Darkness_Rare_UnbreakableWill = "SE_S_S_Darkness_Rare_UnbreakableWill";
        public static string SkillEffect_SE_S_S_Darkness_ShieldofFaith = "SE_S_S_Darkness_ShieldofFaith";
        public static string SkillEffect_SE_T_S_Darkness_ClumsySlash = "SE_T_S_Darkness_ClumsySlash";
        public static string SkillEffect_SE_T_S_Darkness_CrusaderDomination = "SE_T_S_Darkness_CrusaderDomination";
        public static string SkillEffect_SE_T_S_Darkness_Decoy = "SE_T_S_Darkness_Decoy";
        public static string SkillEffect_SE_T_S_Darkness_HerosParry_0 = "SE_T_S_Darkness_HerosParry_0";
        public static string SkillEffect_SE_T_S_Darkness_KnightsResolve = "SE_T_S_Darkness_KnightsResolve";
        public static string SkillEffect_SE_T_S_Darkness_LastStand = "SE_T_S_Darkness_LastStand";
        public static string SkillEffect_SE_T_S_Darkness_PartyKnight = "SE_T_S_Darkness_PartyKnight";
        public static string SkillEffect_SE_T_S_Darkness_Rare_GuardiansGrace = "SE_T_S_Darkness_Rare_GuardiansGrace";
        public static string SkillEffect_SE_T_S_Darkness_SideSlash = "SE_T_S_Darkness_SideSlash";
		/// <summary>
		/// Clumsy Slash
		/// Cost reduced by 1 if this skill is fixed ability.
		/// This skill always lands if you have 15 or more barrier remaining.
		/// Create 'Side Slash' in your hand.
		/// </summary>
        public static string Skill_S_Darkness_ClumsySlash = "S_Darkness_ClumsySlash";
		/// <summary>
		/// Crusader Domination
		/// This skill always lands against targets with a Weakening<sprite=0> debuff.
		/// If you have 15 or more barrier remaining, apply an additional 'Trial of Weakness' debuff and <color=#FF69B4>Hit Me Harder ♡</color> to all target's.
		/// </summary>
        public static string Skill_S_Darkness_CrusaderDomination = "S_Darkness_CrusaderDomination";
		/// <summary>
		/// Decoy
		/// </summary>
        public static string Skill_S_Darkness_Decoy = "S_Darkness_Decoy";
        public static string Skill_S_Darkness_DummyHeal = "S_Darkness_DummyHeal";
		/// <summary>
		/// Hero's Parry
		/// Gain &a barrier <color=#FF7C34>(Max HP * 0.4)</color>. 
		/// Apply <color=#6A5ACD>Darkness Protection</color> to all allies.
		/// </summary>
        public static string Skill_S_Darkness_HerosParry = "S_Darkness_HerosParry";
		/// <summary>
		/// I Live to Suffer ♡
		/// </summary>
        public static string Skill_S_Darkness_HerosParry_0 = "S_Darkness_HerosParry_0";
		/// <summary>
		/// Knight's Resolve
		/// Cost reduced by 1 if this skill is a fixed ability.
		/// </summary>
        public static string Skill_S_Darkness_KnightsResolve = "S_Darkness_KnightsResolve";
		/// <summary>
		/// Last Stand
		/// This skill always lands if you have 15 barrier remaining.
		/// Deal additional &a damage equals <color=#FF7C34>(All allies barriers * 0.4)</color>.
		/// </summary>
        public static string Skill_S_Darkness_LastStand = "S_Darkness_LastStand";
		/// <summary>
		/// Battle Prep
		/// Draw 3 skills and create a party barrier equal <color=#FF7C34>(Darkness Max HP * 0.5)</color>.
		/// If Darkness is fainted, do not create barrier.
		/// </summary>
        public static string Skill_S_Darkness_LucyDraw = "S_Darkness_LucyDraw";
		/// <summary>
		/// Masochist's ♡ Courage
		/// Take non-lethal <color=purple>&a Pain Damage</color> <color=#FF7C34>(Max HP * 0.8)</color> and create a party barrier by half that amount.
		/// If your Health is below 50%, gain <color=#C71585>Darkness ♡ Ecstasy</color> buff and create additional &b party barrier <color=#FF7C34>(Max HP * 0.2)</color>.
		/// </summary>
        public static string Skill_S_Darkness_MasochistsCourage = "S_Darkness_MasochistsCourage";
		/// <summary>
		/// Party Knight
		/// Apply &a barrier <color=#FF7C34>(Max HP * 0.2)</color> and remove 1 random debuff.
		/// This skill can be played repeatedly during this turn.
		/// </summary>
        public static string Skill_S_Darkness_PartyKnight = "S_Darkness_PartyKnight";
		/// <summary>
		/// Guardian's Grace
		/// If you have 15 or more Barrier remaining, gain guaranteed Critical.
		/// If the Barrier is 30 or more, also reduce this skill's Cost by 1.
		/// Deal &a additional damage for each stack of buff on self.
		/// </summary>
        public static string Skill_S_Darkness_Rare_GuardiansGrace = "S_Darkness_Rare_GuardiansGrace";
		/// <summary>
		/// Iron Maiden's Embrace
		/// When used create a party barrier equal &a <color=#FF7C34>(Max HP * 0.5)</color>.
		/// </summary>
        public static string Skill_S_Darkness_Rare_IronMaidensEmbrace = "S_Darkness_Rare_IronMaidensEmbrace";
		/// <summary>
		/// Unbreakable Will
		/// When used apply <color=#FF1493>Busty ♡ Taunt</color> to all enemies.
		/// All ally buffs remain 2 extra turn. Debuffs remain 2 turn less.
		/// </summary>
        public static string Skill_S_Darkness_Rare_UnbreakableWill = "S_Darkness_Rare_UnbreakableWill";
		/// <summary>
		/// Exit Iron Maiden Mode
		/// Remove 'Iron Maiden Mode' buff.
		/// </summary>
        public static string Skill_S_Darkness_Rare_UnbreakableWill_0 = "S_Darkness_Rare_UnbreakableWill_0";
		/// <summary>
		/// Remove Apply Taunt
		/// At the start of the turn DO NOT APPLY <color=#FF1493>Busty ♡ Taunt</color> to all enemies.
		/// This option can be changed without restrictions.
		/// </summary>
        public static string Skill_S_Darkness_Rare_UnbreakableWill_1 = "S_Darkness_Rare_UnbreakableWill_1";
		/// <summary>
		/// Apply Taunt
		/// At the start of the turn apply <color=#FF1493>Busty ♡ Taunt</color> to all enemies.
		/// This option can be changed without restrictions.
		/// </summary>
        public static string Skill_S_Darkness_Rare_UnbreakableWill_2 = "S_Darkness_Rare_UnbreakableWill_2";
		/// <summary>
		/// Remove current Taunt
		/// Remove current <color=#FF1493>Busty ♡ Taunt</color> from all enemies.
		/// Can only be used once.
		/// </summary>
        public static string Skill_S_Darkness_Rare_UnbreakableWill_3 = "S_Darkness_Rare_UnbreakableWill_3";
		/// <summary>
		/// Shield of Faith
		/// Create a party barrier (&a) equal <color=#FF7C34>(Max HP * 0.6)</color> and apply <color=#FF1493>Busty ♡ Taunt</color> to all enemies.
		/// </summary>
        public static string Skill_S_Darkness_ShieldofFaith = "S_Darkness_ShieldofFaith";
		/// <summary>
		/// Side Slash
		/// This skill always lands if you have 15 or more barrier remaining.
		/// </summary>
        public static string Skill_S_Darkness_SideSlash = "S_Darkness_SideSlash";
		/// <summary>
		/// Stubborn Knight (KonoSuba Edition)
		/// </summary>
        public static string Buff_S_Darkness_StubbornKnight = "S_Darkness_StubbornKnight";
		/// <summary>
		/// Stubborn Knight (Darkness Edition)
		/// </summary>
        public static string Buff_S_Darkness_StubbornKnight_0 = "S_Darkness_StubbornKnight_0";
		/// <summary>
		/// Hurt Me More ♡ Please ♡
		/// <color=#919191>When attacked, counterattack for <color=#FF7C34>(Attack Power * 0.6)</color>.
		/// <color=#919191>Only Darkness can have this buff.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_HurtMeMore = "KeyWord_HurtMeMore";
		/// <summary>
		/// Darkness ♡ Ecstasy
		/// <color=#919191>All incoming damage is reduced by 15%.</color>
		/// </summary>
        public static string SkillKeyword_KeyWord_DarknessEcstasy = "KeyWord_DarknessEcstasy";

    }

    public static class ModLocalization
    {
		/// <summary>
		/// Korean:
		/// 내 공격이 적중하고 있어! 진짜로 맞고 있다고!
		/// English:
		/// My attacks are landing! They're actually landing!
		/// Japanese:
		/// 私の攻撃が当たってる！本当に当たってる！
		/// Chinese:
		/// 我的攻击打中了！真的打中了！
		/// Chinese-TW:
		/// </summary>
        public static string DarknessAttackLands_0 => ModManager.getModInfo("Darkness").localizationInfo.SystemLocalizationUpdate("DarknessAttackLands_0");
		/// <summary>
		/// Korean:
		/// 아이즈의 검술 훈련은 정말 대단해! 적중의 쾌감에 중독되기 시작했어!
		/// English:
		/// Aiz's sword training is amazing! I'm starting to get addicted to the thrill of landing hits!
		/// Japanese:
		/// アイズの剣の訓練はすごいよ！攻撃が当たるスリルにハマりそう！
		/// Chinese:
		/// 艾丝的剑术训练太厉害了！我已经开始迷上击中敌人的快感了！
		/// Chinese-TW:
		/// </summary>
        public static string DarknessAttackLands_1 => ModManager.getModInfo("Darkness").localizationInfo.SystemLocalizationUpdate("DarknessAttackLands_1");
		/// <summary>
		/// Korean:
		/// 힘과 체력에는 자신이 있는데, 너무 서툴러서 공격이 전혀 맞질 않아...
		/// English:
		/// I'm confident in my strength and endurance, but I'm so clumsy my attacks never land...
		/// Japanese:
		/// 体力と持久力には自信があるけど、不器用すぎて攻撃が全然当たらない…
		/// Chinese:
		/// 我对自己的力量和耐力很有自信，但太笨拙了，攻击总是打不中……
		/// Chinese-TW:
		/// </summary>
        public static string DarknessAttackMisses => ModManager.getModInfo("Darkness").localizationInfo.SystemLocalizationUpdate("DarknessAttackMisses");
		/// <summary>
		/// Korean:
		/// 더 괴롭혀줘, 제발 ♡
		/// English:
		/// Hurt Me More, Please ♡
		/// Japanese:
		/// もっと痛めつけて、お願い ♡
		/// Chinese:
		/// 请再多伤我一点吧，拜托了 ♡
		/// Chinese-TW:
		/// </summary>
        public static string HurtMe => ModManager.getModInfo("Darkness").localizationInfo.SystemLocalizationUpdate("HurtMe");

    }
}