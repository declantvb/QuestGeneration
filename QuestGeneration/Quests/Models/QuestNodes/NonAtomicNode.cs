using System.Collections.Generic;

namespace QuestGeneration
{
	public abstract class NonAtomicNode : QuestNode
	{
		public abstract List<QuestNode[]> Replacements();
	}
}