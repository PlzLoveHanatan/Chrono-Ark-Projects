using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using ChronoArkMod;

namespace EnDub
{
	public class Console : MonoBehaviour
	{
		private static Console _instance;
		public static Console Instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject go = new GameObject("Console");
					_instance = go.AddComponent<Console>();
					DontDestroyOnLoad(go);
				}
				return _instance;
			}
		}

		private bool _showConsole = false;
		private bool _focusInput = false;

		private string _input = "";
		private Vector2 _scrollPosition;
		private readonly List<string> _outputLog = new List<string>();

		private Rect _windowRect = new Rect(100, 100, 800, 500);
		private const int MAX_LOG = 50;

		private bool _isResizing = false;
		private Vector2 _resizeStartMouse;
		private Rect _resizeStartRect;
		private const float RESIZE_AREA = 18f;

		private Texture2D _backgroundTexture;

		private Dictionary<string, System.Action<string[]>> _commands;

		private bool _autoScroll = true;

		void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
				_backgroundTexture = Utils.GetSpriteFromMod("watermark.png")?.texture;
				InitCommands();
				ShowHelp();
			}			
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.BackQuote) && EnDubModSettings.Console /*Input.GetKeyDown(KeyCode.F12)*/)
			{
				_showConsole = !_showConsole;
				_input = "";
				_focusInput = true;
			}

			if (_backgroundTexture == null && !EnDubModSettings.SimpleBackground)
			{
				var sprite = Utils.GetSpriteFromMod("watermark.png");
				_backgroundTexture = sprite.texture;
				Log("Background loaded successfully");
			}
		}

		void OnGUI()
		{
			if (!_showConsole) return;

			if (_backgroundTexture != null && !EnDubModSettings.SimpleBackground)
			{
				GUI.DrawTexture(_windowRect, _backgroundTexture, ScaleMode.StretchToFill);
			}
			else
			{
				GUI.DrawTexture(_windowRect, Texture2D.blackTexture);
				GUI.color = new Color(0, 0, 0, 0.4f);
			}

			_windowRect = GUI.Window(115, _windowRect, DrawConsoleWindow, "EnDub Console");
		}

		private void DrawConsoleWindow(int windowID)
		{
			float padding = 100f;

			_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(_windowRect.height - padding));

			if (_autoScroll)
			{
				_scrollPosition.y = float.MaxValue;
				_autoScroll = false;
			}

			foreach (string log in _outputLog)
			{
				GUILayout.Label(log);
			}

			if (Event.current.type == EventType.ScrollWheel)
			{
				_autoScroll = false;
			}

			GUILayout.EndScrollView();
			GUILayout.BeginHorizontal();
			GUI.SetNextControlName("ConsoleInput");
			_input = GUILayout.TextField(_input);

			if (_focusInput)
			{
				GUI.FocusControl("ConsoleInput");
				_focusInput = false;
			}

			Event e = Event.current;

			if (e.isKey && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter))
			{
				if (GUI.GetNameOfFocusedControl() == "ConsoleInput")
				{
					ProcessCommand(_input);
					_input = "";
					_focusInput = true;
					e.Use();
				}
			}

			if (GUILayout.Button("Run", GUILayout.Width(80)))
			{
				ProcessCommand(_input);
				_input = "";
				_focusInput = true;
			}

			if (GUILayout.Button("Clear", GUILayout.Width(60)))
			{
				_outputLog.Clear();
				ShowHelp();
				_autoScroll = true;
			}

			if (GUILayout.Button("Close", GUILayout.Width(60)))
			{
				_showConsole = false;
			}

			GUILayout.EndHorizontal();

			Rect resizeRect = new Rect(_windowRect.width - RESIZE_AREA, _windowRect.height - RESIZE_AREA, RESIZE_AREA, RESIZE_AREA);
			GUI.Box(resizeRect, "");

			Vector2 mouse = Event.current.mousePosition;

			if (Event.current.type == EventType.MouseDown && resizeRect.Contains(mouse))
			{
				_isResizing = true;
				_resizeStartMouse = mouse;
				_resizeStartRect = _windowRect;
			}

			if (_isResizing && Event.current.type == EventType.MouseDrag)
			{
				Vector2 delta = mouse - _resizeStartMouse;
				_windowRect.width = Mathf.Max(300, _resizeStartRect.width + delta.x);
				_windowRect.height = Mathf.Max(200, _resizeStartRect.height + delta.y);
				Event.current.Use();
			}

			if (Event.current.type == EventType.MouseUp)
			{
				_isResizing = false;
			}

			GUI.DragWindow();
		}

		private void ProcessCommand(string fullCommand)
		{
			if (string.IsNullOrWhiteSpace(fullCommand)) return;

			Log($"> {fullCommand}");

			string[] parts = fullCommand.Trim().Split(' ');
			string cmd = parts[0].ToLower();
			string[] args = parts.Skip(1).ToArray();

			if (_commands.TryGetValue(cmd, out var action))
			{
				action(args);
			}
			else
			{
				Log($"Unknown command: {cmd}");
			}
		}

		private void InitCommands()
		{
			var saveData = SaveManager.Instance.CurrentData;

			_commands = new Dictionary<string, System.Action<string[]>>
			{
				["main"] = (args) =>
				{
					if (args.Length == 0)
					{
						Log($"Main: {saveData.MainAudioVolume}");
						return;
					}

					if (float.TryParse(args[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float vol))
					{
						vol = Mathf.Clamp(vol, 0f, 6f);
						saveData.MainAudioVolume = vol;
						SaveManager.Instance.Save();
						Log($"Main set to {vol}");
					}
					else
					{
						Log($"Invalid number: {args[0]}");
					}
				},

				["restore"] = (args) =>
				{
					Log("Restoring original dialogue texts...");
					try
					{
						DialogueFixer.Restore();
						Log("Restore complete!");
					}
					catch (System.Exception ex)
					{
						Log($"Restore failed: {ex.Message}");
					}
				},

				["loadfixes"] = (args) =>
				{
					Log("Applying dialogue fixes...");
					try
					{
						DialogueFixer.Initialize();
						Log("Fixes applied successfully!");
					}
					catch (System.Exception ex)
					{
						Log($"Failed to apply fixes: {ex.Message}");
					}
				}
			};

			AddCharacter("azar", v => saveData.Azar = v);
			AddCharacter("charon", v => saveData.Charon = v);
			AddCharacter("huz", v => saveData.Huz = v);
			AddCharacter("johan", v => saveData.Johan = v);
			AddCharacter("lian", v => saveData.Lian = v);
			AddCharacter("narhan", v => saveData.Narhan = v);
			AddCharacter("silverstein", v => saveData.Silverstein = v);
			AddCharacter("sizz", v => saveData.Sizz = v);

			_commands["clear"] = (args) =>
			{
				_outputLog.Clear();
				ShowHelp();
				_autoScroll = true;
			};

			_commands["help"] = (args) => ShowHelp();
		}

		private void AddCharacter(string name, System.Action<float> setter)
		{
			_commands[name] = (args) =>
			{
				var saveData = SaveManager.Instance.CurrentData;

				string key = name.Substring(0, 1).ToUpper() + name.Substring(1);
				float? current = SaveManager.Instance.GetCharacterVolume(key);

				if (args.Length == 0)
				{
					Log($"{key}: {current}");
					return;
				}

				if (float.TryParse(args[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float volF))
				{
					float vol = Mathf.Clamp(volF, 0f, 6f);
					setter(vol);
					SaveManager.Instance.Save();
					Log($"{key} set to {vol}");
				}
			};
		}

		private void ShowHelp()
		{
			Log("======================================");
			Log("         EN-DUB CONSOLE HELP         ");
			Log("======================================");
			Log("");

			Log("MAIN AUDIO VOLUME");
			Log("  main [0-6]            - Set global voice volume (float, e.g. 2.5)");
			Log("                         Used as fallback for all characters");
			Log("");

			Log("INDIVIDUAL CHARACTER VOLUME");
			Log("  azar [0-6]            - Azar voice volume");
			Log("  charon [0-6]          - Charon voice volume");
			Log("  huz [0-6]             - Huz voice volume");
			Log("  johan [0-6]           - Johan voice volume");
			Log("  lian [0-6]            - Lian voice volume");
			Log("  narhan [0-6]          - Narhan voice volume");
			Log("  silverstein [0-6]     - Silverstein voice volume");
			Log("  sizz [0-6]            - Sizz voice volume");
			Log("");
			Log("  NOTE: If character volume = 0, Main Audio volume will be used instead");
			Log("  NOTE: You can use float values (e.g. 2.5, 3.7, 4.2)");
			Log("");

			Log("SYSTEM");
			Log("  help                  - Show this help");
			Log("  clear                 - Clear console output");
			Log("  restore               - Restore original dialogue texts (CSV + JSON)");
			Log("  loadfixes             - Load changed dialogue texts (CSV + JSON) for Mod compability");
			Log("");

			Log("======================================");
		}

		private void Log(string message)
		{
			if (string.IsNullOrWhiteSpace(message)) return;
			_outputLog.Add($"[{System.DateTime.Now:HH:mm:ss}] {message}");
			if (_outputLog.Count > MAX_LOG) _outputLog.RemoveAt(0);
			_autoScroll = true;
			//Debug.Log("[Console] " + message);
		}

		public void ShowConsole()
		{
			_showConsole = true;
			_focusInput = true;
		}
	}
}