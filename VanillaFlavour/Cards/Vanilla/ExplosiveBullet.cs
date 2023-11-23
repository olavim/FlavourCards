using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class ExplosiveBullet : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Explosive_Bullet_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Explosive_Bullet_SoundImpactModifier"];
			gun.attackSpeed = 2f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					effect = (GameObject) VanillaFlavour.RoundsResources["A_Explosion"],
					normalOffset = 0.1f,
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_ExplosionSpark"],
					scaleStacks = true,
					scaleStackM = 0.7f,
					scaleFromDamage = 0.5f
				}
			};
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Explosive_Bullet"];
		}

		protected override string GetDescription()
		{
			return "Bullet explodes on impact";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("-100%", "ATKSPD", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		protected override string GetTitle()
		{
			return "Explosive bullet";
		}
	}
}
