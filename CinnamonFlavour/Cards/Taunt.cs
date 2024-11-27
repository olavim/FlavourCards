using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class Taunt : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			var go = new GameObject("A_Taunt", typeof(TauntAttachment));
			go.transform.SetParent(player.transform);
			characterStats.GetAdditionalData().BrandDuration -= 0.25f;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Taunt";
		}

		protected override string GetDescription()
		{
			return "-50% block cooldown for 3s after branding an opponent";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("-0.25s", "Brand duration", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.slightlyLower)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
