using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : HexaEventCallback
{
    [ComponentInject] private ButtonUpdater buttonUpdater;
        
    private void Start()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateAllAbilities(setToUnselected: true);
        }
    }

    protected override void OnEnemyFaseStarted()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
    }

    protected override void OnEndRound(bool reachedMiddle, PlayerScript pWinner)
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
    }

    protected override void OnEndGame()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
    }

    protected override void OnUnitMovingFinished(IUnit unit, Hex hex)
    {
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        StartCoroutine(UpdatePlayerAbilityButtons());
    }

    private IEnumerator UpdatePlayerAbilityButtons()
    {
        yield return Wait4Seconds.Get(0.1f); // wijziging moet verwerkt worden....
        if (Netw.CurrPlayer().IsOnMyNetwork())
        {
            UpdateAllAbilities(setToUnselected: true);
        }       
    }

    private void UpdateAllAbilities(bool setToUnselected, bool? interactable = null)
    {
        foreach (AbilityType abilityType in Utils.GetValues<AbilityType>())
        {
            if (setToUnselected)
            {
                buttonUpdater.SetToUnselected(abilityType);
            }           
            if (interactable.HasValue)
            {
                buttonUpdater.SetAbilityInteractable(abilityType, interactable.Value);
                if (!interactable.Value)
                {
                    continue;
                }
            }
            if (Netw.CurrPlayer() == null)
            {
                buttonUpdater.SetAbilityInteractable(abilityType, false);
            }
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript currentPlayer) => StartCoroutine(CheckEnableButtonsNewTurn(currentPlayer));
    protected override void OnNewPlayerTurn(PlayerScript currentPlayer) => StartCoroutine(CheckEnableButtonsNewTurn(currentPlayer));

    private IEnumerator CheckEnableButtonsNewTurn(PlayerScript currentPlayer)
    {
        yield return Wait4Seconds.Get(0.1f);// wacht tot wijziging is verwerkt
        if(!currentPlayer.IsOnMyNetwork()) { yield break; } // AI beurt verandert niet bij andere spelers

        if (GameHandler.instance.GameStatus == GameStatus.PlayerFase) 
        {
            UpdateAllAbilities(interactable: true, setToUnselected: true);
        }
    }
}