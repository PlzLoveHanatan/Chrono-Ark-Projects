using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using Spine;
using UnityEngine;

namespace MiyukiSone
{
	public class SkillsExtended
	{
		public class GlitchedSkill : Skill_Extended
		{
			public override void Init()
			{
				MySkill.NoExchange = true;
				MySkill.MySkill.Name = "Glitching " + MySkill.MySkill.Name;
				base.Init();
			}

			private GameObject glitchEffect;

			public override void FixedUpdate()
			{
				if (glitchEffect == null && BattleSystem.instance != null)
				{
					var prefab = Resources.Load<GameObject>("StoryGlitch/GlitchSkillEffect");
					glitchEffect = UnityEngine.Object.Instantiate(prefab, MySkill.MyButton.transform);
					glitchEffect.SetActive(true);
				}
			}
		}

		public class Miyuki_Ex_0 : Skill_Extended
		{
			public override void Init()
			{
				OnePassive = true;
				Disposable = true;
				base.Init();
			}			

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (!MySkill.FreeUse) BattleSystem.DelayDialogueInput(CheckSkillData());
				else EventsData.MiyukiTextEvent(MiyukiAffection.Yandere);
				base.SkillUseSingle(SkillD, Targets);
			}

			private IEnumerator CheckSkillData()
			{
				yield return null;
				var skillData = MyChar.SkillDatas.FirstOrDefault(sd => sd == MySkill.CharinfoSkilldata);
				if (skillData != null)
				{
					MyChar.SkillDatas.Remove(MySkill.CharinfoSkilldata);
					MyChar.SkillAdd(new GDESkillData(ModItemKeys.Skill_S_Miyuki_Special_Yabeley), DataToExtended(GDEItemKeys.SkillExtended_SkillWe_NoExchange));
					Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_Yabeley, BChar, BChar.MyTeam);
					MySkill.SkillChange(newSkill, false, false, true);
					newSkill.ExtendedAdd_Battle(GDEItemKeys.SkillExtended_SkillWe_NoExchange);
					GainEquip();
				}
				else
				{
					EventsData.MiyukiTextEvent(MiyukiAffection.Yandere);
					//if (PlayData.TSavedata.SpRule != null)
					//{
					//	PlayData.TSavedata.SpRule.RuleChange.CharacterSkillMin++;
					//}
					//else
					//{
					//	PlayData.TSavedata.SpRule = new MiyukiSpecialRules();
					//	PlayData.TSavedata.SpRule.Init();
					//	PlayData.TSavedata.SpRule.GameSetting();
					//}
				}
			}

			private void GainEquip()
			{
				List<ItemBase> Equip = Enumerable.Range(0, 3).Select(_ => ItemBase.GetItem(PlayData.GetEquipRandom(4, false, null))).ToList();
				UIManager.InstantiateActive(UIManager.inst.SelectItemUI).GetComponent<SelectItemUI>().Init(Equip,
					new RandomItemBtn.SelectItemClickDel(i => { PlayData.TSavedata.EquipList_Legendary.Add(i.itemkey); InventoryManager.Reward(i); } ));
			}
		}

		public class Miyuki_Ex_1 : Skill_Extended
		{

		}
	}
}
