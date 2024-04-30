using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hamtory
{
    
    public class BattleManager
    {   
        private int MonstertunCount = 0;
        private TextManager textManager = new();
        private DungeonManager dungeonManager = new();

        private BattleScene Scene = BattleScene.MAIN;
        private BattleScene currentScene
        {
            get { return Scene; }
            set
            {
                Scene = value;
                switch (value)
                {
                    case BattleScene.MAIN:
                        textManager.ShowBattlemap(dungeonManager.monsters);
                        break;

                    case BattleScene.PLAYER_CHOICE:
                        textManager.ShowBattlemapForATTACK(dungeonManager.monsters);
                        break;

                    case BattleScene.PLAYER_ATTACK:
                        break;

                    case BattleScene.ENEMY_TURN:
                        break;

                    default:
                        break;
                }
            }
        }

        public void StartBattle(Player player)
        {
            dungeonManager.MonsterSetting();
            Scene = BattleScene.MAIN;
            textManager.ShowBattlemap(dungeonManager.monsters);
            bool isBattle = true;
            while(isBattle)
            {
                string input = null;

                input = Console.ReadLine();


                switch(currentScene)
                {
                    case BattleScene.MAIN:
                        if(input == "1")
                        {
                            currentScene = BattleScene.PLAYER_CHOICE;
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();
                        }
                        break;

                    case BattleScene.PLAYER_CHOICE:
                        if(input == "0")
                        {
                            currentScene = BattleScene.MAIN;
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
                                BattleResult(player, monster);
                                currentScene = BattleScene.PLAYER_ATTACK;
                            }
                        }
                        break;

                    case BattleScene.PLAYER_ATTACK:
                        if(input == "0")
                        {
                            Console.WriteLine($"\n몬스터 턴으로");
                            currentScene = BattleScene.ENEMY_TURN;
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();

                            /// 배틀 다 끝나고 메인가는지 확인
                            isBattle = false;
                            textManager.ShowMainMenu();
                            dungeonManager.monsters.Clear();
                            ///
                        }
                        break;
                    case BattleScene.ENEMY_TURN:
                        {
                            if (input == "0") 
                            {
                                    MonstertunCount++;
                                    var monster = dungeonManager.AttackEnemy(MonstertunCount);
                                    if (monster == null)
                                    {
                                        if (MonstertunCount > dungeonManager.monsters.Count)
                                        {
                                            currentScene = BattleScene.MAIN;
                                            MonstertunCount = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine("\n0. 다음\n");
                                            Console.Write(">> ");
                                         }
                                        break;
                                    }
                                    BattleResult(monster, player);
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
                                BattleResult(monster, player);
                            }
                            
                            break;
                        }
                }
            }
        }


        public void BattleResult(Unit attacker, Unit defender)
        {
            int damage = attacker.stats.ATK;
            int originHp = defender.stats.HP;
            if(attacker is Player)
            {
                var player = (Player)attacker;
                damage += player.inventory.equipmentStats.ATK;
            }
            defender.OnAttack(damage);

            Console.WriteLine($"\n{attacker.name}의 공격 !");
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
