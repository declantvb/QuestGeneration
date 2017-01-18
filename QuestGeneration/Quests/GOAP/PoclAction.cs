using QuestGeneration;
using System.Collections.Generic;

public abstract class PoclAction
{
	public List<Fact> Preconditions;
	public List<Fact> Effects;
	
	public float Cost = 1f;

	public PoclAction()
	{
		Preconditions = new List<Fact>();
		Effects = new List<Fact>();
	}

	public abstract PoclAction Clone();

	public abstract bool FillGivenEffect(Fact fact);
	public abstract bool FillGivenPrecondition(Fact fact);
}