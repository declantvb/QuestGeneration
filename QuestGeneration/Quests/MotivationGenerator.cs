using System;

namespace QuestGeneration
{
	public class MotivationGenerator
	{
		public MotivationGenerator()
		{
		}

		public Motivation Motive()
		{
			return new Motivation
			{
				SubjectNoun = "mech",
				ProblemAdjective = "broken",
				ResolutionVerb = "repair",
				Reason = new MotivationReason
				{
					Noun = "battle",
					Time = "recent",
					Location = "nearby",
				}
			};
		}
	}
}