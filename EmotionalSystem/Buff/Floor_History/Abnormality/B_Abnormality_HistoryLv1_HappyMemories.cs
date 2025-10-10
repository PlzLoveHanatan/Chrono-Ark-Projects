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
using System.ServiceModel.Channels;
namespace EmotionalSystem
{
    public class B_Abnormality_HistoryLv1_HappyMemories : Buff, IP_PlayerTurn, IP_Awake
    {
		public void Awake()
		{
			Utils.AddBuff(BChar, BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv1_HappyMemories_0);
		}

		public void Turn()
        {
			Utils.AddBuff(BChar, BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv1_HappyMemories_0);
		}
    }
}