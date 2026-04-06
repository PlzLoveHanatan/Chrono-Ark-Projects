using System.Collections.Generic;

public class MiyukCV : CustomValue
{
	// Character Starting Equip
	public bool Equip = false;

	// Miyuki Special Interaction
	public int CurrentKissNoType = 0;
	public int KissNoCallCount = 0;
	public int FinalViewCharge = 1;
	public int FinalViewDamage = 0;
	public bool BGMVolumeIncreased = false;
	public bool PauseOpen = false;
	public bool SlotsCheck = false;
	public bool GameUpdated = false;

	// Indexes
	public int LastAffection = -1;
	public int LastArtIndex = -1;	
	public int LastPhoneImage = -1;
	public int MiyukiArtIndex = -1;
	public int Affection;

	// Saving Buff Key for Skill
	public string LastGlitchedPhoneBuff;
}