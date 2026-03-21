using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using I2.Loc;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class MiyukiJsonReader
	{
		public static string GetFullPath(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ThisMod.DirectoryName, "Assets/JsonData", fileName);
		}

		public static string LoadJson(string fileName)
		{
			string path = GetFullPath(fileName);

			if (!File.Exists(path))
			{
				Debug.LogError($"[MiyukiJsonReader] Файл не найден: {path}");
				return null;
			}

			try
			{
				return File.ReadAllText(path);
			}
			catch (Exception ex)
			{
				Debug.LogError($"[MiyukiJsonReader] Ошибка чтения файла {fileName}: {ex.Message}");
				return null;
			}
		}

		public static bool TryLoadJson(string fileName, out string content)
		{
			content = LoadJson(fileName);
			return content != null;
		}
	}
}