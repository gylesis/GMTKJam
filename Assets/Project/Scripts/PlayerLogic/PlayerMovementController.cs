using System;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerMovementController
    {
        private readonly PlayerFacade _playerFacade;
        private readonly LevelInfoService _levelInfoService;
        private readonly PlayerMovementContainer _playerMovementContainer;

        private bool _canMove;
        private bool _isMoving;
        private IPlayerMovement _playerMovement;
        private Cell _currentCell;
        private DeathFinishHandler _deathFinishHandler;
        private Vector2 _moveDirection;
        private Cell _lastTeleportCell;

        public PlayerMovementController(PlayerFacade playerFacade, PlayerMovementContainer playerMovementContainer,
            LevelInfoService levelInfoService,
            DeathFinishHandler deathFinishHandler)
        {
            _deathFinishHandler = deathFinishHandler;
            _playerMovementContainer = playerMovementContainer;
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        public async void Move()
        {
            if(!CanMove) return;
            if (_isMoving) return;

            PlayerCubicSlot currentSticker = _playerFacade.GetCurrentSticker();
            //IPlayerMovement playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();

            _playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();

            if (currentSticker == null)
            {
                Debug.Log("no sticker");
                return;
            }

            PlayerCubicSlotData playerCubicSlot = currentSticker.SlotData;
            MoveSide moveSide = playerCubicSlot.MoveSide;

            Vector3 temp = currentSticker.SlotData.Forward;
            temp = _playerFacade.Transform.TransformDirection(temp);
            Debug.Log(temp);
            _moveDirection = new Vector2(temp.x, temp.z);

            Cell moveCell = _levelInfoService.GetCellByDirection(_moveDirection);

            Debug.Log($"Move direction {_moveDirection}, moveside {moveSide}, currentSticker {currentSticker}",
                currentSticker);

            if (moveCell == null)
            {
                await _playerFacade.TryMoveToNonExistCell(_moveDirection);
                _deathFinishHandler.OnDeath();

                return;
            }

            _playerMovement.Moved += OnPlayerMoved;
            _playerMovement.Move(moveCell, _moveDirection);

            _isMoving = true;
            //   _playerDestinationTracker.StartTracking();
            //_playerDestinationTracker.CellChanged += OnPlayerCellChanged;
        }

        private void OnPlayerCellChanged(Cell cell)
        {
            /*if (cell is JumpingCell jumpingCell)
            {
                _playerMovement.Moved -= OnPlayerMoved;

                IPlayerMovement playerMovement = _playerMovementContainer.GetMovement<JumpMovement>();

                _playerMovement = playerMovement;

                // _playerMovement.Move(jumpingCell.JumpCell);
                _playerMovement.Moved += OnPlayerMoved;

                _playerMovement = playerMovement;
            }*/
        }

        private void OnPlayerMoved(Cell cell)
        {
            Debug.Log("Player moved");
            //  _playerDestinationTracker.StopTracking();

            _currentCell = cell;

            // _playerDestinationTracker.CellChanged -= OnPlayerCellChanged;
            _playerMovement.Moved -= OnPlayerMoved;

            _isMoving = false;

            _currentCell.CellView.Highlight(Color.blue);

            if (cell is JumpingCell jumpingCell)
            {
                Debug.Log("jump");

                _playerMovement = _playerMovementContainer.GetMovement<JumpMovement>();

                Move(jumpingCell.JumpCell, _moveDirection);
                //_isMoving = true;

                return;
            }

            if (cell is TeleportCell teleportCell)
            {
                if (teleportCell == _lastTeleportCell)
                    return;

                _lastTeleportCell = teleportCell.TargetCell;
                _playerMovement = _playerMovementContainer.GetMovement<TeleportMovement>();

                Move(_lastTeleportCell, _moveDirection);
                //_isMoving = true;

                return;
            }

            _lastTeleportCell = null;

            if (cell == _levelInfoService.CurrentFinishCell)
            {
                _deathFinishHandler.OnFinishCell();
            }

            //PlayerCubicSlot playerCubicSlot = _playerFacade.GetCurrentSticker();
            //_playerMovement = playerCubicSlot.SlotData.Movement;

            // _playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();
        }


        private void Move(Cell moveCell, Vector2 direction)
        {
            _playerMovement.Move(moveCell, direction);
            _playerMovement.Moved += OnPlayerMoved;
        }
    }
}