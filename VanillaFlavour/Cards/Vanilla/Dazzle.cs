using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Dazzle : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new()
				{
					effect = (GameObject) VanillaFlavour.RoundsResources["E_StunOverTime"],
					spawnAsChild = true,
					scaleStacks = true,
					scaleStackM = 1f,
					scaleFromDamage = 0.5f
				}
			};
			gun.projectileColor = new Color(0f, 1f, 0.7739825f, 1f);

			/* Original:
			 * gun.reloadTimeAdd = 0.25f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Dazzle"];
		}

		protected override string GetDescription()
		{
			return "Bullets stun the oponent multiple times";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[0];
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		protected override string GetTitle()
		{
			return "Dazzle";
		}
	}
}
