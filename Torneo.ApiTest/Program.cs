using Newtonsoft.Json;
using System.Text;
using Torneo.Modelos;

namespace Torneo.ApiTest
{
    internal class Program
    {
        private static readonly HttpClient http = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7218/")
        };

        static void Main(string[] args)
        {
            Console.WriteLine("=== PRUEBA COMPLETA DEL SISTEMA DE TORNEOS ===");

            // 1 Crear torneo
            int torneoId = CrearTorneo().Result;

            // 2 Crear e inscribir 16 equipos
            var equipos = CrearEquipos(16).Result;
            InscribirEquipos(torneoId, equipos).Wait();

            // 3 Iniciar torneo (fase de grupos)
            IniciarTorneo(torneoId).Wait();

            // 4 Registrar resultados de TODOS los partidos de grupos
            RegistrarResultadosDeGrupos(torneoId).Wait();

            // 5 Avanzar a eliminación directa
            AvanzarEliminatoria(torneoId).Wait();

            // 6 Registrar cuartos, semis y final
            RegistrarEliminatorias(torneoId).Wait();

            // 7 Consultar campeón
            MostrarCampeon(torneoId).Wait();

            // 8 Goleadores
            MostrarGoleadores(torneoId).Wait();

            // 9 Historial entre dos equipos
            MostrarHistorial(torneoId, equipos[0], equipos[1]).Wait();

            Console.WriteLine("\n=== PROCESO COMPLETO ===");
            Console.ReadLine();
        }

        // 1) Crear torneo

        static async Task<int> CrearTorneo()
        {
            var torneo = new
            {
                Nombre = "Copa Primavera 2024",
                Tipo = "Mixto",
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddMonths(2)
            };

            var response = await http.PostAsync(
                "api/torneos",
                new StringContent(JsonConvert.SerializeObject(torneo), Encoding.UTF8, "application/json")
            );

            string json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResult<GesTorneo>>(json);

            Console.WriteLine("Torneo creado: " + result.Data.Id);
            return result.Data.Id;
        }

        
        // 2) Crear equipos
        static async Task<List<int>> CrearEquipos(int cantidad)
        {
            List<int> ids = new List<int>();

            for (int i = 1; i <= cantidad; i++)
            {
                var equipo = new
                {
                    Nombre = "Equipo " + i
                };

                var response = await http.PostAsync(
                    "api/equipos",
                    new StringContent(JsonConvert.SerializeObject(equipo), Encoding.UTF8, "application/json")
                );

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ApiResult<Equipo>>(json);

                ids.Add(data.Data.Id);
                Console.WriteLine($"Equipo creado: {data.Data.Nombre}");
            }

            return ids;
        }


        // 3) Inscribir equipos

        static async Task InscribirEquipos(int torneoId, List<int> equipos)
        {
            foreach (var idEquipo in equipos)
            {
                var body = new
                {
                    TorneoId = torneoId,
                    EquipoId = idEquipo
                };

                await http.PostAsync(
                    "api/torneos/inscribir",
                    new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                );
            }

            Console.WriteLine("Equipos inscritos.");
        }


        // 4) Iniciar torneo (genera calendario)
        static async Task IniciarTorneo(int torneoId)
        {
            await http.PostAsync($"api/torneos/{torneoId}/iniciar", null);
            Console.WriteLine("Torneo iniciado.");
        }


        // 5) Registrar resultados de grupos
        static async Task RegistrarResultadosDeGrupos(int torneoId)
        {
            var response = await http.GetAsync($"api/partidos/torneo/{torneoId}");
            var json = await response.Content.ReadAsStringAsync();
            var partidos = JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json).Data;

            foreach (var p in partidos)
            {
                if (!p.Jugado)
                {
                    var body = new
                    {
                        PartidoId = p.Id,
                        GolesA = new Random().Next(0, 5),
                        GolesB = new Random().Next(0, 5)
                    };

                    await http.PostAsync(
                        $"api/partidos/resultado",
                        new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                    );
                }
            }

            Console.WriteLine("Resultados de grupos registrados.");
        }

        // 6) Pasar a eliminatorias

        static async Task AvanzarEliminatoria(int torneoId)
        {
            await http.PostAsync($"api/torneos/{torneoId}/avanzar", null);
            Console.WriteLine("Pasado a eliminación directa");
        }

        // 7) Registrar cuartos, semis y final

        static async Task RegistrarEliminatorias(int torneoId)
        {
            var response = await http.GetAsync($"api/partidos/torneo/{torneoId}");
            var json = await response.Content.ReadAsStringAsync();
            var partidos = JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json).Data;

            foreach (var p in partidos)
            {
                if (!p.Jugado)
                {
                    var body = new
                    {
                        PartidoId = p.Id,
                        GolesA = new Random().Next(0, 5),
                        GolesB = new Random().Next(0, 5)
                    };

                    await http.PostAsync(
                        $"api/partidos/resultado",
                        new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                    );
                }
            }

            Console.WriteLine("Eliminatorias registradas.");
        }


        // 8) Mostrar campeón
        static async Task MostrarCampeon(int torneoId)
        {
            var response = await http.GetAsync($"api/torneos/{torneoId}/campeon");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Campeón:");
            Console.WriteLine(json);
        }


        // 9) Mostrar goleadores
        static async Task MostrarGoleadores(int torneoId)
        {
            var response = await http.GetAsync($"api/jugadores/goleadores/{torneoId}");
            Console.WriteLine("Goleadores:");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }


        // 10) Historial
        static async Task MostrarHistorial(int torneoId, int equipo1, int equipo2)
        {
            var response = await http.GetAsync($"api/partidos/historial?torneo={torneoId}&a={equipo1}&b={equipo2}");
            Console.WriteLine("Historial:");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }


}
