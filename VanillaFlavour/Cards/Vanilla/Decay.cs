using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Decay : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 1.5f;
			statModifiers.secondsToTakeDamageOver = 4f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Decay"];
		}

		protected override string GetDescription()
		{
			return "Damage done to you is dealt over 4 seconds";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			// Original: Uncommon
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+50%", "HP", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		protected override string GetTitle()
		{
			return "Decay";
		}
	}
}
