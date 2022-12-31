using System.Collections.Generic;

public class DestroyOnGameStart : BaseEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        Destroy(gameObject);
    }
}
