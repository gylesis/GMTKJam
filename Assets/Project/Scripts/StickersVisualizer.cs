using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickersVisualizer : MonoBehaviour
{
    [SerializeField] private Transform _cube;

    public GameObject Create(GameObject sticker, Vector3 up)
    {
        var clone = Instantiate(sticker, _cube);
        clone.transform.rotation = Quaternion.LookRotation(up);
        return clone;
    }

    public void DestroySticker(GameObject sticker)
    {
        Destroy(sticker);
    }
}
