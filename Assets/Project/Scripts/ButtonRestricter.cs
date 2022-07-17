using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using Project.Scripts.Raycast.Selecting;
using UnityEngine;
using Zenject;

public class ButtonRestricter : MonoBehaviour
{
    private bool _inSession;

    private PlayerMovementController _controller;
    private PlayerCubicSlotsBuilder _slotsBuilder;
    private UICubicSlotContainer _slotContainer;
    private SelectionManager _selectionManager;

    [Inject]
    public void Init(PlayerCubicSlotsBuilder slotsBuilder, UICubicSlotContainer slotContainer, PlayerMovementController controller, SelectionManager selectionManager)
    {
        _slotsBuilder = slotsBuilder;
        _slotContainer = slotContainer;
        _controller = controller;
        _selectionManager = selectionManager;
    }


    public void BeginGame()
    {
        if (_inSession) return;
        
        _slotsBuilder.SetAllStickers(_slotContainer.Slots.ToArray());
        _selectionManager.Active = false;
        _slotContainer.enabled = false;
        _controller.CanMove = true;
        _inSession = true;
    }

    public void SetStickersPhase()
    {
        if(!_inSession) return;
        _selectionManager.Active = true;
        //_slotContainer.enabled = true;
        _controller.CanMove = false;
        _inSession = false;
    }
}
