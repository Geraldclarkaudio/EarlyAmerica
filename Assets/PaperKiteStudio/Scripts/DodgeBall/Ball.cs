using System;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{ 
    public enum BallType
    {
        French,
        British, 
        LoudMouth,
        Player,
        Audience
    }
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] protected BallType ballType;
        public BallType BallType => ballType;
        [SerializeField] protected float _speed;
        [SerializeField] protected GameObject _associatedCharacter;
        [SerializeField] private GameEvent ballEnabledEvent;
        [SerializeField] private GameEvent ballDisabledEvent;

        protected Vector3 _targetDirection;
        protected Vector3 _targetObject;
        [SerializeField] protected bool isThrown = false;
        private bool hasBeenInitialized = false;

        [SerializeField] protected PolygonCollider2D _collider;

        public static event Action<BallType> onHitPlayer;

        private void OnEnable()
        {
            _collider.enabled = false;

            if (!hasBeenInitialized)
            {
                hasBeenInitialized = true;
                return;
            }
            ballEnabledEvent.Raise();
            isThrown = false;
            transform.position = _associatedCharacter.transform.GetChild(0).position;
        }

        private void OnDisable()
        {
            ballDisabledEvent.Raise();
            CancelInvoke();
        }
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HitPlayer();
                gameObject.SetActive(false);
            }
        }
        protected void DisableBall()
        {
            gameObject.SetActive(false);
        }

        public virtual void HitPlayer()
        {
            //update UI for scoreboard. 
            onHitPlayer?.Invoke(ballType);
        }

        public virtual void Throw()
        {
            _collider.enabled = true;

            if (isThrown) return;

            // Calculate direction at the moment of throw
            _targetObject = GameObject.FindGameObjectWithTag("Player").transform.position;
            _targetDirection = (_targetObject - transform.position).normalized;

            isThrown = true;

            // Schedule disable after 2 seconds
            Invoke(nameof(DisableBall), 3f);
        }



        public virtual void Update()
        {
            if (isThrown)
            {
                transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.World);
            }
        }
    }


}