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
		/// <summary>
		/// Miarrow
		/// The next Sheathe effect occurs twice.
		/// </summary>
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
		/// If Mia is level 6, the first Sheathe effect occurs twice, once per turn.
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
		/// All attacks inflict Pawcut (<sprite=1> 105%).
		/// Whenever a discard occurs, gain Pawtience (Max 4 per turn).
		/// <color=#919191>Pawtience - At 2 stacks, gain <color=#FF0070>Savage Impulse</color> (if buff owner is Mia),
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
		/// 버릴 스킬을 선택하세요.
		/// 왼쪽은 아래의 스킬이며, 마나를 스킬 비용만큼 회복합니다 (최대 2).
		/// 오른쪽은 위의 스킬이며, 스킬 비용만큼 카드를 뽑습니다 (최대 2).
		/// <color=#737373>손에 스킬이 하나뿐이라면, 항상 마나를 회복합니다.</color>
		/// English:
		/// Select skill to discard. 
		/// Left is bottom skill, restore Mana equal skill cost (Max 2).
		/// Right is top skill, draw equal skill cost (Max 2).
		/// <color=#737373>If you have only 1 skill in hand, you will always restore Mana.</color>
		/// Japanese:
		/// 捨てるスキルを選んでください。
		/// 左は一番下のスキルで、スキルコスト分のマナを回復します（最大2）。
		/// 右は一番上のスキルで、スキルコスト分カードを引きます（最大2枚）。
		/// <color=#737373>手札にスキルが1枚だけの場合、常にマナを回復します。</color>
		/// Chinese:
		/// 选择要弃置的技能。
		/// 左边是最底部的技能，恢复等同技能费用的法力（最多2点）。
		/// 右边是最顶部的技能，抽取等同技能费用的牌（最多2张）。
		/// <color=#737373>如果手中只有一张技能牌，将始终恢复法力。</color>
		/// Chinese-TW:
		/// </summary>
        public static string Discard => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("Discard");
		/// <summary>
		/// Korean:
		/// 에이미, 미야, 그리고 스콸 경은 모두 악기를 잘 다뤄요! 미야는 춤추고 응원하는 담당이에요!
		/// English:
		/// Amy, Miya, and Lord Squall are all amazing with instruments! Miya's in charge of dancing and cheering!
		/// Japanese:
		/// エイミー、ミヤ、そしてスコール様は楽器が得意！ミヤはダンスと応援担当だよ！
		/// Chinese:
		/// 艾米、米娅和斯奎尔大人都精通乐器！米娅负责跳舞和加油！
		/// Chinese-TW:
		/// </summary>
        public static string MiaBattleStart_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaBattleStart_0");
		/// <summary>
		/// Korean:
		/// 고기는 최고지만, 에이미가 야채도 꼭 먹으라고 했어요.
		/// English:
		/// Meat is great, but Amy said you've gotta eat your veggies too.
		/// Japanese:
		/// お肉は最高だけど、エイミーが野菜も食べなきゃって言ってたよ。
		/// Chinese:
		/// 肉很好吃，但艾米说也要吃蔬菜哦。
		/// Chinese-TW:
		/// </summary>
        public static string MiaBattleStart_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaBattleStart_1");
		/// <summary>
		/// Korean:
		/// 클릭하여 <color=#FF0070>야성의 충동</color>을 발동하세요.
		/// English:
		/// Click to activate <color=#FF0070>Savage Impulse</color>.
		/// Japanese:
		/// クリックして<color=#FF0070>野性の衝動</color>を発動！
		/// Chinese:
		/// 点击激活<color=#FF0070>野性冲动</color>。
		/// Chinese-TW:
		/// </summary>
        public static string MiaButton_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaButton_0");
		/// <summary>
		/// Korean:
		/// 클릭하거나 [V]를 눌러 <color=#FF0070>야성의 충동</color>을 발동하세요.
		/// English:
		/// Click or press [V] to activate <color=#FF0070>Savage Impulse</color>.
		/// Japanese:
		/// クリックまたは [V] キーで <color=#FF0070>野性の衝動</color> を発動！
		/// Chinese:
		/// 点击或按 [V] 激活 <color=#FF0070>野性冲动</color>。
		/// Chinese-TW:
		/// </summary>
        public static string MiaButton_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaButton_1");
		/// <summary>
		/// Korean:
		/// <color=#FF0070>야성의 충동</color>은 사용할 수 없습니다.
		/// English:
		/// <color=#FF0070>Savage Impulse</color> is not available.
		/// Japanese:
		/// <color=#FF0070>野性の衝動</color> は現在使用できません。
		/// Chinese:
		/// <color=#FF0070>野性冲动</color> 当前不可用。
		/// Chinese-TW:
		/// </summary>
        public static string MiaButton_2 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaButton_2");
		/// <summary>
		/// Korean:
		/// 미야와 함께 보물 사냥을 떠나요! 낙타를 타고 출발~!
		/// English:
		/// Let's go treasure hunting with Miya! Mount the camel, and off we go!
		/// Japanese:
		/// ミヤと一緒に宝探しに行こう！ラクダに乗って、しゅっぱーつ！
		/// Chinese:
		/// 和米娅一起去寻宝吧！骑上骆驼，出发！
		/// Chinese-TW:
		/// </summary>
        public static string MiaChest => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaChest");
		/// <summary>
		/// Korean:
		/// 모든 음식을 맛있게 먹는 것이 예의예요!
		/// English:
		/// It's good manners to enjoy any kind of food deliciously!
		/// Japanese:
		/// どんな食べ物もおいしくいただくのがマナーだよ！
		/// Chinese:
		/// 无论什么食物，好好享受才是礼貌哦！
		/// Chinese-TW:
		/// </summary>
        public static string MiaCri => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaCri");
		/// <summary>
		/// Korean:
		/// 정리할 시간이에요! 케팔, 시작해요!
		/// English:
		/// Time to clean up! Let's do this, Keppal!
		/// Japanese:
		/// お片づけの時間だよ！ケッパル、いくよー！
		/// Chinese:
		/// 该打扫啦！来吧，凯帕尔！
		/// Chinese-TW:
		/// </summary>
        public static string MiaCurse => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaCurse");
		/// <summary>
		/// Korean:
		/// 미야는 아직 안 졸려요. 오늘 밤은 아침까지 잠 못 자요~
		/// English:
		/// Miya's not sleepy yet. Tonight, no one's getting to sleep until morning...
		/// Japanese:
		/// ミヤはまだ眠くないよ。今夜は朝まで寝かせないんだから～
		/// Chinese:
		/// 米娅还不困呢。今晚大家都别想睡觉啦……
		/// Chinese-TW:
		/// </summary>
        public static string MiaDD => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaDD");
		/// <summary>
		/// Korean:
		/// 처음 보는 게임이 정말 많아요! 바깥 세상은 정말 재밌어요!
		/// English:
		/// There are so many games I've never seen before! The outside world is so much fun!
		/// Japanese:
		/// 見たことないゲームがいっぱい！外の世界って楽しいね！
		/// Chinese:
		/// 有好多从没见过的游戏！外面的世界好有趣！
		/// Chinese-TW:
		/// </summary>
        public static string MiaDDAlly => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaDDAlly");
		/// <summary>
		/// Korean:
		/// 맛있는 걸 먹는 것도 일종의 운동이에요!
		/// English:
		/// Eating delicious food is its own kind of exercise!
		/// Japanese:
		/// おいしいものを食べるのも、ある意味運動だよね！
		/// Chinese:
		/// 吃美食也是一种锻炼哦！
		/// Chinese-TW:
		/// </summary>
        public static string MiaHealed => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaHealed");
		/// <summary>
		/// Korean:
		/// 미야는 랜서라구요!
		/// English:
		/// Miya is a lancer, you know!
		/// Japanese:
		/// ミヤはランサーなんだよ！
		/// Chinese:
		/// 你知道吗，米娅可是个枪兵！
		/// Chinese-TW:
		/// </summary>
        public static string MiaIdleB_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaIdleB_0");
		/// <summary>
		/// Korean:
		/// 미야는 진짜 강해요! 근데 힘 쓰면 배가 고파져요...
		/// English:
		/// Miya is really strong! But using all that strength sure makes me hungry...
		/// Japanese:
		/// ミヤはすっごく強いよ！でもいっぱい動くとお腹すいちゃう…
		/// Chinese:
		/// 米娅超级强！不过用力过头也会肚子饿……
		/// Chinese-TW:
		/// </summary>
        public static string MiaIdleB_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaIdleB_1");
		/// <summary>
		/// Korean:
		/// 미야가 태어난 곳에 비하면, 악셀 시티는 신기한 걸로 가득해요!
		/// English:
		/// Compared to where Mia was born, Axel city is full of wonders!
		/// Japanese:
		/// ミヤの故郷と比べて、アクセルシティは驚きがいっぱい！
		/// Chinese:
		/// 和米娅的故乡比起来，艾克塞尔城真是神奇又热闹！
		/// Chinese-TW:
		/// </summary>
        public static string MiaIdleF => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaIdleF");
		/// <summary>
		/// Korean:
		/// 순발력도 좋고, 몬스터도 쓰러뜨리고, 이 창은 정말 만능이에요!
		/// English:
		/// I can think on my feet, defeat monsters, and this spear is just perfect for everything!
		/// Japanese:
		/// とっさの判断もバッチリ、モンスターだって倒せるし、この槍はなんでもできるよ！
		/// Chinese:
		/// 我反应很快，打怪兽也不在话下，这把长枪超好用的！
		/// Chinese-TW:
		/// </summary>
        public static string MiaKill => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaKill");
		/// <summary>
		/// Korean:
		/// 도시에는 맛있는 게 정말 많아요.
		/// English:
		/// The city has all kinds of delicious food.
		/// Japanese:
		/// 街にはおいしいものがいっぱいあるんだよ。
		/// Chinese:
		/// 城里有各种各样好吃的。
		/// Chinese-TW:
		/// </summary>
        public static string MiaMaster => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaMaster");
		/// <summary>
		/// Korean:
		/// 바닷가에서 먹는 오징어 구이가 최고예요! 에이미도 먹어봤으면 좋겠어요!
		/// English:
		/// Grilled squid by the sea is the best! I want Amy to try some too!
		/// Japanese:
		/// 海辺で食べるイカ焼きは最高！エイミーにも食べさせたいな！
		/// Chinese:
		/// 海边的烤鱿鱼最棒了！真想让艾米也尝一尝！
		/// Chinese-TW:
		/// </summary>
        public static string MiaOther_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaOther_0");
		/// <summary>
		/// Korean:
		/// 하아~ 푸우~ 맛있는 게 너무 많아요! 크리스마스는 정말 멋진 날이에요!
		/// English:
		/// Haah! Phew! There's so much delicious food! Christmas really is an amazing day!
		/// Japanese:
		/// はぁ～ふぅ～おいしいものがいっぱい！クリスマスって本当に素敵！
		/// Chinese:
		/// 哈啊呼美食太多了！圣诞节真是太棒啦！
		/// Chinese-TW:
		/// </summary>
        public static string MiaOther_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaOther_1");
		/// <summary>
		/// Korean:
		/// 에이미랑 할로윈 장식했어요! 어때요? 귀엽죠?
		/// English:
		/// I decorated for Halloween with Amy! What do you think Cute, right ?
		/// Japanese:
		/// エイミーと一緒にハロウィンの飾り付けしたの！どう？かわいいでしょ？
		/// Chinese:
		/// 我和艾米一起装饰了万圣节！怎么样？可爱吧？
		/// Chinese-TW:
		/// </summary>
        public static string MiaOther_2 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaOther_2");
		/// <summary>
		/// Korean:
		/// 슬립오버엔 역시 베개 싸움이죠! 미야의 스피드 베개 받아보세요!
		/// English:
		/// Sleepovers mean pillow fights! Try to catch Miya's speedy pillow!
		/// Japanese:
		/// お泊まり会といえば枕投げ！ミヤのスピード枕、受けてみなさいっ！
		/// Chinese:
		/// 过夜派对当然要打枕头战啦！来接住米娅的极速枕头！
		/// Chinese-TW:
		/// </summary>
        public static string MiaPharos_0 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPharos_0");
		/// <summary>
		/// Korean:
		/// 배가 부르면 졸려지죠... 달콤한 꿈 꾸세요!
		/// English:
		/// When you're full, you get sleepy... Hope you have really sweet dreams!
		/// Japanese:
		/// お腹いっぱいになると、眠くなるよね…素敵な夢が見られますように！
		/// Chinese:
		/// 吃饱了就容易困……祝你做个甜甜的美梦！
		/// Chinese-TW:
		/// </summary>
        public static string MiaPharos_1 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPharos_1");
		/// <summary>
		/// Korean:
		/// 니쿠만이다~ 너무 행복해요! 전부 혼자 먹을 거예요!
		/// English:
		/// Nikuman, Nikuman, I'm so happy! I'm going to eat it all myself!
		/// Japanese:
		/// 肉まんだ～うれし～い！全部ひとりで食べちゃうもんね！
		/// Chinese:
		/// 肉包、肉包，好开心啊！我要一个人全吃掉！
		/// Chinese-TW:
		/// </summary>
        public static string MiaPharos_2 => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPharos_2");
		/// <summary>
		/// Korean:
		/// 그 과일도 맛있어 보이지만... 미야는 고기를 더 좋아해요!
		/// English:
		/// That fruit looks delicious... but Miya prefers meat!
		/// Japanese:
		/// その果物、おいしそうだけど…ミヤはやっぱりお肉派！
		/// Chinese:
		/// 那个水果看起来很好吃……但米娅更喜欢肉！
		/// Chinese-TW:
		/// </summary>
        public static string MiaPotion => ModManager.getModInfo("Mia").localizationInfo.SystemLocalizationUpdate("MiaPotion");

    }
}