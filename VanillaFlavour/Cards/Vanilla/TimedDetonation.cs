using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class TimedDetonation : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Explosive_Bullet_SoundShotModifier"];
			gun.damage = 0.85f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					effect = (GameObject) VanillaFlavour.RoundsResources["A_Bomb_Timed_Detonation"],
					normalOffset = 0.05f,
					stickToAllTargets = true,
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_BombEffect"],
					scaleStacks = true,
					scaleStackM = 0.7f,
					scaleFromDamage = 0.5f
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
			return (GameObject) VanillaFlavour.RoundsResources["C_TimedDetonation"];
		}

		protected override string GetDescription()
		{
			return "Bullets spawn bombs that explode after half a second";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("-15%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.slightlyLower)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		protected override string GetTitle()
		{
			return "Timed detonation";
		}
	}
}
