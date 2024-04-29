using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    public class TextManager
    {
        public void ShowStartText()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n");               
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("\n\n---------------------------------------------------");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점\n");
            ShowInputField();
        }

        public void ShowPlayerState(Player player)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            ShowPlayerInfo(player);

            Console.WriteLine("\n 0. 나가기\n");
            ShowInputField();
        }
        
        public void ShowInventory(Player player)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");
            ShowPlayerItem(player.inventory.equipments);

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            ShowInputField();
        }

        public void ShowInventoryForEquipment(Player player)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");

            ShowPlayerItemForEquip(player.inventory.equipments);

            Console.WriteLine("0. 나가기\n");
            ShowInputField();
        }

        public void ShowShop(Player player, ShopManager shop)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.gold} G\n");

            Console.WriteLine($"[아이템목록]");
            ShowShopItems(shop.items);

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기\n");
            ShowInputField();
        }

        public void ShowShopForBuy(Player player, ShopManager shop)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.gold} G\n");

            Console.WriteLine($"[아이템목록]");
            ShowShopItemsForBuy(shop.items);

            Console.WriteLine("\n0. 나가기\n");
            ShowInputField();
        }



        public void ShowChoiceErrorText()
        {
            Console.WriteLine("\n잘못된 입력입니다.");
            Console.Write(">> ");
        }

        public void ShowInputField()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }
        private void ShowShopItems(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                switch (item)
                {
                    case Armor armor:
                        if (armor.isBuy)
                        {
                            Console.WriteLine($" - {armor.name} | 방어력 +{armor.defense} | {armor.explain} | 구매완료");
                        }
                        else
                        {
                            Console.WriteLine($" - {armor.name} | 방어력 +{armor.defense} | {armor.explain} | {armor.price} G");
                        }
                        break;
                    case Weapon weapon:
                        if (weapon.isBuy)
                        {
                            Console.WriteLine($" - {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain} | 구매완료");
                        }
                        else
                        {
                            Console.WriteLine($" - {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain} | {weapon.price} G");
                        }
                        break;
                }
            }
        }
        private void ShowShopItemsForBuy(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                switch (item)
                {
                    case Armor armor:
                        if (armor.isBuy)
                        {
                            Console.WriteLine($" - {i + 1} {armor.name} | 방어력 +{armor.defense} | {armor.explain} | 구매완료");
                        }
                        else
                        {
                            Console.WriteLine($" - {i + 1} {armor.name} | 방어력 +{armor.defense} | {armor.explain} | {armor.price} G");
                        }
                        break;
                    case Weapon weapon:
                        if (weapon.isBuy)
                        {
                            Console.WriteLine($" - {i + 1} {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain} | 구매완료");
                        }
                        else
                        {
                            Console.WriteLine($" - {i + 1} {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain} | {weapon.price} G");
                        }
                        break;
                }
            }
        }
        private void ShowPlayerInfo(Player player)
        {
            Console.WriteLine($"Lv. {player.stats.level}");
            Console.WriteLine($"직업 : {player.job}");
            Console.WriteLine($"공격력 : {player.stats.ATK} (+{player.inventory.equipmentStats.ATK})");
            Console.WriteLine($"방어력 : {player.stats.DEF} (+{player.inventory.equipmentStats.DEF})");
            Console.WriteLine($"체  력 : {player.stats.HP}");
            Console.WriteLine($"Gold : {player.gold}");
        }
        private void ShowPlayerItem(List<Equipment> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                switch (item)
                {
                    case Armor armor:
                        if (armor.isEquip)
                        {
                            Console.WriteLine($" - [E]{armor.name} | 방어력 +{armor.defense} | {armor.explain}");
                        }
                        else
                        {
                            Console.WriteLine($" - {armor.name} | 방어력 +{armor.defense} | {armor.explain}");
                        }
                        break;
                    case Weapon weapon:
                        if (weapon.isEquip)
                        {
                            Console.WriteLine($" - [E]{weapon.name} | 방어력 +{weapon.damage} | {weapon.explain}");
                        }
                        else
                        {
                            Console.WriteLine($" - {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain}");
                        }
                        break;
                }
            }
        }
        private void ShowPlayerItemForEquip(List<Equipment> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                switch (item)
                {
                    case Armor armor:
                        if (armor.isEquip)
                        {
                            Console.WriteLine($" - {i + 1} [E]{armor.name} | 방어력 +{armor.defense} | {armor.explain}");
                        }
                        else
                        {
                            Console.WriteLine($" - {i + 1} {armor.name} | 방어력 +{armor.defense} | {armor.explain}");
                        }
                        break;
                    case Weapon weapon:
                        if (weapon.isEquip)
                        {
                            Console.WriteLine($" - {i + 1} [E]{weapon.name} | 방어력 +{weapon.damage} | {weapon.explain}");
                        }
                        else
                        {
                            Console.WriteLine($" - {i + 1} {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain}");
                        }
                        break;
                }
            }
        }
    }
}
