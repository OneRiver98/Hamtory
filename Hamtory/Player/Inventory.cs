using System;

namespace rpg
{
    public class Inventory
    {
        public List<Item> items = new();
        public List<Equipment> equipments = new();

        public Stats equipmentStats;

        public bool Equip(int num)
        {
            if (num > equipments.Count) return false;

            Equipment equipment = equipments[num - 1];

            if (equipment.isEquip)
            {
                Console.WriteLine($"\n{equipment.name}을 장착해제 했습니다.");
                equipment.isEquip = false;
            }
            else
            {
                Console.WriteLine($"\n{equipment.name}을 장착 했습니다.");
                equipment.isEquip = true;                
            }
            SetStat(equipment, equipment.isEquip);
            return true;
        }

        public void SetStat(Equipment equipment, bool isEquip)
        {
            int cal = isEquip ? 1 : -1;

            switch(equipment)
            {
                case Armor armor:
                    equipmentStats.DEF += armor.defense * cal;
                    break;

                case Weapon weapon:
                    equipmentStats.ATK += weapon.damage * cal;
                    break;
            }
        }


        public void GetItem(Item item)
        {
            switch (item)
            {
                case Equipment equipment:
                    equipments.Add(equipment);
                    break;

                case Portion portion:
                    items.Add(portion);
                    break;

                default:
                    break;
            }
        }
    }
}

