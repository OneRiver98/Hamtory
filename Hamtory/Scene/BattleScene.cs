using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hamtory
{   
    public class BattleScene
    {   
        private int MonstertunCount = 0;
        private BattleTextManager textManager = new();
        private DungeonManager dungeonManager = new();

        private BattleState Scene = BattleState.MAIN;
        private BattleState currentScene
        {
            get { return Scene; }
            set
            {
                Scene = value;
                switch (value)
                {
                    case BattleState.MAIN:
                        textManager.ShowBattlemap(dungeonManager.monsters);
                        break;

                    case BattleState.PLAYER_CHOICE:
                        textManager.ShowBattlemapForATTACK(dungeonManager.monsters);
                        break;

                    case BattleState.PLAYER_ATTACK:
                        break;

                    case BattleState.ENEMY_ATTACK:
                        break;

                    default:
                        break;
                }
            }
        }

        public void StartBattle(Player player)
        {
            dungeonManager.MonsterSetting();
            Scene = BattleState.MAIN;
            textManager.ShowBattlemap(dungeonManager.monsters);
            bool isBattle = true;
            while(isBattle)
            {
                string input = null;

                input = Console.ReadLine();


                switch(currentScene)
                {
                    case BattleState.MAIN:
                        if(input == "1")
                        {
                            currentScene = BattleState.PLAYER_CHOICE;
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();
                        }
                        break;

                    case BattleState.PLAYER_CHOICE:
                        if(input == "0")
                        {
                            currentScene = BattleState.MAIN;
                        }
                        else
                        {
                            var monster = dungeonManager.AttackEnemy(int.Parse(input));
                            if (monster == null)
                            {
                                textManager.ShowChoiceErrorText();
                            }
                            else
                            {
                                Battle(player, monster);
                                if (dungeonManager.monsters.All(monster => monster.stats.HP == 0))
                                {
                                    Console.WriteLine("Battle!! - Result\n");
                                    Console.WriteLine("Victory\n");
                                    foreach (var monsters in dungeonManager.monsters)
                                    {
                                        Console.WriteLine($"{monsters.stats.level} {monsters.name}");
                                        Console.WriteLine($"HP {monsters.stats.HP} -> Dead");
                                    }
                                    Console.WriteLine("\n0. 다음\n");
                                    Console.Write(">> ");
                                    input = Console.ReadLine();
                                    if (input == "0")
                                    {
                                        isBattle = false;
                                        break;
                                    }
                                }
                                else if (player.stats.HP == 0)
                                {
                                    Console.WriteLine("Battle!! - Result\n");
                                    Console.WriteLine("You Lose\n");
                                    foreach (var monsters in dungeonManager.monsters)
                                    {
                                        Console.WriteLine($"{monsters.stats.level} {monsters.name}");
                                        Console.WriteLine($"HP {monsters.stats.HP}");
                                    }
                                    Console.WriteLine("\n0. 다음\n");
                                    Console.Write(">> ");
                                    input = Console.ReadLine();
                                    if (input == "0")
                                    {
                                        isBattle = false;
                                        break;
                                    }
                                }
                                currentScene = BattleState.PLAYER_ATTACK;
                            }
                        }
                        break;

                    case BattleState.PLAYER_ATTACK:
                        if(input == "0")
                        {
                            Console.WriteLine($"\n몬스터 턴으로");
                            currentScene = BattleState.ENEMY_ATTACK;
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();

                            /// 배틀 다 끝나고 메인가는지 확인
                            //isBattle = false;
                            //textManager.ShowMainMenu();
                            //dungeonManager.monsters.Clear();
                            ///
                        }
                        break;
                    case BattleState.ENEMY_ATTACK:
                        {
                            if (input == "0") 
                            {
                                    MonstertunCount++;
                                    var monster = dungeonManager.AttackEnemy(MonstertunCount);
                                    if (monster == null)
                                    {
                                        if (MonstertunCount > dungeonManager.monsters.Count)
                                        {
                                            currentScene = BattleState.MAIN;
                                            MonstertunCount = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine("\n0. 다음\n");
                                            Console.Write(">> ");
                                         }
                                        break;
                                    }
                                    Battle(monster, player);
                            }
                            if (MonstertunCount == 0)
                            {
                                MonstertunCount++;
                                var monster = dungeonManager.AttackEnemy(MonstertunCount);
                                if (monster == null)
                                {
                                    Console.WriteLine("\n0. 다음\n");
                                    Console.Write(">> ");
                                    break;
                                }
                                Battle(monster, player);
                            }
                            
                            break;
                        }
                }
            }
        }


        public void Battle(Unit attacker, Unit defender)
        {
            int damage = attacker.stats.ATK;
            int originHp = defender.stats.HP;
            bool critical = false;
            if(attacker is Player)
            {
                Random random = new Random();
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
            defender.OnAttack(damage);

            Console.WriteLine($"\n{attacker.name}의 공격 !");
            Console.WriteLine(critical? "치명타!" : "");
            Console.WriteLine($"{defender.stats.level} {defender.name}을(를) 맞췄습니다. 데미지 : [{damage}]");

            Console.WriteLine($"\n{defender.stats.level} {defender.name}");
            if(defender.stats.HP == 0)
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
    }
}
