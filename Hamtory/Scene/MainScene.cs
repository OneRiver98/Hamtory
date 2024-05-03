using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class MainScene
    {
        private MainTextManager textManager = new();
        private Shop shopManager = new();
        private BattleScene battleManager = new();

        private Player player = new();

        private MainState Scene = MainState.MENU;
        private MainState currentScene
        {
            get { return Scene; }
            set
            {
                Scene = value;
                switch (value)
                {
                    case MainState.MENU:
                        textManager.ShowMainMenu();
                        break;

                    case MainState.STATE:
                        textManager.ShowPlayerInfo(player);
                        break;

                    case MainState.INVENTORY:
                        textManager.ShowInventory(player);
                        break;

                    case MainState.INVENTORY_EQUIP:
                        textManager.ShowInventoryForEquipment(player);
                        break;

                    case MainState.SHOP:
                        textManager.ShowShop(player, shopManager);
                        break;

                    case MainState.SHOP_BUY:
                        textManager.ShowShopForBuy(player, shopManager);
                        break;

                }
            }
        }

        public void StartGame()
        {
            shopManager.OnBuy += player.Buy;
            shopManager.ShopSetting();

            string s = $"{player.stats.DEF}";

            Console.WriteLine($"테스트 {s}");



            textManager.ShowTitle();
            string name = Console.ReadLine();
            player.name = name;
            textManager.ShowMainMenu();

            bool isPlaying = true;
            while (isPlaying)
            {
                string input = Console.ReadLine();

                switch (currentScene)
                {
                    case MainState.MENU:
                        switch (input)
                        {
                            case "1":
                                currentScene = MainState.STATE;
                                break;

                            case "2":
                                currentScene = MainState.INVENTORY;
                                break;

                            case "3":
                                currentScene = MainState.SHOP;
                                break;

                            case "4":
                                battleManager.StartBattle(player);
                                break;

                            default:
                                textManager.ShowChoiceErrorText();
                                break;
                        }
                        break;

                    case MainState.STATE:
                        if (input == "0")
                        {
                            currentScene = MainState.MENU;

                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case MainState.INVENTORY:
                        if (input == "1")
                        {
                            currentScene = MainState.INVENTORY_EQUIP;
                        }
                        else if (input == "0")
                        {
                            currentScene = MainState.MENU;

                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case MainState.INVENTORY_EQUIP:
                        if (input == "0")
                        {
                            currentScene = MainState.MENU;
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

                    case MainState.SHOP:
                        if (input == "1")
                        {
                            currentScene = MainState.SHOP_BUY;
                        }
                        else if ( input == "2")
                        {
                            ++shopManager.page;
                            if (shopManager.maxPage < shopManager.page)
                            {
                                Console.WriteLine("마지막 페이지 입니다.");
                                --shopManager.page;
                            }
                            currentScene = MainState.SHOP;
                        }
                        else if ( input == "3")
                        {
                            if (shopManager.page == 0)
                            {
                                Console.WriteLine("첫 번째 페이지입니다.");
                            }
                            else
                            {
                                --shopManager.page;
                            }
                            currentScene = MainState.SHOP;
                        }
                        else if (input == "0")
                        {
                            currentScene = MainState.MENU;
                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case MainState.SHOP_BUY:
                        if (input == "0")
                        {
                            currentScene = MainState.MENU;
                        }
                        else
                        {
                            bool isBuy = shopManager.BuyItem(int.Parse(input)+ 9*shopManager.page, player.gold);
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
                }
            }
        }
    }
}

