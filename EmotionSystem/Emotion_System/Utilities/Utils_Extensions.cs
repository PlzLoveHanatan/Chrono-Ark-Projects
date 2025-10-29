using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using EmotionSystem;
using HarmonyLib;

namespace EmotionSystem
{
	public static class Utils_Extensions
	{
		public static T GetField<T>(this object obj, string fieldName)
		{
			return Traverse.Create(obj).Field<T>(fieldName).Value;
		}

		public static void SetField<T>(this object obj, string fieldName, T value)
		{
			Traverse.Create(obj).Field<T>(fieldName).Value = value;
		}

		public static string GetTranslation(this string key)
		{
			try
			{
				return ModManager.getModInfo(EmotionlSystem_Plugin.modname).localizationInfo.
					SystemLocalizationUpdate(EmotionlSystem_Plugin.modname + "/" + key);
			}
			catch
			{
			}
			return key;
		}
	}
}
