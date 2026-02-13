using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class EventField
	{
		public static void MiyukiAction()
		{
			
		}

		private static void RecievingGift()
		{
			ChangeAffectionPoints(5);
		}

		private static void UsingConsumbales()
		{
			ChangeAffectionPoints(1);
		}

		private static void Using101()
		{
			ChangeAffectionPoints(2);
		}
	}
}
