using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesForPieceDisplayHandler : BaseEventCallback
{
    private List<AbilityDisplayScript> ActiveAbilityButtons = new List<AbilityDisplayScript>();
    //private AbilityDisplayScript AbilityButtonPrefab;
    private GameObject AbilityContainer;

    [ComponentInject] private CanvasGroup canvasGroup;
 
    public AbilityDisplayScript AbilityButtonPrefab;

    private void Start()
    {
        //var abilityButtonPrefabGo = Rsc.GoGuiMap.GetEnumerator("AbilityButtonPrefab");
        //AbilityButtonPrefab = abilityButtonPrefabGo.GetComponent<AbilityDisplayScript>();

        AbilityContainer = transform.GetChild(0).gameObject;
    }

    protected override void OnHexDeselected()
    {
        if(timeUnitSelected.EnoughTimeForNewEvent())
        {
            RemoveAllActiveAbilities();
        }
    }

    private DateTime? timeUnitSelected;

    protected override void OnNewHexSelected(Vector3Int hexId)
    {
        timeUnitSelected = DateTime.Now;
        RemoveAllActiveAbilities();

        var hex = hexId.GetHex();
        if (hex.HasPiece())
        {
            ShowAbilitiesForPiece(hex);
        }
    }

    private void ShowAbilitiesForPiece(Hex hex)
    {
        canvasGroup.alpha = 0;

        var piece = hex.GetPiece();
        var abilities = piece.GetAbilities();

        foreach (var ability in abilities)
        {
            var abilityButton = Instantiate(AbilityButtonPrefab, AbilityContainer.transform);
            abilityButton.SetAbility(ability, hex.HexCoordinates);
            ActiveAbilityButtons.Add(abilityButton);
        }

        MonoHelper.instance.FadeIn(canvasGroup, 0.45f);
    }

    private void RemoveAllActiveAbilities()
    {
        foreach (var ability in ActiveAbilityButtons)
        {
            Destroy(ability.gameObject);
        }
        ActiveAbilityButtons.Clear();
    }   
}