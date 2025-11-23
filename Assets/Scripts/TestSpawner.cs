using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    //[SerializeField]
    //GameObject _objectToSpawn;

    [SerializeField]
    Rigidbody _objectToSpawn;
    [SerializeField]
    Transform _parent;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = ray.origin + ray.direction * 15f;
            //Instantiate(_objectToSpawn, pos, Quaternion.identity);
            //NewObjectPoolManager.SpawnObject(_objectToSpawn, pos, Quaternion.identity);
            //Rigidbody rb = NewObjectPoolManager.SpawnObject(_objectToSpawn, pos, Quaternion.identity, NewObjectPoolManager.PoolType.ParticleSystems);
            Rigidbody rb = NewObjectPoolManager.SpawnObject(_objectToSpawn, _parent, Quaternion.identity, NewObjectPoolManager.PoolType.ParticleSystems);
            rb.useGravity = true;
        }
    }
}
