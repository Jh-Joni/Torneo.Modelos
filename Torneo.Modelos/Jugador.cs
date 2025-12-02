using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Torneo.Modelos
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int Goles { get; set; }
        public string? Targetas { get; set; }

        // FK
        public int EquipoId { get; set; }

        // Navegación
        public Equipo? Equipo { get; set; }
        public List<EstadisticaJugador>? Estadisticas { get; set; }
    }
}
