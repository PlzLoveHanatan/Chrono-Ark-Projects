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
namespace Xao
{
    /// <summary>
    /// Affection
    /// </summary>
    public class B_Xao_Affection : Buff, IP_BuffAddAfter
    {
        public bool FirstTransform;

        public override void BuffOneAwake()
        {
            BuffIcon.AddComponent<Button>().onClick.AddListener(XaoCall);
        }

        public void XaoCall()
        {
            Utils.AffectionSelection(BChar);
        }

        public override void BuffStat()
        {
            PlusStat.dod = 3 * StackNum;
            PlusStat.cri = 3 * StackNum;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData == Utils.Xao.Info.KeyData && addedbuff == this)
            {
                Xao_Hearts.HeartsCheck(BChar, 1);
                Utils.PopHentaiText(BChar);

                if (StackNum >= 3 && !FirstTransform)
                {
                    Utils.PlayXaoVoice(BChar, true);
                    SkinData skin = SaveManager.NowData.EnableSkins.FirstOrDefault(v => v.skinKey.StartsWith(ModItemKeys.Character_Xao));

                    if (!string.IsNullOrEmpty(skin.skinKey))
                    {
                        Xao_Face_Change.ChooseFace(BChar, skin.skinKey);
                    }
                    else
                    {
                        Xao_Face_Change.ChooseFace(BChar, ModItemKeys.Character_Xao);
                    }

                    if (BChar.Info.Passive is P_Xao passive)
                    {
                        passive.HornyMod = true;
					}

					Utils.ChooseBattleChibi(BChar, 3, false, true);
					BattleSystem.DelayInput(Utils.ChangeXaoSkills(BChar));
                    BChar.Overload = 0;
                    FirstTransform = true;
                }
            }
        }
    }
}