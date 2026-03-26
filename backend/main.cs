using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Numerics;
using System.Reflection;
using System.Security;
using System.Text;
using System.Linq;

namespace Domain
{
    class Program
    {
        static void Main(string[] args)
        {

            Card card1 = new Card(1,"espada", 13);
            Card card2 = new Card(1, "basto", 12);
            Card card3 = new Card(4,"copa",0);

            List <Card> Mano = new List<Card>();

            Mano.AddRange(new[] {card1,card2,card3});

            Mano.ForEach(Console.WriteLine);
        }

        public static List<Card> CrearMazo()
        {
            List<Card> nuevoMazo = new List<Card>();
            string[] palos = { "Espada", "Basto", "Oro", "Copa" };
            int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 10, 11, 12 };

            foreach (string palo in palos)
            {
                foreach (int numero in numeros)
                {
                    // Calculamos el poder antes de crear la carta
                    int poderCalculado = CalcularPoderTruco(numero, palo);
                    nuevoMazo.Add(new Card(numero, palo, poderCalculado));
                }
            }
            return nuevoMazo;
        }

        // El diccionario de jerarquías del Truco convertido a código
        public static int CalcularPoderTruco(int numero, string palo)
        {
            // Las 4 cartas mágicas
            if (numero == 1 && palo == "Espada") return 14;
            if (numero == 1 && palo == "Basto") return 13;
            if (numero == 7 && palo == "Espada") return 12;
            if (numero == 7 && palo == "Oro") return 11;

            // Los comunes de mayor a menor
            if (numero == 3) return 10;
            if (numero == 2) return 9;
            if (numero == 1) return 8; // Anchos falsos (Copa y Oro)
            if (numero == 12) return 7;
            if (numero == 11) return 6;
            if (numero == 10) return 5;
            if (numero == 7) return 4; // Sietes falsos (Copa y Basto)
            if (numero == 6) return 3;
            if (numero == 5) return 2;
            if (numero == 4) return 1;

            return 0; // Por si las moscas
        }
    }
    class Card
    {
        int numero { get; set; }
        string palo { get; set; }
        public int poder { get; set; }

        public Card(int numero, string palo, int poder)
        {
            this.numero = numero;
            this.palo = palo;
            this.poder = poder;
        }

        public override string ToString()
        {
            return $"{numero} de {palo}";
        }
    }

    class Player
    {
        int id;
        string nombre { get; set; }
        int puntos { get; set; }
        public List <Card> cartas = new List<Card>();
        int apuesta { get; set; }
        public int victorias { get; set; }
        public Card cartaSeleccionada { get; set; }
        public int turno { get; set; }

        public Player(int id,string nombre)
        {
            this.nombre = nombre;
            this.puntos = 0;
        }

        public Card ThrowCard(int index)
        {
            Card cartaSeleccionada = cartas[index];

            cartas.RemoveAt(index);

            return cartaSeleccionada;
        }   

        public void CalculatePoints()
        {
            if (victorias == apuesta)
            {
                puntos = puntos + 10 + apuesta;
            }
            if ( victorias > apuesta)
            {
                puntos = puntos + victorias - apuesta;
            }
 
        }
    }

    

    class Table
    {
        int numPartida { get; set; }
        int cantCartas { get; set; }
        List <Player> jugadores = new List<Player>();

        public Table ()
        {
            this.numPartida = 0;
            this.cantCartas = 3;
        }

        public void AddPlayer(Player jugador)
        {
            if (jugadores.Count() > 6)
            {
                Console.WriteLine("No pueden haber mas de 6 jugadores");
            }
            else
            {
                jugadores.Add(jugador);
                jugador.turno = jugadores.Count();
            }
        }

        public void DistributeCards()
        {
            List<Card> mazo = Program.CrearMazo();
            mazo = mazo.OrderBy(x => Guid.NewGuid()).ToList();
            int cartaIndex = 0;

            jugadores = jugadores.OrderBy(jugador => jugador.turno).ToList();

            foreach (Player jugador in jugadores)
            {
                jugador.cartas.Clear();       
            }

            for (int i = 0; i < cantCartas; i++)
            {  
                foreach (Player jugador in jugadores)
                {
                    jugador.cartas.Add(mazo[cartaIndex]);
                    cartaIndex++;
                }
            }

            if (numPartida < 3)
            {
                cantCartas++;
            }
            if (numPartida >= 4)
            {
                cantCartas--;
            }

            numPartida++;

            foreach (Player jugador in jugadores)
            {
                if (jugador.turno == jugadores.Count())
                {
                    jugador.turno = 1;
                }
                else
                {
                    jugador.turno ++;
                }
            }
        }

        /*public void SetBet(int apuesta)
        {
            if (apuesta <= cantCartas)
            {
                
            }
        }*/

        public Player SelectWinner()
        {
            int maxPoder = -1;
            Player ganador = null;

            foreach (Player jugador in jugadores)
            {
                if (jugador.cartaSeleccionada.poder > maxPoder)
                {
                    maxPoder = jugador.cartaSeleccionada.poder;
                    ganador = jugador;
                }
                if (jugador.cartaSeleccionada.poder == maxPoder)
                {
                    if (jugador.turno < ganador.turno)
                    {
                        ganador = jugador;
                    }
                }
            }

            if(ganador!= null)
            {
                ganador.victorias ++;
            }

            return ganador;
        }

    }
}


