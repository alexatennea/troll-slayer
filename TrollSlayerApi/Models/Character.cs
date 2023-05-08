using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrollSlayerApi.Models
{
    public class Character
    {
        public int Health { get; set; }
        public List<Weapon> Weapons { get; set; }
    }
}