using UnityEngine;

public abstract class Piece : BaseEventCallback
{
    private GameObject modelGo;
    public GameObject GameObject => gameObject;
    public PlayerScript Owner;

    public abstract PieceType PieceType { get; }

    private new void Awake()
    {
        modelGo = transform.GetChild(0).gameObject; // aanname voor nu: Child van enemy is altijd Model!
    }
    private void Start()
    {
        gameObject.AddComponent<PieceAttackMovementHandler>();
        CurrentHexTile = GetComponentInParent<Hex>();
        IsAlive = true;
    }

    public Hex CurrentHexTile { get; private set; }
    public void SetCurrentHexTile(Hex hex) => CurrentHexTile = hex;
    public bool IsAlive { get; private set; }
    public void Die(bool isAlive)
    {
        IsAlive = false;
        modelGo.SetActive(false);
    }
    public void SetVisible(bool isVisible) => modelGo.SetActive(isVisible);
    public GameObject GetModel() => modelGo;
}
