using UnityEngine;

public enum CharacterType
{
    Commando,
    Heavy,
    Scout
}

public class Character : MonoBehaviour
{
    public float CurrentHealth;
    public float MoveSpeed;
    public CharacterType CurrentCharacterType;
    public SkinnedMeshRenderer CharacterMesh;

    public WeaponData CurrentWeaponData;
    public GameObject CurrentWeaponInstance;
    

    public Transform WeaponAnchor;

}
