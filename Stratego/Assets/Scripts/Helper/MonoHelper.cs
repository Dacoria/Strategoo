using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonoHelper : MonoBehaviour
{
    public static MonoHelper instance;

    private void Awake()
    {
        instance = this;
    }

    public AnimationCurve CurveGradual;
    public AnimationCurve CurveLinear;
    public AnimationCurve CurveSlowStart;
    public AnimationCurve CurveSlowEnd;

    public Material P0_Color;
    public Material P1_Color;
    public Material P2_Color;

    public Material GetPlayerColorMaterial(PlayerScript player)
    {
        if (player?.Index == 1)
        {
            return P1_Color;
        }
        if (player?.Index == 2)
        {
            return P2_Color;
        }

        return P0_Color;
    }

    public void FadeIn(CanvasGroup canvasGroup, float aTime) => FadeTo(canvasGroup, 1, aTime);
    public void FadeOut(CanvasGroup canvasGroup, float aTime) => FadeTo(canvasGroup, 0, aTime);

    public void FadeTo(CanvasGroup canvasGroup, float aValue, float aTime)
    {
        StartCoroutine(CR_FadeTo(canvasGroup, aValue, aTime));
    }

    private IEnumerator CR_FadeTo(CanvasGroup canvasGroup, float aValue, float aTime)
    {
        float alpha = canvasGroup.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            canvasGroup.alpha = Mathf.Lerp(alpha, aValue, t);
            yield return null;
        }
        canvasGroup.alpha = aValue;
    }
}