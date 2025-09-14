using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class EnemyPool : MonoBehaviour
    {
        public static EnemyPool SharedInstance;
        public List<GameObject> pooledObjects;
        public GameObject[] objectToPool;
        public int amountToPool;

        [SerializeField]
        private float _spawnTimer;
        [SerializeField]
        private float _originalSpawnTime;

        DialogueManager _dialogueManger;
        GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private bool _canSpawn;
        void Awake()
        {
            SharedInstance = this;
        }
        void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _dialogueManger = FindAnyObjectByType<DialogueManager>();
            _originalSpawnTime = 3;
            _spawnTimer = _originalSpawnTime;

            pooledObjects = new List<GameObject>();

            GameObject tmp;

            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool[0], this.transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }

        private void Spawn()
        {
            GameObject obstacle = EnemyPool.SharedInstance.GetPooledObject();

            if (obstacle != null)
            {
                obstacle.SetActive(true);
            }

            _originalSpawnTime = Random.Range(1, 5);
        }

        private void StopSpawn()
        {
            _canSpawn = false;
        }

        private void Update()
        {
            //this could probably live in some kind of manager script, but I dont have time right now. ////////////////////
            if (_dialogueManger != null)
            {
                if (_dialogueManger.dialogueIsActive)
                {
                    return;
                }
            }
            if (_canSpawn == false)
            {
                return;
            }
            ////////////////////////////  ////////////////////////////  //////////////////////////// 
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0)
            {
                Spawn();
                _spawnTimer = _originalSpawnTime;
            }
        }
    }
}