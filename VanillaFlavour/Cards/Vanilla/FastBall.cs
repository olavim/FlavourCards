using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class FastBall : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.soundShotModifier = (SoundImplementation.SoundShotModifier) VanillaFlavour.RoundsResources["Quick_SoundShotModifier"];
			gun.soundImpactModifier = (SoundImplementation.SoundImpactModifier) VanillaFlavour.RoundsResources["Quick_SoundImpactModifier"];
			gun.attackSpeed = 1.5f;
			gun.projectileSpeed = 3.5f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_FastBall"],
					scaleStacks = true,
					scaleStackM = 0.3f
				}
			};
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Fastballer"];
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
				Utils.CreateCardInfoStat("+250%", "Bullet speed ", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aHugeAmountOf),
				Utils.CreateCardInfoStat("-50%", "ATKSPD", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Fast ball";
		}
	}
}
