using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class HighNoon : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) CinnamonFlavour.CustomResources["A_HighNoon"];
			statModifiers.automaticReload = false;
			gun.reloadTimeAdd = 0.5f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			characterStats.GetAdditionalData().ShotsAfterReload += 1;
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
			return "Brand visible opponents on reload start. Shoot at a branded opponent after reloading.\n<nobr>Disables continuous reloading.</nobr></size>";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+0.5s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
