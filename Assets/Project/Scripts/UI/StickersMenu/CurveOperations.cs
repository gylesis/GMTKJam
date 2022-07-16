using UnityEngine;

public class CurveOperations
{
    public static AnimationCurve NormalizeCurve(AnimationCurve initCurve)
    {
        var curve = new AnimationCurve(initCurve.keys);
        //curve = SetStartToZero(curve);


        float horizontalRatio = 1 / curve.keys[curve.length - 1].time;
        for (int i = 0; i < curve.keys.Length; i++)
        {
            curve.keys[i].time *= horizontalRatio;
        }

        float verticalRatio = GetVerticalRatio(curve);
        for (int i = 0; i < curve.keys.Length; i++)
        {
            curve.keys[i].value *= verticalRatio;
            curve.keys[i].inTangent /= horizontalRatio;
            curve.keys[i].outTangent /= horizontalRatio;
        }

       
        var normalized = new AnimationCurve(curve.keys);
        
        
        return normalized;
    }

    public static float GetVerticalRatio(AnimationCurve curve)
    {
        float maxValue = GetMaxValue(curve);
        return 1 / maxValue;
    }

    public static float GetMaxValue(AnimationCurve curve)
    {
        Keyframe[] keys = curve.keys;

        float duration = keys[keys.Length - 1].time;
        float maxValue = Mathf.NegativeInfinity;

        float sliceFrequency = 0.01f;
        sliceFrequency *= duration;

        for (float i = 0; i < duration; i += sliceFrequency)
        {
            float value = curve.Evaluate(i);
            if (value > maxValue) maxValue = value;
        }
        return maxValue;
    }

    public static AnimationCurve SetStartToZero(AnimationCurve curve)
    {
        Keyframe[] keys = curve.keys;

        if (keys[0].value != 0)
        {
            float verticalOffset = -keys[0].value;

            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].value += verticalOffset;
            }
        }

        if (keys[0].time != 0)
        {
            float horizontalOffset = -keys[0].time;

            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].time += horizontalOffset;
            }
        }

        return new AnimationCurve(keys);
    }
}