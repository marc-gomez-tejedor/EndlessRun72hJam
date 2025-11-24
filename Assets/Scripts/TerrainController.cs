using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    GameObject[] platforms;

    [SerializeField]
    public static Vector3 dz = new Vector3(0f, 0f, 5f);
    Vector3 zDif = new Vector3(0f, 0f, 90f);

    void FixedUpdate()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].transform.position -= dz*Time.deltaTime;
            if (platforms[i].transform.position.z < -30f)
            {
                platforms[i].transform.position += zDif;
            }
        }
    }
}
