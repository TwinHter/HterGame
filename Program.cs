using System;
using System.Transactions;
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
    public class Game_Setting { // Game Setting
        private static int trader_cycle = 3; 
        public static int GetCycleLength() { // Get trader cycle length
            return trader_cycle;
        }
        public static Monster[] GenerateMonster(int ground_monsters = 1, int flying_monsters = 1) { // Generate monster + shuffle
            Random rand = new Random();
            Monster[] monsters = new Monster[ground_monsters + flying_monsters];

            for(int i=0; i<ground_monsters + flying_monsters; i++) {
                int monster_hp = rand.Next(1, Monster.max_1_hp);
                if(i >= 2) {
                    monster_hp = rand.Next(1, Math.Min(Monster.max_3_hp - monsters[i-1].HP - monsters[i-2].HP, Monster.max_1_hp));
                }
                if(i < ground_monsters) monsters[i] = new Ground_Monster(monster_hp);
                else monsters[i] = new Flying_Monster(monster_hp);
            }

            rand.Shuffle(monsters);
            return monsters;
        }
        public static bool HeroMove(ref Hero current_hero, ref Monster current_monster, ref bool isExit) { // User Move
            Console.WriteLine();
            Notification.RoundInfo(current_hero, current_monster);

            Console.Write("Type in your choice: ");
            string inp = Console.ReadLine();

            if(!int.TryParse(inp, out int value)) {
                switch (inp)
                {
                    case "exit":
                        isExit = true;
                        return true;
                    case "remain":
                        Notification.RemainPower(current_hero);
                        return false;
                    case "hint":
                        Notification.KillingHint();
                        return false;
                    default:
                        Notification.ErrorHeroMove();
                        return false;
                }
            }
            else {
                Console.Write("Type in holy - earth - wind magic you use: ");
                int[] magic = new int[3];
                for(int i=0; i<3; i++) {
                    magic[i] = Convert.ToInt32(Console.ReadLine());
                }
                if(magic[0] > current_hero.holy_magic || magic[1] > current_hero.earth_magic || magic[2] > current_hero.wind_magic) {
                    Notification.ErrorHeroMove();
                    return false;
                }
                if(!current_monster.BeingDealedDamage(magic[0], magic[1], magic[2], ref current_hero)) {
                    current_hero.BeingAttacked(ref current_monster);
                }
                return true;
            }    
        }
        public static class Trader { // Trader
            public static void TraderAppear(Hero current_hero) {
                Notification.TraderHint();
                bool isStop = false;
                while(true) {
                    if(isStop) {
                        break;
                    }
                    Console.Write("Type in your choice: ");
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
    public class Normal_Game_Play {
        private Hero hero;
        private Monster[] monsters;
        private int ground_monsters;
        private int flying_monsters;
        private bool isExit;
        private int killed_monsters;
        public Normal_Game_Play(int hero_power = 2, int ground_monsters = 2, int flying_monsters = 2) {
            this.ground_monsters = ground_monsters;
            this.flying_monsters = flying_monsters;
            this.hero = new Hero(hero_power, hero_power);
            this.monsters = Game_Setting.GenerateMonster(this.ground_monsters, this.flying_monsters);
            this.isExit = false;
            this.killed_monsters = 0;
        }

        public void Game_Run() {
            for(int round=0; round < monsters.Length; round++) {
                Monster current_monster = monsters[round];
                if((round + 1) % Game_Setting.GetCycleLength() == 0) {
                    Game_Setting.Trader.TraderAppear(hero);
                }
                
                while(!Game_Setting.HeroMove(ref hero, ref current_monster, ref isExit)) {}

                if(isExit == true) {
                    Notification.ResultNotification(2);
                    break;
                }
                if(current_monster.HP <= 0) killed_monsters += 1;
                if(hero.HP <= 0) break;
            }

            if(!isExit) {
                if(hero.HP <= 0) Notification.ResultNotification(1, killed_monsters);
                else Notification.ResultNotification(0, killed_monsters);
            }
        }
    }
    class Program {
        static void Main(string[] args) {
            Notification.Introduction();
            Normal_Game_Play beta_game = new Normal_Game_Play();
            beta_game.Game_Run();
        }
    }
}