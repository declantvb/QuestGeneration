using Humanizer;
using System;
using System.Collections.Generic;

namespace QuestGeneration
{
	public class CharacterGenerator
	{
		public List<string> maleFirstNames = new List<string> { "james", "john", "robert", "michael", "william", "david", "richard", "charles", "joseph", "thomas", "christopher", "daniel", "paul", "mark", "donald", "george", "kenneth", "steven", "edward", "brian", "ronald", "anthony", "kevin", "jason", "matthew" };
		public List<string> femaleFirstNames = new List<string> { "mary", "patricia", "linda", "barbara", "elizabeth", "jennifer", "maria", "susan", "margaret", "dorothy", "lisa", "nancy", "karen", "betty", "helen", "sandra", "donna", "carol", "ruth", "sharon", "michelle", "laura", "sarah", "kimberly", "deborah" };
		public List<string> lastNames = new List<string> { "smith", "johnson", "williams", "jones", "brown", "davis", "miller", "wilson", "moore", "taylor", "anderson", "thomas", "jackson", "white", "harris", "martin", "thompson", "garcia", "martinez", "robinson", "clark", "rodriguez", "lewis", "lee", "walker" };

		private Random rand;

		public CharacterGenerator()
		{
			rand = new Random();
		}

		public Character Character()
		{
			var character = new Character
			{
				LastName = lastNames[rand.Next(lastNames.Count)].Transform(To.TitleCase)
			};

			if (rand.Next(1) == 0)
			{
				character.Gender = Gender.Male;
				character.FirstName = maleFirstNames[rand.Next(maleFirstNames.Count)].Transform(To.TitleCase);
				
			}
			else
			{
				character.Gender = Gender.Female;
				character.FirstName = femaleFirstNames[rand.Next(femaleFirstNames.Count)].Transform(To.TitleCase);
			}

			return character;
		}
	}
}