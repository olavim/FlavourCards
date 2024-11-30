using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class PoisonBullets : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Poison_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Poison_SoundImpactModifier"];
			gun.damage = 1.7f;
			gun.reloadTime = 0.7f;
			gun.ammo = -1;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_Poison"],
					scaleStacks = true,
					scaleStackM = 0.7f,
					scaleFromDamage = 1f
				}
			};
			gun.projectileColor = new Color(0.5842636f, 1f, 0.1254902f, 1f);
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_PoisonBullet"];
		}

		protected override string GetDescription()
		{
			return "Bullets deal damage over 3 seconds";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			// Original: Common
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+70%", "DMG", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("-30%", "Reload time", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("-1", "Bullet", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.PoisonGreen;
		}

		protected override string GetTitle()
		{
			return "Poison bullets";
		}
	}
}
