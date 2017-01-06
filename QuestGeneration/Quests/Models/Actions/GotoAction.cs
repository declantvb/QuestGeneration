using System;
using System.Collections.Generic;

namespace QuestGeneration
{
	public class GotoAction : PoclAction
	{
		public Character char1 { get; set; }
		public Location loc1 { get; set; }
		public Location loc2 { get; set; }

		public GotoAction()
		{
			char1 = Character.Unknown;
			loc1 = Location.Unknown;
			loc2 = Location.Unknown;

			Init();
		}

		private void Init()
		{
			Preconditions = new Dictionary<Fact, bool>();
			Effects = new Dictionary<Fact, bool>();

			Preconditions.Add(new Fact(GoapKeys.AtLocation, char1, loc1), true);
			//Effects.Add(new Fact(GoapKeys.AtLocation, char1, loc1), false);
			Effects.Add(new Fact(GoapKeys.AtLocation, char1, loc2), true);
		}

		public override PoclAction Clone()
		{
			return new GotoAction();
		}

		public override void FillGiven(KeyValuePair<Fact, bool> kvp)
		{
			var fact = kvp.Key;

			if (fact.Type == GoapKeys.AtLocation)
			{
				if (kvp.Value)
				{
					char1 = fact.Subject as Character;
					loc2 = fact.Object as Location;
					Init();
				}
				else
				{
					char1 = fact.Subject as Character;
					loc1 = fact.Object as Location;
					Init();
				}
			}
			else
			{
				Console.WriteLine("err");
			}
		}
	}
}