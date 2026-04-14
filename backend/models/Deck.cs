using System;
using System.Collections.Generic;
using System.Linq;

namespace models
{
    class Deck
    {
        public static List<Card> CrearMazo()
        {
            List<Card> nuevoMazo = new List<Card>();
            string[] palos = { "Espada", "Basto", "Oro", "Copa" };
            int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 10, 11, 12 };

            foreach (string palo in palos)
                foreach (int numero in numeros)
                    nuevoMazo.Add(new Card(numero, palo, CalcularPoder(numero, palo)));

            return nuevoMazo;
        }

        private static int CalcularPoder(int numero, string palo)
        {
            if (numero == 1 && palo == "Espada") return 14;
            if (numero == 1 && palo == "Basto")  return 13;
            if (numero == 7 && palo == "Espada") return 12;
            if (numero == 7 && palo == "Oro")    return 11;
            if (numero == 3)  return 10;
            if (numero == 2)  return 9;
            if (numero == 1)  return 8;
            if (numero == 12) return 7;
            if (numero == 11) return 6;
            if (numero == 10) return 5;
            if (numero == 7)  return 4;
            if (numero == 6)  return 3;
            if (numero == 5)  return 2;
            if (numero == 4)  return 1;
            return 0;
        }
    }
}