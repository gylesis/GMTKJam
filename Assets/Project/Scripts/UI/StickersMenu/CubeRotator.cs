using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private AnimationCurve _inertiaCurve;
    [SerializeField] private float _inertiaTime;

    private IEnumerator _inertiaCoroutine;
    private float _lastX;
    private float _lastY;

    private bool _isDragging;

    public void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            _isDragging = true;
        }
    }

    private void Update()
    {
        if (_isDragging)
        {
            HandleDrag();
            if (!Input.GetMouseButton(1))
            {
                _isDragging = false;
                OnEndDrag();
            }
        }
           
    }

    private void HandleDrag()
    {
        _lastX = 0;
        _lastY = 0;
        if (_inertiaCoroutine != null)
        {
            StopCoroutine(_inertiaCoroutine);
            _inertiaCoroutine = null;
        }

        float rotationX = Input.GetAxis("Mouse X") * _rotationSpeed * Mathf.Deg2Rad;
        float rotationY = Input.GetAxis("Mouse Y") * _rotationSpeed * Mathf.Deg2Rad;

        _lastX = rotationX;
        _lastY = rotationY;

        Rotate(rotationX, rotationY);
    }

    private void Rotate(float rotationX, float rotationY)
    {
        Vector3 right = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 up = Vector3.Cross(transform.position - camera.transform.position, right);

        transform.rotation = Quaternion.AngleAxis(-rotationX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotationY, right) * transform.rotation;
    }

    public void OnEndDrag()
    {
        _inertiaCoroutine = InertiaCoroutine();
        StartCoroutine(_inertiaCoroutine);
    }

    private IEnumerator InertiaCoroutine()
    {
        var normalizedCurve = CurveOperations.NormalizeCurve(_inertiaCurve);

        float t = 0;
        while (t < 1)
        {
            float time = t;
            float rotationX = normalizedCurve.Evaluate(time) * _lastX;
            float rotationY = normalizedCurve.Evaluate(time) * _lastY;

            Rotate(rotationX, rotationY);

            t += Time.deltaTime / _inertiaTime;
            yield return null;
        }
    }
}
