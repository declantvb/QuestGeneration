public class Ordering
{
	public PoclAction From { get; protected set; }
	public PoclAction To { get; protected set; }

	public Ordering(PoclAction from, PoclAction to)
	{
		From = from;
		To = to;
	}
}