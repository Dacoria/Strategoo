using UnityEngine;
using System.Linq;
using System.Collections;

public partial class PieceModelHandler : BaseEventCallback
{   
    private IEnumerator UpdateColors(float waitTimeInSeconds = 0.05f)
    {
        yield return Wait4Seconds.Get(waitTimeInSeconds);

        var defaultColor = MonoHelper.instance.P0_Color;
        var pieceColor = MonoHelper.instance.GetPlayerColorMaterial(piece?.Owner);
        unknownPieceGo.GetComponentInChildren<Renderer>().material = pieceColor;

        var pieceColorModels = GetComponentsInChildren<PieceColorModel>(includeInactive: true);
        if (pieceColorModels.Any())
        {
            foreach (var pieceColorModel in pieceColorModels)
            {
                var renderers = pieceColorModel.GetComponentsInChildren<Renderer>(includeInactive: true);
                foreach (var renderer in renderers)
                {
                    renderer.material = renderer.material.color == defaultColor.color ? pieceColor : renderer.material;

                    var intMaterials = new Material[renderer.materials.Length];
                    for (var i = 0; i < renderer.materials.Length; i++)
                    {
                        intMaterials[i] = renderer.materials[i].color == defaultColor.color ? pieceColor : renderer.materials[i];
                    }
                    renderer.materials = intMaterials;
                }
            }
        }
    }
}