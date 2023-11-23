using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class DemonicPact : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Demonic_Pact_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Demonic_Pact_SoundImpactModifier"];
			gun.attackSpeed = 0.025f;
			gun.ammo = 9;
			gun.dontAllowAutoFire = true;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					effect = (GameObject) VanillaFlavour.RoundsResources["A_DemonicExplosion"],
					normalOffset = 0.1f,
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_DemonSpark"],
					scaleStacks = true,
					scaleStackM = 0.7f,
					scaleFromDamage = 0.5f
				}
			};
			gun.projectileColor = new Color(0.6471141f, 0.4575472f, 1f, 1f);

			statModifiers.AddObjectToPlayer = (GameObject) VanillaFlavour.RoundsResources["A_DemonicPact"];
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_DemonicPact"];
		}

		protected override string GetDescription()
		{
			return "Shooting costs 10HP\nRemoves shooting cooldown";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+9", "Bullets", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("+2", "Splash DMG", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLittleBitOf),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		protected override string GetTitle()
		{
			return "Demonic pact";
		}
	}
}
