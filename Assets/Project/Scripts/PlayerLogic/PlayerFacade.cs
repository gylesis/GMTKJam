using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerFacade
    {
        private PlayerSpawner _playerSpawner;
        private Player _player;

        public Transform Transform => _player.transform;

        public PlayerFacade(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            
        }

        public async Task TryMoveToNonExistCell(Vector2 direction)
        {
            var directionToMove = Transform.position + (Vector3)direction;

            await Task.Delay(100);
        }
        
        public void ResetPlayer()
        {
            Transform.rotation = new Quaternion(0,0,0,1);
            _player.Slots = null;
        }
        
        public void SpawnPlayer()
        {
            if (_player != null) return;

            _player = _playerSpawner.Spawn(Vector3.zero);

            HidePlayer();
        }

        public PlayerCubicSlot GetCurrentSticker()
        {
            Vector3 up = Vector3.up;

            foreach (PlayerCubicSlot slot in _player.Slots)
            {
                Vector3 slotForward = slot.transform.forward;

                var signedAngle = Vector3.SignedAngle(up , slotForward, Vector3.up);

                var cos = Mathf.Cos(signedAngle);

                if (Mathf.Approximately(cos,1))
                    return slot;
            }

            return null;
        }

        public void SetPlayerSlots(PlayerCubicSlot[] slots)
        {
            _player.Slots = slots;
        }
        
        public void HidePlayer()
        {
            _player.gameObject.SetActive(false);
        }

        public void ShowPlayer()
        {
            _player.gameObject.SetActive(true);
        }
    }
}