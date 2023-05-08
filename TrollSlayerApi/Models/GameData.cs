using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrollSlayerApi.Models
{
    public class GameData
    {
        public int GameId { get; set; }
        public Player Player { get; set; }
        public List<Troll> Trolls { get; set; }
        public Dragon Dragon { get; set; }
    }
}