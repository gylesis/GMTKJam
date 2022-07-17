using UnityEngine;
using Zenject;

namespace Project.Scripts.Raycast
{
    public class CubeSideSelectable : MonoBehaviour, ICustomSelectable
    {
        private UICubicSlotContainer _uiCubicSlotContainer;
        private SelectedStickerObserver _selectedStickerObserver;
        private SideId _id;

        [Inject]
        public void Init(UICubicSlotContainer uiCubicSlotContainer, SelectedStickerObserver selectedStickerObserver)
        {
            _uiCubicSlotContainer = uiCubicSlotContainer;
            _selectedStickerObserver = selectedStickerObserver;
            _id = GetComponent<SideId>();
        }

        public void Interact()
        {
            _selectedStickerObserver.TryCycleThroughStickers(_id.GetId());
            _uiCubicSlotContainer.SetSlot(_id.GetId());
        }
    }
}
