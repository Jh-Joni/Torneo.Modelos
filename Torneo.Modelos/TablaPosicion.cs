using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo.Modelos
{
    public class TablaPosicion
    {
        [Key]
        public int Id { get; set; }
         
        public int PartidosJugados { get; set; }
        public int Puntos { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int Diferencia { get; set; }

        // FK
        public int TorneoId { get; set; }
        public int EquipoId { get; set; }

        // Navegación
        public GesTorneo? GesTorneo { get; set; }
        public Equipo? Equipo { get; set; }
    }
}
