namespace QuestGeneration
{
	public class Location
	{
		public string Name { get; set; }
		public bool Friendly { get; set; }

		public static Location Unknown = new Location { Name = "Unknown", Friendly = true };

		public override string ToString()
		{
			return Name;
		}
	}
}