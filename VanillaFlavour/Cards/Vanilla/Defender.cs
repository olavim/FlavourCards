using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Defender : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 0.75f;
			statModifiers.health = 1.25f;

			/* Original:
			 * block.cdMultiplier = 0.7f;
			 * statModifiers.health = 1.3f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Defender"];
		}

		protected override string GetDescription()
		{
			return "";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			// Original: Uncommon
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+25%", "HP", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLittleBitOf),
				Utils.CreateCardInfoStat("-25%", "Block cooldown", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.slightlyLower)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DefensiveBlue;
		}

		protected override string GetTitle()
		{
			return "Defender";
		}
	}
}
