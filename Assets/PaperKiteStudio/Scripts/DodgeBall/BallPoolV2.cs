using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class BallPoolV2 : MonoBehaviour
    {
        public GameObject[] objectToPool; // Each prefab should inherit from Ball
        public int amountToPool = 3;

        [SerializeField] private List<GameObject> frenchBalls = new();
        [SerializeField] private List<GameObject> britishBalls = new();
        [SerializeField] private List<GameObject> loudMouthBalls = new();

        //[SerializeField] private float _spawnTimer;
        [SerializeField] private float frenchSpawnTimer;
        [SerializeField] private float britishSpawnTimer;

        [SerializeField] private Vector2 spawnTimeRange = new Vector2(0.5f, 4f);
        //[SerializeField] private float _originalSpawnTime = 3f;
        [SerializeField] private bool _canSpawn = true;

        private DialogueManager _dialogueManager;

        void Start()
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
            frenchSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            britishSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

            //_spawnTimer = _originalSpawnTime;

            foreach (GameObject ballPrefab in objectToPool)
            {
                Ball ballComponent = ballPrefab.GetComponent<Ball>();
                if (ballComponent == null)
                {
                    Debug.LogWarning("Prefab missing Ball component: " + ballPrefab.name);
                    continue;
                }

                for (int i = 0; i < amountToPool; i++)
                {
                    GameObject tmp = Instantiate(ballPrefab, this.transform);
                    tmp.SetActive(false);

                    switch (ballComponent.BallType)
                    {
                        case BallType.French:
                            frenchBalls.Add(tmp);
                            break;
                        case BallType.British:
                            britishBalls.Add(tmp);
                            break;
                        case BallType.LoudMouth:
                            loudMouthBalls.Add(tmp);
                            break;
                    }
                }
            }
        }

        private void Update()
        {
            if (_dialogueManager != null && _dialogueManager.dialogueIsActive)
                return;

            if (!_canSpawn)
                return;

            frenchSpawnTimer -= Time.deltaTime;
            britishSpawnTimer -= Time.deltaTime;

            if (frenchSpawnTimer <= 0)
            {
                TrySpawn(frenchBalls);
                frenchSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            }

            if (britishSpawnTimer <= 0)
            {
                TrySpawn(britishBalls);
                britishSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            }
        }


        private void TrySpawn(List<GameObject> pool)
        {
            foreach (GameObject obj in pool)
            {
                if (obj.activeInHierarchy)
                    return; // Already one active of this type
            }

            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return;
                }
            }
        }

        public void StopSpawn() => _canSpawn = false;
        public void ResumeSpawn() => _canSpawn = true;
    }
}