using System.Collections.Generic;

namespace QuestGeneration
{
	public class PhraseDictionary
	{
		private Dictionary<string, List<string>> _dict;

		public PhraseDictionary()
		{
			_dict = new Dictionary<string, List<string>>();

			//phrases

			_dict.Add("collect task", new List<string> {
				"{{intro}}. {{explain_motivation}}. {{fetch_reward}}.",
				"{{reward_entice}} {{explain_motivation}}. {{fetch_noun}}."
			});

			_dict.Add("intro", new List<string> {
				"hey, I'm {{$giver_name}}, you're {{$taker_name}} right? maybe you can help me",
				"hey you, looking for {{some work}}?",
				"i've got {{some work}} if you're interested in a payout",
			});

			_dict.Add("explain_motivation", new List<string> {
				"i need to {{$motive_verb}} my {{$motive_noun}}, it is {{$motive_adjective}} because of the {{$motivereason_time}} {{$motivereason_noun}}",
				"the {{$motivereason_noun}} that happened {{$motivereason_location}} caused my {{$motive_noun}} to be {{$motive_adjective}}",
				"my {{$motive_noun}} was {{$motive_adjective}} is the {{$motivereason_time}} {{$motivereason_noun}}, this will help me to {{$motive_verb}} it"
			});

			_dict.Add("fetch_reward", new List<string> {
				"{{fetch_if}}, {{reward_then}}",
				"{{fetch_if}}, {{reward_then}} in return",
				"{{fetch_if?}} {{reward_then}} if {{#second_subject}} do",
				"{{reward_then}} {{fetch_if}}",
				"{{#first_subject}} need {{noun_fetch}}, {{reward_then}} for {{pronoun_reward}}"
			});

			_dict.Add("fetch_if", new List<string> {
				"if {{#second_subject}} {{fetch_noun}}",
				"assuming {{#second_subject}} {{fetch_noun}}"
			});
			_dict.Add("fetch_if?", new List<string> {
				"could {{#second_subject}} {{fetch_noun}}?",
				"please {{fetch_noun}},"
			});
			_dict.Add("fetch_noun", new List<string> {
				"{{fetch}} {{#first_object}} {{noun_fetch}}",
				"{{fetch}} {{noun_fetch}} for {{#first_object}}",
				"{{fetch}} {{noun_fetch}}"
			});

			_dict.Add("reward_then", new List<string> {
				"{{#first_subject}} will {{reward_noun}}",
				"{{#first_subject}} promise to {{reward_noun}}"
			});
			_dict.Add("reward_noun", new List<string> {
				"{{give}} {{#second_subject}} {{noun_reward}}",
				"{{reward}} {{#second_subject}} with {{noun_reward}}"
			});

			_dict.Add("reward_entice", new List<string> {
				"if you are interested in earning {{noun_reward}}, i have a job for you.",
			});

			//keywords
			_dict.Add("fetch", new List<string> { "fetch", "retrieve", "get", "reclaim" });
			_dict.Add("give", new List<string> { "give", "gift", "award", "grant" });
			_dict.Add("reward", new List<string> { "reward", "compensate", "repay" });
			_dict.Add("some work", new List<string> { "some work", "a job", "something to do" });
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