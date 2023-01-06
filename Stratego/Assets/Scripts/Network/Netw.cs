using System.Collections.Generic;
using System.Linq;
public static class Netw
{
    public static PlayerScript MyPlayer() => NetworkHelper.instance.GetMyPlayer();
    public static PlayerScript CurrPlayer() => GameHandler.instance.GetCurrentPlayer();
    public static List<PlayerScript> PlayersOnMyNetwork(bool? isAi = null) => NetworkHelper.instance.GetMyPlayers(isAi: isAi);
    public static bool IsOnMyNetwork(this PlayerScript player) => PlayersOnMyNetwork().Any(x => x == player);
    public static bool GameHasAiPlayer() => NetworkHelper.instance.GetMyPlayers(isAi: true).Any();
    public static bool HasPiecesOnGrid(this PlayerScript player) => PieceManager.instance.GetPieces(playerOwner: player).Count() > 0;

}