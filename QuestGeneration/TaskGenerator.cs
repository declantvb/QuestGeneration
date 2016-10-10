using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QuestGeneration
{
	public class TaskGenerator
	{
		private ItemDatabase id;
		private Random rand;
		private PhraseDictionary pd;

		public TaskGenerator()
		{
			rand = new Random();
			id = new ItemDatabase();
			pd = new PhraseDictionary();
		}

		public Task MakeFetchTask()
		{
			var fetch = id.GetRandomItem();
			var reward = id.GetRandomReward(fetch);

			string text = "{{fetch_reward}}";

			MatchCollection matches;
			do
			{
				matches = Regex.Matches(text, @"\{\{(.+?)\}\}");

				foreach (Match match in matches)
				{
					var matchString = match.Groups[1].ToString();

					var options = pd.Get(matchString);

					var replace = options[rand.Next(options.Count)];

					text = ReplaceToken(text, matchString, replace);
				}

			} while (matches.Count > 0);
			
			text = ReplaceWithArticle(text, "item_fetch", fetch.Name);
			text = ReplaceWithArticle(text, "item_reward", reward.Name);

			return new Task
			{
				Name = "Retrieve a " + fetch.Name,
				Description = text
			};
		}

		private static string ReplaceWithArticle(string text, string token, string replacement)
		{
			string article = "a"; 

			var itemWithArticle = string.Format("{0} {1}", article, replacement);

			return ReplaceToken(text, "item_reward", itemWithArticle);
		}

		private static string ReplaceToken(string text, string token, string replacement)
		{
			return text.Replace("{{" + token + "}}", replacement);
		}
	}
}
