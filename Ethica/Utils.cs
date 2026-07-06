using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace Ethica
{
	public static  class Utils
	{
		public static BattleChar Ethica => BattleSystem.instance.AllyTeam?.AliveChars.FirstOrDefault(c => c.Info?.KeyData == ModItemKeys.Character_Ethica);
		public static bool EthicaInParty => PlayData.TSavedata.Party.Any(c => c?.KeyData == ModItemKeys.Character_Ethica);

		private static readonly Lazy<List<GDESkillData>> _allGameSkills = new Lazy<List<GDESkillData>>(() =>
		{
			var list = new List<GDESkillData>();
			list.AddRange(PlayData.ALLSKILLLIST);
			list.AddRange(PlayData.ALLRARESKILLLIST);
			return list;
		});

		public static List<GDESkillData> AllGameSkills => _allGameSkills.Value;

		public static T Let<T>(this T obj, Action<T> action)
		{
			if (obj != null) action(obj);
			return obj;
		}

		public static TResult Let<T, TResult>(this T obj, Func<T, TResult> selector)
		{
			return selector(obj);
		}

		public static T RandomElement<T>(this IEnumerable<T> source, string key = null)
		{
			var list = source.ToList();
			if (string.IsNullOrEmpty(key)) key = "EthicaRandom";
			return list.Count == 0 ? default : list.Random(key);
		}
	}
}
