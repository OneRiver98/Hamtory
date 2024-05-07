using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hamtory
{
    public class MainTextManager : TextManager
    {




        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }



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
            ShowTitle("원하시는 이름을 설정해주세요.");
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
            Console.Write($"공격력 : {player.stats.ATK} ");
            TextHighlights("(", $"+{player.inventory.equipmentStats.ATK}", ")");
            Console.Write($"방어력 : {player.stats.DEF} ");
            TextHighlights("(", $"+{player.inventory.equipmentStats.DEF}", ")");
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
                        sb.Append($" {PadRightForMixedText(armor.name,20)} | 방어력 +{PadRightForMixedText(armor.defense.ToString(), 3)} | {PadRightForMixedText(armor.explain,50)} |");
                        break;
                    case Weapon weapon:
                        sb.Append($" {PadRightForMixedText(weapon.name,20)} | 공격력 +{PadRightForMixedText(weapon.damage.ToString(), 3)} | {PadRightForMixedText(weapon.explain,50)} |");
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
                int num = 20;


                sb.Clear();
                sb.Append(" -");
                Equipment equipment = equipments[i];

                if(isNum) sb.Append($" {i + 1}");
                if (equipment.isEquip)
                {
                    sb.Append($" [E]");
                    num = 16;
                }

                switch (equipment)
                {
                    case Armor armor:
                        sb.Append($" {PadRightForMixedText(armor.name, num)} | 방어력 +{PadRightForMixedText(armor.defense.ToString(), 3)} | {PadRightForMixedText(armor.explain,50)}");
                        break;
                    case Weapon weapon:
                        sb.Append($" {PadRightForMixedText(weapon.name, num)} | 공격력 +{PadRightForMixedText(weapon.damage.ToString(), 3)} | {PadRightForMixedText(weapon.explain,50)}");
                        break;
                }

                Console.WriteLine(sb.ToString());
            }
        }        
    }
}
