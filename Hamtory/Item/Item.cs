using System;

namespace rpg
{
    public class Item : IBuyable
    {
        public bool isBuy = false;

        public string name = "";
        public string explain = "";
        public int price = 0;


        public void Buy()
        {
            isBuy = true;
        }
    }
}
