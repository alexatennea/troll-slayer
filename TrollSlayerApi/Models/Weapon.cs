using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrollSlayerApi.Models
{
    public class Weapon
    {
        public string Name { get; set; }
        public string DiceRoll { get; set; }

        public Weapon(string name, string diceRoll)
        {
            Name = name;
            DiceRoll = diceRoll;
        }

        public int GetDamage()
        {
            var parts = DiceRoll.Split(new[] { 'd', '+' }, StringSplitOptions.RemoveEmptyEntries);
            int numDice = int.Parse(parts[0]);
            int sides = int.Parse(parts[1]);
            int bonus = parts.Length > 2 ? int.Parse(parts[2]) : 0;

            var random = new Random();
            int damage = bonus;
            for (int i = 0; i < numDice; i++)
            {
                damage += random.Next(1, sides + 1);
            }

            return damage;
        }
    }
}