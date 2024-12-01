using CinnamonFlavour.Extensions;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	[Card]
	public sealed class Riposte : CinnamonFlavourCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.AddObjectToPlayer = (GameObject) CinnamonFlavour.CustomResources["A_Riposte"];
			block.cdAdd = 0.25f;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.GetOrAddComponent<RiposteAttachment>();
			characterStats.GetAdditionalData().ShotsAfterBlockRefresh += 1;
		}

		protected override GameObject GetCardArt()
		{
			return new GameObject();
		}

		protected override string GetTitle()
		{
			return "Riposte";
		}

		protected override string GetDescription()
		{
			return "Brand nearby enemies when you block. Shoot at a branded opponent on block refresh.";
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[] {
				Utils.CreateCardInfoStat("+0.25s", "Block cooldown", CardInfoStatType.Negative, CardInfoStat.SimpleAmount.notAssigned)
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}
	}
}
