using System;
using System.Collections.Generic;

namespace QuestGeneration
{
	public class GetItemAction : PoclAction
	{
		public Character char1 { get; set; }
		public Item item1 { get; set; }
		public Location loc1 { get; set; }

		public GetItemAction()
		{
			char1 = Character.Unknown;
			item1 = Item.Unknown;
			loc1 = Location.Unknown;

			Init();
		}

		private void Init()
		{
			Preconditions = new List<Fact>();
			Effects = new List<Fact>();

			Preconditions.Add(new CharacterAtLocationFact(char1, loc1, true));
			Preconditions.Add(new ItemAtLocationFact(item1, loc1, true));

			Effects.Add(new ItemAtLocationFact(item1, loc1, false));
			Effects.Add(new HasItemFact(char1, item1, true));
		}

		public override PoclAction Clone()
		{
			return new GetItemAction();
		}

		public override bool FillGivenEffect(Fact fact)
		{
			var itemAtLocationFact = fact as ItemAtLocationFact;
			if (itemAtLocationFact != null && !fact.Value)
			{
				if ((item1 == Item.Unknown || item1 == itemAtLocationFact.Subject) &&
					(loc1 == Location.Unknown || loc1 == itemAtLocationFact.Object))
				{
					item1 = itemAtLocationFact.Subject;
					loc1 = itemAtLocationFact.Object;
					Init();
					return true;
				}
				return false;
			}

			var hasItemFact = fact as HasItemFact;
			if (hasItemFact != null && fact.Value)
			{
				if ((char1 == Character.Unknown || char1 == hasItemFact.Subject) &&
					(item1 == Item.Unknown || item1 == hasItemFact.Object))
				{
					char1 = hasItemFact.Subject;
					item1 = hasItemFact.Object;
					Init();
					return true;
				}
				return false;
			}

			Console.WriteLine("err");
			return false;
		}

		public override bool FillGivenPrecondition(Fact fact)
		{
			var characterAtLocationFact = fact as CharacterAtLocationFact;
			if (characterAtLocationFact != null && fact.Value)
			{
				if ((char1 == Character.Unknown || char1 == characterAtLocationFact.Subject) &&
					(loc1 == Location.Unknown || loc1 == characterAtLocationFact.Object))
				{
					char1 = characterAtLocationFact.Subject;
					loc1 = characterAtLocationFact.Object;
					Init();
					return true;
				}
				return false;
			}

			var itemAtLocationFact = fact as ItemAtLocationFact;
			if (itemAtLocationFact != null && fact.Value)
			{
				if ((item1 == Item.Unknown || item1 == itemAtLocationFact.Subject) &&
					(loc1 == Location.Unknown || loc1 == itemAtLocationFact.Object))
				{
					item1 = itemAtLocationFact.Subject;
					loc1 = itemAtLocationFact.Object;
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