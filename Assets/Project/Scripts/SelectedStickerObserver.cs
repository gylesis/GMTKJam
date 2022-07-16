using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;
using Zenject;

public class SelectedStickerObserver
{
    private PlayerMovementContainer _movementContainer;
    private StickerPrefabContainer _stickerPrefabContainer;
    private PlayerCubicSlot _currentSticker;
    private int _prevSide = -1;

    [Inject]
    public SelectedStickerObserver(PlayerMovementContainer movementContainer, StickerPrefabContainer stickerPrefabContainer)
    {
        _movementContainer = movementContainer;
        _stickerPrefabContainer = stickerPrefabContainer;
        _currentSticker = null;
        _index = 0;
    }

    public PlayerCubicSlot CurrentSticker => _currentSticker;

    private int _index;

    public void TryCycleThroughStickers(int side)
    {
        if (side != _prevSide)
        {
            _index = 0;
        }
        else
        {
            _index++;
            if (_index >= _stickerPrefabContainer.Slots.Length)
            {
                _index = 0;
            }
        }
        _currentSticker = _stickerPrefabContainer.Slots[_index];
        _prevSide = side;
    }
}
