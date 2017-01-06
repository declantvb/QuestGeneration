using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGeneration
{
	public class LocationDatabase
	{
		private Dictionary<string, Location> _database;
		private Random _random;

		public LocationDatabase()
		{
			_database = new Dictionary<string, Location>();
			_random = new Random();

			AddItem(new Location { Name = "Great Plains", Friendly = true });
			AddItem(new Location { Name = "Frotown", Friendly = true });
			AddItem(new Location { Name = "Kangorg's Lair", Friendly = false });
			AddItem(new Location { Name = "The Slash", Friendly = false });
		}

		public Location GetRandomTarget()
		{
			var targets = _database.Where(x => !x.Value.Friendly);
			return targets.ElementAt(_random.Next(targets.Count())).Value;
		}

		public Location GetRandom()
		{
			var targets = _database.Where(x => x.Value.Friendly);
			return targets.ElementAt(_random.Next(targets.Count())).Value;
		}

		public Location Get(string key)
		{
			if (!_database.ContainsKey(key))
			{
				return null;
			}

			return _database[key];
		}

		public void AddItem(Location item)
		{
			_database.Add(item.Name, item);
		}
	}
}