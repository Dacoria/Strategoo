using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Piece : BaseEventCallback
{
    public GameObject GameObject => gameObject;
    public PlayerScript Owner;

    public List<SkillType> Skills;

    public abstract PieceType PieceType { get; }
    public Hex CurrentHexTile { get; private set; }
    public void SetCurrentHexTile(Hex hex) => CurrentHexTile = hex;
    public bool IsAlive { get; private set; }


    private PieceModelHandler pieceModelVisibility;
    public bool IsKnown() => pieceModelVisibility.IsKnown();


    private void Start()
    {
        gameObject.AddComponent<PieceMovementHandler>();
        CurrentHexTile = GetComponentInParent<Hex>();
        IsAlive = true;
        Skills = PieceSkillManager.GetSkills(this);

        pieceModelVisibility = gameObject.AddComponent<PieceModelHandler>();
    }

    public void Die(bool isAlive)
    {
        IsAlive = false;
        pieceModelVisibility.SetToInvisible();
    }
    public GameObject GetModel() => pieceModelVisibility.GetModelGo();    
}
