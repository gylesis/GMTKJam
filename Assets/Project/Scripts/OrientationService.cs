using System;
using Project.Scripts;
using UnityEngine;

public class OrientationService
{
    public static void SetOrientationFromSide(CubeSide side, Transform clone)
    {
        switch (side)
        {
            case CubeSide.X:
                clone.transform.localEulerAngles = new Vector3(0, 90, -90);
                break;
            case CubeSide.NegX:
                clone.transform.localEulerAngles = new Vector3(0, -90, 90);
                break;
            case CubeSide.Y:
                clone.transform.localEulerAngles = new Vector3(-90, 0, 0);
                break;
            case CubeSide.NegY:
                clone.transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case CubeSide.Z:
                clone.transform.localEulerAngles = new Vector3(180, 0, 0);
                break;
            case CubeSide.NegZ:
                clone.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
    }

    
}