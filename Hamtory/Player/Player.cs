using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class Player : Unit
    {
        public string job = "전사";
        public int gold = 4500;

        public Inventory inventory = new();

        public Player()
        {
            name = "전사";
            stats = new(stats.level = 1, stats.ATK = 10, stats.DEF = 5, stats.HP = 100);
        }

        public void Buy(int price, Item item)
        {
            gold -= price;
            inventory.GetItem(item);
        }
    }
}
