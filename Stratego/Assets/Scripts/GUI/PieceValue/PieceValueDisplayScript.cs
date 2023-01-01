using UnityEngine;
using TMPro;

public class PieceValueDisplayScript : MonoBehaviourSlowUpdate
{
    private void Awake()
    {
        this.ComponentInject();
    }

    [ComponentInject] private Piece piece;
    [ComponentInject] private TMP_Text Text;

    protected override void SlowUpdate()
    {
        if(!piece.IsAlive)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        var value = "";
        switch(piece.PieceType)
        {
            case PieceType.Castle:
                value = "C";
                break;
            case PieceType.Trap:
                value = "T";
                break;
            case PieceType.Unit:
                value = ((Unit)piece).Value.ToString();
                break;
            default:
                throw new System.Exception("");
        }

        Text.text = value;
    }
}
