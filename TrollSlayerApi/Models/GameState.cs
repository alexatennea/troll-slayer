using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrollSlayerApi.Data;

namespace TrollSlayerApi.Models
{
    public class GameState
    {
        public GameData GameData { get; set; }
        private readonly GameContext _context;

        public GameState()
        {
            GameData = new GameData
            {
                GameId = new Random().Next(),
                Player = CreatePlayer(),
                Trolls = CreateTrolls()
            };
        }

        private Player CreatePlayer()
        {
            return new Player
            {
                Health = 30,
                Weapons = new List<Weapon>
                {
                    new Weapon("Sword", "1d6+2"),
                    new Weapon("Iron Fist","1d8")
                }
            };
        }

        private List<Troll> CreateTrolls()
        {
            int numTrolls = new Random().Next(3, 7);
            var trolls = new List<Troll>();

            for (int i = 0; i < numTrolls; i++)
            {
                trolls.Add(new Troll
                {
                    Health = new Random().Next(6, 19),
                    Weapons = new List<Weapon>
                    {
                        new Weapon("Club", "1d4+2"),
                        new Weapon("Fist", "1d4" )
                    }
                });
            }

            return trolls;
        }

        public static Dragon CreateDragon()
        {
            return new Dragon
            {
                Health = 50,
                Weapons = new List<Weapon>
                {
                    new Weapon("Fire Breath", "1d12+4")
                }
            };
        }
    }
}