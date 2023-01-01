using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PieceModelVisibility : BaseEventCallback
{
    private GameObject modelGo;
    private GameObject unknownPieceGo;

    [ComponentInject] private Piece piece;

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
    }

    public bool IsKnown() => modelGo.activeInHierarchy;

    private bool initSuccesfull;

    private void Update()
    {
        if(!initSuccesfull)
        {
            TryInit();
        }
    }

    private void TryInit()
    {
        var me = NetworkHelper.instance.GetMyPlayer();
        if (me != null)
        {
            if (piece.Owner == null || piece.Owner == NetworkHelper.instance.GetMyPlayer())
            {
                MakePieceModelKnown();
            }
            else
            {
                MakePieceModelUnknown();
            }

            initSuccesfull = true;
        }
    }

    private void MakePieceModelKnown()
    {
        modelGo.SetActive(true);
        unknownPieceGo?.SetActive(false);
    }

    private void MakePieceModelUnknown()
    {
        modelGo.SetActive(false);
        unknownPieceGo?.SetActive(true);
    }

    public GameObject GetModelGo() => modelGo;    

    public void SetToInvisible()
    {
        modelGo.SetActive(false);
        unknownPieceGo?.SetActive(false);
    }
}
