using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField] GameObject[] prefabs;
    int poolSize = 1;
    List<GameObject>[] objectPools;

    void Awake()
    {
        instance = this;
        InitObjectPool();
    }

   void InitObjectPool()
    {
        objectPools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < prefabs.Length; i++)
        {
            objectPools[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                objectPools[i].Add(obj);
            }
        }

    }

    public GameObject ActivateObject(int index)
    {
        GameObject obj = null;

        for(int i = 0; i < objectPools[index].Count; i++)
        {
            if (!objectPools[index][i].activeInHierarchy)
            {
                obj = objectPools[index][i];
                obj.SetActive(true);
                return obj;
            }
        }
        obj = Instantiate(prefabs[index]);
        objectPools[index].Add(obj);
        obj.SetActive(true);

        return obj;
    }

}
