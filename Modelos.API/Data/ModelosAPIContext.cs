using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneo.Modelos;

    public class ModelosAPIContext : DbContext
    {
        public ModelosAPIContext (DbContextOptions<ModelosAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Torneo.Modelos.Equipo> Equipos { get; set; } = default!;

public DbSet<Torneo.Modelos.EstadisticaJugador> EstadisticasJugadores { get; set; } = default!;

public DbSet<Torneo.Modelos.Jugador> Jugadores { get; set; } = default!;

public DbSet<Torneo.Modelos.Partido> Partidos { get; set; } = default!;

public DbSet<Torneo.Modelos.TablaPosicion> TablaPosiciones { get; set; } = default!;

public DbSet<Torneo.Modelos.GesTorneo> GesTorneos { get; set; } = default!;




    }
