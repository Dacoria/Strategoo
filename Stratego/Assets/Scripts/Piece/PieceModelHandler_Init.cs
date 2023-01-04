using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public partial class PieceModelHandler : BaseEventCallback
{   

    private bool initSuccesfull;

    private void Update()
    {
        if (!initSuccesfull)
        {
            TryInit();
        }
    }

    private void TryInit()
    {
        if (piece.Owner?.Index == 1 || piece.Owner?.Index == 2)
        {
            UpdateModelViewAndRotation();
            initSuccesfull = true;
            StartCoroutine(UpdateColors(0f));
        }
    }
}