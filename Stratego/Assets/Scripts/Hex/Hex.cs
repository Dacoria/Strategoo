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

    public HexStructureType HexStructureType;
    public HexSurfaceType HexSurfaceType;

    public PieceType PieceType;
    [ConditionalHide("PieceType", PieceType.Unit)] public int UnitValue;
    public HexObjectOnTileType HexObjectOnTileType; // voor debug + setting purposes


    public Vector3 OrigPosition;

    private HexSurfaceScript hexSurfaceScript;
    private HexStructureScript hexStructureScript;

    public bool FogIsActive() => fogOnHex?.FogIsActive() == true;

    private HexStructureType initHexStructureType;
    private HexSurfaceType initHexSurfaceType;
    private HexObjectOnTileType initHexObjectOnTileType;

    new void Awake()
    {
        base.Awake();
        this.hexSurfaceScript = gameObject.AddComponent<HexSurfaceScript>();
        this.hexStructureScript = gameObject.AddComponent<HexStructureScript>();

        initHexStructureType = HexStructureType;
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
        ChangeHexStructureType(HexStructureType, alsoChangeType: false);
    }

    public bool IsObstacle() => HexSurfaceType.IsObstacle() || HexStructureType.IsObstacle();
    public void ChangeHexSurfaceType(HexSurfaceType changeToType, bool alsoChangeType = true)
    {
        hexSurfaceScript.HexSurfaceTypeChanged(changeToType);

        if (alsoChangeType)
        {
            HexSurfaceType = changeToType;
        }
    }

    public void ChangeHexStructureType(HexStructureType changeToType, bool alsoChangeType = true)
    {
        if (alsoChangeType)
        {
            // daadwekelijk verwijderen
            hexStructureScript.HexStructureTypeChanged(changeToType);
            HexStructureType = changeToType;
        }
        else
        {
            // alleen hiden/showen van model
            var structureGo = Utils.GetUnderlyingStructureGoFromHex(this);
            if (structureGo != null)
            {
                var structureGoModel = structureGo.GetComponentInChildren<StructureModel>(true)?.gameObject;
                structureGoModel?.SetActive(changeToType != HexStructureType.None);
            }
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        if (HexStructureType != initHexStructureType)
        {
            ChangeHexStructureType(initHexStructureType);
        }
        if (HexSurfaceType != initHexSurfaceType)
        {
            ChangeHexSurfaceType(initHexSurfaceType);
        }
    }
}
