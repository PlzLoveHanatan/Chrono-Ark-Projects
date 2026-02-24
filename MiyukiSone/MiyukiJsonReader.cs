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
	// ============= ХЕЛПЕР ТОЛЬКО ДЛЯ ЧТЕНИЯ ФАЙЛОВ =============
	public static class MiyukiJsonReader
	{
		/// <summary>
		/// Возвращает полный путь к JSON-файлу в папке Assets мода
		/// </summary>
		public static string GetFullPath(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ThisMod.DirectoryName, "Assets", fileName);
		}

		/// <summary>
		/// Загружает содержимое JSON-файла. Возвращает null если файл не найден или ошибка чтения
		/// </summary>
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

		/// <summary>
		/// Пытается загрузить JSON-файл. Возвращает true при успехе
		/// </summary>
		public static bool TryLoadJson(string fileName, out string content)
		{
			content = LoadJson(fileName);
			return content != null;
		}
	}
}