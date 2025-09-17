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
        [SerializeField] private List<GameObject> playerBalls = new();
        [SerializeField] private List<GameObject> audienceBalls = new();

        [SerializeField] private float frenchSpawnTimer;
        [SerializeField] private float britishSpawnTimer;
        [SerializeField] private float playerSpawnTimer;
        [SerializeField] private float audienceSpawnTimer;

        [SerializeField] private Vector2 spawnTimeRange = new Vector2(0.5f, 4f);
        [SerializeField] private bool _canSpawn = true;

        private DialogueManager _dialogueManager;
        [SerializeField] private DodgeBallManager dodgeballManager;

        void Start()
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
            frenchSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            britishSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            playerSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            audienceSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

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
                        case BallType.Player:
                            playerBalls.Add(tmp);
                            break;
                        case BallType.Audience:
                            audienceBalls.Add(tmp);
                            break;
                    }
                }
            }
        }

        private void Update()
        {
            if (dodgeballManager.gameState == DodgeBallManager.GameState.Playing)
                ResumeSpawn();
            else
                StopSpawn();

            if (_dialogueManager != null && _dialogueManager.dialogueIsActive)
                return;

            if (!_canSpawn)
                return;

            frenchSpawnTimer -= Time.deltaTime;
            britishSpawnTimer -= Time.deltaTime;
            playerSpawnTimer -= Time.deltaTime;
            audienceSpawnTimer -= Time.deltaTime;

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

            if (playerSpawnTimer <= 0)
            {
                TrySpawn(playerBalls);
                playerSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            }

            if (audienceSpawnTimer <= 0)
            {
                TrySpawn(audienceBalls);
                audienceSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
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

        public void ResetPool()
        {
            foreach (GameObject ball in frenchBalls)
                ball.SetActive(false);
            foreach (GameObject ball in britishBalls)
                ball.SetActive(false);
            foreach (GameObject ball in playerBalls)
                ball.SetActive(false);
            foreach (GameObject ball in audienceBalls)
                ball.SetActive(false);

            frenchSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            britishSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            playerSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            audienceSpawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        }
    }
}