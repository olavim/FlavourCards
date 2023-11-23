using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Emp : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_EMP"];
			statModifiers.health = 1.3f;

			block.cdAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_EMP"];
		}

		protected override string GetDescription()
		{
			return "Blocking spawns a ring of slowing projectiles";
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
			return "Emp";
		}
	}
}
