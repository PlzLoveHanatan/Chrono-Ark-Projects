using UnityEngine;
using UnityEngine.EventSystems;

namespace MiyukiSone
{
	public class DialogueBoxDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		[Header("Drag Settings")]
		public bool allowDrag = true;
		public RectTransform boundaryRectTransform; // Для ограничения перемещения

		private bool isDragging = false;
		private Vector3 dragOffset;
		private RectTransform rectTransform;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();

			// Если boundaryRectTransform не задан, используем родителя
			if (boundaryRectTransform == null && transform.parent != null)
			{
				boundaryRectTransform = transform.parent.GetComponent<RectTransform>();
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!allowDrag || eventData.button != PointerEventData.InputButton.Left) return;

			isDragging = true;
			transform.SetAsLastSibling();

			// Ключевое исправление: используем boundaryRectTransform для конвертации координат
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
				boundaryRectTransform != null ? boundaryRectTransform : rectTransform,
				eventData.position,
				eventData.pressEventCamera,
				out Vector2 localPoint))
			{
				// Вычисляем offset между позицией окна и точкой клика
				dragOffset = rectTransform.localPosition - new Vector3(localPoint.x, localPoint.y, 0);
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!isDragging || !allowDrag) return;

			// Ключевое исправление: используем тот же RectTransform что и в OnPointerDown
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
				boundaryRectTransform != null ? boundaryRectTransform : rectTransform,
				eventData.position,
				eventData.pressEventCamera,
				out Vector2 localPoint))
			{
				// Вычисляем новую позицию с учетом offset
				Vector3 newPosition = new Vector3(
					localPoint.x + dragOffset.x,
					localPoint.y + dragOffset.y,
					rectTransform.localPosition.z
				);

				// Ограничиваем позицию границами
				if (boundaryRectTransform != null)
				{
					newPosition = ClampPositionToBoundary(newPosition);
				}

				rectTransform.localPosition = newPosition;
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				isDragging = false;
			}
		}

		private Vector3 ClampPositionToBoundary(Vector3 position)
		{
			if (boundaryRectTransform == null) return position;

			Vector2 boundarySize = boundaryRectTransform.rect.size;
			Vector2 windowSize = rectTransform.rect.size;
			Vector2 boundaryPivot = boundaryRectTransform.pivot;
			Vector2 windowPivot = rectTransform.pivot;

			// Вычисляем границы с учетом пивотов
			// Для boundary с pivot (0.5, 0.5) - центр
			float minX = -boundarySize.x / 2 + windowSize.x * windowPivot.x;
			float maxX = boundarySize.x / 2 - windowSize.x * (1 - windowPivot.x);
			float minY = -boundarySize.y / 2 + windowSize.y * windowPivot.y;
			float maxY = boundarySize.y / 2 - windowSize.y * (1 - windowPivot.y);

			// Альтернативный расчет (проще для понимания):
			// Vector3 boundaryCorners = new Vector3[4];
			// boundaryRectTransform.GetLocalCorners(boundaryCorners);
			// 
			// Vector3 windowCorners = new Vector3[4];
			// rectTransform.GetLocalCorners(windowCorners);
			// 
			// float minX = boundaryCorners[0].x + (windowCorners[2].x - windowCorners[0].x) * windowPivot.x;
			// float maxX = boundaryCorners[2].x - (windowCorners[2].x - windowCorners[0].x) * (1 - windowPivot.x);
			// float minY = boundaryCorners[0].y + (windowCorners[2].y - windowCorners[0].y) * windowPivot.y;
			// float maxY = boundaryCorners[2].y - (windowCorners[2].y - windowCorners[0].y) * (1 - windowPivot.y);

			// Ограничиваем
			position.x = Mathf.Clamp(position.x, minX, maxX);
			position.y = Mathf.Clamp(position.y, minY, maxY);

			return position;
		}

		// Дополнительные методы для управления
		public void SetDragEnabled(bool enabled)
		{
			allowDrag = enabled;
			if (!enabled && isDragging)
			{
				isDragging = false;
			}
		}

		public void SetBoundary(RectTransform newBoundary)
		{
			boundaryRectTransform = newBoundary;
		}

		// Опционально: для дебага можно включить этот метод
		private void OnDrawGizmosSelected()
		{
			if (boundaryRectTransform != null && rectTransform != null)
			{
				// Визуализация границ в редакторе
				Vector3[] boundaryCorners = new Vector3[4];
				boundaryRectTransform.GetWorldCorners(boundaryCorners);

				Gizmos.color = Color.green;
				for (int i = 0; i < 4; i++)
				{
					Gizmos.DrawLine(boundaryCorners[i], boundaryCorners[(i + 1) % 4]);
				}
			}
		}
	}
}