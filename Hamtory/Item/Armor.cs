using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class Armor : Equipment
    {
        public int defense;

        public Armor(string name, string explain, int defense, int price)
        {
            this.name = name;
            this.explain = explain;
            this.defense = defense;
            this.price = price;
        }
    }
}
