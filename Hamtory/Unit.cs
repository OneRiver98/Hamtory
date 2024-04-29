using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class Unit : IDamageable
    {
        public string name;
        public Stats stats;

        public void OnAttack(int damage)
        {
            stats.HP -= damage;
            if(stats.HP <= 0)
            { 
                stats.HP = 0;
            }
        }
    }
}
