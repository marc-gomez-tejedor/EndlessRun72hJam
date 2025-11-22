using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _objectToSpawn;
    float time;

    private void Awake()
    {
        time = 0f;
    }
    private void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.Cos(time);
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Instantiate(_objectToSpawn, ray.origin+ray.direction*(15f+t), Quaternion.identity);
        }
    }
}
