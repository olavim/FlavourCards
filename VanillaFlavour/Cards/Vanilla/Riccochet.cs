using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Riccochet : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.attackSpeed = 0.8f;
			gun.overheatMultiplier = 0.15f;
			gun.reflects = 2;
			gun.speedMOnBounce = 0.5f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_ScreenEdge"]
				}
			};
			gun.reloadTimeAdd = 0.25f;

			/* Original:
			 * gun.attackSpeed = 0.75f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Riccochet"];
		}

		protected override string GetDescription()
		{
			return "Bullets lose half of their speed when they bounce";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+2", "Bullet bounces", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("+25%", "ATKSPD", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLittleBitOf),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Riccochet";
		}
	}
}
