using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
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
