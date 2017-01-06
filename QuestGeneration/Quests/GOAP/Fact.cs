using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGeneration
{
	public class Fact
	{
		public string Type { get; protected set; }

		public object Subject { get; protected set; }
		public object Object { get; protected set; }

		public Fact(string key, object subject = null, object @object = null)
		{
			Type = key;
			Subject = subject;
			Object = @object;
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != this.GetType())
			{
				return false;
			}

			var objFact = obj as Fact;

			if (Type != objFact.Type)
			{
				return false;
			}

			if (Subject != objFact.Subject)
			{
				return false;
			}

			if (Object != objFact.Object)
			{
				return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			var hash = 37 + Type.GetHashCode();
			if (Subject != null) hash = (hash * 19) + Subject.GetHashCode();
			if (Object != null) hash = (hash * 19) + Object.GetHashCode();

			return hash;
		}

		public override string ToString()
		{
			var ret = Type;

			if (Subject != null)
			{
				ret += "-" + Subject.ToString();
			}

			if (Object != null)
			{
				ret += "-" + Object.ToString();
			}

			return ret;
		}
	}
}