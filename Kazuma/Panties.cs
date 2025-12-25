using System;
using System.Linq;
using GameDataEditor;

namespace Kazuma
{
	public class Panties
	{
		public class PantiesMain : Skill_Extended
		{
			public int AppearanceCount { get; protected set; } = 1;
			public int CharmCount { get; protected set; } = 1;
			public int WornCount { get; protected set; } = 1;
			public int TotalCount { get; protected set; } = 1;
			public int Cotton { get; protected set; } = 100;
			public int Polyurethanes { get; protected set; } = 0;

			private string GetStars(int count) => new string('☆', count);
			private string GetCotton(int value) => $"Cotton {value}%";
			private string GetPolyurethanes(int value) => value > 0 ? $"Polyurethanes {value}%" : "";

			public override void Init()
			{
				var keyword = MySkill.MySkill.PlusKeyWords
					.FirstOrDefault(k => k.Key == ModItemKeys.SkillKeyword_KeyWord_Panties);

				if (keyword != null)
				{
					keyword.Desc = keyword.Desc
						.Replace("&a", GetStars(AppearanceCount))
						.Replace("&b", GetStars(CharmCount))
						.Replace("&c", GetStars(WornCount))
						.Replace("&d", GetStars(TotalCount))
						.Replace("&f", GetCotton(Cotton))
						.Replace("&g", GetPolyurethanes(Polyurethanes));
				}
			}
		}

		public class Standart : PantiesMain
		{
			public Standart()
			{
				AppearanceCount = 3;
				CharmCount = 3;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class FloweredPatterned : PantiesMain
		{
			public FloweredPatterned()
			{
				AppearanceCount = 3;
				CharmCount = 2;
				WornCount = 2;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class Regular : PantiesMain
		{
			public Regular()
			{
				AppearanceCount = 2;
				CharmCount = 3;
				WornCount = 4;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class Star : PantiesMain
		{
			public Star()
			{
				AppearanceCount = 3;
				CharmCount = 3;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class SimpleWhite : PantiesMain
		{
			public SimpleWhite()
			{
				AppearanceCount = 3;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}

		public class SimpleDotted : PantiesMain
		{
			public SimpleDotted()
			{
				AppearanceCount = 3;
				CharmCount = 3;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}

		public class SimpleStriped : PantiesMain
		{
			public SimpleStriped()
			{
				AppearanceCount = 3;
				CharmCount = 3;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}

		public class Striped : PantiesMain
		{
			public Striped()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class StandartStudent : PantiesMain
		{
			public StandartStudent()
			{
				AppearanceCount = 4;
				CharmCount = 3;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 65;
				Polyurethanes = 35;
			}
		}

		public class StripedStandart : PantiesMain
		{
			public StripedStandart()
			{
				AppearanceCount = 4;
				CharmCount = 2;
				WornCount = 2;
				TotalCount = 3;
				Cotton = 65;
				Polyurethanes = 35;
			}
		}

		public class FittedLace : PantiesMain
		{
			public FittedLace()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class FittedLaceGray : PantiesMain
		{
			public FittedLaceGray()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class Fluffy : PantiesMain
		{
			public Fluffy()
			{
				AppearanceCount = 1;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 100;
				Polyurethanes = 0;
			}
		}

		public class DottedFluffy : PantiesMain
		{
			public DottedFluffy()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 100;
				Polyurethanes = 0;
			}
		}

		public class CheckeredFluffy : PantiesMain
		{
			public CheckeredFluffy()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 100;
				Polyurethanes = 0;
			}
		}

		public class FittedRegular : PantiesMain
		{
			public FittedRegular()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 4;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class FittedPolkaDot : PantiesMain
		{
			public FittedPolkaDot()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 5;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class FittedPolkaDotGray : PantiesMain
		{
			public FittedPolkaDotGray()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 5;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class Frilly : PantiesMain
		{
			public Frilly()
			{
				AppearanceCount = 4;
				CharmCount = 1;
				WornCount = 1;
				TotalCount = 2;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}

		public class String : PantiesMain
		{
			public String()
			{
				AppearanceCount = 3;
				CharmCount = 1;
				WornCount = 1;
				TotalCount = 2;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}

		public class Pumpkin : PantiesMain
		{
			public Pumpkin()
			{
				AppearanceCount = 1;
				CharmCount = 2;
				WornCount = 2;
				TotalCount = 2;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}

		public class Sport : PantiesMain
		{
			public Sport()
			{
				AppearanceCount = 2;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 3;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class PinkRibbon : PantiesMain
		{
			public PinkRibbon()
			{
				AppearanceCount = 4;
				CharmCount = 3;
				WornCount = 2;
				TotalCount = 4;
				Cotton = 65;
				Polyurethanes = 35;
			}
		}

		public class Comfy : PantiesMain
		{
			public Comfy()
			{
				AppearanceCount = 3;
				CharmCount = 4;
				WornCount = 3;
				TotalCount = 4;
				Cotton = 95;
				Polyurethanes = 5;
			}
		}

		public class VelvetNoir : PantiesMain
		{
			public VelvetNoir()
			{
				AppearanceCount = 4;
				CharmCount = 3;
				WornCount = 2;
				TotalCount = 4;
				Cotton = 88;
				Polyurethanes = 12;
			}
		}
	}
}