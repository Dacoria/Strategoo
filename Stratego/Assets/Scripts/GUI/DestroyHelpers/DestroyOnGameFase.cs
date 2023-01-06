using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameFase : BaseEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        Destroy(gameObject);
    }    
}
