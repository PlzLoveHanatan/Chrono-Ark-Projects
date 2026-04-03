using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MiyukiSone
{
	public class MiyukiVisual : MonoBehaviour
	{
		private static MiyukiVisual _instance;
		private static bool _isDestroying = false;

		public static MiyukiVisual Instance
		{
			get
			{
				if (_isDestroying) return null;

				if (_instance == null)
				{
					GameObject go = GameObject.Find("MiyukiVisualManager");
					if (go == null)
					{
						go = new GameObject("MiyukiVisualManager");
						_instance = go.AddComponent<MiyukiVisual>();

						Canvas canvas = GameObject.FindObjectOfType<Canvas>();
						if (canvas != null)
						{
							go.transform.SetParent(canvas.transform, false);
						}

						RectTransform rt = go.GetComponent<RectTransform>();
						if (rt == null) rt = go.AddComponent<RectTransform>();
						rt.anchorMin = Vector2.zero;
						rt.anchorMax = Vector2.one;
						rt.offsetMin = Vector2.zero;
						rt.offsetMax = Vector2.zero;
					}
					else
					{
						_instance = go.GetComponent<MiyukiVisual>();
						if (_instance == null)
						{
							_instance = go.AddComponent<MiyukiVisual>();
						}
					}

					DontDestroyOnLoad(_instance.gameObject);
				}
				return _instance;
			}
		}

		private GameObject particlesObject;
		private Coroutine particleRoutine;

		[Header("Heart Settings")]
		public string heartPath = "MiyukiVisual/Effects/heart.png";
		public int initialHeartPoolSize = 30;

		[Header("Petal Settings")]
		public string petalPath = "MiyukiVisual/Effects/petal.png";
		public int initialPetalPoolSize = 30;

		public float pulseStrength = 0.3f;
		public float heartChance = 0.3f;

		private Queue<GameObject> heartPool = new Queue<GameObject>();
		private Queue<GameObject> petalPool = new Queue<GameObject>();

		#region Settings Classes

		[System.Serializable]
		public class ParticleSettings
		{
			public float spawnInterval = 0.8f;
			public int minParticles = 3;
			public int maxParticles = 8;
			public float moveXRange = 150f;
			public float moveYMin = 200f;
			public float moveYMax = 350f;
			public float minDuration = 0.8f;
			public float maxDuration = 2f;
			public bool randomColors = true;
			public Vector2 spawnOffset = Vector2.zero;
			public Vector2 randomOffsetRange = Vector2.zero;
			public float spawnHeight = 0f;
		}

		public static ParticleSettings DefaultSettings = new ParticleSettings();

		public static ParticleSettings CharStatSettings = new ParticleSettings
		{
			spawnInterval = 0.8f,
			minParticles = 2,
			maxParticles = 6,
			moveXRange = 400f,
			moveYMin = 600f,
			moveYMax = 1500f,
			minDuration = 4f,
			maxDuration = 10f,
			randomColors = false,
			randomOffsetRange = new Vector2(300f, 50f),
			spawnHeight = 600f
		};

		public static ParticleSettings PauseSettings = new ParticleSettings
		{
			spawnInterval = 0.8f,
			minParticles = 2,
			maxParticles = 6,
			moveXRange = 400f,
			moveYMin = 600f,
			moveYMax = 1500f,
			minDuration = 4f,
			maxDuration = 10f,
			randomColors = false,
			randomOffsetRange = new Vector2(300f, 50f),
			spawnHeight = 600f
		};

		public static ParticleSettings ButtonSettings = new ParticleSettings
		{
			spawnInterval = 0.1f,
			minParticles = 8,
			maxParticles = 20,
			moveXRange = 100f,
			moveYMin = -100f,
			moveYMax = -250f,
			minDuration = 0.8f,
			maxDuration = 2.5f,
			randomColors = true,
			randomOffsetRange = new Vector2(25f, 0f),
			spawnHeight = 50f
		};

		#endregion

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
				return;
			}

			_instance = this;
			DontDestroyOnLoad(gameObject);
			InitializeHeartPool();
			InitializePetalPool();
		}

		private void InitializeHeartPool()
		{
			Sprite sprite = UtilsUI.GetSpriteFromMod(heartPath);
			if (sprite == null)
			{
				Debug.LogError($"[MiyukiVisual] Failed to load heart sprite: {heartPath}");
				return;
			}

			for (int i = 0; i < initialHeartPoolSize; i++)
			{
				GameObject obj = CreateVisualObject(sprite, "Heart");
				obj.SetActive(false);
				heartPool.Enqueue(obj);
			}
		}

		private void InitializePetalPool()
		{
			Sprite sprite = UtilsUI.GetSpriteFromMod(petalPath);
			if (sprite == null)
			{
				Debug.LogError($"[MiyukiVisual] Failed to load petal sprite: {petalPath}");
				return;
			}

			for (int i = 0; i < initialPetalPoolSize; i++)
			{
				GameObject obj = CreateVisualObject(sprite, "Petal");
				obj.SetActive(false);
				petalPool.Enqueue(obj);
			}
		}

		private GameObject CreateVisualObject(Sprite sprite, string name)
		{
			GameObject obj = new GameObject(name);
			obj.transform.SetParent(transform, false);

			Image img = obj.AddComponent<Image>();
			img.sprite = sprite;
			img.raycastTarget = false;

			RectTransform rt = obj.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(40, 40);
			rt.pivot = new Vector2(0.5f, 0.5f);

			return obj;
		}

		private void ReturnAllToPools()
		{
			foreach (Transform child in transform)
			{
				if (child.gameObject.activeSelf)
				{
					if (child.name.Contains("Heart"))
						ReturnToPool(child.gameObject, heartPool);
					else if (child.name.Contains("Petal"))
						ReturnToPool(child.gameObject, petalPool);
				}
			}
		}

		#region Public Spawn Methods

		public void StartParticlesOnTransform(Transform parent, bool isPetal, ParticleSettings settings = null)
		{
			StopParticles();

			if (settings == null) settings = DefaultSettings;

			particlesObject = new GameObject("MiyukiParticles");
			particlesObject.transform.SetParent(parent, false);

			RectTransform rt = particlesObject.AddComponent<RectTransform>();
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.one;
			rt.offsetMin = Vector2.zero;
			rt.offsetMax = Vector2.zero;

			particleRoutine = StartCoroutine(ParticleRoutine(isPetal, settings));
		}

		public void StartParticlesOneShot(Transform target, bool isPetal, ParticleSettings settings = null)
		{
			if (settings == null) settings = ButtonSettings;

			if (isPetal)
				SpawnPetals(target, settings);
			else
				SpawnHearts(target, settings);
		}

		public void StopParticles()
		{
			if (particleRoutine != null)
			{
				StopCoroutine(particleRoutine);
				particleRoutine = null;
			}

			if (particlesObject != null)
			{
				Destroy(particlesObject);
				particlesObject = null;
			}

			ReturnAllToPools();
		}

		private IEnumerator ParticleRoutine(bool isPetal, ParticleSettings settings)
		{
			// Получаем родителя один раз в начале корутины
			Transform parentTransform = particlesObject.transform;
			RectTransform parentRT = parentTransform.GetComponent<RectTransform>();

			while (particlesObject != null && parentTransform != null)
			{
				if (isPetal)
					SpawnPetals(parentTransform, settings);
				else
					SpawnHearts(parentTransform, settings);

				yield return new WaitForSeconds(Random.Range(settings.spawnInterval * 0.7f, settings.spawnInterval * 1.3f));
			}
		}

		public void SpawnHearts(Transform target, ParticleSettings settings = null)
		{
			if (target == null) return;

			if (settings == null) settings = ButtonSettings;

			RectTransform targetRT = target.GetComponent<RectTransform>();
			if (targetRT == null)
			{
				Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
				StartCoroutine(SpawnRoutineFromPosition(screenPos + settings.spawnOffset, heartPool, true, settings));
				return;
			}

			StartCoroutine(SpawnRoutine(targetRT, heartPool, true, settings));
		}

		public void SpawnPetals(Transform target, ParticleSettings settings = null)
		{
			if (target == null) return;

			if (settings == null)
				settings = ButtonSettings;

			RectTransform targetRT = target.GetComponent<RectTransform>();
			if (targetRT == null)
			{
				Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
				StartCoroutine(SpawnRoutineFromPosition(screenPos + settings.spawnOffset, petalPool, false, settings));
				return;
			}

			StartCoroutine(SpawnRoutine(targetRT, petalPool, false, settings));
		}

		#endregion

		#region Spawn Routines

		private IEnumerator SpawnRoutine(RectTransform targetRT, Queue<GameObject> pool, bool useRandomColor, ParticleSettings settings)
		{
			if (targetRT == null) yield break;

			// Сохраняем ссылку на родительский RectTransform
			RectTransform parentRT = targetRT;

			int count = Random.Range(settings.minParticles, settings.maxParticles);

			for (int i = 0; i < count; i++)
			{
				// Проверяем что родитель все еще существует
				if (parentRT == null || parentRT.gameObject == null)
				{
					Debug.LogWarning("[MiyukiVisual] Parent was destroyed during spawn");
					yield break;
				}

				GameObject obj = GetFromPool(pool);
				if (obj == null) yield break;

				RectTransform rt = obj.GetComponent<RectTransform>();
				Image img = obj.GetComponent<Image>();

				Vector2 spawnPos = parentRT.anchoredPosition + settings.spawnOffset;
				spawnPos.x += Random.Range(-settings.randomOffsetRange.x, settings.randomOffsetRange.x);
				spawnPos.y += settings.spawnHeight + Random.Range(-settings.randomOffsetRange.y, settings.randomOffsetRange.y);

				Vector2 offset = new Vector2(
					Random.Range(-20f, 20f),
					Random.Range(-20f, 20f)
				);

				rt.anchoredPosition = spawnPos + offset;
				rt.localScale = Vector3.one;
				rt.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

				if (useRandomColor && settings.randomColors)
					img.color = GetRandomHeartColor();
				else if (!useRandomColor && settings.randomColors)
					img.color = GetRandomPetalColor();
				else
					img.color = Color.white;

				StartCoroutine(Animate(rt, img, obj, pool, settings));

				yield return new WaitForSeconds(0.05f);
			}
		}

		private IEnumerator SpawnRoutineFromPosition(Vector2 position, Queue<GameObject> pool, bool useRandomColor, ParticleSettings settings)
		{
			int count = Random.Range(settings.minParticles, settings.maxParticles);

			for (int i = 0; i < count; i++)
			{
				GameObject obj = GetFromPool(pool);
				if (obj == null) yield break;

				RectTransform rt = obj.GetComponent<RectTransform>();
				Image img = obj.GetComponent<Image>();

				Vector2 spawnPos = position;
				spawnPos.y = settings.spawnHeight;

				Vector2 offset = new Vector2(
					Random.Range(-50f, 50f),
					Random.Range(-30f, 30f)
				);

				rt.anchoredPosition = spawnPos + offset;
				rt.localScale = Vector3.one;
				rt.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

				if (useRandomColor && settings.randomColors)
					img.color = GetRandomHeartColor();
				else if (!useRandomColor && settings.randomColors)
					img.color = GetRandomPetalColor();
				else
					img.color = Color.white;

				StartCoroutine(Animate(rt, img, obj, pool, settings));

				yield return new WaitForSeconds(0.05f);
			}
		}

		private IEnumerator Animate(RectTransform rt, Image img, GameObject obj, Queue<GameObject> pool, ParticleSettings settings)
		{
			if (rt == null || img == null || obj == null || pool == null) yield break;

			Vector3 startPos = rt.localPosition;
			Vector3 endPos = startPos + new Vector3(
				Random.Range(-settings.moveXRange, settings.moveXRange),
				-Random.Range(settings.moveYMin, settings.moveYMax),
				0
			);

			float duration = Random.Range(settings.minDuration, settings.maxDuration);
			if (duration <= 0f) duration = 1f;

			float elapsed = 0f;
			float pulsePhase = Random.Range(0f, Mathf.PI * 2f);
			float startRotation = rt.rotation.eulerAngles.z;
			float endRotation = startRotation + Random.Range(-180f, 180f);

			Color startColor = img.color;

			while (elapsed < duration)
			{
				if (!obj.activeInHierarchy) yield break;

				elapsed += Time.deltaTime;
				float p = elapsed / duration;
				float eased = 1f - Mathf.Pow(1f - p, 2f);

				rt.localPosition = Vector3.Lerp(startPos, endPos, eased);
				rt.localScale = Vector3.one * (1f + Mathf.Sin(p * Mathf.PI * 2f + pulsePhase) * pulseStrength);

				float rotation = Mathf.Lerp(startRotation, endRotation, p);
				rt.rotation = Quaternion.Euler(0, 0, rotation);

				Color c = Color.Lerp(startColor, new Color(1f, 1f, 1f, 0f), p);
				c.a = 1f - p;
				img.color = c;

				yield return null;
			}

			ReturnToPool(obj, pool);
		}

		#endregion

		private GameObject GetFromPool(Queue<GameObject> pool)
		{
			if (pool.Count > 0)
			{
				GameObject obj = pool.Dequeue();
				if (obj != null) obj.SetActive(true);
				return obj;
			}
			return null;
		}

		private void ReturnToPool(GameObject obj, Queue<GameObject> pool)
		{
			if (obj == null || pool == null) return;
			obj.SetActive(false);
			pool.Enqueue(obj);
		}

		private Color GetRandomHeartColor()
		{
			return new Color(
				Random.Range(0.8f, 1f),
				Random.Range(0.3f, 0.6f),
				Random.Range(0.4f, 0.7f),
				1f
			);
		}

		private Color GetRandomPetalColor()
		{
			return new Color(
				Random.Range(0.8f, 1f),
				Random.Range(0.6f, 0.9f),
				Random.Range(0.7f, 1f),
				0.9f
			);
		}

		private void OnDestroy()
		{
			if (_instance == this)
			{
				_isDestroying = true;
				StopParticles();
				_instance = null;
			}
		}

		private void OnApplicationQuit()
		{
			_isDestroying = true;
		}
	}
}