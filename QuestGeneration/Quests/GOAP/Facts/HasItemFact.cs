namespace QuestGeneration
{
	public class HasItemFact : Fact
	{
		public Character Subject { get; set; }
		public Item Object { get; set; }

		public HasItemFact(Character subject, Item @object, bool value)
		{
			Subject = subject;
			Object = @object;
			Value = value;
		}

		public override bool CouldEqual(Fact other)
		{
			var atLocationFact = other as HasItemFact;
			if (atLocationFact == null)
			{
				return false;
			}

			if (Subject != Character.Unknown && atLocationFact.Subject != Subject)
			{
				return false;
			}

			if (Object != Item.Unknown && atLocationFact.Object != Object)
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

			var atLocationFact = obj as HasItemFact;
			if (atLocationFact == null)
			{
				return false;
			}

			if (Subject == Character.Unknown || atLocationFact.Subject != Subject)
			{
				return false;
			}

			if (Object == Item.Unknown || atLocationFact.Object != Object)
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
			return Subject != Character.Unknown && Object != Item.Unknown;
		}

		public override Fact Clone(bool negate)
		{
			return new HasItemFact(Subject, Object, negate ? !Value : Value);
		}

		public override string ToString()
		{
			return string.Join("|", "HasItem", Subject, Object, Value);
		}
	}
}