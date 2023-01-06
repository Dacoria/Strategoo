using System.Collections;
using UnityEngine;

public partial class GameHandler : BaseEventCallback
{
    public GameStatus GameStatus;

    protected override void OnGridLoaded()
    {
        StartCoroutine(SetupPieces(1.0f));
    }

    private IEnumerator SetupPieces(float waitTimeInSeconds)
    {
        yield return Wait4Seconds.Get(waitTimeInSeconds);

        PieceManager.instance.CreateNewLevelSetup(randomizePieces: true);
        GameStatus = GameStatus.UnitPlacement;
    }
}