using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class XYZSpawner : MonoBehaviour
    {
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private List<GameObject> _xyzObjects;
        [SerializeField]
        private float _spawnTimer;
        [SerializeField]
        private float _originalSpawnTimer;

        [SerializeField]
        private bool _canSpawn;
        private void Start()
        {
           
            _canSpawn = false; // flips to true through dialogue event
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _spawnTimer = _originalSpawnTimer;

            XYZHUD.oncoinDeplete += SetCanSpawnFalse;
            XYZTimer.onTimeOut += SetCanSpawnFalse;
            //  StartCoroutine(SpawnRoutine());
        }

        private void OnDisable()
        {
            XYZHUD.oncoinDeplete -= SetCanSpawnFalse;
            XYZTimer.onTimeOut -= SetCanSpawnFalse;
        }
        public void SetCanSpawnFalse()
        {
            _canSpawn = false;
        }
        public void SetCanSpawnTrue()
        {
            _canSpawn = true;
        }

        private IEnumerator SpawnRoutine()
        {
            while (_canSpawn == true)
            {
                yield return new WaitForSeconds(_originalSpawnTimer);

                Spawn();
            }
        }
        public void BeginSpawn()
        {
            _canSpawn = true;
            StartCoroutine(SpawnRoutine());
        }

        private void Spawn()
        {
            List<GameObject> inactiveAgents = new List<GameObject>();

            foreach (GameObject agent in _xyzObjects) // loop through the xyzObject list 
            {
                if (!agent.activeInHierarchy)
                {
                    inactiveAgents.Add(agent);
                }
            }
            if (inactiveAgents.Count > 0)
            {
                int index = Random.Range(0, inactiveAgents.Count);
                inactiveAgents[index].SetActive(true);
                Debug.Log($"Spawned: {inactiveAgents[index].name} at {Time.time}");
            }
            else
            {
                Debug.LogWarning("No inactive agents left to spawn.");
            }
        }
    }
}