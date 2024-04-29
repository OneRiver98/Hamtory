using Hamtory.Manager;
using System.Numerics;

namespace Hamtory
{
    internal class Program
    {
        private static GameManager gameManager = new();
  
        static void Main(string[] args)
        {
            gameManager.StartGame();                   
        }
    }
}
