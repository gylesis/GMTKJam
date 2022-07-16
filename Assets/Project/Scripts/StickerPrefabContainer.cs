using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;

[CreateAssetMenu]
public class StickerPrefabContainer : ScriptableObject
{
    [SerializeField]private GameObject[] _slots;

    public GameObject[] Slots => _slots;
}
