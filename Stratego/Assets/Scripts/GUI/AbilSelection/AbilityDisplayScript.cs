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
            UiActionSelection.instance.ClearTileSelection();
            // TODO ACTION?!
        }
        else
        {
            // bevestig tile om te targeten


            // TODO wijkt af per unit/abil combi! (wss abil)
            var hexesToSelect = HexGrid.instance.GetNeighboursFor(hexId);


            UiActionSelection.instance.UnitSelected.SetAbilitySelected(AbilityType, hexesToSelect);
        }
    }
}