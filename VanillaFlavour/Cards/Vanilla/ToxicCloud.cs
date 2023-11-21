using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class ToxicCloud : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Toxic_Cloud_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Toxic_Cloud_SoundImpactModifier"];
			gun.damage = 0.75f;
			gun.reloadTimeAdd = 0.5f;
			gun.attackSpeed = 1.2f;
			gun.overheatMultiplier = 1f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					effect = (GameObject) VanillaFlavour.RoundsResources["A_ToxicCloud"],
					normalOffset = 0.05f,
					scaleStacks = true,
					scaleStackM = 0.7f,
					scaleFromDamage = 0.5f
				}
			};
			gun.projectileColor = new Color(0.6330439f, 1f, 0.2311321f, 1f);
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Toxic Cloud"];
		}

		protected override string GetDescription()
		{
			return "Bullets spawn a poison cloud on impact. \nClouds deal damage and slow.";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("-20%", "Attack Speed", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.slightlyLower),
				Utils.CreateCardInfoStat("+0.5s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.PoisonGreen;
		}

		protected override string GetTitle()
		{
			return "Toxic cloud";
		}
	}
}
