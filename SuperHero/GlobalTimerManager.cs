using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimerManager : MonoBehaviour
{
    public static GlobalTimerManager Instance { get; private set; }

    private float timer = 0f;
    private bool isRunning = false;
    private bool isPaused = false;
    private bool wasPaused = false;

    public Text TimerText => timerText;
    public GameObject TimerUI => timerUI;
    public bool IsRunning => isRunning;
    public bool IsPaused => isPaused;
    public bool WasPaused => wasPaused;

    private static string savedTime = "00:00:00";

    private GameObject timerUI;
    private Text timerText;
    private Image[] timerImages;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUI();
            Debug.Log("[GlobalTimerManager] Awake and initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadUI()
    {
        timerUI = Instantiate(Resources.Load("Prefebs/TrialOfTime") as GameObject);

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            timerUI.transform.SetParent(canvas.transform, false);
            Debug.Log("[GlobalTimerManager] Timer UI parent set to Canvas.");
        }
        else
        {
            Debug.LogWarning("[GlobalTimerManager] Canvas not found. Timer UI instantiated without parent.");
        }

        RectTransform rectTransform = timerUI.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(400f, 485f);
            Debug.Log("[GlobalTimerManager] Timer UI position set to (400, 485).");
        }

        timerText = timerUI.GetComponentInChildren<Text>();
        timerImages = timerUI.GetComponentsInChildren<Image>(true);

        Debug.Log("[GlobalTimerManager] Timer UI loaded.");
    }

    // Перезапуск таймера — если есть сохранённое время, загружаем его
    public void StartTimer()
    {
        if (savedTime != "00:00:00")
        {
            timer = ParseTimeString(savedTime);
            Debug.Log($"[GlobalTimerManager] Timer started from saved time: {savedTime}");
        }
        else
        {
            timer = 0f;
            Debug.Log("[GlobalTimerManager] Timer started from zero.");
        }

        isRunning = true;
        isPaused = false;
    }

    public void StopTimer()
    {
        isRunning = false;
        Debug.Log("[GlobalTimerManager] Timer stopped.");
    }

    public void PauseTimer()
    {
        if (isRunning && !isPaused)
        {
            isPaused = true;
            Debug.Log("[GlobalTimerManager] Timer paused.");
        }
    }

    public void ResumeTimer()
    {
        if (isRunning && isPaused)
        {
            isPaused = false;
            Debug.Log("[GlobalTimerManager] Timer resumed.");
        }
    }

    public void Update()
    {
        if (!isRunning) return;
        if (timerText == null) return;

        bool isCurrentlyPaused = PauseWindow.IsOn;

        if (isCurrentlyPaused && !wasPaused)
        {
            PauseTimer();
        }
        else if (!isCurrentlyPaused && wasPaused)
        {
            ResumeTimer();
        }

        wasPaused = isCurrentlyPaused;

        if (isPaused) return;

        timer += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}",
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds / 10);
    }

    public void DestroySelfAndUI()
    {
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        if (timerUI != null)
        {
            Destroy(timerUI);
            Debug.Log("[GlobalTimerManager] Timer UI destroy requested.");
        }

        if (timerImages != null)
        {
            foreach (var img in timerImages)
            {
                if (img != null)
                {
                    Destroy(img.gameObject);
                }
            }
            Debug.Log("[GlobalTimerManager] All timer UI images destroy requested.");
        }

        yield return null;

        Destroy(this.gameObject);
        Debug.Log("[GlobalTimerManager] Timer Manager destroyed.");
    }

    public static void SaveCurrentTime()
    {
        if (Instance != null)
        {
            savedTime = Instance.GetCurrentTimeString();
            Debug.Log($"[GlobalTimerManager] Current time saved: {savedTime}");
        }
        else
        {
            Debug.LogWarning("[GlobalTimerManager] Cannot save time, instance is null.");
        }
    }

    public static string GetSavedTime()
    {
        return savedTime;
    }

    public string GetCurrentTimeString()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        return string.Format("{0:00}:{1:00}:{2:00}",
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds / 10);
    }

    // Парсит строку формата "mm:ss:ms" в секунды (float)
    private float ParseTimeString(string timeString)
    {
        try
        {
            var parts = timeString.Split(':');
            if (parts.Length != 3)
                return 0f;

            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            int milliseconds = int.Parse(parts[2]);

            return minutes * 60f + seconds + (milliseconds / 100f);
        }
        catch (Exception e)
        {
            Debug.LogWarning("[GlobalTimerManager] Failed to parse time string: " + e.Message);
            return 0f;
        }
    }
    public void ResetTimer()
    {
        if (Instance != null)
        {
            Instance.timer = 0f;
            Instance.isRunning = false;
            Instance.isPaused = false;
            savedTime = "00:00:00";
            if (Instance.timerText != null)
            {
                Instance.timerText.text = savedTime;
            }
            Debug.Log("[GlobalTimerManager] Timer reset.");
        }
        else
        {
            savedTime = "00:00:00";
            Debug.LogWarning("[GlobalTimerManager] Cannot reset timer, instance is null.");
        }
    }
}
