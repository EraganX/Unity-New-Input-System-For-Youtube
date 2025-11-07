using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public GameObject prefab;
    public int poolSize = 100;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        if (instance == null) instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletGO = Instantiate(prefab);
            bulletGO.SetActive(false);
            pool.Enqueue(bulletGO);
        }
    }

    public GameObject GetPrefab()
    {
        GameObject bulletPrefab = pool.Dequeue();
        bulletPrefab.SetActive(true);
        return bulletPrefab;
    }

    public void SetToPool(GameObject bulletGO)
    {
        pool.Enqueue(bulletGO);
    }
}
