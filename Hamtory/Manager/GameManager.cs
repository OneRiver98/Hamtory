using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory.Manager
{
    public class GameManager
    {
        private TextManager textManager = new();
        private ShopManager shopManager = new();
        private BattleManager battleManager = new();

        private Player player = new();

        private MainScene Scene = MainScene.MENU;
        private MainScene currentScene
        {
            get { return Scene; }
            set
            {
                Scene = value;
                switch (value)
                {
                    case MainScene.MENU:
                        textManager.ShowMainMenu();
                        break;

                    case MainScene.STATE:
                        textManager.ShowPlayerState(player);
                        break;

                    case MainScene.INVENTORY:
                        textManager.ShowInventory(player);
                        break;

                    case MainScene.INVENTORY_EQUIP:
                        textManager.ShowInventoryForEquipment(player);
                        break;

                    case MainScene.SHOP:
                        textManager.ShowShop(player, shopManager);
                        break;

                    case MainScene.SHOP_BUY:
                        textManager.ShowShopForBuy(player, shopManager);
                        break;

                }
            }
        }

        public void StartGame()
        {
            shopManager.OnBuy += player.Buy;
            shopManager.ShopSetting();

            textManager.ShowStartText();
            textManager.ShowMainMenu();

            bool isPlaying = true;
            while (isPlaying)
            {
                string input = Console.ReadLine();

                switch (currentScene)
                {
                    case MainScene.MENU:
                        switch (input)
                        {
                            case "1":
                                currentScene = MainScene.STATE;
                                break;

                            case "2":
                                currentScene = MainScene.INVENTORY;
                                break;

                            case "3":
                                currentScene = MainScene.SHOP;
                                break;

                            case "4":
                                battleManager.StartBattle(player);
                                break;

                            default:
                                textManager.ShowChoiceErrorText();
                                break;
                        }
                        break;

                    case MainScene.STATE:
                        if (input == "0")
                        {
                            currentScene = MainScene.MENU;

                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case MainScene.INVENTORY:
                        if (input == "1")
                        {
                            currentScene = MainScene.INVENTORY_EQUIP;
                        }
                        else if (input == "0")
                        {
                            currentScene = MainScene.MENU;

                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case MainScene.INVENTORY_EQUIP:
                        if (input == "0")
                        {
                            currentScene = MainScene.MENU;
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

                    case MainScene.SHOP:
                        if (input == "1")
                        {
                            currentScene = MainScene.SHOP_BUY;
                        }
                        if ( input == "2")
                        {
                            ++shopManager.choicePage;
                            if (shopManager.maxPage < shopManager.choicePage)
                            {
                                Console.WriteLine("마지막 페이지 입니다.");
                                --shopManager.choicePage;
                            }
                            currentScene = MainScene.SHOP;
                        }
                        if ( input == "3")
                        {
                            if (shopManager.choicePage == 0)
                            {
                                Console.WriteLine("첫 번째 페이지입니다.");
                            }
                            else
                            {
                                --shopManager.choicePage;
                            }
                            currentScene = MainScene.SHOP;
                        }
                        else if (input == "0")
                        {
                            currentScene = MainScene.MENU;
                        }
                        else textManager.ShowChoiceErrorText();
                        break;

                    case MainScene.SHOP_BUY:
                        if (input == "0")
                        {
                            currentScene = MainScene.MENU;
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
                }
            }
        }
    }
}

