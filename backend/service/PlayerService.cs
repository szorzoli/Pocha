using models;

namespace services
{
    class PlayerService
    {

        public Player CreatePlayer(string nombre)
        {
            Player newPlayer = new Player(nombre);
            return  newPlayer;
        }

        public void PlayCard(int index, Player jugador)
        {
            jugador.ThrowCard(index);
        }

        
    }
}