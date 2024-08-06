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
        public Hero(int hp = 5, int magic = 5) {
            HP = hp;
            holy_magic = magic;
            wind_magic = magic;
            earth_magic = magic;
        }
        public void BeingAttacked(ref Monster monster) { // Hero attacked by monster
            int damage = Math.Min(monster.HP, HP);
            Console.WriteLine($"Monster deal {damage} damage.");
            HP -= damage;
            monster.HP -= damage;
            Console.WriteLine($"You have remain {HP} HP.");
        }
    }
    public class Monster {
        public static int max_3_hp = 7; // max 3 consecutive monster HP
        public static int max_1_hp = 3; // Max 1 monster HP
        public int HP{get; set;}
        protected int monster_type;
        // Taken damage of each monster for each magic
        protected int earth_taken = 0; 
        protected int wind_taken = 0;
        protected int holy_taken = 1;
        // Drop chance when die of each type of monster
        protected static int default_drop_chance = 10;
        public Monster(int hp = 3) {
            HP = hp;
        }
        public bool BeingDealedDamage(int holy_damage, int earth_damage, int wind_damage, ref Hero hero) { // Monster attacked by hero
            hero.holy_magic -= holy_damage;
            hero.earth_magic -= earth_damage;
            hero.wind_magic -= wind_damage;
            int total_damage = earth_damage * earth_taken + wind_damage * wind_taken + holy_damage * holy_taken;
            if(total_damage >= HP) { // Hero have enough damage + drop item
                HP = 0;
                Console.WriteLine("You defeat a monster");
                if((this.monster_type == 0 && Flying_Monster.DropItem()) || (this.monster_type == 1 && Ground_Monster.DropItem())) {
                    hero.earth_magic += earth_taken;
                    hero.wind_magic += wind_taken;
                    Console.WriteLine($"Monster Drop: 0 holy magic, {earth_taken} earth magic, {wind_taken} wind magic");
                }   
                else {
                    Console.WriteLine("Monster Drop: Nothing");
                }
                return true;
            }
            else { // Hero dont have enough damage
                Console.WriteLine($"Your damage is not enough. This monster have {HP} HP, you just deal {total_damage} damagae. Remaining Monster HP: {HP - total_damage}");
                HP -= total_damage;
                return false;
            }
        }

        public static string GetMonsterType(Monster monster) {
            return (monster.monster_type == 1) ? "ground" : "flying";
        }

        public void DamageTakenInfo() {
            Console.WriteLine($"Holy: {holy_taken}, Earth: {earth_taken}, Wind: {wind_taken}");
        }
    }
    public class Flying_Monster : Monster {
        private static int drop_chance = Monster.default_drop_chance;
        public Flying_Monster(int hp) {
            HP = hp;
            this.monster_type = 0;
            wind_taken = 1;
        }

        public static bool DropItem() { // drop item function - valid for each type of monster not each instance
            Random rand = new Random();
            int value = rand.Next(0, 100);
            if(value >= drop_chance) {
                drop_chance = Monster.default_drop_chance;
                return true;
            }
            else {
                drop_chance += 10;
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
                drop_chance += 10;
                return false;
            }
        }
    }
}
