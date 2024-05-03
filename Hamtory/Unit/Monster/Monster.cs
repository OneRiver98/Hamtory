using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class Monster : Unit
    {
        public Monster(string name, int level, int atk, int def, int hp)
        {
            this.name = name;
            stats.level = level;
            stats.ATK = atk;
            stats.DEF = def;
            stats.HP = hp;
        }
    }
}
