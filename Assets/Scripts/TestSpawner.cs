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
            Instantiate(_objectToSpawn, ray.origin+ray.direction*15f, Quaternion.identity);
        }
    }
}
