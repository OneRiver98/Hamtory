using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    public struct Stats
    {
        public int level;
        public int ATK;
        public int DEF;
        public int HP;

        public Stats(int level, int atk, int def, int hp)
        {
            this.level = level;
            ATK = atk;
            DEF = def;
            HP = hp;
        }

    }
}
