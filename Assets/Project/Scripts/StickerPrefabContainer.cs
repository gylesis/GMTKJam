using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;

[CreateAssetMenu]
public class StickerPrefabContainer : ScriptableObject
{
    [SerializeField]private PlayerCubicSlot[] _slots;

    public PlayerCubicSlot[] Slots => _slots;
}
