using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Character : MonoBehaviour
{
    public float CurrentHealth;
    public float MoveSpeed;
    public string DisplayName;

    public SkinnedMeshRenderer CharacterMesh;

    public WeaponData CurrentWeaponData;
    public GameObject CurrentWeaponInstance;
    

    public Transform WeaponAnchor;

    public Action OnCharacterDeath;


    [Header("AI")]
    public Character CurrentTarget;
    public CharacterStates ActiveCharacterAIStates;
    public NavMeshAgent Agent;
    public CharacterBaseState CurrentState;
    [SerializeField] private string debugCurrentStateName;

    public float aiUpdatesPerSecond = 5f; 

    private Coroutine aiLoopCoroutine;


    private void Awake()
    {
        ActiveCharacterAIStates = new CharacterStates(this);

        SetState(ActiveCharacterAIStates.Searching);
    }

    private void OnEnable()
    {
        CharacterManager.Instance.CharacterSpawningFinished += StartCharacterLogic;
    }

    private void OnDisable()
    {
        // Stop the AI loop
        if (aiLoopCoroutine != null)
        {
            StopCoroutine(aiLoopCoroutine);
        }

        CharacterManager.Instance.CharacterSpawningFinished -= StartCharacterLogic;
    }

    private void StartCharacterLogic()
    {
        aiLoopCoroutine = StartCoroutine(AILoop());
    }

    private IEnumerator AILoop()
    {
        while (true)
        {
            if (CurrentState != null)
            {
                CurrentState.OnUpdate();
            }

            float waitTime = 1f / aiUpdatesPerSecond;
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetState(CharacterBaseState newState)
    {
        if (CurrentState != null)
            CurrentState.OnExit();

        CurrentState = newState;
        debugCurrentStateName = CurrentState.GetType().Name;

        if (CurrentState != null)
            CurrentState.OnEnter();
    }

    public void SetCharacterMesh(Mesh newMesh)
    {
        if (CharacterMesh == null || newMesh == null)
        {
            Debug.LogWarning("Can't set new mesh, skinnedmesh or newmesh is null");
            return;
        }

        CharacterMesh.sharedMesh = newMesh;
    }

    public void SetCharacterWeaponData(WeaponData newWeapon)
    {
        if (newWeapon != null)
        {
            CurrentWeaponData = newWeapon;
            AssignWeapon(CurrentWeaponData);
        }
    }

    public void SetCharacterName(string newName)
    {
        DisplayName = newName;
    }

    public void AssignWeapon(WeaponData newWeapon)
    {
        CurrentWeaponData = newWeapon;

        // Remove old weapon mesh if we have one
        if (CurrentWeaponInstance != null)
        {
            Destroy(CurrentWeaponInstance);
            CurrentWeaponInstance = null;
        }

        if (CurrentWeaponData == null)
        {
            Debug.Log("Weapon unequipped.");
            return;
        }

        // Instantiate weapon at anchor
        if (CurrentWeaponData.WeaponMeshPrefab != null && WeaponAnchor != null)
        {
            CurrentWeaponInstance = Instantiate(
                CurrentWeaponData.WeaponMeshPrefab,
                WeaponAnchor.position,
                WeaponAnchor.rotation,
                WeaponAnchor
            );

            // Apply offset
            CurrentWeaponInstance.transform.localPosition = CurrentWeaponData.WeaponPositionOffset;
            CurrentWeaponInstance.transform.localRotation = CurrentWeaponData.WeaponRotationOffset;
        }
        else
        {
            Debug.LogWarning("Weapon data or anchor is missing prefab or anchor reference.");
        }
    }


    public void TakeDamage(float damage)
    {
        Debug.Log($"{DisplayName} is taking {damage} damage! Current Health Before: {CurrentHealth}");

        CurrentHealth -= damage;

        Debug.Log($"{DisplayName} New Health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Debug.Log($"{DisplayName} has died!");
            SetState(ActiveCharacterAIStates.Death);
        }
    }

}
