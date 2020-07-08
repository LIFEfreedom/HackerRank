namespace determining_dna_health
{
	public struct Pattern
	{
		public readonly uint PatternNumber;

		public readonly int Cost;

		public Pattern(uint patternNumber, int cost)
		{
			PatternNumber = patternNumber;
			Cost = cost;
		}
	}
}
