using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    float health;


    [SerializeField]
    TMP_Text selftText;


    public void UpdateValues(float n)
    {
        health = n;
        selftText.text = n.ToString();
    }

    public void Damage(float n)
    {
        health -= n;
        if (health <= 0 )
        {
            NewObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void Update()
    {
        transform.position -= TerrainController.dz * Time.deltaTime;
    }
}
