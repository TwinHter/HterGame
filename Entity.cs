using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Xml.XPath;
using HterGame.Notify;
namespace HterGame.Entity {
    public class Hero {
        public int HP{ get; set;}
        public int holy_magic{get; set;}
        public int wind_magic{get; set;}
        public int earth_magic{get; set;}
        public bool BeingAttacked(Monster monster) {
            int damage = monster.HP;
            Console.WriteLine($"Monster deal {damage} damage.");
            if(damage < HP) {
                HP -= damage;
                return true;
            }
            else {
                HP = 0;
                Notification.ResultNotification(false);
                return false;
            }
        }
    }
    public class Monster {
        public static int max_3_hp = 7;
        public static int max_1_hp = 3;
        public int HP{get; set;}
        protected int monster_type;
        protected int earth_taken = 0;
        protected int wind_taken = 0;
        protected int holy_taken = 1;
        protected static int default_drop_chance = 20;
        public Monster(int hp = 10) {
            HP = hp;
        }
        public bool BeingDealedDamage(int earth_damage, int wind_damage, int holy_damage, Hero hero) {
            int total_damage = earth_damage * earth_taken + wind_damage * wind_taken + holy_damage * holy_taken;
            if(total_damage >= HP) {
                HP = 0;
                Console.WriteLine("You defeat a monster");
                if((this.monster_type == 0 && Flying_Monster.DropItem()) || (this.monster_type == 1 && Ground_Monster.DropItem())) {
                    hero.holy_magic += holy_taken * 2;
                    hero.earth_magic += earth_taken * 2;
                    hero.wind_magic += wind_taken * 2;
                    Console.WriteLine($"Monster Drop: {holy_taken*2} holy magic, {earth_taken*2} earth magic, {wind_taken*2} wind magic");
                }   
                else {
                    Console.WriteLine("Monster Drop: Nothing");
                }
                return true;
            }
            else {
                Console.WriteLine($"Your damage is not enough. This monster have {HP} HP, you just deal {total_damage} damagae. Remaining Monster HP: {HP - total_damage}");
                HP -= total_damage;
                return false;
            }
        }

    }
    public class Flying_Monster : Monster {
        private static int drop_chance = Monster.default_drop_chance;
        public Flying_Monster(int hp) {
            HP = hp;
            this.monster_type = 0;
            wind_taken = 1;
        }

        public static bool DropItem() {
            Random rand = new Random();
            int value = rand.Next(0, 100);
            if(value >= drop_chance) {
                drop_chance = Monster.default_drop_chance;
                return true;
            }
            else {
                drop_chance += 20;
                return false;
            }
        }
    }
    public class Ground_Monster : Monster {
        private static int drop_chance = Monster.default_drop_chance;
        public Ground_Monster(int hp = 10) {
            HP = hp;
            this.monster_type = 1;
            earth_taken = 1;
        }
        public static bool DropItem() {
            Random rand = new Random();
            int value = rand.Next(0, 100);
            if(value >= drop_chance) {
                drop_chance = Monster.default_drop_chance;
                return true;
            }
            else {
                drop_chance += 20;
                return false;
            }
        }
    }
}
