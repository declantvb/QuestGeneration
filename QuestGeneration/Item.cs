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
		public string Name { get; set; }

		public ItemType Type { get; set; }

		public Item(string key, string name, ItemType type)
		{
			Key = key;
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
