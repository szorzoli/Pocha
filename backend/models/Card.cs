using System;
using System.Collections.Generic;

namespace models
{
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
}