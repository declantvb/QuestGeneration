using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QuestGeneration
{
	public class Program
	{
		static void Main(string[] args)
		{
			var taskGen = new TaskGenerator();
			var charGen = new CharacterGenerator();
			var motiveGen = new MotivationGenerator();

			for (int i = 0; i < 14; i++)
			{
				var giver = charGen.Character();
				var taker = charGen.Character();
				var motive = motiveGen.Motive();

				var task = taskGen.CollectTask(giver, taker, motive);

				//Console.WriteLine(task.Name);
				Console.WriteLine(task.Description);
				Console.WriteLine();
			}

			Console.ReadKey();
		}
	}
}
