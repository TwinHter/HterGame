using System;
using HterGame.Entity;
namespace HterGame
{
    public class Game_Setting {
        public void GenerateMonster(int number_monster = 10) {
            
        }
        public void HeroMove() {

        }
        public class Trader {
            private int trader_cycle = 3;
            public void TraderAppear() {
                Notification.TraderHint();
                bool isStop = false;
                while(true) {
                    if(isStop) {
                        break;
                    }
                    Console.WriteLine("Type in your choice: ")
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice))
                    {
                        case 0: 
                            isStop = true;
                            break;
                        case 1:
                            
                        default:
                    }
                }    
            }
        }
    }
    class Program {
        static void Main(string[] args) {}
    }
}