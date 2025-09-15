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
        private GameObject[] _xyzObjects;
        [SerializeField]
        private float _spawnTimer;
        [SerializeField]
        private float _originalSpawnTimer;

        [SerializeField]
        private bool _canSpawn;

        [SerializeField]
        private GameObject[] _agentButtons;
        [SerializeField]
        private RectTransform _canvasRect;

        private void Start()
        {
            _canSpawn = false; // flips to true through dialogue event
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _spawnTimer = _originalSpawnTimer;

            XYZHUD.oncoinDeplete += SetCanSpawnFalse;
        }

        private void OnDisable()
        {
            XYZHUD.oncoinDeplete -= SetCanSpawnFalse;   
        }
        public void SetCanSpawnFalse()
        {
            _canSpawn = false;
        }
        public void SetCanSpawnTrue()
        {
            _canSpawn = true;
        }
        private void Spawn()
        {
            List<GameObject> inactiveAgents = new List<GameObject>();
            Debug.Log("Inactive agents count: " + inactiveAgents.Count);

            foreach (GameObject agent in _xyzObjects)
            {
                if (!agent.activeInHierarchy)
                {
                    inactiveAgents.Add(agent);
                }
            }
            if (inactiveAgents.Count > 0)
            {
                int INDEX = Random.Range(0, inactiveAgents.Count);
                inactiveAgents[INDEX].SetActive(true);
            }
        }

        private void Update()
        {
            //if (_dialogueManager.dialogueIsActive) // commented out to prevent the need to start from init. 
            //{
            //    return;
            //}
            if (_canSpawn)
            {
                if (_spawnTimer > 0)
                {
                    _spawnTimer -= Time.deltaTime;

                    if (_spawnTimer < 0)
                    {
                        Spawn();
                        _spawnTimer = _originalSpawnTimer;
                    }
                }
            }
        }
    }
}