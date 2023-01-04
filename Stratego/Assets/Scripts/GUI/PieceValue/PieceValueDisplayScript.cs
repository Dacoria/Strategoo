using UnityEngine;
using TMPro;
using System.Collections;

public class PieceValueDisplayScript : BaseEventCallbackSlowUpdate
{
    [ComponentInject] private Piece piece;
    [ComponentInject] private TMP_Text Text;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }    

    protected override void SlowUpdate()
    {
        if (!piece.IsAlive)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!piece.IsKnown())
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        SetRotation();
        ShowText();
    }

    private void SetRotation()
    {
        if (Settings.RotateTowardsMyPlayer && piece.IsKnown())
        {
            var playerRotationDir = Utils.GetRotationDir();
            if (playerRotationDir.z == -1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void ShowText()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        var value = "";
        switch (piece.PieceType)
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
