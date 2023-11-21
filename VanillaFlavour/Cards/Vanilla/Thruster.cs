using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Thruster : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Thruster_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Thruster_SoundImpactModifier"];
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					effect = (GameObject) VanillaFlavour.RoundsResources["A_Thruster"],
					stickToAllTargets = true,
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_Thruster"],
					removeScriptsFromProjectileObject = true,
					scaleStacks = true,
					scaleStackM = 0.7f,
					scaleFromDamage = 0.7f
				}
			};

			/* Original:
			 * gun.reloadTimeAdd = 0.25f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Thrusters"];
		}

		protected override string GetDescription()
		{
			return "Bullets have thrusters that push targets";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[0];
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		protected override string GetTitle()
		{
			return "Thruster";
		}
	}
}
