namespace QuestGeneration
{
	public class CollectTask : Task
	{
		public Item Item { get; set; }
		public int Count { get; set; }

		public Item Reward { get; set; }
		public int RewardCount { get; set; }
	}
}