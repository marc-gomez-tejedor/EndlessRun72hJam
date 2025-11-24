using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    Rigidbody _objectToSpawn;
    [SerializeField]
    Transform spawnPoint;

    float timer;

    private void Awake()
    {
        timer = PlayerController.bulletSpawnTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = PlayerController.bulletSpawnTimer;
            Vector3 pos = spawnPoint.position;
            Rigidbody rb = NewObjectPoolManager.SpawnObject(_objectToSpawn, pos, Quaternion.identity);
        }
    }
}
