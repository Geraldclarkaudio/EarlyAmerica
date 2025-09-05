using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{ 
    public enum BallType
    {
        French,
        British, 
        LoudMouth
    }
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField]
        protected BallType ballType;
        [SerializeField]
        protected float _speed;
        [SerializeField]
        protected GameObject _associatedCharacter;

        protected Vector3 _targetDirection;
        protected Vector3 _targetObject;

        public static event Action<BallType> onHitPlayer;
        private void Start()
        {
            Invoke("Disable", 2.0f);
        }

        private void OnEnable()
        {
            transform.position = _associatedCharacter.transform.position;

            _targetObject = GameObject.FindGameObjectWithTag("Player").transform.position;
            _targetDirection = (_targetObject - transform.position).normalized;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HitPlayer();
                gameObject.SetActive(false);
            }
        }
        private void Disable()
        {
            gameObject.SetActive(false);
        }

        public virtual void HitPlayer()
        {
            //update UI for scoreboard. 
            onHitPlayer?.Invoke(ballType);
        }
    
        public virtual void Update()
        {
            transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.World);
        }
    }


}