using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterType", menuName = "Combat System/CharacterTypeData")]
public class CharacterTypeData : ScriptableObject
{
    public CharacterType Type;

    public float BaseHealth = 100f;
    public float MoveSpeed = 3.5f;

    public WeaponData Weapon;
}
