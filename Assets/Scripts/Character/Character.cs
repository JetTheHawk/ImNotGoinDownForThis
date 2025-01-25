using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

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


    public void ChangeHealth (float healthChange)
    {
        CurrentHealth += healthChange;
        if (CurrentHealth < CharacterManager.Instance.GlobalData.BaseHealth)
        {
            OnCharacterDeath?.Invoke();
        }
    }

}
