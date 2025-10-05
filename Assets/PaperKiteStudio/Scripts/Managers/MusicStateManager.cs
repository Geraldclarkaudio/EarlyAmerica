using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace PaperKiteStudio.Dangers
{
    public class MusicStateManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _currentAudioSource;
        [SerializeField]
        private AudioSource _src1;
        [SerializeField]
        private AudioSource _src2;

        [SerializeField]
        private AudioClip _mainMenu;
        [SerializeField]
        private AudioClip _levelSelect;
        [SerializeField]
        private AudioClip _dodgeball;
        [SerializeField]
        private AudioClip _XYZ;
        [SerializeField]
        private AudioClip _quasi;
        [SerializeField]
        private AudioClip _barbary;
        [SerializeField]
        private AudioClip _embargo;
        [SerializeField]
        private AudioClip _impressment;
        [SerializeField]
        private AudioClip _mcHenry;

        private void Start()
        {
        }
        private void OnEnable()
        {
            _currentAudioSource = _src1;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ChangeMusicSource(scene);
        }

        private void ChangeMusicSource(Scene scene)
        {
            if (_currentAudioSource == _src1)
            {
                _src2.DOFade(1, 2);
                _src1.DOFade(0, 2);
                _currentAudioSource = _src2;
            }
            else if (_currentAudioSource == _src2)
            {
                _src1.DOFade(1, 2);
                _src2.DOFade(0, 2);
                _currentAudioSource = _src1;
            }

            switch (scene.name)
            {
                case "Init":
                    _currentAudioSource.clip = _mainMenu;
                    break;
                case "LevelSelect":
                    _currentAudioSource.clip = _levelSelect;
                    break;
                case "DodgeBall":
                    _currentAudioSource.clip = _dodgeball;
                    break;
                case "XYZ":
                    _currentAudioSource.clip = _XYZ;
                    break;
                case "QuasiWar":
                    _currentAudioSource.clip = _quasi;
                    break;
                case "BarbaryWars":
                    _currentAudioSource.clip = _barbary;
                    break;
                case "Embargo":
                    _currentAudioSource.clip = _embargo;
                    break;
                case "Impressment":
                    break;
                case "McHenry":
                    break;
            }

            _currentAudioSource.Play();

            //assign appropriate clip to current audio source;
        }
    }
}