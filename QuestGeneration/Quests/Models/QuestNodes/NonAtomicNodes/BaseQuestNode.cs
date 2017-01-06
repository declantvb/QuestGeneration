using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGeneration
{
	public class BaseQuestNode : NonAtomicNode
	{
		public override List<QuestNode[]> Replacements()
		{
			return new List<QuestNode[]>
			{
				new QuestNode[]{ new KnowledgeNode() },
				new QuestNode[]{ new ComfortNode() },
				//new NonAtomicNode[]{ new ReputationNode() },
				//new NonAtomicNode[]{ new SerenityNode() },
				//new NonAtomicNode[]{ new ProtectionNode() },
				//new NonAtomicNode[]{ new ConquestNode() },
				//new NonAtomicNode[]{ new WealthNode() },
				//new NonAtomicNode[]{ new AbilityNode() },
				//new NonAtomicNode[]{ new EquipmentNode() },
			};
		}
	}
}
