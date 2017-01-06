using QuestGeneration;
using System.Collections.Generic;

public abstract class PoclAction
{
	public Dictionary<Fact, bool> Preconditions;
	public Dictionary<Fact, bool> Effects;
	
	public float Cost = 1f;

	public PoclAction()
	{
		Preconditions = new Dictionary<Fact, bool>();
		Effects = new Dictionary<Fact, bool>();
	}

	public abstract PoclAction Clone();

	public abstract void FillGiven(KeyValuePair<Fact,bool> kvp);
}