using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class Lifeline : CinnamonFlavourCard
	{

		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) CinnamonFlavour.CustomResources["A_Lifeline"];
			statModifiers.lifeSteal = 0.3f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			characterStats.GetAdditionalData().EnableBrandLifeSteal = true;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Lifeline";
		}

		protected override string GetDescription()
		{
			return "Steal hp from branded opponents";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+30%", "Life steal", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLittleBitOf),
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
