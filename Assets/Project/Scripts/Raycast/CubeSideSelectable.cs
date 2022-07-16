using Project.Scripts.Raycast;
using Project.Scripts.Raycast.Selecting;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Raycast
{
    public class CubeSideSelectable : MonoBehaviour, ICustomSelectable
    {
        private PlayerCubicSlotContainer _playerCubicSlotContainer;
        private SelectedStickerObserver _selectedStickerObserver;
        private SideId _id;

        [Inject]
        public void Init(PlayerCubicSlotContainer playerCubicSlotContainer, SelectedStickerObserver selectedStickerObserver)
        {
            _playerCubicSlotContainer = playerCubicSlotContainer;
            _selectedStickerObserver = selectedStickerObserver;
            _id = GetComponent<SideId>();
        }

        


        public void Interact()
        {
            _selectedStickerObserver.TryCycleThroughStickers(_id.GetId());
            _playerCubicSlotContainer.SetSlot(_id.GetId());
        }
    }
}
