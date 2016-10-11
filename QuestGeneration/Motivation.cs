using System;

namespace QuestGeneration
{
	public class Motivation
	{
		public string Descriptor { get; set; }
		public string Location { get; set; }
		public string Time { get; set; }
		public Tense Tense { get; set; }
		public bool Plural { get; set; }
		public MotivationReason Reason { get; set; }

		public string GetText(string token)
		{
			switch (token.ToLowerInvariant())
			{
				case "descriptor":
					return Descriptor;
				case "location":
					return Location;
				case "time":
					return Time;
				default:
					throw new Exception("unknown token");
			}
		}
	}

	public class MotivationReason
	{
		public string Object { get; set; }
		public string Descriptor { get; set; }
		public string Resolution { get; set; }

		public string GetText(string token)
		{
			switch (token.ToLowerInvariant())
			{
				case "object":
					return Object;
				case "descriptor":
					return Descriptor;
				case "resolution":
					return Resolution;
				default:
					throw new Exception("unknown token");
			}
		}
	}
}