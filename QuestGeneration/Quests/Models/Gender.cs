namespace QuestGeneration
{
	public class Gender
	{
		public PronounSet Pronouns { get; set; }

		private Gender() { }

		public static Gender Male = new Gender
		{
			Pronouns = PronounSet.ThirdMale,
		};
		public static Gender Female = new Gender
		{
			Pronouns = PronounSet.ThirdFemale,
		};
		public static Gender Neuter = new Gender
		{
			Pronouns = PronounSet.ThirdNeuter,
		};
	}
}