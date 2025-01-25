using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Combat System/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;

    //time from deciding to attack to actual damage dealt. aka wind up
    //i'm assuming to keep bullet travel better synced with damage timing we will launch a bullet projectile and actual damage will happen a few frames after.
    public float AttackSpeed;

    // max distance weapon can reach
    public float Range;

    public BulletData BulletType;

    public GameObject WeaponMeshPrefab;
    public Vector3 WeaponPositionOffset;
    public Quaternion WeaponRotationOffset;

}
