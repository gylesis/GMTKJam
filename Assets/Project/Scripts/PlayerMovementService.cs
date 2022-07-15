using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class PlayerMovementService: ITickable
    {
        private PlayerMovementController _playerMovementController;

        public PlayerMovementService(PlayerMovementController playerMovementController)
        {
            _playerMovementController = playerMovementController;
        }
        
        public void Tick() // input service
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _playerMovementController.Move();
            }
        }
    }

    
}