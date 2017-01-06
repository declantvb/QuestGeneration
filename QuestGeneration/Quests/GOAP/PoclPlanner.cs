using QuestGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

public class PoclPlanner
{
	private int maxIterations = 199;

	public Tuple<List<PoclAction>, List<Ordering>> MakePlan(List<PoclAction> availableActions,
		Dictionary<Fact, bool> worldState,
		Dictionary<Fact, bool> goal)
	{
		var rand = new Random();

		var selectedActions = new List<PoclAction>();
		var ordering = new List<Ordering>();
		var openPreconditions = goal.ToList();

		var i = 0;

		while (i < maxIterations)
		{
			//attempt to solve one precondition
			var precondition = openPreconditions[rand.Next(openPreconditions.Count)];
			var applicableActions = availableActions.Where(a => a.Effects.Any(e => e.Key.Type == precondition.Key.Type && e.Value == precondition.Value));

			var shuffled = applicableActions.OrderBy(x => x.Cost, new FloatRandomComparer(rand));

			foreach (var action in shuffled)
			{
				var newAction = action.Clone();
				newAction.FillGiven(precondition);

				openPreconditions.Remove(precondition);
				foreach (var newPrecond in newAction.Preconditions)
				{
					if (worldState.Any(x => x.Key.Equals(newPrecond.Key) && x.Value == newPrecond.Value))
					{
						//already satisfied
						continue;
					}
					var matches = worldState.Where(x => CouldMatch(x.Key, newPrecond.Key) && x.Value == newPrecond.Value).ToList();

					if (matches.Any())
					{
						//choose?
						var match = matches.First();
						newAction.FillGiven(match);
					}
					else
					{
						openPreconditions.Add(newPrecond);
					}
				}
			}

			i++;
		}

		return new Tuple<List<PoclAction>, List<Ordering>>(selectedActions, ordering);
	}

	private bool CouldMatch(Fact key1, Fact key2)
	{
		throw new NotImplementedException();
	}
}

internal class FloatRandomComparer : IComparer<float>
{
	private Random rand;

	public FloatRandomComparer(Random rand)
	{
		this.rand = rand;
	}

	public int Compare(float x, float y)
	{
		if (x > y)
		{
			return 1;
		}
		else if (x < y)
		{
			return -1;
		}
		else
		{
			return rand.Next(-1, 2);
		}
	}
}