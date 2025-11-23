using System.Collections;
using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    const float TIME_TO_DESPAWN = 2.5f;
    float timer;

    void OnEnable()
    {
        timer = TIME_TO_DESPAWN;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
            //ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
