using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;

public class PlayerSlotUI : MonoBehaviour
{
    private MoveSide _moveSide;
    [SerializeField] private CubeSide _cubeSide;
    [SerializeField]private Sticker _sticker;

    public Sticker Sticker
    {
        get => _sticker;
        set => _sticker = value;
    }

    public MoveSide Movement
    {
        get => _sticker._moveSide;
        set => _sticker._moveSide = value;
    }

    public CubeSide CubeSide => _cubeSide;
}
