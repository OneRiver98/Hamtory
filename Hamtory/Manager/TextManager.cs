using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamtory
{
    public class TextManager
    {
        public void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        public void TextHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
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

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n던전 가야대");
            Console.WriteLine("가기 전에 정비를 하자.");
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
    }
}
