using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrollSlayerApi.Models
{
    public class AttackData
    {
        public int GameId { get; set; }
        public string TargetType { get; set; }
        public int TargetIndex { get; set; }
        public int WeaponIndex { get; set; }
    }
}