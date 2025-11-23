using UnityEngine;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    LayerMask raycastLayer;

    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    float distance = 0.1f;

    [SerializeField]
    List<GameObject> characters;
    float x;
    private void Awake()
    {
        Cursor.visible = false;
        characters = new List<GameObject>();
        characters.Add(transform.GetChild(0).gameObject);
        characters[0].transform.position = GetOffset(0);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, raycastLayer))
        {
            x = hitInfo.point.x;
            x = Mathf.Clamp(x, -4f, 4f);
            for (int i = 0; i < characters.Count; i++)
            {
                Vector2 offset = GetOffset(i);
                characters[i].transform.position = new Vector3(x+offset.x, transform.position.y, transform.position.z+offset.y);
            }            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCharacter();
        }
    }

    Vector2 GetOffset(int i)
    {
        Vector2 offset;
        int rW = 11;
        int r = i / rW;
        int j = i % rW;

        int k = (j==0) ? 0 : (j+1) / 2;
        k = (j < 5) ? k : (j-4) / 2;
        int sign = (j % 2 == 1) ? -1 : 1;
        int dx = k * sign;
        int dx2 = (j < 5)? 0 : -1;

        offset.x = dx * distance + dx2 * distance *0.5f;
        offset.y = -r * 2f * distance + dx2 * distance;

        return offset;
    }

    public void SpawnCharacter()
    {
        Vector2 offset = GetOffset(characters.Count);
        Vector3 pos = new Vector3(offset.x, 0f, offset.y);
        GameObject obj = NewObjectPoolManager.SpawnObject(playerPrefab, transform, playerPrefab.transform.rotation);
        obj.transform.localPosition = pos;
        obj.transform.localScale = playerPrefab.transform.localScale;
        characters.Add(obj);
    }
}
