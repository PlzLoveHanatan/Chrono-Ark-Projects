using System;
using UnityEngine;
using EmotionSystem;

namespace QoH
{
	public class QoH_Chibi_Script : MonoBehaviour
	{
		public enum ChibiEffect
		{
			PopIn,
			Tilt,
			//Bounce,
			SpinBlast,
			BounceRampage,
			LightningSlide,
			//BouncyTwirl,
			//SlideUpPop,
			FlutterSpin,
			HeartBounce
		}

		// Параметры эффектов
		public float popInDuration = 1.2f;
		public float tiltDuration = 1.5f;
		public float tiltAngle = 20f;
		public float tiltSpeed = 8f;
		public float bounceHeight = 25f;
		public float bounceDuration = 1.2f;
		public int bounceCount = 3;

		public float spinBlastHeight = 80f;
		public int spinBlastTurns = 1;
		public float spinBlastDuration = 1.5f;

		public float bounceRampageHeight = 80f;
		public int bounceRampageCount = 4;
		public float bounceRampageDuration = 2f;

		public float lightningSlideDistance = 100f;
		public float lightningSlideJump = 50f;
		public float lightningSlideDuration = 1.5f;

		public float bouncyTwirlHeight = 40f;
		public float bouncyTwirlRotation = 180f;
		public float bouncyTwirlDuration = 1.5f;

		public float slideUpPopHeight = 50f;
		public float slideUpPopDuration = 1.5f;

		public float flutterSpinRotation = 30f;
		public float flutterSpinScale = 1.2f;
		public float flutterSpinDuration = 1.5f;

		public float heartBounceHeight = 30f;
		public float heartBounceScale = 1.2f;
		public float heartBounceDuration = 1.3f;

		private Vector3 startPos;
		private Vector3 baseScale;
		private float startRot;

		private ChibiEffect currentEffect;
		private float timer;
		private bool active;

		private static ChibiEffect? lastEffectPlayed = null;

		public void Awake()
		{
			startPos = transform.localPosition;
			baseScale = transform.localScale;
			startRot = transform.localRotation.eulerAngles.z;

			ChooseRandomEffect();
		}

		public void Update()
		{
			if (!active) return;

			timer += Time.deltaTime;

			switch (currentEffect)
			{
				case ChibiEffect.PopIn: PopIn(); break;
				case ChibiEffect.Tilt: Tilt(); break;
				//case ChibiEffect.Bounce: Bounce(); break;
				case ChibiEffect.SpinBlast: SpinBlast(); break;
				case ChibiEffect.BounceRampage: BounceRampage(); break;
				case ChibiEffect.LightningSlide: LightningSlide(); break;
				//case ChibiEffect.BouncyTwirl: BouncyTwirl(); break;
				//case ChibiEffect.SlideUpPop: SlideUpPop(); break;
				case ChibiEffect.FlutterSpin: FlutterSpin(); break;
				case ChibiEffect.HeartBounce: HeartBounce(); break;
			}
		}

		private void ChooseRandomEffect()
		{
			Array values = Enum.GetValues(typeof(ChibiEffect));
			ChibiEffect newEffect;

			if (values.Length == 1)
			{
				newEffect = (ChibiEffect)values.GetValue(0);
			}
			else
			{
				do
				{
					newEffect = (ChibiEffect)values.GetValue(UnityEngine.Random.Range(0, values.Length));
				} while (lastEffectPlayed.HasValue && newEffect == lastEffectPlayed.Value);
			}

			currentEffect = newEffect;
			timer = 0f;
			active = true;

			Debug.Log("[CHIBI QUEEN] Effect selected: " + currentEffect);
		}

		private void EndCurrentEffect()
		{
			lastEffectPlayed = currentEffect;
			active = false;
			Destroy(gameObject);
		}

		/* ================= EFFECTS ================= */

		private void PopIn()
		{
			float t = timer / popInDuration;
			float scale = Mathf.Sin(t * Mathf.PI) * 0.5f + 0.7f;
			transform.localScale = baseScale * scale;

			if (t >= 1f)
			{
				transform.localScale = baseScale;
				EndCurrentEffect();
			}
		}

		private void Tilt()
		{
			float angle = Mathf.Sin(Time.time * tiltSpeed) * tiltAngle;
			transform.localRotation = Quaternion.Euler(0, 0, startRot + angle);

			if (timer > tiltDuration)
			{
				transform.localRotation = Quaternion.Euler(0, 0, startRot);
				EndCurrentEffect();
			}
		}

		private void Bounce()
		{
			float t = timer / bounceDuration;
			float y = Mathf.Sin(t * Mathf.PI * bounceCount) * bounceHeight;
			transform.localPosition = startPos + new Vector3(0, y, 0);

			if (t >= 1f)
			{
				transform.localPosition = startPos;
				EndCurrentEffect();
			}
		}

		private void SpinBlast()
		{
			float t = timer / spinBlastDuration;
			float y = Mathf.Sin(t * Mathf.PI) * spinBlastHeight;
			float angle = 360f * spinBlastTurns * t;
			transform.localPosition = startPos + new Vector3(0, y, 0);
			transform.localRotation = Quaternion.Euler(0f, 0f, startRot + angle);

			if (t >= 1f)
			{
				transform.localPosition = startPos;
				transform.localRotation = Quaternion.Euler(0f, 0f, startRot);
				EndCurrentEffect();
			}
		}

		private void BounceRampage()
		{
			float t = timer / bounceRampageDuration;
			int currentBounce = Mathf.Min(Mathf.FloorToInt(t * bounceRampageCount), bounceRampageCount - 1);
			float bounceT = (t * bounceRampageCount) - currentBounce;
			float y = Mathf.Sin(bounceT * Mathf.PI) * bounceRampageHeight * (currentBounce + 1) / bounceRampageCount;
			transform.localPosition = startPos + new Vector3(0, y, 0);

			if (t >= 1f)
			{
				transform.localPosition = startPos;
				EndCurrentEffect();
			}
		}

		private void LightningSlide()
		{
			float t = timer / lightningSlideDuration;

			Vector3 fromPos = startPos + new Vector3(-lightningSlideDistance, 0, 0);

			// сам слайд
			float slideT = Mathf.Clamp01(t);
			Vector3 slidePos = Vector3.Lerp(fromPos, startPos, Mathf.SmoothStep(0f, 1f, slideT));

			float y = Mathf.Sin(slideT * Mathf.PI) * lightningSlideJump;

			// ===== ФИНАЛЬНЫЙ ПРЫЖОК (как в Rampage, только один) =====
			if (t > 1f)
			{
				float bounceT = (t - 1f) / 0.5f; // длина прыжка
				float bounce = Mathf.Sin(bounceT * Mathf.PI) * (lightningSlideJump * 0.6f);

				y = bounce;
			}

			transform.localPosition = slidePos + new Vector3(0, y, 0);

			if (t >= 1.35f)
			{
				transform.localPosition = startPos;
				transform.localScale = baseScale;
				EndCurrentEffect();
			}
		}

		private void BouncyTwirl()
		{
			float t = timer / bouncyTwirlDuration;
			float y = Mathf.Sin(t * Mathf.PI) * bouncyTwirlHeight;
			float angle = Mathf.Lerp(0, bouncyTwirlRotation, t);
			transform.localPosition = startPos + new Vector3(0, y, 0);
			transform.localRotation = Quaternion.Euler(0f, 0f, startRot + angle);
			if (t >= 1f) { transform.localPosition = startPos; transform.localRotation = Quaternion.Euler(0, 0, startRot); EndCurrentEffect(); }
		}

		private void SlideUpPop()
		{
			float t = timer / slideUpPopDuration;
			float y = Mathf.Sin(t * Mathf.PI) * slideUpPopHeight;
			transform.localPosition = startPos + new Vector3(0, y, 0);
			if (t >= 1f) { transform.localPosition = startPos; EndCurrentEffect(); }
		}

		private void FlutterSpin()
		{
			float t = timer / flutterSpinDuration;
			float angle = Mathf.Lerp(0f, flutterSpinRotation, t);
			float scale = Mathf.Lerp(1f, flutterSpinScale, Mathf.Sin(t * Mathf.PI));
			transform.localRotation = Quaternion.Euler(0f, 0, startRot + angle);
			transform.localScale = baseScale * scale;
			if (t >= 1f) { transform.localRotation = Quaternion.Euler(0f, 0, startRot); transform.localScale = baseScale; EndCurrentEffect(); }
		}

		private void HeartBounce()
		{
			float t = timer / heartBounceDuration;
			float y = Mathf.Sin(t * Mathf.PI) * heartBounceHeight;
			float scale = 1f + Mathf.Sin(t * Mathf.PI) * (heartBounceScale - 1f);
			transform.localPosition = startPos + new Vector3(0, y, 0);
			transform.localScale = baseScale * scale;
			if (t >= 1f) { transform.localPosition = startPos; transform.localScale = baseScale; EndCurrentEffect(); }
		}
	}
}