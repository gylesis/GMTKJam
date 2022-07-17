using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;

public class StickersVisualizer : MonoBehaviour
{

    public Sticker Create(Sticker sticker, CubeSide side, Transform root)
    {
        if (sticker == null) return null;
        var clone = Instantiate(sticker, root);
        clone.gameObject.layer = root.gameObject.layer;

        OrientationService.SetOrientationFromSide(side, clone.transform);

        return clone;
    }

    public void DestroySticker(GameObject sticker)
    {
        Destroy(sticker);
    }
}
