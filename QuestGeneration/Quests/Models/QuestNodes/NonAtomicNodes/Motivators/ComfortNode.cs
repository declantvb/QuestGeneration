using System.Collections.Generic;

namespace QuestGeneration
{
	public class ComfortNode : NonAtomicNode
	{
		public override List<QuestNode[]> Replacements()
		{
			return new List<QuestNode[]>
			{
				new QuestNode[] { new GetNode(), new GotoNode(), new GiveNode() },
				new QuestNode[] { new GotoNode(), new KillNode(), new GotoNode(), new ReportNode() }
			};
		}
	}
}