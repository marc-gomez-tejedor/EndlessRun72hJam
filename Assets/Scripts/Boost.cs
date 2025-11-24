using System;
using TMPro;
using UnityEngine;

public class Boost : MonoBehaviour
{
    float value;
    BoostType boostType;

    [SerializeField]
    TMP_Text selftText;

    public void UpdateValues(float n, BoostType type)
    {
        value = n;
        boostType = type;
        selftText.text = n.ToString();
    }

    private void Update()
    {
        transform.position -= TerrainController.dz * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Mathf.Abs(PlayerController.GetX() - transform.position.x) < 3f)
        {
            if (boostType == BoostType.Number)
            {
                PlayerController.UpdateAllies((int)value);
            }
            else if (boostType == BoostType.Cadence)
            {
                PlayerController.UpdateCadence(value);
            }
            else
            {
                PlayerController.UpdateDamage(value);
            }
        }
        NewObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
