using UnityEngine;
using TMPro;

public enum BoostType
{
    Number,
    Cadence,
    Damage
};

public class PowerManager : MonoBehaviour
{
    [SerializeField]
    float spawnEnemyTime = 2f;

    [SerializeField]
    float spawnBoostTime = 2f;

    [SerializeField]
    float multiplierEnemies;

    [SerializeField]
    float multiplierTopBoost = 2f;

    [SerializeField]
    float multiplierWeakBoost;

    float spawnEnemyTimer, spawnBoostTimer, nextMultiplierGap = 1f;
    float nextCadenceBoost, nextAlliesBoost, nextDamageBoost;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject cadenceBoostPrefab;

    [SerializeField]
    GameObject alliesBoostPrefab;

    [SerializeField]
    GameObject damageBoostPrefab;

    

    int s;
    BoostType weak;
    float weakN;
    BoostType strong;
    float strongN;

    [SerializeField]
    Transform leftSpawnPosition, rightSpawnPosition, enemySpawnPosition;

    private void Awake()
    {
        spawnEnemyTimer = spawnEnemyTime;
        spawnBoostTimer = spawnBoostTime;
    }

    private void Update()
    {
        spawnEnemyTimer -= Time.deltaTime;
        spawnBoostTimer -= Time.deltaTime;

        if (spawnBoostTimer < 0f)
        {
            spawnBoostTimer = spawnBoostTime;
            PowerCreep();
            Spawn();
        }
    }
    void PowerCreep()
    {
        nextMultiplierGap = multiplierTopBoost * Random.Range(0.5f, 2f);
        int p = PlayerController.GetNewCharactersCountIncreased(ref nextMultiplierGap);
        int n = Random.Range(1, 4);
        nextAlliesBoost = 0;
        nextCadenceBoost = 0;
        nextDamageBoost = 1f;
        s = Random.Range(1, 3); // 1 strong left, 2 strong right
        if (n == 1)
        {
            nextAlliesBoost = p - PlayerController.characters.Count;
            strong = BoostType.Number;
            strongN = nextAlliesBoost;

            n = Random.Range(1, 3);
            if (n == 1)
            {
                nextCadenceBoost = PlayerController.bulletSpawnTimer - PlayerController.GetNewSpawnTimerIncreased(nextMultiplierGap * 0.5f);
                weak = BoostType.Cadence;
                weakN = nextCadenceBoost;
            }
            else if (n == 2)
            {
                nextDamageBoost = PlayerController.bulletDamage * nextMultiplierGap * 0.5f / PlayerController.bulletDamage;
                weak = BoostType.Damage;
                weakN = nextDamageBoost;

            }
        }
        else if (n == 2)
        {
            strong = BoostType.Cadence;
            nextCadenceBoost = PlayerController.bulletSpawnTimer - PlayerController.GetNewSpawnTimerIncreased(nextMultiplierGap*2f);
            strongN = nextCadenceBoost;
            n = Random.Range(1, 3);
            if (n == 1)
            {
                weak = BoostType.Number;
                nextAlliesBoost = p - PlayerController.characters.Count;
                weakN = nextAlliesBoost;
            }
            else if (n == 2)
            {
                weak = BoostType.Damage;
                nextDamageBoost = PlayerController.bulletDamage * nextMultiplierGap / PlayerController.bulletDamage;
                weakN = nextDamageBoost;
            }
        }
        else if (n == 3)
        {
            strong = BoostType.Damage;
            nextDamageBoost = PlayerController.bulletDamage * nextMultiplierGap / PlayerController.bulletDamage;
            strongN = nextDamageBoost;
            n = Random.Range(1, 3);
            if (n == 1)
            {
                weak = BoostType.Cadence;
                nextCadenceBoost = PlayerController.bulletSpawnTimer - PlayerController.GetNewSpawnTimerIncreased(nextMultiplierGap * 0.5f);
                weakN = nextCadenceBoost;
            }
            else if (n == 2)
            {
                weak = BoostType.Number;
                nextAlliesBoost = p - PlayerController.characters.Count;
                weakN = nextAlliesBoost;
            }
        }
    }
    void Spawn()
    {
        GameObject left, right;
        if (s == 1)
        {
            if (strong == BoostType.Number)
            {
                left = NewObjectPoolManager.SpawnObject(alliesBoostPrefab, leftSpawnPosition, Quaternion.identity);
            }
            else if (strong == BoostType.Cadence)
            {
                left = NewObjectPoolManager.SpawnObject(cadenceBoostPrefab, leftSpawnPosition, Quaternion.identity);
            }
            else
            {
                left = NewObjectPoolManager.SpawnObject(damageBoostPrefab, leftSpawnPosition, Quaternion.identity);
            }
            if (weak == BoostType.Number)
            {
                right = NewObjectPoolManager.SpawnObject(alliesBoostPrefab, rightSpawnPosition, Quaternion.identity);
            }
            else if (weak == BoostType.Cadence)
            {
                right = NewObjectPoolManager.SpawnObject(cadenceBoostPrefab, rightSpawnPosition, Quaternion.identity);
            }
            else
            {
                right = NewObjectPoolManager.SpawnObject(damageBoostPrefab, rightSpawnPosition, Quaternion.identity);
            }
            left.GetComponent<Boost>().UpdateValues(strongN, strong);
            right.GetComponent<Boost>().UpdateValues(weakN, weak);
        }
        else
        {
            if (strong == BoostType.Number)
            {
                right = NewObjectPoolManager.SpawnObject(alliesBoostPrefab, leftSpawnPosition, Quaternion.identity);
            }
            else if (strong == BoostType.Cadence)
            {
                right = NewObjectPoolManager.SpawnObject(cadenceBoostPrefab, leftSpawnPosition, Quaternion.identity);
            }
            else
            {
                right = NewObjectPoolManager.SpawnObject(damageBoostPrefab, leftSpawnPosition, Quaternion.identity);
            }
            if (weak == BoostType.Number)
            {
                left = NewObjectPoolManager.SpawnObject(alliesBoostPrefab, rightSpawnPosition, Quaternion.identity);
            }
            else if (weak == BoostType.Cadence)
            {
                left = NewObjectPoolManager.SpawnObject(cadenceBoostPrefab, rightSpawnPosition, Quaternion.identity);
            }
            else
            {
                left = NewObjectPoolManager.SpawnObject(damageBoostPrefab, rightSpawnPosition, Quaternion.identity);
            }
            right.GetComponent<Boost>().UpdateValues(strongN, strong);
            left.GetComponent<Boost>().UpdateValues(weakN, weak);
        }
        
        int hp = (int)(PlayerController.GetDps(nextMultiplierGap) * 4f);
        Quaternion rot = Quaternion.Euler(0, 90, 0);
        GameObject enemy = NewObjectPoolManager.SpawnObject(enemyPrefab, enemySpawnPosition.position, rot);
        enemy.transform.localScale = Vector3.one * 3f;
        enemy.GetComponent<Enemy>().UpdateValues(hp);

    }
}
