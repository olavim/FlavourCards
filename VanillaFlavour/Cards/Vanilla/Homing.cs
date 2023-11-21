using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Homing : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 0.5f;
			gun.reloadTimeAdd = 0.5f;
			gun.attackSpeed = 1.5f;
			gun.projectileSpeed = 0.7f;
			gun.gravity = 0.5f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_Homing"]
				}
			};

			/* Original:
			 * gun.reloadTimeAdd = 0f;
			 * gun.damage = 0.75f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Homing"];
		}

		protected override string GetDescription()
		{
			return "Bullets home towards visible targets";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("-50%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower),
				Utils.CreateCardInfoStat("-50%", "ATKSPD", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower),
				Utils.CreateCardInfoStat("+0.5s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		protected override string GetTitle()
		{
			return "Homing";
		}
	}
}
