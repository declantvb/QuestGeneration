using System;
using System.Collections.Generic;

namespace QuestGeneration
{
	public class QuestGenerator
	{
		private Random rand;
		private ItemDatabase id;
		private LocationDatabase ld;
		private PoclPlanner planner;
		private List<PoclAction> actions;

		public QuestGenerator()
		{
			rand = new Random();
			id = new ItemDatabase();
			ld = new LocationDatabase();
			planner = new PoclPlanner();

			actions = new List<PoclAction>
			{
				new GotoAction(),
			};
		}

		public Quest GenerateQuest(Character player, Character questGiver)
		{
			var q = new Quest
			{
				Taker = player,
				Giver = questGiver
			};

			//select motivation;
			q.Motivation = QuestMotivation.Motivations[rand.Next(QuestMotivation.Motivations.Length)];

			q.Strategy = q.Motivation.Strategies[rand.Next(q.Motivation.Strategies.Length)];

			q.GoalState = RealiseGoalState(q);
			q.WorldState = GetStartState(q);

			var asd = planner.MakePlan(actions, q.WorldState, q.GoalState);

			return q;
		}

		private Dictionary<Fact, bool> GetStartState(Quest q)
		{
			return new Dictionary<Fact, bool>
			{
				{ new Fact(GoapKeys.AtLocation, q.Taker, ld.GetRandom()), true }
			};
		}

		private Dictionary<Fact, bool> RealiseGoalState(Quest q)
		{
			var ret = new Dictionary<Fact, bool>();
			foreach (var kvp in q.Strategy.GoalState)
			{
				Fact fact;
				switch (kvp.Key.Type)
				{
					case GoapKeys.AtLocation:
						fact = new Fact(kvp.Key.Type, q.Taker, ld.GetRandomTarget());
						break;

					default:
						throw new Exception("bad fact key");
				}
				ret.Add(fact, kvp.Value);
			}
			return ret;
		}
	}
}