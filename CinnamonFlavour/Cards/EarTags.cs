using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class EarTags : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			characterStats.GetAdditionalData().BrandDuration += 10f;
			characterStats.GetAdditionalData().BrandDamageMultiplier = 0;
			gun.GetAdditionalData().BrandChance += 0.2f;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Ear Tags";
		}

		protected override string GetDescription()
		{
			return "Your brands deal no damage";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+10s", "Brand duration", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aHugeAmountOf),
				Utils.CreateCardInfoStat("+20%", "Brand chance", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
