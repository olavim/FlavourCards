using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class TargetBounce : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 0.75f;
			gun.overheatMultiplier = 0.1f;
			gun.reflects = 1;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.CustomResources["A_TargetBounce"]
				},
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_ScreenEdge"]
				}
			};
			gun.reloadTimeAdd = 0.25f;

			/* Original:
			 * gun.damage = 0.8f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_TargetBounce"];
		}

		protected override string GetDescription()
		{
			return "Bullets aim for nearby visible targets when bouncing";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+1", "Bullet bounce", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("-25%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		protected override string GetTitle()
		{
			return "Target bounce";
		}
	}
}
