using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Kazuma
{
	public class Buffs
	{
		//public class Test : Buff, IP_Awake, IP_SkillUse_User
		//{
		//	private GameObject bulletPrefab;
		//	private GameObject bulletImage;

		//	public void Awake()
		//	{
		//		bulletPrefab = Utils.GetAssets<GameObject>("Assets/ModAssets/Bullet.prefab", "kazumaunityassetbundle");

		//		if (bulletPrefab == null)
		//		{
		//			Debug.Log("UI PREFAB NOT LOADED");
		//			return;
		//		}

		//		bulletImage = UnityEngine.Object.Instantiate(bulletPrefab, BChar.UI.transform.GetChild(0));
		//		bulletImage.transform.localPosition = new Vector3(35, 125, 0);
		//		bulletImage.AddComponent<BulletVisual>();

		//		Debug.Log("Spawned UI bullet!");
		//	}

		//	public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		//	{
		//		if (SkillD.Master == BChar)
		//		{
		//			bulletImage.GetComponent<BulletVisual>().Next();
		//		}
		//	}
		//}
	}
}
