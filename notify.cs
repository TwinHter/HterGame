using HterGame.Entity;
using HterGame;
namespace HterGame.Notify {
    class Notification { // Notification when enter a game
        public static void Introduction() {
            Console.WriteLine("Welcome to Beta Version of HterGame - created by TwinHter, Nguyen Huu Dang Nguyen");
            Console.WriteLine("This is the guildance.");
        }
        public static void ResultNotification(int Type, int killed_monsters = 0) { // Result notify (win, lose, exit game)
            Console.WriteLine($"You killed {killed_monsters} monsters");
            if(Type == 0) {
                Console.WriteLine("Congratulation - You are the winner");
            }
            if(Type == 1) {
                Console.WriteLine("Try your best next time.");
            }
            if(Type == 2) {
                Console.WriteLine("See you next time");
            }
        }
        public static void TraderHint() { // Hint each time trader appear
            Console.WriteLine("This is Trader Hint");
            Console.WriteLine($"Every {Game_Setting.GetCycleLength()} rounds, trader will appear.");
            Console.WriteLine("Type 1: exchange 2 earth -> 1 wind");
            Console.WriteLine("Type 2: exchange 2 wind -> 1 earth");
            Console.WriteLine("Type 3: exchange 3 earth -> 1 holy");
            Console.WriteLine("Type 4: exchange 3 wind -> 1 holy");
            Console.WriteLine("Type another number: Goodbye Trader!!!");
        }
        public static void ErrorTraderMove() { // invalid exchange with trader
            Console.WriteLine("Invalid Move - You dont have enough magic to exchange.");
        }
        public static void ErrorHeroMove() { // invalid move to defeat monster
            Console.WriteLine("Invalid Move - Maybe your input datatype is not true or you don't have enough magic");
        }
        public static void KillingHint() { 
            Console.WriteLine("This is killing hint");
            Console.WriteLine("Ground monsters are injured by earth magic and holy magic. Flying monsters are being attack by wind magic and holy magic. Each magic will make 1 damage if it can affect that monster.");
            Console.WriteLine("If you cant kill that monsters, it will attack you with the damage equal to its remaining HP.");
            Console.WriteLine("Type 'exit' to exit game. 'remain' to show your remain magic and HP, 'hint' for the killing and trader hint");
        }
        public static void RemainPower(Hero hero) { // Remain HP and magic of hero
           Console.WriteLine($"You have remain {hero.HP} HP, {hero.holy_magic} holy magic, {hero.earth_magic} earth magic, {hero.wind_magic} wind magic left");
        }
        public static void RoundInfo(Hero hero, Monster monster) { // Current monster's status and hero's status
            Console.WriteLine($"Current Monster have {monster.HP} HP. This monster is {Monster.GetMonsterType(monster)}");
            // monster.DamageTakenInfo();
            RemainPower(hero);
        }
    }
}