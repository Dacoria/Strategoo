using System.Collections.Generic;

public class GridHighlight : BaseEventCallback
{
    protected override void OnNewPlayerTurn(PlayerScript playersTurn) => ClearAllHighlightsOnGrid();
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currPlayer) => ClearAllHighlightsOnGrid();

    private void ClearAllHighlightsOnGrid()
    {
        var allTiles = HexGrid.instance.GetAllHexes();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlight();
        }
    }
}