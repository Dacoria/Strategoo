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


    private PieceModelVisibility pieceVisibility;
    public bool IsKnown() => pieceVisibility.IsKnown();


    private void Start()
    {
        gameObject.AddComponent<PieceMovementHandler>();
        CurrentHexTile = GetComponentInParent<Hex>();
        IsAlive = true;
        Skills = PieceSkillManager.GetSkills(this);

        pieceVisibility = gameObject.AddComponent<PieceModelVisibility>();
    }

    public void Die(bool isAlive)
    {
        IsAlive = false;
        pieceVisibility.SetToInvisible();
    }
    public GameObject GetModel() => pieceVisibility.GetModelGo();  
}
