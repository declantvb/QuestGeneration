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
				Descriptor = "battle",
				Time = "recently",
				Location = "nearby",
				Tense = Tense.Past,
				Reason = new MotivationReason
				{
					Object = "my mech",
					Descriptor = "is broken",
					Resolution = "to repair"
				}
			};
		}
	}
}