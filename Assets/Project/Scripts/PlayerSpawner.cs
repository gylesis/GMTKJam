using UnityEngine;

namespace Project.Scripts
{
    public class PlayerSpawner
    {
        private Player _player;
        private PlayerFactory _playerFactory;

        public PlayerSpawner(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public Player Spawn(Vector3 pos)
        {
            if (_player != null) return _player;

            _player = _playerFactory.Create();

            return _player;
        }
    }
}