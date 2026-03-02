using MiyukiSone;
using System.Collections.Generic;

public class MiyukiCV : CustomValue
{
	public int MiyukiAffectionPoints = 0;
	public bool Equip = false;
	public int LastDereAction = -1;
	public int LastYandereAction = -1;
	public int LastTurnAction = -1;
	public int LastHelpAction = -1;
	public int LastPrank = -1;

	// TurnEnd
	public TryType CurrentTryType = TryType.FirstTry;
	public int CurrentTryCallCount = 0;

	// Kiss no answer
	public TryType CurrentKissNoType = TryType.FirstTry;
	public int KissNoCallCount = 0;

	public int LastYesBoxAnimation = -1;
	public int LastNoBoxAnimation = -1;
}