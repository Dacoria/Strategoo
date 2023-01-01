using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillPlayerColorOnTile : MonoBehaviour
{
    /**
    [ComponentInject] private Tile tile;
    [ComponentInject] private new Renderer renderer;

    private Color initialColor;

    private void Awake()
    {
        this.ComponentInject();
        initialColor = renderer.material.color;
    }

    void Update()
    {
        if (tile.HasUnitOnTile && tile.Unit.Player != null)
        {
            renderer.material.color = tile.Unit.Player.Color;
        }
        else if(!tile.HasUnitOnTile && renderer.material.color != initialColor)
        {
            renderer.material.color = initialColor;
        }
    }
    */
}
