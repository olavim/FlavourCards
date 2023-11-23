using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class BigBullet : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Big_Bullet_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Big_Bullet_SoundImpactModifier"];
			gun.size = 0.5f;
			gun.reloadTimeAdd = 0.25f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["E_BigBullet"],
					scaleStacks = true,
					scaleStackM = 0f
				}
			};
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_BigBullet"];
		}

		protected override string GetDescription()
		{
			return "Bigger bullets";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Big bullet";
		}
	}
}
