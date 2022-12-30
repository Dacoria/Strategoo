using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class SelectMovementAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Movement;

    private bool movementAbilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        movementAbilIsActive = false;
    }

    public void InitAbilityAction()
    {
        NeighbourHexTileSelectionManager.instance.HighlightNeighbourTilesPlayer(excludeObstacles: false, onlyMoveInOneDirection: false);
        movementAbilIsActive = true;
    }

    private void Update()
    {
        if(!movementAbilIsActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {            
            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(Hex hexSelected)
    {
        //Netw.CurrPlayer().Ability(hexSelected, AbilityType);
    }
}