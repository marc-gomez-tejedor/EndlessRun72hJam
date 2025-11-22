using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    GameObject _objectToSpawn;
    float time;

    [SerializeField]
    GameObject[] objectsToChoose;

    private void Awake()
    {
        _objectToSpawn = objectsToChoose[0];
        time = 0f;
    }
    private void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.Cos(0.25f*time);
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Instantiate(_objectToSpawn, ray.origin+ray.direction*(15f+t), Quaternion.identity);
        }
    }
    public void SwapSpawnedObj(int id)
    {
        _objectToSpawn = objectsToChoose[id];
    }
}
