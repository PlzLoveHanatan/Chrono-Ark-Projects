using MiyukiSone;
using System.Collections.Generic;

public class MiyukiCV : CustomValue
{
	public int MiyukiAffectionPoints = 0;
	public bool Equip = false;
	public int LastLoveAction = -1;
	public int LasthateAction = -1;

	// TurnEnd
	public TryType CurrentTryType = TryType.FirstTry;
	public int CurrentTryCallCount = 0;

	// Kiss no answer
	public TryType CurrentKissNoType = TryType.FirstTry;
	public int KissNoCallCount = 0;
}