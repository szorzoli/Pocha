using Microsoft.AspNetCore.Http.HttpResults;
using models;

namespace services
{
    class TableService
    {
        Table _mesa = new Table(); 
        private readonly PlayerService _playerService;
        public TableService(PlayerService playerService)
        {
            _playerService = playerService;
        }

        private Table CreateTable()
        {
            Table newMesa  = new Table();
            return newMesa;
        }

        private Player AddPlayer(string nombre)
        {
            Player newJugador = _playerService.CreatePlayer(nombre);
            _mesa.AddPlayer(newJugador);
            return newJugador;
        }

        private void DistributeCards()
        {
            _mesa.DistributeCards();
        }

        private Player SelectWinner()
        {
            if (_mesa.jugadores.Any(j => j.cartaSeleccionada == null))
                {
                    Console.WriteLine("Hay jugadores que no tiraron carta.");
                    return null;
                }
            Player ganador = _mesa.SelectHandWinner();
            return ganador;
        }

        private List<Player> GetPlayers()
        {
            List<Player> jugadores = _mesa.jugadores;
            return jugadores;
        }

        //private end
    }

}