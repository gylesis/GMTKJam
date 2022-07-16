using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StickerPrefabContainer : ScriptableObject
{
    [SerializeField]private Sticker[] _slots;

    public Sticker[] Slots => _slots;
}