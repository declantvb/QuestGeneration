using System;

namespace QuestGeneration
{
	public class CharacterGenerator
	{
		public CharacterGenerator()
		{
		}

		public Character Character()
		{
			return new Character
			{
				FirstName = "Hans",
				LastName = "Fenga",
				Gender = Gender.Male,
			};
		}
	}
}