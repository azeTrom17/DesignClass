using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sharedInstance;
    public List<BulletInfo> pooledObjects;
    public GameObject objectToPool; //assigned in inspector
    private int amountToPool;

    private void Awake()
    {
        sharedInstance = this;

        amountToPool = 20;
    }

    private void Start()
    {
        pooledObjects = new List<BulletInfo>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            BulletInfo newInfo = new()
            {
                obj = tmp,
                bullet = tmp.GetComponent<Bullet>()
            };
            pooledObjects.Add(newInfo);
        }
    }

    public BulletInfo GetPooledInfo()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].obj.activeInHierarchy)
                return pooledObjects[i];
        }
        return default;
    }
}
public struct BulletInfo
{
    public GameObject obj;
    public Bullet bullet;
}