using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class BallPool : MonoBehaviour
    {
        public static BallPool SharedInstance;
        public List<GameObject> pooledObjects;
        public GameObject[] objectToPool;
        public int amountToPool;

        [SerializeField] private float _spawnTimer;
        [SerializeField] private float _originalSpawnTime;
        [SerializeField] private bool _canSpawn;

        DialogueManager _dialogueManger;

        void Awake()
        {
            SharedInstance = this;
        }

        void Start()
        {
            _dialogueManger = FindObjectOfType<DialogueManager>();
            _spawnTimer = _originalSpawnTime;

            pooledObjects = new List<GameObject>();

            foreach(GameObject ballPrefab in objectToPool)
            {
                for(int i = 0; i < 3; i++)
                {
                    GameObject tmp = Instantiate(ballPrefab, this.transform);
                    tmp.SetActive(false);
                    pooledObjects.Add(tmp);
                }
            }
        }
        public GameObject GetPooledObject()
        {
            List<GameObject> inactiveObjects = new List<GameObject>();

            foreach (GameObject obj in pooledObjects)
            {
                if (!obj.activeInHierarchy)
                {
                    inactiveObjects.Add(obj);
                }
            }

            // If there are any inactive objects, pick one at random
            if (inactiveObjects.Count > 0)
            {
                int randomIndex = Random.Range(0, inactiveObjects.Count);
                return inactiveObjects[randomIndex];
            }

            return null;
        }

        private void Spawn()
        {
            GameObject ballToSpawn = BallPool.SharedInstance.GetPooledObject();

            if (ballToSpawn != null)
            {
                ballToSpawn.SetActive(true);
            }
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