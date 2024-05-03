﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hamtory
{
    public class MainTextManager : TextManager
    {
        public void ShowTitle()
        {
            Console.WriteLine("____ ___ _______.___ ________________.___.________ ________     _________ ____ ___");
            Console.WriteLine("|    |\\\\      \\  |   |\\__ ___/\\__ |   | / _____ / \\_____  \\   / _____ /|    |   \\");
            Console.WriteLine("|    |   \\\\      \\  |   |\\__ ___/\\__ |   | / _____ / \\_____  \\   / _____ /|    |   \\ ");
            Console.WriteLine("|   //   |   \\ |   |  |    |    /   |   |/   \\  ___  /   |   \\  \\_____  \\ |    |   /");
            Console.WriteLine("|  //    |    \\|   |  |    |    \\____   |\\    \\_\\  \\/    |    \\ /        \\|    |  /");
            Console.WriteLine("| ______ / \\____ | __ /| ___ |  | ____ |    / ______ | \\______ /\\_______  //_______  /|______/");
            Console.WriteLine("\\/                  \\/               \\/         \\/         \\/");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 이름을 설정해주세요.");
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("\n던전 가야대");
            Console.WriteLine("가기 전에 정비를 하자.\n\n");
            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
            Console.WriteLine(" __");
            Console.WriteLine(" ____ _/  |_  ____   ");
            Console.WriteLine(" /  _ \\\\   __\\/  _ \\");
            Console.WriteLine("(  <_> )|  | (  <_> )");
            Console.WriteLine(" \\____/ |__|  \\____/");
            Console.WriteLine("        ______");
            Console.WriteLine("       /_____/");
            Console.WriteLine("       /_____/");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장\n");
            ShowInputField();
        }

        public void ShowPlayerInfo(Player player)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("상태보기");
            Console.WriteLine("햄스터의 정보가 표시됩니다.\n");

            ShowStats(player);

            Console.WriteLine("\n 0. 나가기\n");
            ShowInputField();
        }

        public void ShowInventory(Player player)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");
            ShowEquipment(player.inventory.equipments, false);

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

            ShowEquipment(player.inventory.equipments, true);

            Console.WriteLine("0. 나가기\n");
            ShowInputField();
        }

        public void ShowShop(Player player, Shop shop)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.gold} G\n");

            Console.WriteLine("[아이템목록]  " + (1 + shop.page) + "/" + (shop.maxPage+1) +" 페이지");
            ShowItems(shop.items, shop, false);

            Console.WriteLine("\n1. 아이템 구매\n");
            Console.WriteLine("2. 다음 페이지");
            Console.WriteLine("3. 이전 페이지\n");
            Console.WriteLine("0. 나가기\n");
            ShowInputField();
        }

        public void ShowShopForBuy(Player player, Shop shop)
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{player.gold} G\n");

            Console.WriteLine("[아이템목록  ]" + (1 + shop.page) + "/" + (shop.maxPage+1) + " 페이지");
            ShowItems(shop.items, shop, true);

            Console.WriteLine("\n0. 나가기\n");
            ShowInputField();
        }

        private void ShowStats(Player player)
        {
            Console.WriteLine($"이름   : {player.name}");
            Console.WriteLine($"Lv. {player.stats.level}");
            // Console.WriteLine($"직업   : {player.job}");
            Console.WriteLine($"공격력 : {player.stats.ATK} (+{player.inventory.equipmentStats.ATK})");
            Console.WriteLine($"방어력 : {player.stats.DEF} (+{player.inventory.equipmentStats.DEF})");
            Console.WriteLine($"체  력 : {player.stats.HP}");
            Console.WriteLine($"Gold   : {player.gold}");
        }

        private void ShowItems(List<Item> items, Shop shop, bool isNum)
        {
            StringBuilder sb = new();
            
            for (int i = 0 + 9 * shop.page; i < items.Count && i - 9 * shop.page < 9; i++)
            {
                sb.Clear();
                sb.Append(" -");
                Item item = items[i];

                if (isNum) sb.Append($" {i + 1 - 9 * shop.page}");

                switch (item)
                {
                    case Armor armor:
                        sb.Append($" {armor.name} | 방어력 +{armor.defense} | {armor.explain} |");
                        break;
                    case Weapon weapon:
                        sb.Append($" {weapon.name} | 공격력 +{weapon.damage} | {weapon.explain} |");
                        break;
                }

                sb.Append(item.isBuy ? " 구매완료" : $" {item.price}");

                Console.WriteLine(sb.ToString());
            }
        }

        private void ShowEquipment(List<Equipment> equipments, bool isNum)
        {
            StringBuilder sb = new();

            for (int i = 0; i < equipments.Count; i++)
            {
                sb.Clear();
                sb.Append(" -");
                Equipment equipment = equipments[i];

                if(isNum) sb.Append($" {i + 1}");
                if(equipment.isEquip) sb.Append($" [E]");

                switch (equipment)
                {
                    case Armor armor:
                        sb.Append($" {armor.name} | 방어력 +{armor.defense} | {armor.explain}");
                        break;
                    case Weapon weapon:
                        sb.Append($" {weapon.name} | 방어력 +{weapon.damage} | {weapon.explain}");
                        break;
                }

                Console.WriteLine(sb.ToString());
            }
        }        
    }
}