using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartReadyEndGameButtonScript : BaseEventCallback
{
    [ComponentInject] private Button button;
    [ComponentInject] private TMP_Text text;
    [ComponentInject] private CanvasGroup canvasGroup;

    public void OnButtonClick()
    {
        if (GameHandler.instance.GameStatus == GameStatus.UnitPlacement)
        {
            ReadyToStartGame();
        }
        if (GameHandler.instance.GameStatus.In(GameStatus.GameFase, GameStatus.RoundEnded))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }

    public void ReadyToStartGame()
    {
        var me = NetworkHelper.instance.GetMyPlayer();
        ReadyToStartGame(me);
    }

    public void ReadyToStartGame(PlayerScript playerScript)
    {
        NAE.PlayerReadyForGame?.Invoke(playerScript);
    }

    private bool playerIsReady;
    protected override void OnPlayerReadyForGame(PlayerScript playerThatIsReady)
    {
        var myPlayer = NetworkHelper.instance.GetMyPlayer();
        if (playerThatIsReady == myPlayer)
        {
            playerIsReady = true;
        }
    }

    private void Update()
    {
        var playerCount = NetworkHelper.instance.GetAllPlayers().Count();
        var hasAtLeastTwoPlayers = playerCount >= 2;

        var piecesCountPerPlayer = LevelSettings.LevelSetting_1_20pcs.UnitsSettings.Sum(x => x.NumberOfPieces);
        var allPiecesAreOnTheField = PieceManager.instance.GetPieces().Count == piecesCountPerPlayer * playerCount;

        canvasGroup.alpha = 0;
        if (GameHandler.instance.GameStatus == GameStatus.UnitPlacement)
        {
            text.text = "Ready";
            canvasGroup.alpha = 1;
            button.interactable = hasAtLeastTwoPlayers && allPiecesAreOnTheField && !playerIsReady;
        }
        if (GameHandler.instance.GameStatus.In(GameStatus.GameFase, GameStatus.RoundEnded))
        {
            text.text = "End Game";
            canvasGroup.alpha = 1;
            button.interactable = true;
        }
    }
}