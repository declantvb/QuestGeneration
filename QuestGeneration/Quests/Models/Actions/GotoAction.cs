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
			Preconditions = new List<Fact>();
			Effects = new List<Fact>();

			Preconditions.Add(new CharacterAtLocationFact(char1, loc1, true));

			Effects.Add(new CharacterAtLocationFact(char1, loc1, false));
			Effects.Add(new CharacterAtLocationFact(char1, loc2, true));
		}

		public override PoclAction Clone()
		{
			return new GotoAction();
		}

		public override bool FillGivenEffect(Fact fact)
		{
			var atLoc = fact as CharacterAtLocationFact;
			if (atLoc != null)
			{
				if (fact.Value)
				{
					if ((char1 == Character.Unknown || char1 == atLoc.Subject) &&
						(loc2 == Location.Unknown || loc2 == atLoc.Object))
					{
						char1 = atLoc.Subject;
						loc2 = atLoc.Object;
						Init();
						return true;
					}
					return false;
				}
				else
				{
					if ((char1 == Character.Unknown || char1 == atLoc.Subject) &&
						(loc1 == Location.Unknown || loc1 == atLoc.Object))
					{
						char1 = atLoc.Subject;
						loc1 = atLoc.Object;
						Init();
						return true;
					}
					return false;
				}
			}

			Console.WriteLine("err");
			return false;
		}

		public override bool FillGivenPrecondition(Fact fact)
		{
			var atLoc = fact as CharacterAtLocationFact;
			if (atLoc != null && fact.Value)
			{
				if ((char1 == Character.Unknown || char1 == atLoc.Subject) &&
					(loc1 == Location.Unknown || loc1 == atLoc.Object))
				{
					char1 = atLoc.Subject;
					loc1 = atLoc.Object;
					Init();
					return true;
				}
				return false;
			}

			Console.WriteLine("err");
			return false;
		}
	}
}