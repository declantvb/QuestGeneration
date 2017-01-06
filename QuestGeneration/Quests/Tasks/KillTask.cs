namespace QuestGeneration
{
	public class KillTask : Task
	{
		public Vehicle Target { get; set; }
		public int Count { get; set; }

		public Item Reward { get; set; }
		public int RewardCount { get; set; }

		public override string ToString()
		{
			return $"Kill: {Count} {Target} for {RewardCount} {Reward}";
		}
	}
}