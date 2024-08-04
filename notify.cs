namespace HterGame.Notify {
    class Notification {
        public static void ResultNotification(bool isWin = true) {
            if(isWin) {
                Console.WriteLine("Congratulation - You are the winner");
            }
            else {
                Console.WriteLine("Try your best next time.");
            }
        }
        public static void TraderHint() {
            Console.WriteLine("This is Trader Hint");
        }
        public static void ErrorTraderMove() {
            Console.WriteLine("Invalid Move - You dont have enough magic to exchange.");
        }
    }
}