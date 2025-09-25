using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embargo_Spawner : MonoBehaviour
{
    [Header("Crate Prefabs")]
    [SerializeField] GameObject[] cratePrefabs;

    [Header("Spawn Points")]
    [SerializeField] GameObject[] spawnPoints;

    [Header("Spawn Settings")]
    [SerializeField] Vector2 defaultSpawnInterval = new Vector2(1f, 3f);
    [SerializeField] int poolSizePerType = 5;

    [Header("Per-SpawnPoint Settings")]
    [SerializeField] Vector3[] spawnScales; // One per spawn point
    [SerializeField] Vector2[] spawnIntervals; // One per spawn point

    private Dictionary<GameObject, Queue<GameObject>> objectPools = new();
    private Dictionary<GameObject, Vector3> scaleMap = new();
    private Dictionary<GameObject, Vector2> intervalMap = new();

    void Start()
    {
        // Validate setup
        if (spawnScales.Length != spawnPoints.Length || spawnIntervals.Length != spawnPoints.Length)
        {
            Debug.LogError("SpawnScales and SpawnIntervals must match the number of spawnPoints.");
            return;
        }

        // Initialize object pools
        foreach (GameObject prefab in cratePrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSizePerType; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
            objectPools[prefab] = pool;
        }

        // Map scale and interval settings
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            scaleMap[spawnPoints[i]] = spawnScales[i];
            intervalMap[spawnPoints[i]] = spawnIntervals[i];
            StartCoroutine(SpawnLoop(spawnPoints[i]));
        }
    }

    IEnumerator SpawnLoop(GameObject spawnPoint)
    {
        while (true)
        {
            Vector2 interval = intervalMap.ContainsKey(spawnPoint) ? intervalMap[spawnPoint] : defaultSpawnInterval;
            float delay = Random.Range(interval.x, interval.y);
            yield return new WaitForSeconds(delay);

            SpawnCrateAt(spawnPoint);
        }
    }

    void SpawnCrateAt(GameObject spawnPoint)
    {
        GameObject cratePrefab = cratePrefabs[Random.Range(0, cratePrefabs.Length)];
        GameObject crate = GetFromPool(cratePrefab);
        if (crate == null) return;

        crate.transform.SetParent(null); // Ensure world position
        crate.transform.position = spawnPoint.transform.position;
        crate.transform.localScale = scaleMap.ContainsKey(spawnPoint) ? scaleMap[spawnPoint] : Vector3.one;
        crate.SetActive(true);
    }

    GameObject GetFromPool(GameObject prefab)
    {
        if (!objectPools.ContainsKey(prefab)) return null;

        Queue<GameObject> pool = objectPools[prefab];
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            pool.Enqueue(obj); // Recycle immediately
            return obj;
        }

        return null;
    }
}