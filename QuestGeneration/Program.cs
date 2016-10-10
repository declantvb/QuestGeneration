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
			var taskgen = new TaskGenerator();

			for (int i = 0; i < 14; i++)
			{
				var task = taskgen.CollectTask();

				//Console.WriteLine(task.Name);
				Console.WriteLine(task.Description);
				Console.WriteLine();
			}

			Console.ReadKey();
		}
	}
}
