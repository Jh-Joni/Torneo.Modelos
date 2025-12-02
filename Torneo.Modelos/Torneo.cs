using System.ComponentModel.DataAnnotations;

namespace Torneo.Modelos
{
    public class Torneo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }     
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }  

        // Navegación
        public List<Equipo>? Equipos { get; set; }
        public List<Partido>? Partidos { get; set; }

    }
}
 