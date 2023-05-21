using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }
    [SerializeField]
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public static PoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = PoolDictionary[name].Dequeue();
        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        PoolDictionary[name].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}