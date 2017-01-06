using System;
using System.Collections.Generic;

namespace QuestGeneration
{
	public class KnowledgeNode : NonAtomicNode
	{
		public override List<QuestNode[]> Replacements()
		{
			return new List<QuestNode[]>
			{
				new QuestNode[] { }
			};
		}
	}
}