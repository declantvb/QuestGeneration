using System.Collections.Generic;
using System.Linq;

namespace QuestGeneration
{
	public class Quest
	{
		public QuestMotivation Motivation;

		public QuestStrategy Strategy;

		public Character Giver;
		public Character Taker;

		public Dictionary<Fact, bool> WorldState;
		public Dictionary<Fact, bool> GoalState;

		public List<Item> Items;
		public List<Character> Characters;

		public Queue<PoclAction> Sequence;

		public Quest()
		{
			Items = new List<Item>();
			Characters = new List<Character>();
		}

		public override string ToString()
		{
			return string.Join(" -> ", Sequence.Select(x => x.GetType().Name));
		}
	}

	public class QuestMotivation
	{
		public string Name;

		public QuestStrategy[] Strategies;

		public static QuestMotivation[] Motivations = new QuestMotivation[]
		{
			new QuestMotivation
			{
				Name = "Test",
				Strategies = new QuestStrategy[]
				{
					new QuestStrategy("Goto place", new Dictionary<Fact, bool>
					{
						{ new Fact(GoapKeys.AtLocation), true }
					}),
					//new QuestStrategy("Interview NPC", new Dictionary<string,bool>
					//{
					//	{ GoapKeys.HaveInformation, true }
					//})
				}
			},
			//new QuestMotivation
			//{
			//	Name = "Comfort",
			//	Strategies = new QuestStrategy[]
			//	{
			//		new QuestStrategy("Kill pests ", new Dictionary<string,bool>
			//		{
			//			{ GoapKeys.IsDead, true }
			//		}),
			//	}
			//}
		};
	}

	public class QuestStrategy
	{
		public string Name;
		public Dictionary<Fact, bool> GoalState;

		public QuestStrategy(string name, Dictionary<Fact, bool> goal)
		{
			Name = name;
			GoalState = goal;
		}
	}
}