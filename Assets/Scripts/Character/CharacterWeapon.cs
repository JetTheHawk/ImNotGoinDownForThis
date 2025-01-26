using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    public WeaponData WeaponData;
    public Transform FirePoint;

    public void FireWeapon(Character target)
    {
        if (WeaponData == null || WeaponData.BulletType == null || WeaponData.BulletType.BulletPrefab == null)
        {
            Debug.LogError($"{gameObject.name}: FireWeapon() - WeaponData or BulletPrefab is missing!");
            return;
        }

        GameObject bulletInstance = Instantiate(
            WeaponData.BulletType.BulletPrefab,
            FirePoint.position,
            FirePoint.rotation
        );

        Bullet visualBullet = bulletInstance.GetComponent<Bullet>();
        if (visualBullet != null)
        {
            visualBullet.Initialize(WeaponData.BulletType,target);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: FireWeapon() - Bullet prefab is missing Bullet script! Only a visual effect will be used.");
        }
    }
}
