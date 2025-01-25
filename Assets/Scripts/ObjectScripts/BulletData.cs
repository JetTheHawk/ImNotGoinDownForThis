using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Combat System/BulletData")]
public class BulletData : ScriptableObject
{
    public string BulletName;

    //amount of health to remove on collision w/ character
    public float BulletDamage;

    //how fast bullet will travel
    public float BulletSpeed;

    //time until bullet self-destroys
    public float BulletLifeTime;

    //Visual for bullet
    public GameObject BulletPrefab;
}
