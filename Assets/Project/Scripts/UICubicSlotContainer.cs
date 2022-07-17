using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts;
using UnityEngine;
using Zenject;

public class UICubicSlotContainer : MonoBehaviour
{
    [SerializeField] private Transform _slotsRoot;
    [SerializeField] private List<PlayerSlotUI> _slots;

    private SelectedStickerObserver _selectedStickerObserver;
    private StickersVisualizer _stickersVisualizer;
    private LevelInfoService _levelInfoService;

    public List<PlayerSlotUI> Slots => _slots;

    [Inject]
    public void Init(SelectedStickerObserver selectedStickerObserver, StickersVisualizer stickersVisualizer, LevelInfoService levelInfoService)
    {
        _levelInfoService = levelInfoService;
        _slots = _slotsRoot.GetComponentsInChildren<PlayerSlotUI>().ToList();
        _selectedStickerObserver = selectedStickerObserver;
        _stickersVisualizer = stickersVisualizer;
    }

    public void SetSlot(CubeSide side)
    {
        Level currentLevel = _levelInfoService.CurrentLevel;

        var busySlots = CountBusySlots();
        
        /*if(busySlots >= currentLevel.MaxCellNumber)
            return;*/
        
        if (_selectedStickerObserver.CurrentSticker != null)
        {
            var slot = _slots.Find(s => s.CubeSide == side);

            if (slot != null)
            {
                if (slot.Sticker != null)
                {
                    _stickersVisualizer.DestroySticker(slot.Sticker.gameObject);
                }

                slot.Sticker = _stickersVisualizer.Create(_selectedStickerObserver.CurrentSticker, side, transform);
            }
        }
        else
        {
            
        }
    }

    private int CountBusySlots()
    {
        int count = 0;
        
        foreach (PlayerSlotUI slot in _slots)
        {
            if (slot.IsEmpty == false)
            {
                count++;
            }
        }

        return count;
    }
    
    public void ClearUISlots()
    {
        foreach (PlayerSlotUI slot in _slots)
        {
            if (slot.IsEmpty == false)
            {
                DestroySticker(slot);
            }
        }
    }

    public void DestroySticker(PlayerSlotUI slot)
    {
        _stickersVisualizer.DestroySticker(slot.Sticker.gameObject);
    }
}