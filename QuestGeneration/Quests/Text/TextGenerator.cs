using Humanizer;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuestGeneration
{
	public class TextGenerator
	{
		private PhraseDictionary pd;
		private Random rand;

		public TextGenerator()
		{
			rand = new Random();
			pd = new PhraseDictionary();
		}

		public void MakeCollectText(CollectTask task)
		{
			int depth = 0;

			MatchCollection matches;
			do
			{
				matches = Regex.Matches(task.Description, @"\{\{(.+?)\}\}");

				foreach (Match match in matches)
				{
					var matchString = match.Groups[1].ToString();

					if (matchString.StartsWith("#"))
					{
						ReplacePronouns(task, matchString);
					}
					else if (matchString.StartsWith("$"))
					{
						ReplaceSubjectObject(task, matchString);
					}
					else if (!ReplaceFetchReward(task, matchString))
					{
						// dict replace
						var options = pd.Get(matchString);
						if (options.Count == 0)
						{
							throw new Exception("No results found for {{" + matchString + "}}");
						}

						var replace = options[rand.Next(options.Count)];

						task.Description = ReplaceToken(task.Description, matchString, replace);
					}
				}

				depth++;

				if (depth > 50)
				{
					throw new Exception("Text building went to deep!");
				}
			} while (matches.Count > 0);

			task.Description = SentenceCaseIt(task.Description);
		}

		private bool ReplaceFetchReward(CollectTask task, string matchString)
		{
			var split = matchString.Split('_');

			if (split.Length != 2)
			{
				return false;
			}

			Item item = null;
			int number = -1;

			if (split[1] == "fetch")
			{
				item = task.Item;
				number = task.Count;
			}
			else if (split[1] == "reward")
			{
				item = task.Reward;
				number = task.RewardCount;
			}

			string replace = null;
			if (item != null)
			{
				if (split[0] == "noun")
				{
					replace = AttachArticle(item, number);
				}
				else if (split[0] == "pronoun")
				{
					replace = PronounFor(item, number);
				}
			}

			if (replace != null)
			{
				task.Description = ReplaceToken(task.Description, matchString, replace);
				return true;
			}

			return false;
		}

		private bool ReplaceSubjectObject(Task task, string matchString)
		{
			// subject/object data
			var cleaned = matchString.Substring(1);
			var split = cleaned.Split('_');
			if (split.Length != 2)
			{
				throw new Exception("invalid pronoun descriptor");
			}

			string value;
			if (split[0] == "giver")
			{
				value = task.Giver.GetText(split[1]);
			}
			else if (split[0] == "taker")
			{
				value = task.Taker.GetText(split[1]);
			}
			else if (split[0] == "motive")
			{
				value = task.Motivation.GetText(split[1]);
			}
			else if (split[0] == "motivereason")
			{
				value = task.Motivation.Reason.GetText(split[1]);
			}
			else
			{
				throw new Exception("unknown subject");
			}

			task.Description = ReplaceToken(task.Description, matchString, value);

			return true;
		}

		private bool ReplacePronouns(Task task, string matchString)
		{
			// pronouns
			var cleaned = matchString.Substring(1);
			var split = cleaned.Split('_');
			if (split.Length != 2)
			{
				throw new Exception("invalid pronoun descriptor");
			}

			//TODO check gender of subject
			var gender = PronounSet.FromString(split[0]);
			var pronoun = gender.GetPronoun(split[1]);

			task.Description = ReplaceToken(task.Description, matchString, pronoun);

			return true;
		}

		private static string SentenceCaseIt(string text)
		{
			var separators = new string[] { ". ", ".\n" };
			var parts = text.Split(separators, StringSplitOptions.None);
			var sentenceCased = parts.Select(x => x.Transform(To.SentenceCase));
			return string.Join(separators[0], sentenceCased);
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