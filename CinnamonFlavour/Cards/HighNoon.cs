using CinnamonFlavour.Extensions;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class HighNoon : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.automaticReload = false;
			gun.reloadTimeAdd = 0.5f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.GetOrAddComponent<HighNoonAttachment>();
			characterStats.GetAdditionalData().ShotsAfterReload += 1;
			gun.GetAdditionalData().DamageBranded *= 0.5f;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "High Noon";
		}

		protected override string GetDescription()
		{
			return "Automatically shoot at a branded opponent after reloading.\nDisables continuous reloading.";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+1", "Shots", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("-50%", "DMG to branded", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.aLotLower),
				Utils.CreateCardInfoStat("+0.5s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
