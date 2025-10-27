using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmotionSystem.Extended.EGO;

namespace EmotionSystem
{
	public class LiteratureSkill
	{
		public class Abnormality
		{
			
		}


		public class EGO
		{
			public class TodayExpression : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}

			public class SanguineDesire : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}

			public class RedEyes : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}

			public class Laetitia : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}

			public class BlackSwan : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}
		}
	}
}
