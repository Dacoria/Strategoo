using System.Linq;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[SelectionBase]
public class Hex : BaseEventCallback
{
    [ComponentInject] private HexCoordinates hexCoordinates;
    [ComponentInject] private HighlightHexScript highlightMove;

    private FogOnHex fogOnHex;

    public HighlightColorType? GetHighlight() => highlightMove.CurrentColorHighlight;

    public Vector3Int HexCoordinates => hexCoordinates.OffSetCoordinates;

    public HexSurfaceType HexSurfaceType;

    public PieceType PieceType;
    public int UnitValue;
    public HexObjectOnTileType HexObjectOnTileType; // voor debug + setting purposes


    public Vector3 OrigPosition;

    private HexSurfaceScript hexSurfaceScript;

    public bool FogIsActive() => fogOnHex?.FogIsActive() == true;

    private HexSurfaceType initHexSurfaceType;
    private HexObjectOnTileType initHexObjectOnTileType;

    new void Awake()
    {
        base.Awake();
        this.hexSurfaceScript = gameObject.AddComponent<HexSurfaceScript>();

        initHexSurfaceType = HexSurfaceType;
        initHexObjectOnTileType = HexObjectOnTileType;
        OrigPosition = this.transform.position;
    }

    private IEnumerator Start()
    {
        while (fogOnHex == null)
        {
            yield return Wait4Seconds.Get(0.2f);
            fogOnHex = GetComponentInChildren<FogOnHex>();
        }
    }


    public void EnableHighlight(HighlightColorType type) => highlightMove.CurrentColorHighlight = type;
    public void DisableHighlight() => highlightMove.CurrentColorHighlight = HighlightColorType.None;
    public void DisableHighlight(HighlightColorType type)
    {
        if (highlightMove.CurrentColorHighlight == type)
        {
            highlightMove.CurrentColorHighlight = HighlightColorType.None;
        }
    }

    public void SetFogOnHex(bool fogEnabled)
    {
        fogOnHex.SetFog(fogEnabled);

        ChangeHexSurfaceType(HexSurfaceType, alsoChangeType: false);
    }

    public bool IsObstacle() => HexSurfaceType.IsObstacle();
    public void ChangeHexSurfaceType(HexSurfaceType changeToType, bool alsoChangeType = true)
    {
        hexSurfaceScript.HexSurfaceTypeChanged(changeToType);

        if (alsoChangeType)
        {
            HexSurfaceType = changeToType;
        }
    }
    
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {       
        if (HexSurfaceType != initHexSurfaceType)
        {
            ChangeHexSurfaceType(initHexSurfaceType);
        }
    }
}
