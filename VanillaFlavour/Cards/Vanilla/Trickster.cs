using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Trickster : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 0.8f;
			gun.reloadTimeAdd = 0.5f;
			gun.overheatMultiplier = 0.1f;
			gun.reflects = 2;
			gun.dmgMOnBounce = 1.8f;
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_ScreenEdge"]
				},
				new()
				{
					AddToProjectile = (GameObject) VanillaFlavour.RoundsResources["A_Trickster"],
					scaleStacks = true,
					scaleFromDamage = 0.5f
				}
			};
			gun.attackID = 0;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Trickster"];
		}

		protected override string GetDescription()
		{
			return "Bullets deal 80% more DMG per bounce";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+2", "Bullet bounces", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("-20%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.slightlyLower),
				Utils.CreateCardInfoStat("+0.5s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		protected override string GetTitle()
		{
			return "Trickster";
		}
	}
}
