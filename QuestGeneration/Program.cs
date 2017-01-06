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
			MakeQuests();

			Console.ReadKey();
		}

		private static void MakeQuests()
		{
			var rand = new Random();
			var taskGen = new TaskGenerator();
			var charGen = new CharacterGenerator();
			var motiveGen = new MotivationGenerator();
			var questGen = new QuestGenerator();

			var player = charGen.Character();
			var giver = charGen.Character();

			//MakeTasks(rand, taskGen, charGen, motiveGen);

			var quest = questGen.GenerateQuest(player, giver);

			Console.WriteLine(quest.ToString());
		}

		private static void MakeTasks(Random rand, TaskGenerator taskGen, CharacterGenerator charGen, MotivationGenerator motiveGen)
		{
			for (int i = 0; i < 14; i++)
			{
				var giver = charGen.Character();
				var taker = charGen.Character();
				var motive = motiveGen.Motive();

				Task task;
				switch (rand.Next(3))
				{
					case 0:
						task = taskGen.CollectTask(giver, taker, motive);
						break;
					case 1:
						task = taskGen.KillTask(giver, taker, motive);
						break;
					case 2:
						task = taskGen.DeliverTask(giver, taker, motive);
						break;
					default:
						throw new Exception("bad num");
				}

				//Console.WriteLine(task.Name);
				Console.WriteLine(task.ToString());
				Console.WriteLine();
			}
		}
	}
}
