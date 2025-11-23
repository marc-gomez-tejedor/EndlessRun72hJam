using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _objectToSpawn;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = ray.origin + ray.direction * 15f;
            //Instantiate(_objectToSpawn, pos, Quaternion.identity);
            NewObjectPoolManager.SpawnObject(_objectToSpawn, pos, Quaternion.identity);
        }
    }
}
