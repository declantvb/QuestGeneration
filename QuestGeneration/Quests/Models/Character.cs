using System;

namespace QuestGeneration
{
	public class Character
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Name { get { return string.Format("{0} {1}", FirstName, LastName); } }
		public Gender Gender { get; set; }

		public static Character Unknown = new Character { FirstName = "Unknown", LastName = "Unknown", Gender = Gender.Neuter };

		public string GetText(string token)
		{
			switch (token.ToLowerInvariant())
			{
				case "name":
					return Name;

				default:
					throw new Exception("unknown token");
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}
}