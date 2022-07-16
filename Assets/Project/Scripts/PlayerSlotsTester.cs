using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;
using Zenject;

public class PlayerSlotsTester : MonoBehaviour
{

    private PlayerCubicSlotsBuilder _slotsBuilder;
    private UICubicSlotContainer _slotContainer;

    [Inject]
    public void Init(PlayerCubicSlotsBuilder slotsBuilder, UICubicSlotContainer slotContainer)
    {
        _slotsBuilder = slotsBuilder;
        _slotContainer = slotContainer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _slotsBuilder.SetAllStickers(_slotContainer.Slots.ToArray());
        }
    }
}
