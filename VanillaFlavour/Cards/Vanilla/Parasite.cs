using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Parasite : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Parasite_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Parasite_SoundImpactModifier"];
			gun.damage = 1.25f;
			gun.reloadTimeAdd = 0.25f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_Parasite"],
					scaleStackM = 0.7f,
					scaleFromDamage = 1f
				}
			};
			gun.projectileColor = new Color(0.5928641f, 0.1254902f, 1f, 1f);

			statModifiers.health = 1.25f;
			statModifiers.lifeSteal = 0.5f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Parasite"];
		}

		protected override string GetDescription()
		{
			return "Bullets deal damage over 5 seconds";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+50%", "Life steal", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("+25%", "HP", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("+25%", "DMG", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		protected override string GetTitle()
		{
			return "Parasite";
		}
	}
}
