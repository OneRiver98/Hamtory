using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class TextManager
    {
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
    }
}
