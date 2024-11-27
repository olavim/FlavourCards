using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class MotivationBoost : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) CinnamonFlavour.CustomResources["A_MotivationBoost"];
			statModifiers.lifeSteal = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			characterStats.GetAdditionalData().BrandDuration -= 0.25f;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Motivation Boost";
		}

		protected override string GetDescription()
		{
			return "+50% movement speed for 3s after branding an opponent";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+25%", "Life steal", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("-0.25s", "Brand duration", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.slightlyLower)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
