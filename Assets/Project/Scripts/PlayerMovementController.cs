using UnityEngine;

namespace Project.Scripts
{
    public class PlayerMovementController
    {
        private PlayerFacade _playerFacade;

        private IPlayerMovement _playerMovement;

        private int _currentCell;
        private bool _isMoving;

        public PlayerMovementController(PlayerFacade playerFacade, PlayerMovementContainer playerMovementContainer)
        {
            _playerFacade = playerFacade;

            _playerMovement = playerMovementContainer.GetMovement<StraightMovement>();
            //_playerMovement = playerMovementContainer.GetMovement<LeftMovement>();
        }

        public void SetPlayerMovement(IPlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        public void Move()
        {
            if (_isMoving) return;
            _isMoving = true;

            _playerMovement.Moved += OnPlayerMoved;
            _playerMovement.Move();
        }

        private void OnPlayerMoved(Cell cell)
        {
            _isMoving = false;
            _playerMovement.Moved -= OnPlayerMoved;

            cell.CellView.Highlight(Color.blue);
        }
    }
}