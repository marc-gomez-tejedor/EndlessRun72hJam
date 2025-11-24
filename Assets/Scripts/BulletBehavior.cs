using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float bulletSpeed, bulletDamage, timer;
    LayerMask destroyBulletLayerMask;

    [SerializeField]
    Rigidbody rb;

    void OnEnable()
    {
        InitializeBulletStats();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            //Destroy(gameObject);

            ReturnToPool();
        }
    }
    void InitializeBulletStats()
    {
        bulletSpeed = PlayerController.bulletSpeed;
        rb.linearVelocity = Vector3.forward * bulletSpeed;
        bulletDamage = PlayerController.bulletDamage;
        timer = PlayerController.bulletDespawnTimer;
        destroyBulletLayerMask = PlayerController.DestroyBulletLayerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((destroyBulletLayerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            // spawn particles

            // play soundfx

            // screenshake

            // damage enemy
            IDamageable iDamageable = other.gameObject.GetComponent<IDamageable>();
            if(iDamageable != null)
            {
               iDamageable.Damage(bulletDamage);
            }


            // destroy bullet
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        rb.linearVelocity = Vector3.zero;
        NewObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
