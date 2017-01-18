using QuestGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

public class PoclPlanner
{
	private int maxIterations = 199;

	private List<Fact> openPreconditions;
	private List<PoclAction> selectedActions;
	private List<Tuple<PoclAction, PoclAction>> partialOrdering;
	private List<Tuple<PoclAction, PoclAction>> causalLinks;

	public Tuple<List<PoclAction>, List<Tuple<PoclAction, PoclAction>>> MakePlan(List<PoclAction> availableActions,
		List<Fact> worldState,
		List<Fact> goal)
	{
		var rand = new Random();

		openPreconditions = goal.ToList();
		selectedActions = new List<PoclAction>();
		partialOrdering = new List<Tuple<PoclAction, PoclAction>>();
		causalLinks = new List<Tuple<PoclAction, PoclAction>>();

		var preconditionMap = new Dictionary<Fact, PoclAction>();

		var i = 0;

		while (i < maxIterations)
		{
			if (!openPreconditions.Any())
			{
				//done!
				break;
			}
			//attempt to solve one precondition
			Fact precondition = openPreconditions[rand.Next(openPreconditions.Count)];

			//var completePreconditions = openPreconditions.Where(x => x.Key.IsComplete()).ToList();
			//if (!completePreconditions.Any())
			//{
			//	var fix = openPreconditions[rand.Next(openPreconditions.Count)];

			//	var matches = worldState.Where(x => fix.Key.CouldEqual(x.Key) && x.Value == fix.Value).ToList();

			//	if (matches.Any())
			//	{
			//		var action = preconditionMap[fix];

			//		//choose?
			//		var match = matches.First();
			//		if (action.FillGivenPrecondition(match))
			//		{
			//			openPreconditions.Remove(fix);
			//		}

			//		//what to do with other preconditions for action?
			//		var delete = preconditionMap.Where(x => x.Value == action);

			//		foreach (var precond in delete)
			//		{
			//			openPreconditions.Remove(precond.Key);
			//		}

			//		foreach (var precond in action.Preconditions)
			//		{
			//			openPreconditions.Add(precond);
			//			preconditionMap.Add(precond, action);
			//		}

			//		continue;
			//	}
			//	else
			//	{
			//		Console.WriteLine("oops");
			//		throw new Exception();
			//	}
			//}
			//else
			//{
			//	precondition = completePreconditions[rand.Next(completePreconditions.Count)];
			//}

			var applicableActions = availableActions.Where(a => a.Effects.Any(e => e.GetType().Name == precondition.GetType().Name && e.Value == precondition.Value));

			var shuffled = applicableActions.OrderBy(x => x.Cost, new FloatRandomComparer(rand));

			foreach (var action in shuffled)
			{
				var newAction = action.Clone();
				newAction.FillGivenEffect(precondition);

				selectedActions.Add(newAction);

				// if precondition is from another action
				if (preconditionMap.ContainsKey(precondition))
				{
					var parentAction = preconditionMap[precondition];

					partialOrdering.Add(new Tuple<PoclAction, PoclAction>(newAction, parentAction));
					causalLinks.Add(new Tuple<PoclAction, PoclAction>(newAction, parentAction));
				}

				openPreconditions.Remove(precondition);
				foreach (var precond in newAction.Preconditions)
				{
					if (worldState.Any(x => x.Equals(precond) && x.Value == precond.Value))
					{
						//already satisfied
						continue;
					}
					//var matches = worldState.Where(x => precond.Key.CouldEqual(x.Key) && x.Value == precond.Value).ToList();

					//if (matches.Any())
					//{
					//	//choose?
					//	var match = matches.First();
					//	newAction.FillGivenPrecondition(match);
					//}
					//else
					//{
					openPreconditions.Add(precond);
					preconditionMap.Add(precond, newAction);
					//}
				}

				foreach (var link in causalLinks)
				{
					bool overlapPossible = CouldFitAfter(newAction, link.Item1) && CouldFitBefore(newAction, link.Item2);
					bool linkViolated = link.Item2.Preconditions.Any(p=>newAction.Effects.Any(e=>e.Clone(true).Equals(p)));
					if (overlapPossible && linkViolated)
					{
						if (CouldFitBefore(newAction, link.Item1))
						{
							partialOrdering.Add(new Tuple<PoclAction, PoclAction>(newAction, link.Item1));
						}
						else if (CouldFitAfter(newAction, link.Item2))
						{
							partialOrdering.Add(new Tuple<PoclAction, PoclAction>(link.Item2, newAction));
						}
						else
						{
							//bad
							throw new Exception();
						}
					}
				}
			}

			i++;
		}

		return new Tuple<List<PoclAction>, List<Tuple<PoclAction, PoclAction>>>(selectedActions, partialOrdering);
	}

	private bool CouldFitBefore(PoclAction newAction, PoclAction test)
	{
		var before = GetAllBefore(newAction);
		before.Add(newAction);
		var beforeSet = new HashSet<PoclAction>(before.Distinct());
		var after = GetAllAfter(test);
		after.Add(test);
		var afterSet = new HashSet<PoclAction>(after.Distinct());

		return !partialOrdering.Any(x => afterSet.Contains(x.Item1) && beforeSet.Contains(x.Item2));
	}

	private bool CouldFitAfter(PoclAction newAction, PoclAction test)
	{
		var before = GetAllBefore(test);
		before.Add(test);
		var beforeSet = new HashSet<PoclAction>(before.Distinct());
		var after = GetAllAfter(newAction);
		after.Add(newAction);
		var afterSet = new HashSet<PoclAction>(after.Distinct());

		return !partialOrdering.Any(x => afterSet.Contains(x.Item1) && beforeSet.Contains(x.Item2));
	}

	private List<PoclAction> GetAllBefore(PoclAction target)
	{
		var ret = new List<PoclAction>();
		foreach (var order in partialOrdering)
		{
			if (order.Item2 == target)
			{
				ret.Add(order.Item1);
				ret.AddRange(GetAllBefore(order.Item1));
			}
		}
		return ret;
	}

	private List<PoclAction> GetAllAfter(PoclAction target)
	{
		var ret = new List<PoclAction>();
		foreach (var order in partialOrdering)
		{
			if (order.Item1 == target)
			{
				ret.Add(order.Item2);
				ret.AddRange(GetAllAfter(order.Item2));
			}
		}
		return ret;
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