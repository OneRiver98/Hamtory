using System;

namespace rpg
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
