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

        public void Move()
        {
            if (_isMoving) return;

            PlayerCubicSlot currentSticker = _playerFacade.GetCurrentSticker();
            IPlayerMovement playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();

            if (currentSticker == null)
            {
                Debug.Log("no sticker");
                return;
            }

            PlayerCubicSlotData playerCubicSlot = currentSticker.SlotData;
            MoveSide moveSide = playerCubicSlot.MoveSide;
            
            Vector2 moveDirection;

            switch (moveSide)
            {
                case MoveSide.Left:
                    moveDirection = Vector2.left;
                    break;
                case MoveSide.Right:
                    moveDirection = Vector2.right;
                    break;
                case MoveSide.Forward:
                    moveDirection = Vector2.up;
                    break;
                case MoveSide.Back:
                    moveDirection = Vector2.down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Vector3 temp = currentSticker.SlotData.Forward;
            temp = _playerFacade.Transform.TransformDirection(temp);
            Debug.Log(temp);
            moveDirection = new Vector2(temp.x, temp.z);


            Cell moveCell = _levelInfoService.GetCellByDirection(moveDirection);

            Debug.Log($"Move direction {moveDirection}, moveside {moveSide}, currentSticker {currentSticker}",
                currentSticker);

            if (moveCell == null)
            {
                _cellsHandler.OnDeath();
                return;
            }

            playerMovement.Moved += OnPlayerMoved;
            playerMovement.Move(moveCell, moveDirection);

            _playerMovement = playerMovement;
            _isMoving = true;
            _playerDestinationTracker.StartTracking();
            _playerDestinationTracker.CellChanged += OnPlayerCellChanged;
        }

        private void OnPlayerCellChanged(Cell cell)
        {
            if (cell is JumpingCell jumpingCell)
            {
                _playerMovement.Moved -= OnPlayerMoved;

                IPlayerMovement playerMovement = _playerMovementContainer.GetMovement<JumpMovement>();

                _playerMovement = playerMovement;

                // _playerMovement.Move(jumpingCell.JumpCell);
                _playerMovement.Moved += OnPlayerMoved;

                _playerMovement = playerMovement;
            }
        }

        private void OnPlayerMoved(Cell cell)
        {
            Debug.Log("Player moved");
            _playerDestinationTracker.StopTracking();

            _currentCell = cell;

            _playerDestinationTracker.CellChanged -= OnPlayerCellChanged;
            _playerMovement.Moved -= OnPlayerMoved;

            _isMoving = false;

            _currentCell.CellView.Highlight(Color.blue);

            if (cell is JumpingCell jumpingCell)
            {
                Debug.Log("jump");
                Move();
            }

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