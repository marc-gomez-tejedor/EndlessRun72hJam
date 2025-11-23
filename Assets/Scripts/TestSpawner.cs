using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    Rigidbody _objectToSpawn;
    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float timeBetweenBullets;
    float timer;

    private void Awake()
    {
        timer = timeBetweenBullets;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timeBetweenBullets;
            Vector3 pos = spawnPoint.position;
            Rigidbody rb = NewObjectPoolManager.SpawnObject(_objectToSpawn, pos, Quaternion.identity);
        }
    }
}
