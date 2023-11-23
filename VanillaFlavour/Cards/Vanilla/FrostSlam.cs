using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class FrostSlam : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_FrostSlam"];
			statModifiers.health = 1.3f;

			block.cdAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_FrostSlam"];
		}

		protected override string GetDescription()
		{
			return "Slows enemies around you when you block";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+30%", "HP", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("+0.25s", "Block cooldown", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.ColdBlue;
		}

		protected override string GetTitle()
		{
			return "Frost slam";
		}
	}
}
