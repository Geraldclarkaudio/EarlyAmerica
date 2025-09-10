using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _spawnTimer = _originalSpawnTimer;
        }

        private void Spawn()
        {
            //grab a random character object and enable it. 
            _xyzObjects[Random.Range(0, _xyzObjects.Length)].SetActive(true);
            _spawnTimer = _originalSpawnTimer;
        }
        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }
            if (_spawnTimer > 0)
            {
                _spawnTimer -= Time.deltaTime;

                if(_spawnTimer < 0)
                {
                    Spawn();
                }
            }
        }
    }
}