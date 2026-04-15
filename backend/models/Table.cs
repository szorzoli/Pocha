using System;
using System.Collections.Generic;
using System.Linq;

namespace models{
    class Table
    {
        public Guid id { get; set; }
        int numPartida { get; set; }
        int cantCartas { get; set; }
        public List <Player> jugadores = new List<Player>();

        public Table ()
        {
            this.id = Guid.NewGuid();
            this.numPartida = 0;
            this.cantCartas = 3;
        }

        public void AddPlayer(Player jugador)
        {
            if (jugadores.Count() >= 6)
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
            List<Card> mazo = Deck.CrearMazo();
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
        }

        public bool SetAllBets (int apuesta, Player player)
        {
            if(apuesta > cantCartas || apuesta < 0)
            {
                Console.WriteLine("No se pueden hacer apuestas mayores a la cantidad de cartas");
                return false;
            }
            if (player == jugadores[jugadores.Count - 1 ] && (jugadores.Sum(j => j.apuesta) + apuesta) == cantCartas)
            {
                Console.WriteLine("No se pueden hacer apuestas iguales a la cantidad de cartas por el pie");
                return false;
            }
            else
            {
                player.SetBet(apuesta);
                return true;
            }
        }

        public Player SelectHandWinner()
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
                else if (jugador.cartaSeleccionada.poder == maxPoder)
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

        public void EndHand()
        {

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
                jugador.cartaSeleccionada = null;
                
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

    }
}