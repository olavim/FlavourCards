using UnityEngine;

namespace VanillaFlavour
{
	[Card]
	public sealed class Spray : VanillaFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.damage = 0.25f;
			gun.recoilMuiltiplier = 0.25f;
			gun.attackSpeed = 0.09090909f;
			gun.ammo = 12;
			gun.spread = 0.1f;

			/* Original:
			 * gun.reloadTimeAdd = 0.25f;
			 */
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override GameObject GetCardArt()
		{
			return (GameObject) VanillaFlavour.RoundsResources["C_Spray"];
		}

		protected override string GetDescription()
		{
			return "";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+1000%", "ATKSPD", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aHugeAmountOf),
				Utils.CreateCardInfoStat("+12", "AMMO", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.notAssigned),
				Utils.CreateCardInfoStat("-75%", "DMG", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.aLotLower)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		protected override string GetTitle()
		{
			return "Spray";
		}
	}
}
