using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGlobalCharacterData", menuName = "Combat System/GlobalCharacterData")]
public class GlobalCharacterData : ScriptableObject
{
    public List<string> PossibleNames;
    public List<Mesh> CharacterMeshes;
    public List<WeaponData> WeaponDatas;
    public List<BulletData> BulletDatas;

    public float BaseHealth = 100f;
    public float MoveSpeed = 3.5f;
    public float CharacterSightRange = 100f;
}
