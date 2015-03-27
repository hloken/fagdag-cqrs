using System;

namespace FagdagCqrs.Specs.Arguments
{
    public class RomReservasjon
    {
        public RomType RomType { get; set; }
        public DateTime FraDato { get; set; }
        public int LengdePåOpphold { get; set; }
    }
}