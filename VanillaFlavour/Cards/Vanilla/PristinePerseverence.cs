using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class PristinePerseverence : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_PristinePerseverence"];
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Pristince_Perserverence"];
		}

		protected override string GetDescription()
		{
			return "+400% HP when above 90% HP";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DefensiveBlue;
		}

		protected override string GetTitle()
		{
			return "Pristine perseverence";
		}
	}
}
