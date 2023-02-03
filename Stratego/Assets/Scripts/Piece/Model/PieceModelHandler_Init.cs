using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class PieceModelHandler : BaseEventCallback
{   

    private bool initSuccesfull;
    private List<ParticleSystem> PlayParticleEffects;

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
            PlayParticleEffects = GetComponentsInChildren<ParticleSystem>().ToList();
            initSuccesfull = true;
            StartCoroutine(UpdateColors(0f));
        }
    }
}