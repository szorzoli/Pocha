using System;
using System.Collections.Generic;

namespace models{
    class Player
    {
        public Guid id;
        public string nombre { get; set; }
        public int puntos { get; set; }
        public List <Card> cartas = new List<Card>();
        public int apuesta { get; set; }
        public int victorias { get; set; }
        public Card cartaSeleccionada { get; set; }
        public int turno { get; set; }

        public Player(string nombre)
        {
            this.id = Guid.NewGuid();
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

        public void SetBet(int apuesta)
        {
            this.apuesta = apuesta;
        }
    }
}
    