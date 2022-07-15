using UnityEngine;

namespace Project.Scripts
{
    public class PlayerFacade
    {
        private PlayerSpawner _playerSpawner;
        private Player _player;

        public Transform Transform => _player.transform;

        public PlayerFacade(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
        }

        public void SpawnPlayer()
        {
            if (_player != null) return;

            _player = _playerSpawner.Spawn(Vector3.zero);
        }
    }
}