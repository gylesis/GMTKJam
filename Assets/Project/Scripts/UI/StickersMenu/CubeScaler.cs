using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScaler : MonoBehaviour
{
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _sensitivity;
    private Vector3 _initScale;

    private void Start()
    {
        _initScale = transform.localScale;
    }

    private void Update()
    {
        var scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Clamp(transform.localScale.x + scrollValue * _sensitivity, _minScale, _maxScale),
                Mathf.Clamp(transform.localScale.y + scrollValue * _sensitivity, _minScale, _maxScale),
                Mathf.Clamp(transform.localScale.z + scrollValue * _sensitivity, _minScale, _maxScale));
        }
    }
}
