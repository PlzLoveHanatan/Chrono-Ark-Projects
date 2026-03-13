using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MiyukiSone
{
	public class MiyukiVisual : MonoBehaviour
	{
		private static MiyukiVisual _instance;

		public static MiyukiVisual Instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject go = new GameObject("MiyukiVisualManager");
					_instance = go.AddComponent<MiyukiVisual>();

					Canvas canvas = GameObject.FindObjectOfType<Canvas>();
					if (canvas != null)
					{
						go.transform.SetParent(canvas.transform, false);
					}
				}
				return _instance;
			}
		}

		[Header("Heart Settings")]
		public string heartPath = "MiyukiVisual/Effects/heart.png";
		public int initialHeartPoolSize = 20;

		[Header("Star Settings")]
		public string starPath = "MiyukiVisual/Effects/star.png";
		public int initialStarPoolSize = 20;

		[Header("Animation Settings")]
		public float moveXRange = 150f;
		public float moveYMin = 200f;
		public float moveYMax = 350f;
		public float minDuration = 0.8f;
		public float maxDuration = 2f;
		public float pulseStrength = 0.3f;

		private Queue<GameObject> heartPool = new Queue<GameObject>();
		private Queue<GameObject> starPool = new Queue<GameObject>();

		private void Awake()
		{
			_instance = this;
			InitializeHeartPool();
			InitializeStarPool();
		}

		private void InitializeHeartPool()
		{
			Sprite sprite = UtilsUI.GetSprite(heartPath);
			if (sprite == null) return;

			for (int i = 0; i < initialHeartPoolSize; i++)
			{
				GameObject obj = CreateVisualObject(sprite, "Heart");
				obj.SetActive(false);
				heartPool.Enqueue(obj);
			}
		}

		private void InitializeStarPool()
		{
			Sprite sprite = UtilsUI.GetSprite(starPath);
			if (sprite == null) return;

			for (int i = 0; i < initialStarPoolSize; i++)
			{
				GameObject obj = CreateVisualObject(sprite, "Star");
				obj.SetActive(false);
				starPool.Enqueue(obj);
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

			return obj;
		}

		public void SpawnHearts(Transform target)
		{
			if (target == null) return;

			RectTransform targetRT = target.GetComponent<RectTransform>();
			if (targetRT == null)
			{
				Debug.LogError("[MiyukiVisual] Target has no RectTransform: " + target.name);
				return;
			}

			StartCoroutine(SpawnRoutine(targetRT, heartPool, true));
		}

		public void SpawnStars(Transform target)
		{
			if (target == null) return;

			RectTransform targetRT = target.GetComponent<RectTransform>();
			if (targetRT == null)
			{
				Debug.LogError("[MiyukiVisual] Target has no RectTransform: " + target.name);
				return;
			}

			StartCoroutine(SpawnRoutine(targetRT, starPool, false));
		}

		private IEnumerator SpawnRoutine(RectTransform targetRT, Queue<GameObject> pool, bool useRandomColor)
		{
			if (targetRT == null) yield break;

			int count = Random.Range(5, 15);

			for (int i = 0; i < count; i++)
			{
				GameObject obj = GetFromPool(pool);
				if (obj == null) yield break;

				RectTransform rt = obj.GetComponent<RectTransform>();
				Image img = obj.GetComponent<Image>();

				Vector2 spawnPos = targetRT.anchoredPosition;

				Vector2 offset = new Vector2(
					Random.Range(-20f, 20f),
					Random.Range(-20f, 20f)
				);

				rt.anchoredPosition = spawnPos + offset;
				rt.localScale = Vector3.one;

				if (useRandomColor)
					img.color = GetRandomColor();
				else
					img.color = Color.white;

				StartCoroutine(Animate(rt, img, obj, pool));

				yield return new WaitForSeconds(0.05f);
			}
		}

		private IEnumerator Animate(RectTransform rt, Image img, GameObject obj, Queue<GameObject> pool)
		{
			if (rt == null || img == null || obj == null || pool == null) yield break;

			Vector3 startPos = rt.localPosition;
			Vector3 endPos = startPos + new Vector3(
				Random.Range(-moveXRange, moveXRange),
				Random.Range(moveYMin, moveYMax),
				0
			);

			float duration = Random.Range(minDuration, maxDuration);
			if (duration <= 0f) duration = 1f;

			float elapsed = 0f;
			float pulsePhase = Random.Range(0f, Mathf.PI * 2f);

			Color startColor = img.color;

			while (elapsed < duration)
			{
				if (!obj.activeInHierarchy) yield break;

				elapsed += Time.deltaTime;
				float p = elapsed / duration;
				float eased = 1f - Mathf.Pow(1f - p, 2f);

				rt.localPosition = Vector3.Lerp(startPos, endPos, eased);
				rt.localScale = Vector3.one * (1f + Mathf.Sin(p * Mathf.PI * 2f + pulsePhase) * pulseStrength);

				Color c = Color.Lerp(startColor, Color.white, Mathf.Sin(p * Mathf.PI));
				c.a = 1f - p;
				img.color = c;

				yield return null;
			}

			ReturnToPool(obj, pool);
		}

		private GameObject GetFromPool(Queue<GameObject> pool)
		{
			if (pool.Count > 0)
			{
				GameObject obj = pool.Dequeue();
				obj.SetActive(true);
				return obj;
			}
			return null;
		}

		private void ReturnToPool(GameObject obj, Queue<GameObject> pool)
		{
			obj.SetActive(false);
			pool.Enqueue(obj);
		}

		private Color GetRandomColor()
		{
			return new Color(
				Random.Range(0.7f, 1f),
				Random.Range(0.5f, 0.7f),
				Random.Range(0.5f, 0.7f),
				1f
			);
		}
	}
}