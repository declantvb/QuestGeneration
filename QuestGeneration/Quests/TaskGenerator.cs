using System;

namespace QuestGeneration
{
	public class TaskGenerator
	{
		private ItemDatabase id;
		private Random rand;
		private VehicleDatabase vd;
		private CharacterGenerator cg;

		public TaskGenerator()
		{
			rand = new Random();
			id = new ItemDatabase();
			vd = new VehicleDatabase();
			cg = new CharacterGenerator();
		}

		public CollectTask CollectTask(Character giver, Character taker, Motivation motivation)
		{
			var fetch = id.GetRandomItem();
			var fetchNumber = rand.Next(19) + 1;

			var reward = id.GetRandomReward(fetch);
			var rewardNumber = rand.Next(19) + 1;

			var task = new CollectTask
			{
				Title = "Retrieve a " + fetch.Name,
				Description = "{{collect task}}",

				Giver = giver,
				Taker = taker,
				Motivation = motivation,

				Item = fetch,
				Count = fetchNumber,
				Reward = reward,
				RewardCount = rewardNumber
			};

			return task;
		}

		public KillTask KillTask(Character giver, Character taker, Motivation motivation)
		{
			var target = vd.GetRandomTarget();
			var targetNumber = rand.Next(19) + 1;

			var reward = id.GetRandomReward();
			var rewardNumber = rand.Next(19) + 1;

			var task = new KillTask
			{
				Title = $"Kill {targetNumber} {target}",
				Description = "{{kill task}}",

				Giver = giver,
				Taker = taker,
				Motivation = motivation,

				Target = target,
				Count = targetNumber,
				Reward = reward,
				RewardCount = rewardNumber
			};

			return task;
		}

		public DeliverTask DeliverTask(Character giver, Character taker, Motivation motivation)
		{
			var destination = cg.Character();

			var item = id.GetRandomDelivery();

			var reward = id.GetRandomReward();
			var rewardNumber = rand.Next(19) + 1;

			var task = new DeliverTask
			{
				Title = $"Deliver {item} to {destination}",
				Description = "{{deliver task}}",

				Giver = giver,
				Taker = taker,
				Motivation = motivation,

				Destination = destination,
				Item = item,
				Reward = reward,
				RewardCount = rewardNumber
			};

			return task;
		}
	}
}