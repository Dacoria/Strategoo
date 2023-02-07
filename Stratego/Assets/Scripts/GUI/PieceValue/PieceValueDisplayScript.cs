using UnityEngine;
using TMPro;
using System.Collections;

public class PieceValueDisplayScript : BaseEventCallbackSlowUpdate
{
    [ComponentInject] private Piece piece;
    [SerializeField] private TMP_Text Text;

    [SerializeField] private GameObject ValueDisplay;
    [SerializeField] private GameObject SkillOptions;
    

    private void Start()
    {
        ValueDisplay.SetActive(false);
    }    

    protected override void SlowUpdate()
    {
        if (!piece.IsAlive)
        {
            gameObject.SetActive(false);
            SkillOptions.SetActive(false);
            return;
        }

        if (!piece.IsKnown())
        {
            ValueDisplay.SetActive(false);
            SkillOptions.SetActive(false);
            return;
        }

        ValueDisplay.SetActive(true);
        SkillOptions.SetActive(true);

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
        string value;
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
