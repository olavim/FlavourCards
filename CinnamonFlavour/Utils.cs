namespace CinnamonFlavour
{
	public static class Utils
	{
		public static CardInfoStat CreateCardInfoStat(string amount, string stat, CardInfoStatType type, CardInfoStat.SimpleAmount simpleAmount = CardInfoStat.SimpleAmount.notAssigned)
		{
			return new()
			{
				amount = amount,
				stat = stat,
				simepleAmount = simpleAmount,
				positive = type == CardInfoStatType.Positive
			};
		}
	}
}