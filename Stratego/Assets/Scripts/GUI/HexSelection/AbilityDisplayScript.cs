using System.Collections.Generic;
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

        var abilProps = AbilityType.GetProperties();
        var hexesResult = Utils.GetHexOptionsForAbility(hexId, abilProps.HexAbilityOptionType1, abilProps.HexAbilityOption1Choices, hexId.GetPiece().Owner, includeSelf: false);
        AE.PieceAbilitySelected?.Invoke(hexId, AbilityType, hexesResult);
    }    
}