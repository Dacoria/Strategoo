using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplayScript : MonoBehaviour
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;

    [SerializeField] public AbilityType AbilityType;
    private Vector3Int hexId;

    private void Awake()
    {
        this.ComponentInject();
        SetAbility(AbilityType.Movement, new Vector3Int());
    }

    public void SetAbility(AbilityType abilityType, Vector3Int hexId)
    {
        AbilityType = abilityType;
        this.hexId = hexId;
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityType.ToString());
    }

    public void OnClick()
    {
        if (Settings.UserInterfaceIsLocked)
        {
            return;
        }

        // TODO wijkt af per unit/abil combi! (wss abil)

        List<Vector3Int> hexesToSelect;
        var hexOptionsType = AbilityType.GetProperties().HexAbilityOptionType;

        if (hexOptionsType == HexAbilityOptionType.DirectNeighbours)
        {
            hexesToSelect = HexGrid.instance.GetNeighboursFor(hexId);
        }
        else if (hexOptionsType == HexAbilityOptionType.NeighboursInLine)
        {
            hexesToSelect = HexGrid.instance.GetNeighboursFor(hexId, range: 100, onlyMoveInOneDirection: true, stopBeforePieceOnTile: true);
        }
        else
        {
            throw new System.Exception("");
        }
        
        var hexPieceOwner = hexId.GetPiece().Owner;

        var hexesResult = hexesToSelect.Where(x => !x.HasPiece() || x.GetPiece().Owner != hexPieceOwner).ToList();
        AE.PieceAbilitySelected?.Invoke(hexId, AbilityType, hexesResult);
    }
}