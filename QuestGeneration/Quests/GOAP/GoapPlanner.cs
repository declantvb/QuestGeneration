//using QuestGeneration;
//using System;
//using System.Collections.Generic;
//using System.Linq;

///**
// * Plans what actions can be completed in order to fulfill a goal state.
// */

//public class GoapPlanner
//{
//	/**
//	 * Plan what sequence of actions can fulfill the goal.
//	 * Returns null if a plan could not be found, or a list of the actions
//	 * that must be performed, in order, to fulfill the goal.
//	 */

//	public Queue<PoclAction> plan(HashSet<PoclAction> availableActions,
//								  List<Fact> worldState,
//								  List<Fact> goal)
//	{
//		// build up the tree and record the leaf nodes that provide a solution to the goal.
//		List<Node> leaves = new List<Node>();

//		// build graph
//		Node start = new Node(null, 0, worldState, null);
//		bool success = buildGraph(start, leaves, availableActions, goal);

//		if (!success)
//		{
//			// oh no, we didn't get a plan
//			Console.WriteLine("NO PLAN");
//			return null;
//		}

//		// get the cheapest leaf
//		Node cheapest = null;
//		foreach (Node leaf in leaves)
//		{
//			if (cheapest == null)
//				cheapest = leaf;
//			else
//			{
//				if (leaf.runningCost < cheapest.runningCost)
//					cheapest = leaf;
//			}
//		}

//		// get its node and work back through the parents
//		List<PoclAction> result = new List<PoclAction>();
//		Node n = cheapest;
//		while (n != null)
//		{
//			if (n.action != null)
//			{
//				result.Insert(0, n.action); // insert the action in the front
//			}
//			n = n.parent;
//		}
//		// we now have this action list in correct order

//		Queue<PoclAction> queue = new Queue<PoclAction>();
//		foreach (PoclAction a in result)
//		{
//			queue.Enqueue(a);
//		}

//		// hooray we have a plan!
//		return queue;
//	}

//	/**
//	 * Returns true if at least one solution was found.
//	 * The possible paths are stored in the leaves list. Each leaf has a
//	 * 'runningCost' value where the lowest cost will be the best action
//	 * sequence.
//	 */

//	private bool buildGraph(Node parent, List<Node> leaves, HashSet<PoclAction> usableActions, List<Fact> goal)
//	{
//		bool foundOne = false;

//		// go through each action available at this node and see if we can use it here
//		foreach (PoclAction action in usableActions)
//		{
//			// if the parent state has the conditions for this action's preconditions, we can use it here
//			if (inState(action.Preconditions, parent.state))
//			{

//				// apply the action's effects to the parent state
//				var currentState = addState(parent.state, action.RealiseEffects(parent.state));
//				//Debug.Log(GoapAgent.prettyPrint(currentState));
//				var node = new Node(parent, parent.runningCost + action.Cost, currentState, action);

//				if (inState(goal, currentState))
//				{
//					// we found a solution!
//					leaves.Add(node);
//					foundOne = true;
//				}
//				else
//				{
//					// not at a solution yet, so test all the remaining actions and branch out the tree
//					HashSet<PoclAction> subset = actionSubset(usableActions, action);
//					bool found = buildGraph(node, leaves, subset, goal);
//					if (found)
//						foundOne = true;
//				}
//			}
//		}

//		return foundOne;
//	}

//	/**
//	 * Create a subset of the actions excluding the removeMe one. Creates a new set.
//	 */

//	private HashSet<PoclAction> actionSubset(HashSet<PoclAction> actions, PoclAction removeMe)
//	{
//		HashSet<PoclAction> subset = new HashSet<PoclAction>();
//		foreach (PoclAction a in actions)
//		{
//			if (!a.Equals(removeMe))
//				subset.Add(a);
//		}
//		return subset;
//	}

//	/**
//	 * Check that all items in 'test' are in 'state'. If just one does not match or is not there
//	 * then this returns false.
//	 */

//	private bool inState(List<Fact> test, List<Fact> state)
//	{
//		bool allMatch = true;
//		foreach (Fact t in test)
//		{
//			bool match = false;
//			foreach (Fact s in state)
//			{
//				if (s.Equals(t))
//				{
//					match = true;
//					break;
//				}
//			}
//			if (!match)
//				allMatch = false;
//		}
//		return allMatch;
//	}

//	private bool inState(Dictionary<string, bool> test, List<Fact> state)
//	{
//		bool allMatch = true;
//		foreach (KeyValuePair<string, bool> t in test)
//		{
//			bool match = false;
//			foreach (Fact s in state)
//			{
//				if (s.Key.Type == t.Key)
//				{
//					match = true;
//					break;
//				}
//			}
//			if (!match)
//				allMatch = false;
//		}
//		return allMatch;
//	}

//	/**
//	 * Apply the stateChange to the currentState
//	 */

//	private List<Fact> addState(List<Fact> currentState, List<Fact> stateChange)
//	{
//		var state = currentState.ToDictionary(x => x.Key, x => x.Value);

//		foreach (KeyValuePair<Fact,bool> change in stateChange)
//		{
//			// if the key exists in the current state, update the Value
//			bool exists = false;

//			foreach (Fact s in state)
//			{
//				if (s.Equals(change))
//				{
//					exists = true;
//					break;
//				}
//			}

//			if (!exists)
//			{
//				state.Add(change.Key, change.Value);
//			}
//		}
//		return state;
//	}

//	private List<Fact> subtractState(List<Fact> currentState, List<Fact> stateChange)
//	{
//		var state = currentState.ToDictionary(x => x.Key, x => x.Value);

//		foreach (Fact change in stateChange)
//		{
//			// if the key exists in the current state, update the Value
//			bool exists = false;

//			foreach (Fact s in state)
//			{
//				if (s.Equals(change))
//				{
//					exists = true;
//					break;
//				}
//			}

//			if (exists)
//			{
//				state.Remove(change.Key);
//			}
//		}
//		return state;
//	}

//	/**
//	 * Used for building up the graph and holding the running costs of actions.
//	 */

//	private class Node
//	{
//		public Node parent;
//		public float runningCost;
//		public List<Fact> state;
//		public PoclAction action;

//		public Node(Node parent, float runningCost, List<Fact> state, PoclAction action)
//		{
//			this.parent = parent;
//			this.runningCost = runningCost;
//			this.state = state;
//			this.action = action;
//		}
//	}
//}