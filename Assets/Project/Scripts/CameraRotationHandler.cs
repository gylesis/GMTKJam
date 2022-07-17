using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraRotationHandler : MonoBehaviour
{
    [Inject]
    private CubeRotator _cubeRotator;
    [SerializeField] private CinemachineFreeLook _freeLook;

    void Update()
    {
        if (_cubeRotator.IsDragging)
        {
            _freeLook.enabled = false;
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                _freeLook.enabled = true;
            }
        }
    }
}
