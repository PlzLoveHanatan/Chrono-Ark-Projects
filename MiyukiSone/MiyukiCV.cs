public class MiyukiCV : CustomValue
{
	public int MiyukiAffectionPoints = 0;
	public bool Equip = false;
	public int LastDereAction = -1;
	public int LastYandereAction = -1;
	public int LastTurnAction = -1;
	public int LastHelpAction = -1;
	public int LastPrank = -1;

	// TurnEnd - храним как int
	public int CurrentTryType = 0; // 0 = FirstTry, 1 = SecondTry и т.д.
	public int CurrentTryCallCount = 0;

	// Kiss no answer - храним как int
	public int CurrentKissNoType = 0;
	public int KissNoCallCount = 0;

	// Dialogue Box animation
	public int LastYesBoxAnimation = -1;
	public int LastNoBoxAnimation = -1;
}