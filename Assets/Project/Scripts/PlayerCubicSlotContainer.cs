using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;
using Zenject;

public class PlayerCubicSlotContainer : MonoBehaviour
{
    [SerializeField] private Transform _slotsRoot;
    private PlayerCubicSlot[] _slots;
    private SelectedStickerObserver _selectedStickerObserver;
    private StickersVisualizer _stickersVisualizer;

    [Inject]
    public void Init(PlayerCubicSlot[] slots, SelectedStickerObserver selectedStickerObserver, StickersVisualizer stickersVisualizer)
    {
        _slots = _slotsRoot.GetComponentsInChildren<PlayerCubicSlot>();
        _selectedStickerObserver = selectedStickerObserver;
        _stickersVisualizer = stickersVisualizer;
    }

    public void SetSlot(int side)
    {
        if(_selectedStickerObserver.CurrentSticker != null)
        {
            var sticker = _slots[side].Sticker;
            if (sticker != null)
            {
                _stickersVisualizer.DestroySticker(sticker.gameObject);
            }

            _slots[side].Sticker = _stickersVisualizer.Create(_selectedStickerObserver.CurrentSticker, _slots[side].transform.up);

        }
    }
}
