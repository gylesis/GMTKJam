using System;
using System.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Project.Scripts
{
    public class JumpMovement : IPlayerMovement
    {
        public event Action<Cell> Moved;

        private IDisposable _disposable;

        private readonly PlayerFacade _playerFacade;
        private readonly LevelInfoService _levelInfoService;

        Vector3 _startPos;

        [Tooltip("Position we want to hit")] public Vector3 _targetPos;

        [Tooltip("Horizontal speed, in units/sec")]
        public float _speed = 10;

        [Tooltip("How high the arc should be, in units")]
        public float _arcHeight = 20;

        private SoundPlayer _soundPlayer;

        public JumpMovement(PlayerFacade playerFacade, LevelInfoService levelInfoService, SoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }

        public async void Move(Cell cellToMove, Vector2 direction)
        {
            _soundPlayer.PlayJumpSound();
            var movePos = cellToMove.Pivot.position + (Vector3.up * (_playerFacade.Transform.localScale.x / 2));

            //await _playerFacade.Transform.DOMove(movePos, 1).SetEase(Ease.InQuad).AsyncWaitForCompletion();

            _startPos = _playerFacade.Transform.position;
            _targetPos = movePos;

            Move();
        }

        private void Move()
        {
            Transform transform = _playerFacade.Transform;

            float x0 = _startPos.x;
            float x1 = _targetPos.x;

            if (x0 - x1 == 0)
            {
                x0 = _startPos.y;
                x1 = _targetPos.y;
            }

            _disposable = Observable.EveryUpdate().Subscribe((l =>
            {
                float dist = Mathf.Abs(x1 - x0);
                float nextX = Mathf.MoveTowards(transform.position.x, x1, _speed * Time.deltaTime);
                dist += float.Epsilon;

                float baseY = Mathf.Lerp(_startPos.y, _targetPos.y, (nextX - x0) / dist);
                float arc = _arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                var nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

                // Rotate to face the next position, and then move there
                //transform.rotation = LookAt2D(nextPos - transform.position);
                transform.position = nextPos;

                // Do something when we reach the target
                if (Close(transform.position, _targetPos))
                {
                    Cell cell = _levelInfoService.GetPlayerBottomCell();
                    Moved?.Invoke(cell);
                    _disposable.Dispose();
                }
            }));
        }

        private bool Close(Vector3 current, Vector3 target)
        {
            var distance = (target - current).sqrMagnitude;

            if (distance < 1)
            {
                return true;
            }

            return false;
        }


        private Quaternion LookAt2D(Vector2 forward)
        {
            return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
        }
    }
}