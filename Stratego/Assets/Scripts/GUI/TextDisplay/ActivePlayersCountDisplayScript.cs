using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ActivePlayersCountDisplayScript : BaseEventCallback
{
    public TMP_Text textNetwCountPlayers;
    public TMP_Text textCountAllPlayers;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currPlayer)
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        textNetwCountPlayers.text = "Active real players: " + NetworkHelper.instance.PlayerList.Count();
        textCountAllPlayers.text = "All active players: " + NetworkHelper.instance.GetAllPlayers().Count();
    }
}