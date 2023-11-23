using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class RadarShot : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_Radar"];
			statModifiers.health = 1.3f;

			block.cdAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Radarshot"];
		}

		protected override string GetDescription()
		{
			return "Blocking scans the area for enemies. You automatically shoot any enemy found.";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
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
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		protected override string GetTitle()
		{
			return "Radar shot";
		}
	}
}
