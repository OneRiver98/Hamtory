using System.Numerics;

namespace Hamtory
{
    internal class Program
    {
        private static TextManager textManager = new();
        private static ShopManager shopManager = new();
        private static DungeonManager dungeonManager = new();
        private static BattleManager battleManager = new();
        private static Player player = new();

        private static CurrentScene CurrentScene = CurrentScene.MAIN_SCENE;
        private static CurrentScene currentScene
        {
            get { return CurrentScene; }
            set
            {
                CurrentScene = value;
                switch (value)
                {
                    case CurrentScene.MAIN_SCENE:
                        textManager.ShowMainMenu();
                        break;

                    case CurrentScene.STATE_SCENE:
                        textManager.ShowPlayerState(player);
                        break;

                    case CurrentScene.INVENTORY_SCENE:
                        textManager.ShowInventory(player);
                        break;

                    case CurrentScene.INVENTORY_EQUIP_SCENE:
                        textManager.ShowInventoryForEquipment(player);
                        break;

                    case CurrentScene.SHOP_SCENE:
                        textManager.ShowShop(player, shopManager);
                        break;

                    case CurrentScene.SHOP_BUY_SCENE:
                        textManager.ShowShopForBuy(player, shopManager);
                        break;

                    case CurrentScene.BATTLE_SCENE:
                        textManager.ShowBattlemap(dungeonManager.monsters);
                        break;

                    case CurrentScene.BATTLE_SCENE_ATTACK:
                        textManager.ShowBattlemapForATTACK(dungeonManager.monsters);
                        break;
                }
            }
        }


        static void Main(string[] args)
        {
            shopManager.OnBuy += player.Buy;

            shopManager.ShopSetting();
            dungeonManager.MonsterSetting();

            textManager.ShowStartText();
            textManager.ShowMainMenu();

            bool isPlaying = true;
            while (isPlaying)
            {
                string input = Console.ReadLine();
                switch (currentScene)
                {
                    case CurrentScene.MAIN_SCENE:
                        switch (input)
                        {
                            case "1":
                                currentScene = CurrentScene.STATE_SCENE;
                                break;

                            case "2":
                                currentScene = CurrentScene.INVENTORY_SCENE;
                                break;

                            case "3":
                                currentScene = CurrentScene.SHOP_SCENE;
                                break;

                            case "4":
                                currentScene = CurrentScene.BATTLE_SCENE;
                                break;

                            default:
                                textManager.ShowChoiceErrorText();
                                break;
                        }
                        break;

                    case CurrentScene.STATE_SCENE:
                        if (input == "0")
                        {
                            currentScene = CurrentScene.MAIN_SCENE;

                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case CurrentScene.INVENTORY_SCENE:
                        if (input == "1")
                        {
                            currentScene = CurrentScene.INVENTORY_EQUIP_SCENE;
                        }
                        else if (input == "0")
                        {
                            currentScene = CurrentScene.MAIN_SCENE;

                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case CurrentScene.INVENTORY_EQUIP_SCENE:
                        if (input == "0")
                        {
                            currentScene = CurrentScene.MAIN_SCENE;
                        }
                        else
                        {
                            bool isEquip = player.inventory.Equip(int.Parse(input));
                            if (!isEquip)
                            {
                                textManager.ShowChoiceErrorText();
                            }
                            else
                            {
                                textManager.ShowInventoryForEquipment(player);
                            }
                        }
                        break;

                    case CurrentScene.SHOP_SCENE:
                        if (input == "1")
                        {
                            currentScene = CurrentScene.SHOP_BUY_SCENE;
                        }
                        else if (input == "0")
                        {
                            currentScene = CurrentScene.MAIN_SCENE;
                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case CurrentScene.SHOP_BUY_SCENE:
                        if (input == "0")
                        {
                            currentScene = CurrentScene.MAIN_SCENE;
                        }
                        else
                        {
                            bool isBuy = shopManager.BuyItem(int.Parse(input), player.gold);
                            if (!isBuy)
                            {
                                textManager.ShowChoiceErrorText();
                            }
                            else
                            {
                                textManager.ShowShopForBuy(player, shopManager);
                            }
                        }
                        break;

                    case CurrentScene.BATTLE_SCENE:


                        if (input == "1")
                        {
                            currentScene = CurrentScene.BATTLE_SCENE_ATTACK;
                        }
                        else
                        {
                            textManager.ShowChoiceErrorText();
                        }
                        break;

                    case CurrentScene.BATTLE_SCENE_ATTACK:
                        if (input == "0")
                        {
                            currentScene = CurrentScene.BATTLE_SCENE;
                        }
                        else
                        {
                            var monster = dungeonManager.AttackEnemy(int.Parse(input));
                            if(monster == null)
                            {
                                textManager.ShowChoiceErrorText();
                            }
                            else
                            {
                                battleManager.BattleResult(player, monster);
                                input = Console.ReadLine();
                                if(input == "0")
                                {
                                    Console.WriteLine($"\n몬스터 턴으로");
                                }
                                else
                                {
                                    textManager.ShowChoiceErrorText();
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
