using System.Collections.Generic;

public class LoadingDisplayShowerScript : BaseEventCallbackSlowUpdate
{
    private void Start()
    {
        Settings.UserInterfaceIsLocked = true;
    }

    protected override void SlowUpdate()
    {
        transform.GetChild(0).gameObject.SetActive(Settings.UserInterfaceIsLocked);
    }

    protected override void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilType, Hex hex2)
    {
        Settings.UserInterfaceIsLocked = true;
    }

    protected override void OnGridLoaded()
    {
        Settings.UserInterfaceIsLocked = false;
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {        
        Settings.UserInterfaceIsLocked = false;
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        Settings.UserInterfaceIsLocked = false;
    }
    protected override void OnEndRound(PlayerScript pWinner)
    {
        Settings.UserInterfaceIsLocked = false;
    }
}