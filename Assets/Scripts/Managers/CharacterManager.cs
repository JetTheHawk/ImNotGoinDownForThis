using System.Collections.Generic;
using UnityEngine;

//This class is for setup,spawning and managing the combat characters
public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CharacterPrefab;



    //Spawned and Living characters
    public List<Character> ActiveCharacters = new List<Character>();

    [Header("Spawn Settings")]
    //expose this to ui slider on main screen for Bonus
    public int NumberOfCharacters = 10;

    [SerializeField]
    private float groundHeight = 0f;
    [SerializeField]
    private float spawnRadius = 10f;



    //DATA

    public GlobalCharacterData globalData;





    // Random options
    // names
    // Meshes
    // weapon
    // 

    /// <summary>
    /// Begin spawning on match start, add slight delay between spawns to smooth loading
    /// clear character list just incase 
    /// loop through the number of characters
    /// each should get
    /// random spawn position on the navmesh
    /// random name
    /// random weapon
    /// 
    /// </summary>


    private void OnEnable()
    {
        GameManager.Instance.OnMatchStart += MatchStarted;
    }

    private void OnDisable()
    {
        
    }

    void MatchStarted()
    {

    }
}
