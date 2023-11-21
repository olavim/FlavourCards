using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class ShieldsUp : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.5f;

			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_ShieldsUp"];
			statModifiers.automaticReload = false;

			block.cdAdd = 0.5f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_ShieldsUp"];
		}

		protected override string GetDescription()
		{
			return "Firing your last bullet triggers a block.\nDisabled continuous reloading.";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+0.5s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("+0.5s", "Block cooldown", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DefensiveBlue;
		}

		protected override string GetTitle()
		{
			return "Shields up";
		}
	}
}
