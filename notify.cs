using HterGame.Entity;
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
        }
        public static void ErrorTraderMove() { // invalid exchange with trader
            Console.WriteLine("Invalid Move - You dont have enough magic to exchange.");
        }
        public static void ErrorHeroMove() { // invalid move to defeat monster
            Console.WriteLine("Invalid Move - Maybe your input datatype is not true or you don't have enough magic");
        }
        public static void KillingHint() { 
            Console.WriteLine("This is killing hint");
        }
        public static void RemainPower(Hero hero) { // Remain HP and magic of hero
           Console.WriteLine($"You have remain {hero.HP} HP, {hero.holy_magic} holy magic, {hero.earth_magic} earth magic, {hero.wind_magic} wind magic left");
        }
        public static void RoundInfo(Hero hero, Monster monster) { // Current monster's status and hero's status
            Console.WriteLine($"Current Monster have {monster.HP} HP. This monster is {Monster.GetMonsterType(monster)}");
            monster.DamageTakenInfo();
            RemainPower(hero);
        }
    }
}