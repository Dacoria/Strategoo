using UnityEngine;

public abstract class Piece : BaseEventCallback
{
    private GameObject modelGo;
    public GameObject GameObject => gameObject;

    public abstract PieceType PieceType { get; }

    private new void Awake()
    {
        modelGo = transform.GetChild(0).gameObject; // aanname voor nu: Child van enemy is altijd Model!
    }
    private void Start()
    {
        Id = MonoHelper.instance.GenerateNewId();
        SetCurrentHexTile(GetComponentInParent<Hex>());
    }

    public int Id;
    public Hex CurrentHexTile { get; private set; }
    public bool IsAlive => true;        
    public void SetCurrentHexTile(Hex hex) => CurrentHexTile = hex;

    public void SetVisible(bool isVisible) => modelGo.SetActive(isVisible);
}
