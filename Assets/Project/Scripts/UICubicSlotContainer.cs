using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
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
    private SoundPlayer _soundPlayer;

    public List<PlayerSlotUI> Slots => _slots;

    [Inject]
    public void Init(SelectedStickerObserver selectedStickerObserver, StickersVisualizer stickersVisualizer,
        LevelInfoService levelInfoService, SoundPlayer soundPlayer)
    {
        _soundPlayer = soundPlayer;
        _levelInfoService = levelInfoService;
        _slots = _slotsRoot.GetComponentsInChildren<PlayerSlotUI>().ToList();
        _selectedStickerObserver = selectedStickerObserver;
        _stickersVisualizer = stickersVisualizer;
    }

    public async void SetSlot(CubeSide side)
    {
        Level currentLevel = _levelInfoService.CurrentLevel;

        var slot = _slots.Find(s => s.CubeSide == side);
        
        if (_selectedStickerObserver.CurrentSticker != null)
        {
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
            DestroySticker(slot);
        }
        
        await Task.Delay(100);
        _soundPlayer.PlayStickerSound();
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