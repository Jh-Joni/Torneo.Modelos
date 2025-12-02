using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo.Modelos
{
    public class EstadisticaJugador
    {
        [Key]
        public int Id { get; set; }
        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }

        // FK
        public int PartidoId { get; set; }
        public int JugadorId { get; set; }

        // Navegación
        public Partido? Partido { get; set; }
        public Jugador? Jugador { get; set; }
    }
}
