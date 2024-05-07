using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class DungeonManager
    {
        public List<Monster> monsters = new();

        public void MonsterSetting()
        {
            monsters.Add(new Monster("미니언",     2, 5, 5, 15));
            monsters.Add(new Monster("대포미니언", 5, 5, 5, 25));
            monsters.Add(new Monster("공허충", 3, 5, 5, 40));
        }

        public Unit? AttackEnemy(int num)
        {
            if (num > monsters.Count) return null;

            var monster = monsters[num - 1];
            if (monster.stats.HP == 0) return null;

            return monster;
        }
    }
}
