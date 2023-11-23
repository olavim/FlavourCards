using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Glasscannon : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 2f;
			gun.reloadTimeAdd = 0.25f;

			statModifiers.health = 0.5f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Glasscannon"];
		}

		protected override string GetDescription()
		{
			return "";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+100%", "DMG", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLotOf),
				Utils.CreateCardInfoStat("-100%", "HP", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.aLotLower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Glasscannon";
		}
	}
}
