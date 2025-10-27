using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
    public partial class InvitationManager
    {
		public class GuestSequence
		{
			public string FirstGuest;
			public string SecondGuest;
			public int FogTurn;

			public GuestSequence(string guest1, string guest2, int fogTurn)
			{
				FirstGuest = guest1;
				SecondGuest = guest2;
				FogTurn = fogTurn;
			}
		}
	}
}
