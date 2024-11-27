using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class EmberInTheDark : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) CinnamonFlavour.CustomResources["A_EmberInTheDark"];
			statModifiers.automaticReload = false;
			gun.reloadTimeAdd = 0.5f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Ember in the Dark";
		}

		protected override string GetDescription()
		{
			return "Brand a visible opponent when you start reloading.\nDisables continuous reloading.";
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
