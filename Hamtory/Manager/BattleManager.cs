using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class BattleManager
    {

        public void Battle(Unit attacker, Unit defender)
        {
            int damage = attacker.stats.ATK;
            int originHp = defender.stats.HP;
            bool critical = false;
            Random random = new Random();
            if (attacker is Player)
            {
                int probability = random.Next(100);
                var player = (Player)attacker;

                if (probability < 15)
                {
                    critical = true;
                    damage = (int)((damage + player.inventory.equipmentStats.ATK) * 1.6f);
                }
                else
                {
                    critical = false;
                    damage += player.inventory.equipmentStats.ATK;
                }
            }

            float damageMultiplier = (float)random.Next(90, 110) / 100;
            damage = (int)(damage * damageMultiplier);

            defender.OnAttack(damage);

            Console.WriteLine($"\n{attacker.name}의 공격 !");
            Console.WriteLine(critical ? "치명타!" : "");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Lv{defender.stats.level} {defender.name}을(를) 맞췄습니다. 데미지 : [{damage}]");
            Console.ResetColor();

            Console.WriteLine($"\n{defender.stats.level} {defender.name}");
            if (defender.stats.HP == 0)
            {
                Console.WriteLine($"HP{originHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP{originHp} -> {defender.stats.HP}");
            }

            Console.WriteLine("\n0. 다음\n");
            Console.Write(">> ");
        }

        public bool? GameCheck(List<Monster> monsters, Player player)
        {
            if (monsters.All(monster => monster.stats.HP == 0))
            {
                return true;
            }
            else if (player.stats.HP == 0)
            {
                return false;
            }
            return null;
        }
    }
}
