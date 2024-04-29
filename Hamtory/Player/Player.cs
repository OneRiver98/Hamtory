using System;

namespace rpg
{
	public class Player
	{
		public string job = "전사";
		public int gold = 4500;

		public Stats stats;
		

        public Inventory inventory = new();


		public Player()
		{ 
			stats = new(stats.level = 1, stats.ATK = 10, stats.DEF = 5, stats.HP = 100);
	
        }

		public void Buy(int price, Item item)
		{
            gold -= price;
			inventory.GetItem(item);
        }
    }
}
