using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
namespace MiyukiSone
{
	public class MiyukiSone_ModDefinition : ModDefinition
	{
		public override Type ModItemKeysType => typeof(ModItemKeys);
		public override List<object> BattleSystem_ModIReturn()
		{
			var list = base.BattleSystem_ModIReturn();

			list.Add(new MiyukiDistortion());

			return list;
		}

		public class MiyukiDistortion : IP_BattleStart_Ones
		{
			public void BattleStart(BattleSystem Ins)
			{
				if (Utils.MiyukiBchar == null)
				{
					// add % chance to show Miyuki dialogue
					// Create Miyuki Image + add text + any nasty behaviour -> remove soulstones/gold or curse equips
				}
			}
		}
	}
}