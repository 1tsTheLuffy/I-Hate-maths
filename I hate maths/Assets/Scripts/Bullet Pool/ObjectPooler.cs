using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public int size;
    public GameObject prefab;
    public Queue<GameObject> Pool;
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Pool = new Queue<GameObject>();
        MakePool();
    }

    private void MakePool()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
           // obj.transform.position = transform.position;
            obj.SetActive(false);
            Pool.Enqueue(obj);
        }

        Debug.Log("Here!!");
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        if (Pool == null)
            return null;

        GameObject poolObj = Pool.Dequeue();
        poolObj.SetActive(true);
        poolObj.transform.position = position;
        poolObj.transform.rotation = rotation;

        Pool.Enqueue(poolObj);
        return poolObj;
    }
}
