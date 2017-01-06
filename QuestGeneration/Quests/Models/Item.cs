namespace QuestGeneration
{
	public class Item
	{
		public string Key { get; set; }
		public string Article { get; set; }
		public string CountablePrefix { get; set; }
		public string Name { get; set; }

		public float Value { get; set; }

		public ItemType Type { get; set; }

		public static Item Unknown = new Item("unknown", "an", null, "Unknown", ItemType.Standard);

		public Item(string key, string article, string countablePrefix, string name, ItemType type, float value = 0f)
		{
			Key = key;
			Article = article;
			CountablePrefix = countablePrefix;
			Name = name;
			Type = type;
			Value = value;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public enum ItemType
	{
		Standard,
		Currency
	}
}