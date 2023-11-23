using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Barrage : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 0.3f;
			gun.ammo = 5;
			gun.numberOfProjectiles = 4;
			gun.spread = 0.13f;
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Barrage"];
		}

		protected override string GetDescription()
		{
			return "Fire many bullets at the same time";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+4", "Bullets", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("+5", "Ammo", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("-70%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.aLotLower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Barrage";
		}
	}
}
