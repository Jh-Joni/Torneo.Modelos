using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo.Modelos
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        // FK
        public int TorneoId { get; set; }

        // Navegación
        public GesTorneo? GesTorneo { get; set; }
        public List<Jugador>? Jugadores { get; set; }
    }
}
