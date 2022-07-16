using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;
using Zenject;

public class PlayerCubicSlotContainer
{
    private PlayerCubicSlot[] _slots = new PlayerCubicSlot[6];
    [Inject]
    private SelectedStickerObserver _selectedStickerObserver;

    public void SetSlot(int side)
    {
        if(_selectedStickerObserver.CurrentSticker != null)
        {
            Debug.Log(_selectedStickerObserver.CurrentSticker + " " + side);
            _slots[side] = _selectedStickerObserver.CurrentSticker;
        }
    }
}
