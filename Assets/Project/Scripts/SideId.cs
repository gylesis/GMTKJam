using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using UnityEngine;

public class SideId : MonoBehaviour
{
    [SerializeField] private CubeSide side;
    public CubeSide GetId()
    {
        return side;
    }
}
