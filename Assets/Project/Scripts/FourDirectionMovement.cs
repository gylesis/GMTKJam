using System;
using System.Threading.Tasks;
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
        private SoundPlayer _soundPlayer;

        public FourDirectionMovement(PlayerFacade playerFacade, LevelInfoService levelInfoService, AnimationCurvesData animationCurves, SoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
            _animationCurves = animationCurves;
        }

        public async void Move(Cell cellToMove, Vector2 direction)
        {
            _soundPlayer.PlayMoveSound();
            
            var movePos = cellToMove.Pivot.position + (Vector3.up * (_playerFacade.Transform.localScale.x / 2));

            await _playerFacade.Transform.DOMove(movePos, _animationCurves.CubeMovementDuration).SetEase(_animationCurves.CubeMovementCurve).AsyncWaitForCompletion();
            
            await Rotate(cellToMove, direction);

            Cell cell = _levelInfoService.GetPlayerBottomCell();

            Moved?.Invoke(cell);
        }

        private async Task Rotate(Cell cellToMove, Vector2 direction)
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
            playerEulers = movingEulersAngle;
            
            
            _playerFacade.Transform.DORotate(playerEulers, _animationCurves.CubeMovementDuration, RotateMode.LocalAxisAdd).SetEase(_animationCurves.CubeRotationCurve);
            
            return;
            if (movingEulersAngle.z != 0)
            {
                await DOVirtual.Float(playerEulers.z, playerEulers.z + movingEulersAngle.z, 0.5f, (value =>
                {
                    Vector3 eulerAngles = _playerFacade.Transform.rotation.eulerAngles;
                    eulerAngles.z = value;

                    _playerFacade.Transform.rotation = Quaternion.Euler(eulerAngles);
                })).AsyncWaitForCompletion();
            }
            
            if (movingEulersAngle.z != 0)
            {
                await DOVirtual.Float(playerEulers.x, playerEulers.x + movingEulersAngle.x, 0.5f, (value =>
                {
                    Vector3 eulerAngles = _playerFacade.Transform.rotation.eulerAngles;
                    eulerAngles.x = value;

                    _playerFacade.Transform.rotation = Quaternion.Euler(eulerAngles);
                })).AsyncWaitForCompletion();
            }
            

            //  _playerFacade.Transform.rotation = Quaternion.Euler(playerEulers);

            _playerFacade.Transform.DOLocalRotate(playerEulers, 1, RotateMode.FastBeyond360);
        }
    }
}