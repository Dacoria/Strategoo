using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplayScript : MonoBehaviour
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;

    [SerializeField] public AbilityType AbilityType;
    private Vector3Int hexId;

    private void Awake()
    {
        this.ComponentInject();
        SetAbility(AbilityType.Movement, new Vector3Int());
    }

    public void SetAbility(AbilityType abilityType, Vector3Int hexId)
    {
        AbilityType = abilityType;
        this.hexId = hexId;
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityType.ToString());
    }

    public void OnClick()
    {
        if (Settings.UserInterfaceIsLocked)
        {
            return;
        }        

        if(!AbilityType.GetProperties().NeedsTileTarget)
        {
            UiActionSelection.instance.ClearHexSelection();
            Debug.Log("NO NeedsTileTarget --> TODO ACTION");
            // TODO ACTION?!
        }
        else
        {
            // bevestig tile om te targeten

            // TODO wijkt af per unit/abil combi! (wss abil)
            var hexesToSelect = HexGrid.instance.GetNeighboursFor(hexId);
            UiActionSelection.instance.PieceSelected?.SetAbilitySelected(AbilityType, hexesToSelect);
        }
    }
}