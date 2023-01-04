using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public partial class PieceModelHandler : BaseEventCallback
{
    private GameObject modelGo;
    private GameObject unknownPieceGo;

    [ComponentInject] private Piece piece;
    [ComponentInject(Required.OPTIONAL)] private Animator animator;

    private new void Awake()
    {
        base.Awake();
        modelGo = transform.GetChild(0).gameObject; // aanname voor nu: Child van enemy is altijd Model!
    }

    private void Start()
    {
        var unknownPrefab = PieceManager.instance.UnknownPrefab;
        unknownPieceGo = Instantiate(unknownPrefab, transform);
        MakePieceModelUnknown();
        StartCoroutine(UpdateColors());
    }

    public bool IsKnown() => modelGo.activeInHierarchy;


}