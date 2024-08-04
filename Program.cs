using System;
using HterGame.Entity;
using HterGame.Notify;
namespace HterGame
{
    static class RandomExtensions
    {
        public static void Shuffle<T> (this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
    public class Game_Setting {
        public static Monster[] GenerateMonster(int ground_monsters = 5, int flying_monsters = 5) {
            Random rand = new Random();
            Monster[] monsters = new Monster[ground_monsters + flying_monsters + 5];

            for(int i=0; i<ground_monsters + flying_monsters; i++) {
                int monster_hp = rand.Next(1, Monster.max_1_hp);
                if(monsters.Length >= 2) {
                    monster_hp = rand.Next(1, Math.Min(Monster.max_3_hp-monsters[i-1].HP-monsters[i-2].HP, Monster.max_1_hp));
                }
                if(i < ground_monsters) monsters[i] = new Ground_Monster(monster_hp);
                else monsters[i] = new Flying_Monster(monster_hp);
            }

            rand.Shuffle(monsters);
            return monsters;
        }
        public void HeroMove() {

        }
        public static class Trader {
            private static int trader_cycle = 3;
            public static int GetCycleLength() {
                return trader_cycle;
            }
            public static void TraderAppear(Hero current_hero) {
                Notification.TraderHint();
                bool isStop = false;
                while(true) {
                    if(isStop) {
                        break;
                    }
                    Console.WriteLine("Type in your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            if(current_hero.earth_magic < 2) {
                                Notification.ErrorTraderMove();
                            }
                            else {
                                Console.WriteLine("Trade 2 earth magic -> wind magic.");
                                current_hero.earth_magic -= 2;
                                current_hero.wind_magic += 1;
                            }
                            break;
                        
                        case 2:
                            if(current_hero.wind_magic < 2) {
                                Notification.ErrorTraderMove();
                            }
                            else {
                                Console.WriteLine("Trade 2 wind magic -> 1 earth magic.");
                                current_hero.wind_magic -= 2;
                                current_hero.earth_magic += 1;
                            }
                            break;
                        
                        case 3:
                            if(current_hero.earth_magic < 3) {
                                Notification.ErrorTraderMove();
                            }
                            else {
                                Console.WriteLine("Trade 3 earth magic -> 1 holy magic");
                                current_hero.earth_magic -= 3;
                                current_hero.holy_magic += 1;
                            }
                            break;
                        case 4:
                            if(current_hero.wind_magic < 3) {
                                Notification.ErrorTraderMove();
                            }
                            else {
                                Console.WriteLine("Trade 3 wind magic -> 1 holy magic");
                                current_hero.wind_magic -= 3;
                                current_hero.holy_magic += 1;
                            }
                            break;

                        default:
                            isStop = true;
                            break;
                    }
                }    
            }
        }
    }
    public class Normal_Game_Play() {
        
    }
    class Program {
        static void Main(string[] args) {
            Normal_Game_Play beta_game = new Normal_Game_Play();
            
        }
    }
}