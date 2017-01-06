using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGeneration
{
	public class VehicleDatabase
	{
		private Dictionary<string, Vehicle> _database;
		private Random _random;

		public VehicleDatabase()
		{
			_database = new Dictionary<string, Vehicle>();
			_random = new Random();

			AddItem(new Vehicle { Key = "Buggy" });
			AddItem(new Vehicle { Key = "Van" });
			AddItem(new Vehicle { Key = "Tank" });
		}

		public Vehicle GetRandomTarget()
		{
			return GetRandom();
		}

		private Vehicle GetRandom()
		{
			return _database.ElementAt(_random.Next(_database.Count)).Value;
		}

		public Vehicle Get(string key)
		{
			if (!_database.ContainsKey(key))
			{
				return null;
			}

			return _database[key];
		}

		public void AddItem(Vehicle item)
		{
			_database.Add(item.Key, item);
		}
	}
}
