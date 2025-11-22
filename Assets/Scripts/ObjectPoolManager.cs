using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools;

    GameObject _objectPoolEmptyHolder;

    static GameObject _particleSystemsEmpty;
    static GameObject _gameObjectsEmpty;

    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        None
    }

    public static PoolType PoolingType;

    private void Awake()
    {
        ObjectPools = new List<PooledObjectInfo>();
        SetupEmpties();
    }

    void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemsEmpty = new GameObject("Particle Effects");
        _particleSystemsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
        // same as:
        //PooledObjectInfo pool = null;
        //foreach (PooledObjectInfo p in ObjectPools)
        //{
        //    if (p.LookupString == objectToSpawn.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}


        // if there is no matching pool we create it 
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // check if there are any available (inactive) objects in pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        // same as:
        //GameObject spawnableObject = null;
        //foreach (GameObject obj in pool.InactiveObjects)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObject = obj;
        //        break;
        //    }
        //}

        // check if we find available object
        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }
    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
        
        // if there is no matching pool we create it 
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // check if there are any available (inactive) objects in pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        
        // check if we find available object
        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, parentTransform);
        }
        else
        {
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // removing the "(Clone)" from the name

        PooledObjectInfo pool = ObjectPools.Find(pool => pool.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;

            case PoolType.GameObject:
                return _gameObjectsEmpty;

            case PoolType.None:
                return null;

            default:
                return null;
        }
    }
}

// pool Object
public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
