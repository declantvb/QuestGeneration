using System;
using System.Collections.Generic;

namespace QuestGeneration
{
	public class GetNode : NonAtomicNode
	{
		public override List<QuestNode[]> Replacements()
		{
			return new List<QuestNode[]>
			{
				new QuestNode[] {},
				new QuestNode[] { new GotoNode() }
			};
		}
	}
}