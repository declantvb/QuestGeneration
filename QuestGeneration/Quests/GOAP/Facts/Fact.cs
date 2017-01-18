using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGeneration
{
	public abstract class Fact
	{
		public bool Value { get; set; }

		public abstract bool CouldEqual(Fact other);
		public abstract bool IsComplete();

		public abstract Fact Clone(bool negate);

		public override bool Equals(object obj)
		{
			var factObj = obj as Fact;
			return factObj != null && factObj.Value == Value;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public abstract override string ToString();
	}
}