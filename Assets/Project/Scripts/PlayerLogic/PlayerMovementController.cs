using System;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerMovementController
    {
        private readonly PlayerFacade _playerFacade;
        private readonly LevelInfoService _levelInfoService;
        private readonly PlayerMovementContainer _playerMovementContainer;
        private readonly PlayerDestinationTracker _playerDestinationTracker;

        private bool _isMoving;
        private IPlayerMovement _playerMovement;
        private Cell _currentCell;
        private CellsHandler _cellsHandler;
        private Vector2 _moveDirection;
        private Cell _lastTeleportCell;

        public PlayerMovementController(PlayerFacade playerFacade, PlayerMovementContainer playerMovementContainer,
            LevelInfoService levelInfoService, PlayerDestinationTracker playerDestinationTracker,
            CellsHandler cellsHandler)
        {
            _cellsHandler = cellsHandler;
            _playerDestinationTracker = playerDestinationTracker;
            _playerMovementContainer = playerMovementContainer;
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }

        public async void Move()
        {
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
                _cellsHandler.OnDeath();

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
                _playerMovement.Move(jumpingCell.JumpCell, _moveDirection);
                _playerMovement.Moved += OnPlayerMoved;
                //_isMoving = true;

                return;
            }

            if (cell is TeleportCell teleportCell)
            {
                if(teleportCell == _lastTeleportCell)
                    return;

                _lastTeleportCell = teleportCell.TargetCell;
                _playerMovement = _playerMovementContainer.GetMovement<TeleportMovement>();
                _playerMovement.Move(teleportCell.TargetCell, _moveDirection);
                _playerMovement.Moved += OnPlayerMoved;
                //_isMoving = true;

                return;
            }

            _lastTeleportCell = null;
            
            if (cell == _levelInfoService.CurrentFinishCell)
            {
                _cellsHandler.OnFinishCell();
            }

            //PlayerCubicSlot playerCubicSlot = _playerFacade.GetCurrentSticker();
            //_playerMovement = playerCubicSlot.SlotData.Movement;

            // _playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();
        }
    }
}