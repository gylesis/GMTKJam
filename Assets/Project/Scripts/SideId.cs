using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideId : MonoBehaviour
{
    public int GetId()
    {
        return transform.GetSiblingIndex();
    }
}
