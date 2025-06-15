using ChronoArkMod;
namespace Darkness
{
    public static class ModItemKeys
    {
		/// <summary>
		/// Armorgasm ♡
		/// When taking damage, gain +5% Armor and +5% Faint Resist.
		/// </summary>
        public static string Buff_B_Darkness_Armorgasm = "B_Darkness_Armorgasm";
		/// <summary>
		/// Busty Buffer
		/// Gain &a barrier when you cast your own skill.
		/// Consumes 1 stack when played.
		/// <color=#919191>Barrier amount equals <color=#FF7C34>(Max HP * 0.25)</color> of the owner of 'Knight's Resolve'.</color>
		/// </summary>
        public static string Buff_B_Darkness_BustyBuffer = "B_Darkness_BustyBuffer";
		/// <summary>
		/// Busty Taunt
		/// Can only target &target.
		/// Removed when this character attacks &target.
		/// </summary>
        public static string Buff_B_Darkness_BustyTaunt = "B_Darkness_BustyTaunt";
		/// <summary>
		/// Darkness Ecstasy
		/// All incoming damage is reduced by 15%.
		/// </summary>
        public static string Buff_B_Darkness_DarknessEcstasy = "B_Darkness_DarknessEcstasy";
		/// <summary>
		/// Darkness Protection
		/// Darkness will receive attacks for this character.
		/// This character also receives Darkness' Armor and healing gauge protection effects.
		/// </summary>
        public static string Buff_B_Darkness_DarknessProtection = "B_Darkness_DarknessProtection";
		/// <summary>
		/// Delightful Defense ♡
		/// At the start of each turn create a party barrier equal &a <color=#FF7C34>(Max HP * 0.7)</color>.
		/// </summary>
        public static string Buff_B_Darkness_DelightfulDefense = "B_Darkness_DelightfulDefense";
		/// <summary>
		/// Ecstatic Endurance
		/// Health cannot fall below current 0.
		/// </summary>
        public static string Buff_B_Darkness_EcstaticEndurance = "B_Darkness_EcstaticEndurance";
		/// <summary>
		/// Hit Me Harder
		/// Taunted by &target
		/// Removed when you attack the target.
		/// </summary>
        public static string Buff_B_Darkness_HitMeHarder = "B_Darkness_HitMeHarder";
		/// <summary>
		/// Hurt Me More, Please ♡
		/// Whenever an enemy targets an ally, attack <b>before the enemy</b> and deal &a damage to them.
		/// </summary>
        public static string Buff_B_Darkness_HurtMeMorePlease = "B_Darkness_HurtMeMorePlease";
		/// <summary>
		/// Iron Maiden Mode
		/// All incoming damage reduced by 50%.
		/// At the start of the turn apply 'Busty Taunt' to all enemies.
		/// </summary>
        public static string Buff_B_Darkness_IronMaidenMode = "B_Darkness_IronMaidenMode";
		/// <summary>
		/// Trial of Weakness
		/// </summary>
        public static string Buff_B_Darkness_TrialofWeakness = "B_Darkness_TrialofWeakness";
		/// <summary>
		/// Darkness
		/// Passive:
		/// Increase Faint Resist by 10 for each Darkness Level.
		/// Level 1 : When taking damage Gain +5% Defense and +5% Faint Resist.
		/// Level 2 : Aggro increased.
		/// Level 3 : All incoming damage is reduced by 15%.
		/// Level 4 : At the start of each turn gain barrier <color=#FF7C34>(Max HP / 3)</color>.
		/// Level 5 : At the start of each turn remove 1 random debuff and apply it to all enemies.
		/// Level 6 : All incoming damage reduced by 30%.
		/// <color=#919191>- This passive is applied from level 1.</color>
		/// </summary>
        public static string Character_Darkness = "Darkness";
		/// <summary>
		/// Busty Taunt
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
		/// Hit Me Harder
		/// <color=#919191>Taunted by &target
		/// Removed when you attack the target.</color>
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
		/// Create 'Side Slash' in your hand.
		/// </summary>
        public static string Skill_S_Darkness_ClumsySlash = "S_Darkness_ClumsySlash";
		/// <summary>
		/// Crusader Domination
		/// This skill always lands against targets with a Weakening<sprite=0> debuff.
		/// If you have 20 or more barrier remaining, apply an additional Destroy Weapon debuff, apply 'Hit Me Harder' to all target's.
		/// </summary>
        public static string Skill_S_Darkness_CrusaderDomination = "S_Darkness_CrusaderDomination";
		/// <summary>
		/// Decoy
		/// </summary>
        public static string Skill_S_Darkness_Decoy = "S_Darkness_Decoy";
        public static string Skill_S_Darkness_DummyHeal = "S_Darkness_DummyHeal";
		/// <summary>
		/// Hero's Parry
		/// Apply 'Darkness Protection' to all allies.
		/// </summary>
        public static string Skill_S_Darkness_HerosParry = "S_Darkness_HerosParry";
		/// <summary>
		/// I Live to Suffer ♡
		/// </summary>
        public static string Skill_S_Darkness_HerosParry_0 = "S_Darkness_HerosParry_0";
		/// <summary>
		/// Knight's Resolve
		/// </summary>
        public static string Skill_S_Darkness_KnightsResolve = "S_Darkness_KnightsResolve";
		/// <summary>
		/// Last Stand
		/// This skill always lands if you have barrier remaining. Deal additional damage based on 50% of all ally barriers.
		/// </summary>
        public static string Skill_S_Darkness_LastStand = "S_Darkness_LastStand";
		/// <summary>
		/// Battle Prep
		/// Draw 3 skills and create a party barrier (&a) equal <color=#FF7C34>(Darkness Max HP / 2)</color>.
		/// If Darkness is fainted, do not create barrier.
		/// </summary>
        public static string Skill_S_Darkness_LucyDraw = "S_Darkness_LucyDraw";
		/// <summary>
		/// Masochist's Courage
		/// Take <color=purple>&a Pain Damage</color> <color=#FF7C34>(Max HP * 0.3)</color> and heal all allies by that amount.
		/// If your Health is below 50%, apply 'Hit me Harder' to all enemies and gain &b Barrier <color=#FF7C34>(Max HP * 0.4)</color>.
		/// </summary>
        public static string Skill_S_Darkness_MasochistsCourage = "S_Darkness_MasochistsCourage";
		/// <summary>
		/// Party Knight
		/// Apply &a barrier <color=#FF7C34>(Max HP / 3)</color> and remove 1 random debuff.
		/// This skill can be played repeatedly during this turn.
		/// </summary>
        public static string Skill_S_Darkness_PartyKnight = "S_Darkness_PartyKnight";
		/// <summary>
		/// Guardian's Grace
		/// If you have 20 or more Barrier, gain guaranteed Critical.
		/// If the Barrier is 40 or more, also reduce this skill's Cost by 1.
		/// Deal &a additional damage for each stack of buff on self.
		/// </summary>
        public static string Skill_S_Darkness_Rare_GuardiansGrace = "S_Darkness_Rare_GuardiansGrace";
		/// <summary>
		/// Iron Maiden's Embrace
		/// When used create a party barrier equal &a <color=#FF7C34>(Max HP * 0.7)</color>.
		/// </summary>
        public static string Skill_S_Darkness_Rare_IronMaidensEmbrace = "S_Darkness_Rare_IronMaidensEmbrace";
		/// <summary>
		/// Unbreakable Will
		/// When used apply 'Busty Taunt' to all enemies.
		/// All ally buffs remain 2 extra turn. Debuffs remain 2 turn less.
		/// </summary>
        public static string Skill_S_Darkness_Rare_UnbreakableWill = "S_Darkness_Rare_UnbreakableWill";
		/// <summary>
		/// Shield of Faith
		/// Create a party barrier (&a) equal user's Maximum Health and apply 'Busty Taunt' to all enemies.
		/// </summary>
        public static string Skill_S_Darkness_ShieldofFaith = "S_Darkness_ShieldofFaith";
		/// <summary>
		/// Side Slash
		/// </summary>
        public static string Skill_S_Darkness_SideSlash = "S_Darkness_SideSlash";
		/// <summary>
		/// Stubborn Knight (KonoSuba Edition)
		/// </summary>
        public static string Buff_S_Darkness_StubbornKnight = "S_Darkness_StubbornKnight";

    }

    public static class ModLocalization
    {

    }
}