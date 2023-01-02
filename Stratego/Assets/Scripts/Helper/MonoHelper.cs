using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public HighlightOneTileDisplayScript GetHighlightOneTileSelection(GameObject gameObject)
    {
        var highlightScripts = gameObject.GetComponents<HighlightOneTileDisplayScript>().ToList();
        if (!highlightScripts.Any())
        {
            return gameObject.AddComponent<HighlightOneTileDisplayScript>();
        }
        for (int i = 1; i < highlightScripts.Count(); i++)
        {
            Destroy(highlightScripts[i]);
        }

        highlightScripts[0].Reset();
        return highlightScripts[0];
    }

    public bool FindTile(Vector3 mousePosition, out List<Hex> result)
    {
        var layermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var hits = Physics.RaycastAll(ray, layermask);
        if (hits.Length > 0)
        {
            result = hits
                .Where(x => x.collider.gameObject.GetComponent<Hex>() != null)
                .Select(x => x.collider.gameObject.GetComponent<Hex>())
                .ToList();

            return result.Any();
        }

        result = null;
        return false;
    }

    public int GenerateNewId() => Random.Range(int.MinValue, int.MaxValue);

    public void DestroyChildrenOfGo(GameObject go)
    {
        for (int i = go.transform.childCount - 1; i >= 0; i--)
        {
            var child = go.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void DestroyInXSeconds(float seconds, GameObject go)
    {
        StartCoroutine(CR_DestroyInXSeconds(seconds, go));
    }

    private IEnumerator CR_DestroyInXSeconds(float seconds, GameObject go)
    {
        yield return Wait4Seconds.Get(seconds);
        Destroy(go);
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