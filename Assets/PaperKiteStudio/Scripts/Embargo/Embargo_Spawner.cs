using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
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
        private Dictionary<GameObject, int> sortingOrderMap = new();

        private bool playing = false;

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleGameStateChanged;
        }

        void StartSpawning()
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
                GameObject point = spawnPoints[i];
                scaleMap[point] = spawnScales[i];
                intervalMap[spawnPoints[i]] = spawnIntervals[i];

                int baseOrder = (spawnPoints.Length - 1 - i) * 2 + 1;
                sortingOrderMap[point] = baseOrder;

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

        //void SpawnCrateAt(GameObject spawnPoint)
        //{
        //    GameObject cratePrefab = cratePrefabs[Random.Range(0, cratePrefabs.Length)];
        //    GameObject crate = GetFromPool(cratePrefab);
        //    if (crate == null) return;

        //    crate.transform.SetParent(null); // Ensure world position
        //    crate.transform.position = spawnPoint.transform.position;
        //    crate.transform.localScale = scaleMap.ContainsKey(spawnPoint) ? scaleMap[spawnPoint] : Vector3.one;
        //    crate.SetActive(true);
        //}

        void SpawnCrateAt(GameObject spawnPoint)
        {
            GameObject cratePrefab = cratePrefabs[Random.Range(0, cratePrefabs.Length)];
            GameObject crate = GetFromPool(cratePrefab);
            if (crate == null) return;

            crate.transform.SetParent(null);
            crate.transform.position = spawnPoint.transform.position;
            crate.transform.localScale = scaleMap.ContainsKey(spawnPoint) ? scaleMap[spawnPoint] : Vector3.one;
            crate.SetActive(true);

            if (sortingOrderMap.TryGetValue(spawnPoint, out int baseOrder))
            {
                SpriteRenderer crateSR = crate.GetComponent<SpriteRenderer>();
                if (crateSR != null)
                {
                    crateSR.sortingOrder = baseOrder;
                }

                if (crate.transform.childCount > 0)
                {
                    SpriteRenderer childSR = crate.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    if (childSR != null)
                    {
                        childSR.sortingOrder = baseOrder + 1;
                    }
                }
            }
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

        private void HandleGameStateChanged(GameStateMachine.GameState newState)
        {
            if (newState == GameStateMachine.GameState.Playing && !playing)
            {
                playing = true;
                StartSpawning();
            }

            if (newState == GameStateMachine.GameState.Lose || newState == GameStateMachine.GameState.Win)
            {
                playing = false;
                StopAllCoroutines();

                // Deactivate all crates in all pools
                foreach (var pool in objectPools.Values)
                {
                    foreach (var crate in pool)
                    {
                        if (crate.activeInHierarchy)
                            crate.SetActive(false);
                    }
                }
            }
        }


    }
}