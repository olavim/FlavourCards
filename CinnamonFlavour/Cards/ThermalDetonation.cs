using CinnamonFlavour.Extensions;
using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class ThermalDetonation : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			var effect = (GameObject) CinnamonFlavour.CustomResources["A_BrandExplosion"];
			var existing = characterStats.GetAdditionalData().BrandObjectsToSpawn.FirstOrDefault(x => x.Effect.name == effect.name);

			if (existing == null)
			{
				characterStats.GetAdditionalData().BrandObjectsToSpawn.Add(
					new BrandObjectsToSpawn
					{
						Effect = effect,
						Trigger = BrandObjectsToSpawn.SpawnTrigger.Expire,
						ScaleStacks = true,
						ScaleStackMultiplier = 2f,
						ScaleFromBrandDamage = 1.25f
					}
				);
			}
			else
			{
				existing.Stacks++;
			}

			characterStats.GetAdditionalData().BrandDurationMultiplier *= 0.5f;
			characterStats.GetAdditionalData().BrandDamageMultiplier *= 0.75f;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Thermal Detonation";
		}

		protected override string GetDescription()
		{
			return "Your brands explode when they expire";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("-50%", "Brand duration", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.aLotLower),
				Utils.CreateCardInfoStat("-25%", "Brand DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.lower)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
