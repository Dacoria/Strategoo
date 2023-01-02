using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class OptionsForPieceDisplayHandler : BaseEventCallback
{
    private List<AbilityDisplayScript> ActiveAbilityButtons = new List<AbilityDisplayScript>();
    private SwapDisplayScript ActiveSwapButton;
    //private AbilityDisplayScript AbilityButtonPrefab;
    private GameObject OptionsContainer;

    [ComponentInject] private CanvasGroup canvasGroup;

    public AbilityDisplayScript AbilityButtonPrefab;
    public SwapDisplayScript SwapButtonPrefab;

    private void Start()
    {
        //var abilityButtonPrefabGo = Rsc.GoGuiMap.GetEnumerator("AbilityButtonPrefab");
        //AbilityButtonPrefab = abilityButtonPrefabGo.GetComponent<AbilityDisplayScript>();

        OptionsContainer = transform.GetChild(0).gameObject;
    }

    protected override void OnHexDeselected()
    {
        if(timeUnitSelected.EnoughTimeForNewEvent())
        {
            RemoveAllActiveOptions();
        }
    }

    private DateTime? timeUnitSelected;

    protected override void OnNewHexSelected(Vector3Int hexId)
    {
        timeUnitSelected = DateTime.Now;
        RemoveAllActiveOptions();

        var hex = hexId.GetHex();
        if (hex.HasPiece())
        {
            ShowOptionsForPiece(hex);
        }
    }

    private void ShowOptionsForPiece(Hex hex)
    {
        canvasGroup.alpha = 0;

        var piece = hex.GetPiece();

        if(GameHandler.instance.GameStatus == GameStatus.UnitPlacement)
        {
            LoadSwapOptionsForPiece(hex, piece);
        }
        else if(GameHandler.instance.GameStatus == GameStatus.GameFase)
        {
            LoadAbilityOptionsForPiece(hex, piece);
        }        

        MonoHelper.instance.FadeIn(canvasGroup, 0.45f);
    }

    private void LoadSwapOptionsForPiece(Hex hex, Piece piece)
    {
        var swapButton = Instantiate(SwapButtonPrefab, OptionsContainer.transform);
        swapButton.SetHexForSwap(hex.HexCoordinates);
        ActiveSwapButton = swapButton;
    }

    private void LoadAbilityOptionsForPiece(Hex hex, Piece piece)
    {
        var abilities = piece.GetAbilities();

        foreach (var ability in abilities)
        {
            var abilityButton = Instantiate(AbilityButtonPrefab, OptionsContainer.transform);
            abilityButton.SetAbility(ability, hex.HexCoordinates);
            ActiveAbilityButtons.Add(abilityButton);
        }
    }

    private void RemoveAllActiveOptions()
    {
        foreach (var ability in ActiveAbilityButtons)
        {
            Destroy(ability.gameObject);
        }
        ActiveAbilityButtons.Clear();

        if(ActiveSwapButton != null)
        {
            Destroy(ActiveSwapButton.gameObject);
        }
    }   
}