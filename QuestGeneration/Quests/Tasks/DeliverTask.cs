namespace QuestGeneration
{
	public class DeliverTask : Task
	{
		public Character Destination { get; set; }

		public Item Item { get; set; }

		public Item Reward { get; set; }
		public int RewardCount { get; set; }

		public override string ToString()
		{
			return $"Deliver: {Item} to {Destination} for {RewardCount} {Reward}";
		}
	}
}