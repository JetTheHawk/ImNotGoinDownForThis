using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is for setup,spawning and managing the combat characters
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField]
    private GameObject characterPrefab;



    //Spawned and Living characters
    public List<Character> ActiveCharacters = new List<Character>();

    [Header("Spawn Settings")]
    //expose this to ui slider on main screen for Bonus
    public int NumberOfCharacters = 10;
    public Transform SpawnOrigin = null;
    public float DelayBetweenSpawns = 0.5f;

    [SerializeField]
    private float groundHeight = 0f;
    [SerializeField]
    private float spawnRadius = 10f;



    //DATA

    public GlobalCharacterData GlobalData;





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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnMatchStart += MatchStarted;
    }

    private void Start()
    {
        MatchStarted();
    }
    void MatchStarted()
    {
        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        //clear active characters if coming from restart


        /// <summary>
        /// for number of characters
        /// 
        /// determin random spawn position
        /// 
        /// instantite character prefab
        /// 
        /// assign random data to character
        /// 
        /// Name,Mesh,Weapon
        /// 
        /// Add to activeCharacters
        /// </summary>
        /// 

        for (int i = 0; i < NumberOfCharacters; i++)
        {
            // Determine a random spawn position
            Vector2 circle2D = Random.insideUnitCircle * spawnRadius;
            
            Vector3 spawnPos = new Vector3(
                circle2D.x + SpawnOrigin.position.x,
                groundHeight,
                circle2D.y + SpawnOrigin.position.z
            );

            // Instantiate the character prefab
            GameObject charGO = Instantiate(characterPrefab, spawnPos, Quaternion.identity);
            Character characterScript = charGO.GetComponent<Character>();

            // Assign random data
            if (characterScript != null)
            {

                //Random Name
                string randomName = GetRandomName();
                characterScript.DisplayName = randomName;

                //Random Mesh
                Mesh tempRandomMesh = GetRandomCharacterMesh();
                if(tempRandomMesh != null)
                {
                    characterScript.SetCharacterMesh(tempRandomMesh);
                }

                //Random Weapon
                WeaponData tempWeaponData = GetRandomWeaponData();
                if (tempWeaponData != null)
                {
                    characterScript.SetCharacterWeaponData(tempWeaponData);
                }

                ActiveCharacters.Add(characterScript);
            }
            yield return new WaitForSeconds(DelayBetweenSpawns);
        }
    }

    private string GetRandomName()
    {
        if (GlobalData.PossibleNames == null || GlobalData.PossibleNames.Count == 0)
        {
            Debug.LogWarning("Global possible names list is null. default to chr plus random number");
            return "Char_" + Random.Range(1000, 9999);
        }
        return GlobalData.PossibleNames[Random.Range(0, GlobalData.PossibleNames.Count)];
    }

    private Mesh GetRandomCharacterMesh()
    {
        if (GlobalData.CharacterMeshes == null || GlobalData.CharacterMeshes.Count == 0)
        {
            Debug.LogWarning("Global charactermeshses data is null. defaulting to base mesh");
            return null;
        }
        return GlobalData.CharacterMeshes[Random.Range(0, GlobalData.CharacterMeshes.Count)];
    }

    private WeaponData GetRandomWeaponData()
    {
        if (GlobalData.WeaponDatas == null || GlobalData.WeaponDatas.Count == 0)
        {
            Debug.LogWarning("Global WeaponDatas is null. defaulting to base weapon");
            return null;
        }
        return GlobalData.WeaponDatas[Random.Range(0, GlobalData.WeaponDatas.Count)];
    }

    private BulletData GetRandomBulletData()
    {
        if (GlobalData.BulletDatas == null || GlobalData.BulletDatas.Count == 0)
        {
            Debug.LogWarning("Global BulletDatas is null. defaulting to base weapon");
            return null;
        }
        return GlobalData.BulletDatas[Random.Range(0, GlobalData.BulletDatas.Count)];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position;
        center.y = groundHeight;
        Gizmos.DrawWireSphere(center, spawnRadius);
    }
}
