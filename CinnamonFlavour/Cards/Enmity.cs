using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class Enmity : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 0.75f;
			gun.reloadTimeAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.GetAdditionalData().DamageBranded *= 2f;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Enmity";
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
				Utils.CreateCardInfoStat("+100%", "DMG to branded", CardInfoStatType.Positive, CardInfoStat.SimpleAmount.aLotOf),
				Utils.CreateCardInfoStat("-25%", "HP", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.slightlyLower),
				Utils.CreateCardInfoStat("+0.25s", "Reload time", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
