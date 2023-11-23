using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Buckshot : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 0.4f;
			gun.knockback = 2.5f;
			gun.projectileSpeed = 3.5f;
			gun.gravity = 0f;
			gun.ammo = 5;
			gun.numberOfProjectiles = 4;
			gun.drag = 10f;
			gun.spread = 0.5f;
			gun.destroyBulletAfter = 0.2f;
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Buckshot"];
		}

		protected override string GetDescription()
		{
			return "Adds a shotgun vibe to your attack";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+4 ", "Bullets", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("+5", "AMMO", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("-60%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		protected override string GetTitle()
		{
			return "Buckshot";
		}
	}
}
