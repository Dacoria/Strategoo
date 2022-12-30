using Chibi.Free;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructions : HexaEventCallback
{
    public bool ShowInstructionOnGridLoad = true;

    protected override void OnGridLoaded()
    {
        if (ShowInstructionOnGridLoad)
        {
            //ShowIntroDialog();
        }
    }

    public void OnButtonClick()
    {
        ShowIntroDialog();
    }

    private void ShowIntroDialog()
    {
        var dialog = FindObjectOfType<Dialog>();
        var ok = new Dialog.ActionButton("OK", () =>
        {
            //Debug.Log("click ok");
        },
        new Color(72f / 255, 173f / 255, 211f / 255), colorButtonText: Color.white);
        Dialog.ActionButton[] buttons = { ok };
        dialog.ShowDialog("Instructions",
            @"
Goal: WIN
            "
            , buttons);
    }
}
