using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameFase : BaseEventCallback
{
    public GameStatus GamestatusToDestroy;

    protected override void OnNewGameStatus(GameStatus newGameStatus)
    {
        if (GamestatusToDestroy == newGameStatus)
        {
            Destroy(gameObject);
        }
    }
}
