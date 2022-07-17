using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts
{
    public class FourDirectionMovement : IPlayerMovement
    {
        public event Action<Cell> Moved;

        private readonly PlayerFacade _playerFacade;
        private readonly LevelInfoService _levelInfoService;
        private readonly AnimationCurvesData _animationCurves;

        public FourDirectionMovement(PlayerFacade playerFacade, LevelInfoService levelInfoService, AnimationCurvesData animationCurves)
        {
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
            _animationCurves = animationCurves;
        }

        public async void Move(Cell cellToMove, Vector2 direction)
        {
            var movePos = cellToMove.Pivot.position + (Vector3.up * (_playerFacade.Transform.localScale.x / 2));

            Rotate(cellToMove, direction);

            await _playerFacade.Transform.DOMove(movePos, _animationCurves.CubeMovementDuration).SetEase(_animationCurves.CubeMovementCurve).AsyncWaitForCompletion();

            Cell cell = _levelInfoService.GetPlayerBottomCell();

            Moved?.Invoke(cell);
        }

        private void Rotate(Cell cellToMove, Vector2 direction)
        {
            Vector2 targetPos = new Vector2(cellToMove.transform.position.x, cellToMove.transform.position.z);
            Vector2 currentPos = new Vector2(_playerFacade.Transform.position.x, _playerFacade.Transform.position.z);

            // Vector2 direction = targetPos - currentPos;
            Vector3 movingEulersAngle = Vector3.zero;


            direction.Normalize();

            Debug.Log(direction);

            if (direction == Vector2.left)
            {
                movingEulersAngle.z = 90;
            }
            else if (direction == Vector2.right)
            {
                movingEulersAngle.z = -90;
            }
            else if (direction == Vector2.up)
            {
                movingEulersAngle.x = 90;
            }
            else if (direction == Vector2.down)
            {
                movingEulersAngle.x = -90;
            }

            Vector3 playerEulers = _playerFacade.Transform.rotation.eulerAngles;
            playerEulers += movingEulersAngle;

            //  _playerFacade.Transform.rotation = Quaternion.Euler(playerEulers);

            _playerFacade.Transform.DOLocalRotate(playerEulers, _animationCurves.CubeMovementDuration, RotateMode.FastBeyond360).SetEase(_animationCurves.CubeRotationCurve);
        }
    }
}