using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayerButtonScript : BaseEventCallback
{
    private List<Button> buttons;
    [ComponentInject] private CanvasGroup canvasGroup;

    private new void Awake()
    {
        base.Awake();
        canvasGroup.alpha = 0;
        this.buttons = GetComponentsInChildren<Button>().ToList();        
    }

    public void OnButtonClick(bool useAi)
    {
        SpawnPlayersManager.instance.SpawnDummyPlayer(useAi);
        Destroy(gameObject);
    }

    private bool gridIsLoaded;
    protected override void OnGridLoaded() => gridIsLoaded = true;

    private bool hasMaxAmountOfPlayers;

    private void Update()
    {
        if(canvasGroup.alpha == 0 && gridIsLoaded && Netw.MyPlayer().HasPiecesOnGrid())
        {
            canvasGroup.alpha = 1;
        }

        if(canvasGroup.alpha == 0)
        {
            return;
        }

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
            Destroy(gameObject);
        }
    }
}
