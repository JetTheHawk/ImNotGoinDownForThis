using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Character target;
    private float speed;
    private float lifeTime;

    public void Initialize(BulletData bulletData, Character targetChar)
    {
        speed = bulletData.BulletSpeed;
        lifeTime = bulletData.BulletLifeTime;
        target = targetChar;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move towards the target smoothly
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position + Vector3.up, speed * Time.deltaTime);

    }
}
