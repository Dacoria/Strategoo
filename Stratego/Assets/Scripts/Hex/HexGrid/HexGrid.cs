using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGrid : BaseEventCallback
{
    private Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();

    public static HexGrid instance;

    public FogOnHex FogPrefab;

    private HexNeighbours hexNeighbours = new HexNeighbours();
    public bool GridIsLoaded;

    private new void Awake()
    {
        base.Awake();
        instance = this;
    }

    private void Start()
    {
        var hexes = FindObjectsOfType<Hex>();        
        var hexesSorted = hexes.OrderBy(x => Vector3.Distance(x.transform.position, new Vector3(0,0,0))).ToList();

        foreach (var hex in hexesSorted)
        {
            if(hexTileDict.ContainsKey(hex.HexCoordinates))
            {
                throw new Exception(hex.gameObject.name + " + " + hexTileDict[hex.HexCoordinates].gameObject.name);
            }

            if (hex.transform.GetChild(0).childCount != 1)
            {
                throw new Exception(hex.gameObject.name + " heeft meerdere tiles" );
            }    

            hexTileDict[hex.HexCoordinates] = hex;

            Instantiate(FogPrefab, hex.transform);

            var lerp = hex.gameObject.AddComponent<LerpMovement>();
            lerp.MoveToDestination(hex.transform.position, duration: 1.5f, startPosition: hex.transform.position + new Vector3(0, -100, 0), delayedStart: hex.transform.position.x * 0.08f);
        }
    }    

    private bool HexGridLoaded;

    private void Update()
    {
        if(!HexGridLoaded)
        {
            HexGridLoaded = hexTileDict.Values.All(x => Vector3.Distance(x.OrigPosition, x.transform.position) < 0.01f);
            if(HexGridLoaded)
            {
                AE.GridLoaded?.Invoke(); 
            }
        }
    }

    public bool IsLoaded() => HexGridLoaded;

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates, int range = 1, bool excludeObstacles = true, bool? withUnitOnHex = null, bool onlyMoveInOneDirection = false, bool showOnlyFurthestRange = false, bool includeStartHex = false, bool excludeWater = false, bool stopBeforePieceOnTile = false)
    {
        return hexNeighbours.GetNeighboursFor(
            hexTileDict: hexTileDict,
            hexCoordinates: hexCoordinates,
            range: range,
            excludeObstacles: excludeObstacles,
            withPieceOnHex: withUnitOnHex,
            onlyMoveInOneDirection: onlyMoveInOneDirection,
            showOnlyFurthestRange: showOnlyFurthestRange,
            includeStartHex: includeStartHex,
            excludeWater: excludeWater,
            stopBeforePieceOnTile: stopBeforePieceOnTile
        );
    }

    // voor A*
    public float Cost(Vector3Int current, Vector3Int directNeighbor) => 1;

    public List<Hex> GetAllHexes() => hexTileDict.Values.ToList();

    public List<Hex> GetHexes(HighlightColorType type) => hexTileDict.Values.Where(x => x.GetHighlight().HasValue && x.GetHighlight().Value == type).ToList();

    public Hex GetHexAt(Vector3Int hexCoordinates)
    {
        Hex result = null;
        hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public Hex GetHexRightUpperCorner()
    {
        var hexRightUpperCornerCoordinate = hexTileDict.OrderByDescending(z => z.Key.z)
            .ThenByDescending(x => x.Key.x)
            .First().Key;

        return hexTileDict[hexRightUpperCornerCoordinate];
    }

    public void MovePieceToNewTile(Piece piece, Hex newHex)
    {
        piece.SetCurrentHexTile(newHex);
        piece.transform.SetParent(newHex.GetGoStructure().transform);
        piece.transform.position = newHex.transform.position + new Vector3(0,1,0);
    }

    public List<Hex> GetPlayerStartTiles(int playerIndex, int hexCount)
    {
        var allHexes = GetAllHexes();
        var hexRightUpper = GetHexRightUpperCorner();

        //Debug.Log("GetPlayerStartTiles " + playerIndex);

        List<Hex> result;
        if (playerIndex == 1)
        {
            result = GetBottomHalfOfHexes(allHexes, hexRightUpper, hexCount);
        }
        else if(playerIndex == 2)
        {
            result = GetUpperHalfOfHexes(allHexes, hexRightUpper, hexCount);
        }
        else
        {
            throw new Exception();
        }
        
        return result;
    }

    private static List<Hex> GetBottomHalfOfHexes(List<Hex> allTiles, Hex tileRightUpper, int hexCount)
    {
        return allTiles
            .Where(hex => !hex.HexSurfaceType.IsObstacle())
            .OrderBy(hex => hex.HexCoordinates.z)
            .Take(hexCount)
            .ToList();
    }

    private static List<Hex> GetUpperHalfOfHexes(List<Hex> allTiles, Hex tileRightUpper, int hexCount)
    {
        return allTiles
            .Where(hex => !hex.HexSurfaceType.IsObstacle())
            .OrderByDescending(hex => hex.HexCoordinates.z)
            .Take(hexCount)
        .ToList();
    }
}