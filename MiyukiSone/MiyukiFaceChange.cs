using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData;
using ChronoArkMod;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public class FaceChange
	{
		public static void ImageChange(string path)
		{
			ModInfo modInfo = ModManager.getModInfo("MiyukiSone");
			string facePath = modInfo.assetInfo.ImageFromFile("MiyukiVisual/" + path);

			MiyukiBchar.Info.GetData.face_Path = facePath;
			var imageComponent = MiyukiBchar.UI.CharImage.GetComponent<UnityEngine.UI.Image>();
			if (imageComponent != null)
			{
				AddressableLoadManager.LoadAsyncAction(facePath, AddressableLoadManager.ManageType.Character, imageComponent);
			}
		}
	}
}
