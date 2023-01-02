using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

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
        StartCoroutine(UpdateColors());
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
            UpdatePieceKnown();
            initSuccesfull = true;
            UpdateColors();
        }
    }


    private void UpdatePieceKnown()
    {
        if (piece.IsAlive)
        {
            if (piece.Owner == null || piece.Owner == NetworkHelper.instance.GetMyPlayer())
            {
                MakePieceModelKnown();
            }
            else
            {
                MakePieceModelUnknown();
            }
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

    protected override void OnNewHexSelected(Vector3Int hexSelected) => UpdatePieceKnown();
    protected override void OnUpdatePlayerIndex(int playerId, int playerIndex) => StartCoroutine(UpdateColors());

    private IEnumerator UpdateColors()
    {
        yield return Wait4Seconds.Get(0.05f);

        var defaultColor = MonoHelper.instance.P0_Color;
        var pieceColor = MonoHelper.instance.GetPlayerColorMaterial(piece?.Owner);
        unknownPieceGo.GetComponentInChildren<Renderer>().material = pieceColor;

        var pieceColorModels = GetComponentsInChildren<PieceColorModel>(includeInactive: true);
        if(pieceColorModels.Any())
        {
            foreach (var pieceColorModel in pieceColorModels)
            {
                var renderers = pieceColorModel.GetComponentsInChildren<Renderer>(includeInactive: true);
                foreach (var renderer in renderers)
                {
                    renderer.material = renderer.material.color == defaultColor.color ? pieceColor : renderer.material;                                    
            
                    var intMaterials = new Material[renderer.materials.Length];
                    for (var i = 0; i < renderer.materials.Length; i ++)
                    {
                        intMaterials[i] = renderer.materials[i].color == defaultColor.color ? pieceColor : renderer.materials[i];
                    }
                    renderer.materials = intMaterials;
                }
            }
        }
    }


    protected override void OnDoPieceAbility(Piece pieceDoingAbility, Hex hexTarget, AbilityType abilType)
    {
        if(abilType.In(AbilityType.Movement, AbilityType.ScoutMove))
        {
            if(pieceDoingAbility == piece)
            {
                MakePieceModelKnown();
            }

            var pieceOnTargetHex = hexTarget.GetPiece();
            if (pieceOnTargetHex != null && pieceOnTargetHex == piece)
            {
                MakePieceModelKnown();
            }
        }
    }
}
