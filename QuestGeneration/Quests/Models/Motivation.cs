using System;

namespace QuestGeneration
{
	public class Motivation
	{
		public string SubjectNoun { get; set; }
		public string ProblemAdjective { get; set; }
		public string ResolutionVerb { get; set; }

		public MotivationReason Reason { get; set; }

		public string GetText(string token)
		{
			switch (token.ToLowerInvariant())
			{
				case "noun":
				case "subject":
					return SubjectNoun;
				case "problem":
				case "adjective":
					return ProblemAdjective;
				case "resolution":
				case "verb":
					return ResolutionVerb;
				default:
					throw new Exception("unknown token");
			}
		}
	}

	public class MotivationReason
	{
		public string Noun { get; set; }
		public string Location { get; set; }
		public string Time { get; set; }
		public bool Plural { get; set; }

		public string GetText(string token)
		{
			switch (token.ToLowerInvariant())
			{
				case "noun":
					return Noun;
				case "location":
					return Location;
				case "time":
					return Time;
				default:
					throw new Exception("unknown token");

			}
		}
	}
}