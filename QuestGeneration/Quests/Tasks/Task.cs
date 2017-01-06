namespace QuestGeneration
{
	public class Task
	{
		public string Description { get; set; }
		public string Title { get; set; }

		public Character Giver { get; set; }
		public Character Taker { get; set; }
		public Motivation Motivation { get; set; }
	}
}