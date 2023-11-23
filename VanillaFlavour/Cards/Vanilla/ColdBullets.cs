using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class ColdBullets : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Cold_Bullets_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Cold_Bullets_SoundImpactModifier"];
			gun.slow = 0.7f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["E_Cold"],
					scaleStacks = true,
					scaleStackM = 0.3f,
					scaleFromDamage = 0.5f
				}
			};
			gun.projectileColor = new Color(0.2122642f, 0.9246516f, 1f, 1f);
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_ColdBullets"];
		}

		protected override string GetDescription()
		{
			return "";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+70%", "Bullet slow", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLotOf),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.ColdBlue;
		}

		protected override string GetTitle()
		{
			return "Cold bullets";
		}
	}
}
