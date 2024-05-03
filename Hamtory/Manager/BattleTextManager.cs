using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    internal class BattleTextManager : TextManager
    {
        public void ShowBattlemap(List<Monster> monsters)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("Battle!!\n");

            ShowMonsters(monsters, false);

            Console.WriteLine("\n1. 공격");
            ShowInputField();
        }

        public void ShowBattlemapForATTACK(List<Monster> monsters)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("Battle!!\n");

            ShowMonsters(monsters, true);

            Console.WriteLine("\n0. 취소\n");
            Console.WriteLine("대상을 선택해 주세요.");
            Console.Write(">> ");
        }

        private void ShowMonsters(List<Monster> monsters, bool isNum)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                var stats = monsters[i].stats;

                string hp = stats.HP.ToString();
                if (stats.HP == 0)
                {
                    hp = "Dead";
                }
              
                if (!isNum)
                {
                    Console.WriteLine($"Lv.{stats.level} {monsters[i].name} HP {hp}");
                }
                else
                {
                    Console.WriteLine($"{i + 1} Lv.{stats.level} {monsters[i].name} HP {hp}");
                }
            }
        }
    }
}
