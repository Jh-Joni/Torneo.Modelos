using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo.Modelos
{
    public class ResultadoPartido
    {
        [Key]
        public int Id { get; set; }
       
        public int GolesA { get; set; }
        public int GolesB { get; set; }
        



        // FK

        public int PartidoId { get; set; }
        public int GanadorId { get; set; }


        

        // Navegación
        public Partido? Partido { get; set; }
        public Equipo? Ganador { get; set; }
    }
}
