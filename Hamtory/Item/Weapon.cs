using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class Weapon : Equipment
    {
        public int damage;

        public Weapon(string name, string explain, int damage, int price)
        {
            this.name = name;
            this.explain = explain;
            this.damage = damage;
            this.price = price;
        }
    }
}
