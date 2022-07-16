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
        
        public void SpawnPlayer()
        {
            if (_player != null) return;

            _player = _playerSpawner.Spawn(Vector3.zero);

            HidePlayer();
        }

        public PlayerCubicSlotData GetCurrentSticker()
        {
            Vector3 up = Transform.up;  

            foreach (PlayerCubicSlot slot in _player.Slots)
            {
                var signedAngle = Vector3.SignedAngle(up , slot.transform.forward, Vector3.up);

                var cos = Mathf.Cos(signedAngle);

                if (Mathf.Approximately(cos,1))
                    return slot.SlotData;
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