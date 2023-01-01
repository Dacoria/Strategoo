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
    [ComponentInject] private Renderer renderer;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    protected override void SlowUpdate()
    {
        if(!piece.IsAlive)
        {
            gameObject.SetActive(false);
            return;
        }

        if(!piece.IsKnown())
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        transform.GetChild(0).gameObject.SetActive(true);

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
