public class MiyukCV : CustomValue
{
	public int MiyukiAffectionPoints = 0;
	public bool Equip = false;

	// Indexes

	public int LastAffection = -1;

	public int LastDereAction = -1;
	public int LastYandereAction = -1;
	public int LastTurnAction = -1;
	public int LastHelpAction = -1;
	public int LastPrank = -1;
	public int LastPhoneImage = -1;
	public int LastSong = -1;
	public int LastPose = -1;

	// TurnEnd
	public int CurrentTryType = 0; // 0 = FirstTry, 1 = SecondTry, etc.
	public int CurrentTryCallCount = 0;

	// Kiss no answer
	public int CurrentKissNoType = 0;
	public int KissNoCallCount = 0;

	// Dialogue Box animation
	public int LastYesBoxAnimation = -1;
	public int LastNoBoxAnimation = -1;

	public bool BGMVolumeIncreased = false;

	public string LastBuff;
}