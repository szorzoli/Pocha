using System;                       
using models;                       

namespace Domain
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("=== INICIANDO PRUEBAS ===\n");

            // --- Crear mesa y jugadores ---
            Table mesa = new Table();
            Player p1 = new Player("Alice");
            Player p2 = new Player("Bob");
            Player p3 = new Player("Carlos");

            Console.WriteLine($"Jugadores creados: {p1.id} - Alice | {p2.id} - Bob | {p3.id} - Carlos");

            // --- Probar AddPlayer ---
            mesa.AddPlayer(p1);
            mesa.AddPlayer(p2);
            mesa.AddPlayer(p3);
            Console.WriteLine($"\nTurnos asignados: Alice={p1.turno} | Bob={p2.turno} | Carlos={p3.turno}");

            // --- Probar DistributeCards ---
            mesa.DistributeCards();
            Console.WriteLine("\n--- Cartas repartidas ---");
            Console.WriteLine($"Alice:  {string.Join(", ", p1.cartas)}");
            Console.WriteLine($"Bob:    {string.Join(", ", p2.cartas)}");
            Console.WriteLine($"Carlos: {string.Join(", ", p3.cartas)}");

            // --- Probar SetAllBets ---
            Console.WriteLine("\n--- Apuestas ---");

            // Apuesta inválida (mayor a cantCartas=3)
            bool r1 = mesa.SetAllBets(5, p1);
            Console.WriteLine($"Alice apuesta 5 (debería fallar): {r1}");

            // Apuestas válidas
            bool r2 = mesa.SetAllBets(1, p1);
            Console.WriteLine($"Alice apuesta 1: {r2}");

            bool r3 = mesa.SetAllBets(1, p2);
            Console.WriteLine($"Bob apuesta 1: {r3}");

            // Último jugador, apuesta prohibida (1+1+1 = 3 = cantCartas)
            bool r4 = mesa.SetAllBets(1, p3);
            Console.WriteLine($"Carlos apuesta 1 (debería fallar, suma daría 3): {r4}");

            // Último jugador, apuesta válida
            bool r5 = mesa.SetAllBets(0, p3);
            Console.WriteLine($"Carlos apuesta 0 (debería pasar): {r5}");

            // --- Probar ThrowCard y SelectWinner ---
            Console.WriteLine("\n--- Jugando cartas ---");
            p1.cartaSeleccionada = p1.ThrowCard(0);
            p2.cartaSeleccionada = p2.ThrowCard(0);
            p3.cartaSeleccionada = p3.ThrowCard(0);

            Console.WriteLine($"Alice juega:  {p1.cartaSeleccionada} (poder: {p1.cartaSeleccionada.poder})");
            Console.WriteLine($"Bob juega:    {p2.cartaSeleccionada} (poder: {p2.cartaSeleccionada.poder})");
            Console.WriteLine($"Carlos juega: {p3.cartaSeleccionada} (poder: {p3.cartaSeleccionada.poder})");

            Player ganador = mesa.SelectWinner();
            Console.WriteLine($"\nGanador de la ronda: Jugador {ganador.nombre} (victorias: {ganador.victorias})");

            // --- Probar CalculatePoints ---
            Console.WriteLine("\n--- Fin de mano ---");
            p1.CalculatePoints();
            p2.CalculatePoints();
            p3.CalculatePoints();

            Console.WriteLine($"Puntos: Alice={p1.puntos} | Bob={p2.puntos} | Carlos={p3.puntos}");

            // --- Probar EndHand ---
            mesa.EndHand();
            Console.WriteLine($"Turnos rotados: Alice={p1.turno} | Bob={p2.turno} | Carlos={p3.turno}");

            Console.WriteLine("\n=== PRUEBAS FINALIZADAS ===");
        }

    }}