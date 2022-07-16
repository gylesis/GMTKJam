using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._3D.Selecting;
using UnityEngine;

public class MousePositionRayProvider : IRayProvider
{
    [SerializeField] private Camera camera;
    public Ray CreateRay()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = camera.nearClipPlane;
        return camera.ScreenPointToRay(screenPosition);
    }
}
