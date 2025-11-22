using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    GameObject _objectToSpawn;
    float time;
    float size = 1f;

    [SerializeField]
    GameObject[] objectsToChoose;

    private void Awake()
    {
        ChangeSize(size);
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
    public void ChangeSize(float s)
    {
        size = s;
        for (int i = 0; i < objectsToChoose.Length; i++)
        { objectsToChoose[i].transform.localScale = Vector3.one * size; }
    }
}
