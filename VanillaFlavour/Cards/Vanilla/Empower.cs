using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Empower : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.attackID = 0;

			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_Empower"];

			block.cdAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Empower"];
		}

		protected override string GetDescription()
		{
			return "Blocking increases the damage and speed of your next shot.\nThe shot also triggers any on block abilities where it lands.";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+0.25s", "Block cooldown", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		protected override string GetTitle()
		{
			return "Empower";
		}
	}
}
