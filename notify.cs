using HterGame.Entity;
namespace HterGame.Notify {
    class Notification {
        public static void Introduction() {
            Console.WriteLine("Welcome to Beta Version of HterGame - created by TwinHter, Nguyen Huu Dang Nguyen");
            Console.WriteLine("This is the guildance.");
        }
        public static void ResultNotification(int Type, int killed_monsters = 0) {
            if(Type == 0) {
                Console.WriteLine("Congratulation - You are the winner");
                Console.WriteLine($"You have kill {killed_monsters} monsters");
            }
            if(Type == 1) {
                Console.WriteLine("Try your best next time.");
                Console.WriteLine($"You have kill {killed_monsters} monsters");
            }
            if(Type == 2) {
                Console.WriteLine("See you next time");
            }
        }
        public static void TraderHint() {
            Console.WriteLine("This is Trader Hint");
        }
        public static void ErrorTraderMove() {
            Console.WriteLine("Invalid Move - You dont have enough magic to exchange.");
        }
        public static void ErrorHeroMove() {
            Console.WriteLine("Invalid Move - Maybe your input datatype is not true or you don't have enough magic");
        }
        public static void KillingHint() {
            Console.WriteLine("This is killing hint");
        }
        public static void RemainPower(Hero hero) {
           Console.WriteLine($"You have remain {hero.HP} HP, {hero.holy_magic} holy magic, {hero.earth_magic} earth magic, {hero.wind_magic} wind magic left");
        }
        public static void RoundInfo(Hero hero, Monster monster) {
            Console.WriteLine($"Current Monster have {monster.HP} HP. This monster is {Monster.GetMonsterType(monster)}");
            RemainPower(hero);
        }
    }
}