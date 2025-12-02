using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo.Modelos
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool Jugado { get; set; }
        public int GolesA { get; set; }
        public int GolesB { get; set; }
        
        public int TorneoId { get; set; }
        public int EquipoAId { get; set; }
        public int EquipoBId { get; set; }
        
        public Torneo? Torneo { get; set; }
        public Equipo? EquipoA { get; set; }
        public Equipo? EquipoB { get; set; }
    }


}
