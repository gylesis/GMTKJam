using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AnimationCurvesData : ScriptableObject
{
    [SerializeField]private AnimationCurve _cubeRotationCurve;
    [SerializeField] private AnimationCurve _cubeMovementCurve;
    [SerializeField] private float _cubeMovementDuration = 0.5f;

    public AnimationCurve CubeRotationCurve => _cubeRotationCurve;

    public float CubeMovementDuration => _cubeMovementDuration;

    public AnimationCurve CubeMovementCurve => _cubeMovementCurve;
}
