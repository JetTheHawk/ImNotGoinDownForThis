using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGlobalCharacterData", menuName = "Combat System/GlobalCharacterData")]
public class GlobalCharacterData : ScriptableObject
{
    public List<string> PossibleNames;

    public List<Mesh> CharacterMeshes;
}
