using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    internal class BattleTextManager : TextManager
    {
        public void ShowDungeon(List<Monster> monsters)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("Battle!!\n");

            ShowMonsters(monsters, false);

            Console.WriteLine("\n1. 공격");
            ShowInputField();
        }

        public void ShowDungeonForATTACK(List<Monster> monsters)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("Battle!!\n");

            ShowMonsters(monsters, true);

            Console.WriteLine("\n0. 취소\n");
            Console.WriteLine("대상을 선택해 주세요.");
            Console.Write(">> ");
        }

        public void ShowVictoryText(int monsterCount, Player player)
        {
            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("Victory\n");

            Console.WriteLine($"\n던전에서 몬스터 {monsterCount}마리를 잡았습니다.\n");

            ShowHpResult(player);

            Console.WriteLine("\n0. 다음\n");
            Console.Write(">> ");
        }

        public void ShowLoseText(int monsterCount, Player player)
        {
            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("You Lose\n");

            ShowHpResult(player);

            Console.WriteLine("\n0. 다음\n");
            Console.Write(">> ");
        }



        private void ShowMonsters(List<Monster> monsters, bool isNum)
        {
            StringBuilder sb = new();

            for (int i = 0; i < monsters.Count; i++)
            {
                sb.Clear();
                sb.Append(" -");
                if (isNum) sb.Append($" {i + 1}");

                var stat = monsters[i].stats;

                sb.Append($" Lv.{stat.level} {monsters[i].name}");
                sb.Append(stat.HP == 0 ? " Dead" : $" HP {stat.HP}");

                Console.WriteLine(sb.ToString());
            }
        }

        private void ShowHpResult(Player player)
        {
            Console.WriteLine($"\nLv.{player.stats.level} {player.name}");
            Console.WriteLine($"Hp {player.originHp} -> {player.stats.HP}");
        }
    }
}
