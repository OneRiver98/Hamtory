using System.Numerics;
using System.Threading;

namespace Hamtory
{
    public class BattleScene
    {
        private int MonstertunCount = 0;
        private BattleTextManager textManager = new();
        private BattleManager battleManager = new();
        private DungeonManager dungeonManager = new();
        private Player player = new();

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
                        Console.Clear();
                        textManager.ShowDungeon(dungeonManager.monsters);
                        break;

                    case BattleState.PLAYER_CHOICE:
                        Console.Clear();
                        textManager.ShowDungeonForATTACK(dungeonManager.monsters);
                        break;

                    case BattleState.ENEMY_ATTACK:                   
                        break;

                    case BattleState.VICTORY:
                        Console.Clear();
                        textManager.ShowVictoryText(dungeonManager.monsters.Count, player);
                        break;

                    case BattleState.LOSE:
                        Console.Clear();
                        textManager.ShowLoseText(dungeonManager.monsters.Count, player);
                        break;

                    default:
                        break;
                }
            }
        }

        public void StartBattle(Player player)
        {
            this.player = player;

            dungeonManager.MonsterSetting();
            Scene = BattleState.MAIN;
            textManager.ShowDungeon(dungeonManager.monsters);
            player.originHp = player.stats.HP;

            bool isBattle = true;
            while (isBattle)
            {
                string input = null;

                input = Console.ReadLine();


                switch (currentScene)
                {
                    case BattleState.MAIN:
                        if (input == "1")
                        {
                            currentScene = BattleState.PLAYER_CHOICE;
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();
                        }
                        break;

                    case BattleState.PLAYER_CHOICE:
                        if (input == "0")
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
                                Console.Clear();
                                battleManager.Battle(player, monster);
                                var result = battleManager.GameCheck(dungeonManager.monsters, player);
                                currentScene = result.HasValue ? BattleState.VICTORY : BattleState.ENEMY_ATTACK;
                            }
                        }
                        break;

                    case BattleState.ENEMY_ATTACK:
                        Console.Clear();

                        Stack<Monster> monsters = new();
                        for(int i = 0; i < dungeonManager.monsters.Count; i++)
                        {
                            if (dungeonManager.monsters[i].stats.HP != 0)
                            {
                                monsters.Push(dungeonManager.monsters[i]);
                            }
                        }

                        while(monsters.Count != 0)
                        {
                            Console.Clear();
                            battleManager.Battle(monsters.Pop(), player);
                            input = "error";
                            while (input != "0")
                            {
                                input = Console.ReadLine();
                                if(input == "0")
                                {
                                    break;
                                }
                                else
                                {
                                    textManager.ShowChoiceErrorText();
                                }

                            }
                        }

                        currentScene = BattleState.MAIN;

                        //if (input == "0")
                        //{
                        //    MonstertunCount++;
                        //    var monster = dungeonManager.AttackEnemy(MonstertunCount);
                        //    if (monster == null)
                        //    {
                        //        if (MonstertunCount > dungeonManager.monsters.Count)
                        //        {
                        //            currentScene = BattleState.MAIN;
                        //            MonstertunCount = 0;
                        //        }
                        //        else
                        //        {
                        //            Console.WriteLine("\n0. 다음\n");
                        //            Console.Write(">> ");
                        //        }
                        //        break;
                        //    }
                        //    battleManager.Battle(monster, player);
                        //    var result = battleManager.GameCheck(dungeonManager.monsters, player);
                        //    if(result.HasValue)
                        //    {
                        //        currentScene = BattleState.LOSE;
                        //    }
                        //}
                        //if (MonstertunCount == 0)
                        //{
                        //    MonstertunCount++;
                        //    var monster = dungeonManager.AttackEnemy(MonstertunCount);
                        //    if (monster == null)
                        //    {
                        //        Console.WriteLine("\n0. 다음\n");
                        //        Console.Write(">> ");
                        //        break;
                        //    }
                        //    battleManager.Battle(monster, player);
                        //    var result = battleManager.GameCheck(dungeonManager.monsters, player);
                        //    if (result.HasValue)
                        //    {
                        //        currentScene = BattleState.LOSE;
                        //    }
                        //}
                        break;

                    case BattleState.VICTORY:
                        if (input == "0")
                        {
                            isBattle = false;
                            dungeonManager.monsters.Clear();
                            Console.Clear();
                            textManager.ShowMainMenu();
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();
                        }
                        break;

                    case BattleState.LOSE:
                        if (input == "0")
                        {
                            isBattle = false;
                            dungeonManager.monsters.Clear();
                            Console.Clear();
                            textManager.ShowMainMenu();
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();
                        }
                        break;
                }
            }
        }      
    }
}
