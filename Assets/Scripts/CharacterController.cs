using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    LayerMask raycastLayer;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, raycastLayer))
        {
            float x = hitInfo.point.x;
            x = Mathf.Clamp(x, -7f, 7f);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
