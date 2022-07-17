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
        private readonly LevelAdvancer _levelAdvancer;

        private bool _isMoving;
        private IPlayerMovement _playerMovement;
        private Cell _currentCell;

        public PlayerMovementController(PlayerFacade playerFacade, PlayerMovementContainer playerMovementContainer,
            LevelInfoService levelInfoService, PlayerDestinationTracker playerDestinationTracker, LevelAdvancer levelAdvancer)
        {
            _playerDestinationTracker = playerDestinationTracker;
            _playerMovementContainer = playerMovementContainer;
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
            _levelAdvancer = levelAdvancer;
        }

        public void Move()
        {
            if (_isMoving) return;
            
            PlayerCubicSlot currentSticker = _playerFacade.GetCurrentSticker();

            //Debug.Log(currentSticker ,currentSticker);
            if (currentSticker == null)
            {
                _levelAdvancer.ResetLevel();
            }

            PlayerCubicSlotData playerCubicSlot = currentSticker.SlotData;
            //IPlayerMovement playerMovement = playerCubicSlot.Movement;

            IPlayerMovement playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();

            if (playerMovement is FourDirectionMovement)
            {
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

                Cell cell = _levelInfoService.GetCellByDirection(moveDirection);

                if(cell == null)
                {
                    _levelAdvancer.ResetLevel();
                    return;
                }
                
                _playerMovement = playerMovement;
                _playerMovement.Moved += OnPlayerMoved;

                playerMovement.Move(cell, moveDirection);
            }
            else if (playerMovement is JumpMovement)
            {
                var cell = _currentCell as JumpingCell;

                Cell jumpCell = cell.JumpCell;

                //playerMovement.Move(jumpCell);
            }

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
            _playerDestinationTracker.StopTracking();

            _currentCell = cell;

            _playerDestinationTracker.CellChanged -= OnPlayerCellChanged;
            _playerMovement.Moved -= OnPlayerMoved;

            _isMoving = false;

            _currentCell.CellView.Highlight(Color.blue);

            if (cell is JumpingCell jumpingCell)
            {
                Move();
            }

            //PlayerCubicSlot playerCubicSlot = _playerFacade.GetCurrentSticker();
            //_playerMovement = playerCubicSlot.SlotData.Movement;

            // _playerMovement = _playerMovementContainer.GetMovement<FourDirectionMovement>();
        }
    }
}