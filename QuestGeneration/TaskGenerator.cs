using Humanizer;
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
			var fetchNumber = rand.Next(20);

			var reward = id.GetRandomReward(fetch);
			var rewardNumber = rand.Next(20);

			string text = "{{fetch_reward}}.";

			int depth = 0;

			MatchCollection matches;
			do
			{
				matches = Regex.Matches(text, @"\{\{(.+?)\}\}");

				foreach (Match match in matches)
				{
					var matchString = match.Groups[1].ToString();

					if (matchString == "noun_fetch")
					{
						text = ReplaceToken(text, "noun_fetch", AttachArticle(fetch, fetchNumber));
					}
					else if (matchString == "noun_reward")
					{
						text = ReplaceToken(text, "noun_reward", AttachArticle(reward, rewardNumber));
					}
					else if (matchString == "pronoun_reward")
					{
						text = ReplaceToken(text, matchString, PronounFor(fetch, fetchNumber));
					}
					else if (matchString == "pronoun_reward")
					{
						text = ReplaceToken(text, matchString, PronounFor(reward, rewardNumber));
					}
					else
					{
						var options = pd.Get(matchString);
						if (options.Count == 0)
						{
							throw new Exception("No results found for {{" + matchString + "}}");
						}

						var replace = options[rand.Next(options.Count)];

						text = ReplaceToken(text, matchString, replace);
					}
				}

				depth++;

				if (depth > 50)
				{
					throw new Exception("Text building went to deep!");
				}
			} while (matches.Count > 0);

			return new Task
			{
				Name = "Retrieve a " + fetch.Name,
				Description = text
			};
		}

		private static string PronounFor(Item item, int count)
		{
			if (count == 1)
			{
				return "it"; 
			}
			else
			{
				return "them";
			}
		}

		private static string AttachArticle(Item item, int count)
		{
			string article = item.Article;
			var prefix = item.CountablePrefix;
			var name = item.Name;
			if (count > 1)
			{
				article = count.ToString();
				if (!string.IsNullOrWhiteSpace(prefix))
				{
					prefix = prefix.Pluralize(); 
				}
				else
				{
					name = name.Pluralize();
				}
			}

			string itemText = name;
			if (!string.IsNullOrWhiteSpace(prefix))
			{
				itemText = prefix + " of " + itemText;
			}

			return string.Format("{0} {1}", article, itemText);
		}

		private static string ReplaceToken(string text, string token, string replacement)
		{
			return text.Replace("{{" + token + "}}", replacement);
		}
	}
}
