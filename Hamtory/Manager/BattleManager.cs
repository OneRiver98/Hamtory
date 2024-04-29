using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class BattleManager
    {
        private BattleSceneEnum Scene = BattleSceneEnum.MAIN;

        private BattleSceneEnum scene
        {
            get { return Scene; }
            set
            {
                Scene = value;
                switch (value)
                {
                    case BattleSceneEnum.MAIN:
                        break;
                    case BattleSceneEnum.MAIN_CHOICE:
                        break;
                    case BattleSceneEnum.PLAYER_TURN:
                        break;
                    case BattleSceneEnum.ENEMY_TURN:
                        break;
                    default:
                        break;
                }
            }
        }


        public void Battle(TextManager textMgr)
        {
            bool isBattle = true;
            while(isBattle)
            {
                



            }

        }


        public void BattleResult(Unit attacker, Unit defender)
        {
            int damage = attacker.stats.ATK;
            int originHp = defender.stats.HP;
            if(attacker is Player)
            {
                var player = (Player)attacker;
                damage += player.inventory.equipmentStats.ATK;
            }
            defender.OnAttack(damage);

            Console.WriteLine($"\n{attacker.name}의 공격 !");
            Console.WriteLine($"{defender.stats.level} {defender.name}을(를) 맞췄습니다. 데미지 : [{damage}]");

            Console.WriteLine($"\n{defender.stats.level} {defender.name}");
            if(defender.stats.HP == 0)
            {
                Console.WriteLine($"HP{originHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP{originHp} -> {defender.stats.HP}");
            }

            Console.WriteLine("\n0. 다음\n");
            Console.Write(">> ");
        }
    }
}
