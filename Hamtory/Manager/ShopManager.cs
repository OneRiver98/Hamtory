using System;

namespace rpg
{
    public class ShopManager
    {
        public List<Item> items = new();

        public event BuyHandler OnBuy;

        public void ShopSetting()
        {
            items.Add(new Armor("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5, 1000));
            items.Add(new Armor("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다. ", 9, 2000));
            items.Add(new Armor("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, 3500));


            items.Add(new Weapon("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 600));
            items.Add(new Weapon("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 1500));
            items.Add(new Weapon("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 3000));
        }

        public bool BuyItem(int num, int playerGold)
        {
            if(num > items.Count) return false;

            Item item = items[num - 1];

            if(item.isBuy)
            {
                Console.WriteLine($"\n이미 구매한 아이템입니다.");
                return false;
            }
            else if (item.price > playerGold)
            {
                Console.WriteLine($"\nGold가 부족합니다.");
                return false;
            }
            else
            {
                Console.WriteLine($"구매를 완료했습니다\n");
                items[num - 1].Buy();
                OnBuy.Invoke(item.price, item);
                return true;
            }
        }
    }
}
