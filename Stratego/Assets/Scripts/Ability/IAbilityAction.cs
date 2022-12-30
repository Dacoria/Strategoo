public interface IAbilityAction
{   
    void InitAbilityAction();
    void DeselectAbility();
    AbilityType AbilityType { get; }
}