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

    public void SetSpriteDirectionOnImage(Image directionImage, AbilityType abilityType, Vector3Int from, Vector3Int to)
    {
        var targetIsRelativeToPlayer = abilityType.GetTargetHexIsRelativeToPlayer();
        if (targetIsRelativeToPlayer)
        {
            var directionsToLocation = from.DeriveDirections(to);
            SetSpriteDirectionOnImage(directionImage, directionsToLocation.First(), Mathf.Min(2, directionsToLocation.Count));
        }
    }

    private void SetSpriteDirectionOnImage(Image directionImage, DirectionType direction, int spriteDirectionRange)
    {
        switch (direction)
        {
            case DirectionType.West:
            case DirectionType.East:
                directionImage.sprite = Rsc.SpriteMap.Get("ArrowUp" + spriteDirectionRange);
                break;
            case DirectionType.NorthEast:
            case DirectionType.NorthWest:
            case DirectionType.SouthEast:
            case DirectionType.SouthWest:
                directionImage.sprite = Rsc.SpriteMap.Get("ArrowRightUpCorner" + spriteDirectionRange);
                break;
            default:
                throw new System.Exception("MonoHelper -> SetSpriteDirectionOnImage -> Exception");
        }

        switch (direction)
        {                
            case DirectionType.West:
            case DirectionType.NorthWest:
                directionImage.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case DirectionType.East:
            case DirectionType.SouthEast:
                directionImage.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case DirectionType.NorthEast:
                directionImage.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;              
            case DirectionType.SouthWest:
                directionImage.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            default:
                throw new System.Exception("MonoHelper -> SetSpriteDirectionOnImage -> Exception");
        }
        
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

    public void DestroyGoAfterXSeconds(GameObject go, float seconds) => StartCoroutine(CR_DestroyGoAfterXSeconds(go, seconds));

    private IEnumerator CR_DestroyGoAfterXSeconds(GameObject go, float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        Destroy(go);
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
    

    public Vector3Int GetClosestFreeNeighbour(Vector3Int target1, Vector3Int referenceTarget2)
    {
        // deterministisch!
        var neighbourClosest = HexGrid.instance.GetNeighboursFor(target1, range: 3, withUnitOnTile: false)
            .OrderBy(neighbourTile => Vector3Int.Distance(target1, neighbourTile))
            .ThenByDescending(neighbourTile => Vector3Int.Distance(referenceTarget2, neighbourTile))
            .ThenBy(neighbourTile => neighbourTile.x)
            .ThenBy(neighbourTile => neighbourTile.z)
            .First();

        return neighbourClosest;
    }
}