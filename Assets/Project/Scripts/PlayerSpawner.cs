using UnityEngine;

namespace Project.Scripts
{
    public class PlayerSpawner
    {
        private Player _player;
        private PlayerFactory _playerFactory;

        public PlayerSpawner(Player player, PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _player = player;
        }

        public Player Spawn(Vector3 pos)
        {
            if (_player != null) return _player;

           // Player player = _playerFactory.Create();

            return _player;
        }
    }
}