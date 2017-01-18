using System;

namespace QuestGeneration
{
	public class CharacterAtLocationFact : Fact
	{
		public Character Subject { get; set; }
		public Location Object { get; set; }

		public CharacterAtLocationFact(Character subject, Location @object, bool value)
		{
			Subject = subject;
			Object = @object;
			Value = value;
		}

		public override bool CouldEqual(Fact other)
		{
			var atLocationFact = other as CharacterAtLocationFact;
			if (atLocationFact == null)
			{
				return false;
			}

			if (Subject != Character.Unknown && atLocationFact.Subject != Subject)
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

			var atLocationFact = obj as CharacterAtLocationFact;
			if (atLocationFact == null)
			{
				return false;
			}

			if (Subject == Character.Unknown || atLocationFact.Subject != Subject)
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
			return Subject != Character.Unknown && Object != Location.Unknown;
		}

		public override Fact Clone(bool negate)
		{
			return new CharacterAtLocationFact(Subject, Object, negate ? !Value : Value);
		}

		public override string ToString()
		{
			return string.Join("|", "CharacterAtLocation", Subject, Object, Value);
		}
	}
}