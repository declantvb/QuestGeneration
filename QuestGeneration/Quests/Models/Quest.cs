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

		public List<Fact> WorldState;
		public List<Fact> GoalState;

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
					new QuestStrategy("Goto place", new List<Fact>
					{
						//{ new CharacterAtLocationFact(Character.Unknown, Location.Unknown), true },

						{ new HasItemFact(Character.Unknown, Item.Unknown, true) }
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
		public List<Fact> GoalState;

		public QuestStrategy(string name, List<Fact> goal)
		{
			Name = name;
			GoalState = goal;
		}
	}
}