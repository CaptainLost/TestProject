using UnityEngine;

public static class UtilsEasing
{
    public static float EaseInOutQuad(float start, float end, float time)
    {
        time = Mathf.Clamp01(time);
        time = time < 0.5f
            ? 2f * time * time
            : 1f - Mathf.Pow(-2f * time + 2f, 2f) * 0.5f;

        return Mathf.Lerp(start, end, time);
    }
}