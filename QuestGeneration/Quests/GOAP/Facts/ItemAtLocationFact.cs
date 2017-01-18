using System;

namespace QuestGeneration
{
	public class ItemAtLocationFact : Fact
	{
		public Item Subject { get; set; }
		public Location Object { get; set; }

		public ItemAtLocationFact(Item subject, Location @object, bool value)
		{
			Subject = subject;
			Object = @object;
			Value = value;
		}

		public override bool CouldEqual(Fact other)
		{
			var atLocationFact = other as ItemAtLocationFact;
			if (atLocationFact == null)
			{
				return false;
			}

			if (Subject != Item.Unknown && atLocationFact.Subject != Subject)
			{
				return false;
			}

			if (Object != Location.Unknown && atLocationFact.Object != Object)
			{
				return false;
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			var atLocationFact = obj as ItemAtLocationFact;
			if (atLocationFact == null)
			{
				return false;
			}

			if (Subject == Item.Unknown || atLocationFact.Subject != Subject)
			{
				return false;
			}

			if (Object == Location.Unknown || atLocationFact.Object != Object)
			{
				return false;
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return Subject.GetHashCode() ^ Object.GetHashCode() ^ base.GetHashCode();
		}

		public override bool IsComplete()
		{
			return Subject != Item.Unknown && Object != Location.Unknown;
		}

		public override Fact Clone(bool negate)
		{
			return new ItemAtLocationFact(Subject, Object, negate ? !Value : Value);
		}

		public override string ToString()
		{
			return string.Join("|", "ItemAtLocation", Subject, Object, Value);
		}
	}
}