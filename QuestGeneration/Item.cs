using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGeneration
{
	public class Item
	{
		public string Key { get; set; }
		public string Article { get; set; }
		public string CountablePrefix { get; set; }
		public string Name { get; set; }

		public ItemType Type { get; set; }

		public Item(string key, string article, string countablePrefix, string name, ItemType type)
		{
			Key = key;
			Article = article;
			CountablePrefix = countablePrefix;
			Name = name;
			Type = type;
		}
	}

	public enum ItemType
	{
		Standard,
		Currency
	}
}
