using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGeneration
{
	public class ItemDatabase
	{
		private Dictionary<string, Item> _database;
		private Random _random;

		public ItemDatabase()
		{
			_database = new Dictionary<string, Item>();
			_random = new Random();

			AddItem(new Item("widget",		"a",	null,			"Widget",			ItemType.Standard));
			AddItem(new Item("ore_chunk",	"a",	"Chunk",	"Ore",				ItemType.Standard));
			AddItem(new Item("servo",		"a",	null,			"Servo",			ItemType.Standard));
			AddItem(new Item("energy_core",	"an",	null,			"Energy Core",		ItemType.Standard));

			AddItem(new Item("gold_bar",	"a",	null,			"Gold bar",			ItemType.Currency));
			AddItem(new Item("coin",		"a",	null,			"Coin",				ItemType.Currency));
		}

		/// <summary>
		/// Random item except specified
		/// Needs refinement
		/// </summary>
		public Item GetRandomReward(params Item[] except)
		{
			Item reward;

			do
			{
				reward = GetRandom();
			} while (except.Any(x => x.Key == reward.Key));

			return reward;
		}

		/// <summary>
		/// Non-currency item
		/// </summary>
		public Item GetRandomItem()
		{
			Item item;

			do
			{
				item = GetRandom();
			} while (item.Type == ItemType.Currency);

			return item;
		}

		private Item GetRandom()
		{
			return _database.ElementAt(_random.Next(_database.Count)).Value;
		}

		public Item Get(string key)
		{
			if (!_database.ContainsKey(key))
			{
				return null;
			}

			return _database[key];
		}

		public void AddItem(Item item)
		{
			_database.Add(item.Key, item);
		}
	}
}
