using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayerButtonScript : MonoBehaviour
{
    private List<Button> buttons;

    private void Awake()
    {
        this.buttons = GetComponentsInChildren<Button>().ToList();        
    }

    public void OnButtonClick(bool useAi)
    {
        SpawnPlayersManager.instance.SpawnDummyPlayer(useAi);
        var newAiPlayer = NetworkHelper.instance.GetAllPlayers(isAi: true, myNetwork: true).First();
        PieceManager.instance.CreateNewLevelSetup(newAiPlayer.Index, randomizePieces: true);

        Destroy(gameObject);
    }

    private bool hasMaxAmountOfPlayers;

    private void Update()
    {
        if (!GameHandler.instance.GameStatus.In(GameStatus.NotStarted, GameStatus.UnitPlacement))
        {
            buttons.ForEach(button => button.interactable = false);
        }
        else if (!hasMaxAmountOfPlayers)
        {
            hasMaxAmountOfPlayers = NetworkHelper.instance.GetAllPlayers().Count() >= 2;
        }
        else
        {
            buttons.ForEach(button => button.interactable = false);
        }
    }
}
