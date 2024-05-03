using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class ShopManager
    {
        public List<Item> items = new();
        public int page = 0;
        public event BuyHandler OnBuy;
        public int maxPage = 0;

        public void ShopSetting()
        {
            items.Add(new Armor("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5, 1000));
            items.Add(new Armor("천 갑옷", " 어떠한 리그에서 가져온 갑옷 입니다.", 3, 300));
            items.Add(new Armor("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다. ", 9, 2000));
            items.Add(new Armor("워모그의 갑옷", "어떠한 리그에서 가져온 튼튼한 갑옷 입니다.", 13, 3000));
            items.Add(new Armor("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, 3500));
            items.Add(new Armor("란두인의태양방패", "이상하게 생긴 방패, 하지만 뜨겁고 튼튼하다.", 25, 7000));
            items.Add(new Armor("수학책", "김건모가 즐겨 쓰는 방어구다", 7, 1000));

            items.Add(new Weapon("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 600));
            items.Add(new Weapon("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 1500));
            items.Add(new Weapon("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 3000));
            items.Add(new Weapon("롱소드", "어디서 많이 본 칼이다.", 10, 4500));
            items.Add(new Weapon("피를 바라는 무한의 연사포", "괴상하게 생긴 총이다.", 20, 15000));
            items.Add(new Weapon("화도일문자", "명검이다.", 15, 10000));
            items.Add(new Weapon("루헨델", "어떤 랭커가 사용 하던 검이다.", 30, 50000));

            maxPage = items.Count / 9;
        }

        public bool BuyItem(int num, int playerGold)
        {
            if (num > items.Count) return false;

            Item item = items[num - 1];

            if (item.isBuy)
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
