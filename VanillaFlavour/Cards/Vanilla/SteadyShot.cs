using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class SteadyShot : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Quick_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Quick_SoundImpactModifier"];
			gun.projectileSpeed = 2f;
			gun.reloadTimeAdd = 0.25f;

			statModifiers.health = 1.4f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_SteadyShot"];
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
				Utils.CreateCardInfoStat("+40%", "HP", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("+100%", "Bullet speed", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.Some),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Steady shot";
		}
	}
}
