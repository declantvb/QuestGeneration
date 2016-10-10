﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGeneration
{
	public class PhraseDictionary
	{
		private Dictionary<string, List<string>> _dict;

		public PhraseDictionary()
		{
			_dict = new Dictionary<string, List<string>>();

			//phrases
			_dict.Add("fetch_reward", new List<string> {
				"{{fetch_if}}, {{then_reward}}",
			});
			_dict.Add("fetch_if", new List<string> {
				"If you {{fetch_noun}}",
				"Could you {{fetch_noun}}?"
			});
			_dict.Add("then_reward", new List<string> {
				"I will {{reward_noun}}",
				"I promise to {{reward_noun}}"
			});
			_dict.Add("fetch_noun", new List<string> {
				"{{fetch}} me {{noun_fetch}}",
				"{{fetch}} {{noun_fetch}} for me",
				"manage to {{fetch}} {{noun_fetch}}"
			});
			_dict.Add("reward_noun", new List<string> {
				"{{give}} you {{noun_reward}} in return",
				"{{give}} you {{noun_reward}}",
				"reward you with {{noun_reward}}"
			});

			//keywords
			_dict.Add("fetch", new List<string> { "fetch", "retrieve", "get", "reclaim" });
			_dict.Add("give", new List<string> { "give", "gift", "award", "grant" });
		}

		public List<string> Get(string key)
		{
			if (!_dict.ContainsKey(key))
			{
				return new List<string>();
			}

			return _dict[key];
		}
	}
}