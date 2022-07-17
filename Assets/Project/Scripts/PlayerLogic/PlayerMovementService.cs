using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class PlayerMovementService : ITickable
    {
        private PlayerMovementController _playerMovementController;

        public bool AllowToPlay { get; set; } = true;

        public PlayerMovementService(PlayerMovementController playerMovementController)
        {
            _playerMovementController = playerMovementController;
        }

        public void Tick() // input service
        {
            if (AllowToPlay == false) return;

            if (Input.GetKeyDown(KeyCode.W))
            {
                _playerMovementController.Move();
            }
        }
    }
}