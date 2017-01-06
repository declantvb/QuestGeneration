using System;

namespace QuestGeneration
{
	public class PronounSet
	{
		public string Subject { get; set; }
		public string Object { get; set; }
		public string DependentPossessive { get; set; }
		public string IndependentPossessive { get; set; }
		public string Reflexive { get; set; }

		private PronounSet() { }

		public string GetPronoun(string test)
		{
			switch (test.ToLowerInvariant())
			{
				case "subject":
					return Subject;
				case "object":
					return Object;
				case "dependentpossessive":
					return DependentPossessive;
				case "independentpossessive":
					return IndependentPossessive;
				case "reflexive":
					return Reflexive;

				default:
					throw new Exception("invalid pronoun");
			}
		}

		public static PronounSet FromString(string test)
		{
			switch (test.ToLowerInvariant())
			{
				case "first":
					return First;
				case "second":
					return Second;
				case "thirdmale":
					return ThirdMale;
				case "thirdfemale":
					return ThirdFemale;
				case "thirdneuter":
					return ThirdNeuter;
				case "firstplural":
					return FirstPlural;
				case "secondplural":
					return SecondPlural;
				case "thirdplural":
					return ThirdPlural;

				default:
					throw new Exception("invalid gender");
			}
		}

		public static PronounSet First = new PronounSet
		{
			Subject = "I",
			Object = "me",
			DependentPossessive = "my",
			IndependentPossessive = "mine",
			Reflexive = "myself"
		};
		public static PronounSet Second = new PronounSet
		{
			Subject = "you",
			Object = "you",
			DependentPossessive = "your",
			IndependentPossessive = "yours",
			Reflexive = "yourself"
		};
		public static PronounSet ThirdMale = new PronounSet
		{
			Subject = "he",
			Object = "him",
			DependentPossessive = "his",
			IndependentPossessive = "his",
			Reflexive = "himself"
		};
		public static PronounSet ThirdFemale = new PronounSet
		{
			Subject = "she",
			Object = "her",
			DependentPossessive = "her",
			IndependentPossessive = "hers",
			Reflexive = "herself"
		};
		public static PronounSet ThirdNeuter = new PronounSet
		{
			Subject = "it",
			Object = "it",
			DependentPossessive = "its",
			IndependentPossessive = "its",
			Reflexive = "itself"
		};
		public static PronounSet FirstPlural = new PronounSet
		{
			Subject = "we",
			Object = "us",
			DependentPossessive = "our",
			IndependentPossessive = "ours",
			Reflexive = "ourselves"
		};
		public static PronounSet SecondPlural = new PronounSet
		{
			Subject = "you",
			Object = "you",
			DependentPossessive = "your",
			IndependentPossessive = "yours",
			Reflexive = "yourselves"
		};
		public static PronounSet ThirdPlural = new PronounSet
		{
			Subject = "they",
			Object = "them",
			DependentPossessive = "their",
			IndependentPossessive = "theirs",
			Reflexive = "themselves"
		};
	}
}