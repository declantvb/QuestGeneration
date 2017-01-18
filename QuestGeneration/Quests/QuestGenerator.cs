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
				new GetItemAction()
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

		private List<Fact> GetStartState(Quest q)
		{
			return new List<Fact>
			{
				{ new CharacterAtLocationFact(q.Taker, ld.Get("Great Plains"), true) },
				{ new ItemAtLocationFact(id.Get("widget"), ld.Get("The Slash"), true) }
			};
		}

		private List<Fact> RealiseGoalState(Quest q)
		{
			var ret = new List<Fact>();
			foreach (var subGoal in q.Strategy.GoalState)
			{
				var atLocationFact = subGoal as CharacterAtLocationFact;
				var hasItemFact = subGoal as HasItemFact;
				if (atLocationFact != null)
				{
					ret.Add(new CharacterAtLocationFact(q.Taker, ld.Get("Great Plains"), subGoal.Value));
				}
				else if (hasItemFact != null)
				{
					ret.Add(new HasItemFact(q.Taker, id.Get("widget"), subGoal.Value));
				}
				else
				{
					throw new Exception();
				}
			}
			return ret;
		}
	}
}