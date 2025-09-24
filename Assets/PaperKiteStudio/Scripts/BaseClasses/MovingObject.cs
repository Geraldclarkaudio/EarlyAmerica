using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public enum MovementMode
    {
        TowardTarget,
        FixedDirection,
        UseRotation
    }

    public enum FixedDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public abstract class MovingObject : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] protected float _speed = 5f;
        [SerializeField] protected MovementMode movementMode = MovementMode.TowardTarget;

        [Header("Target Settings")]
        [SerializeField] protected GameObject targetObject;

        [Header("Fixed Direction Settings")]
        [SerializeField] protected FixedDirection fixedDirection = FixedDirection.Right;

        [Header("Collision Settings")]
        [SerializeField] protected string targetTag = "Player";

        protected Vector3 _targetDirection;
        protected bool isMoving = false;

        protected virtual void OnEnable()
        {
            isMoving = false;
            CalculateDirection();
        }

        protected virtual void Update()
        {
            if (isMoving)
            {
                transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.World);
            }
        }

        public virtual void StartMoving()
        {
            isMoving = true;
            CalculateDirection();
        }

        public virtual void StopMoving()
        {
            isMoving = false;
        }

        protected void CalculateDirection()
        {
            switch (movementMode)
            {
                case MovementMode.TowardTarget:
                    if (targetObject != null)
                        _targetDirection = (targetObject.transform.position - transform.position).normalized;
                    else
                        _targetDirection = Vector3.zero;
                    break;

                case MovementMode.FixedDirection:
                    _targetDirection = fixedDirection switch
                    {
                        FixedDirection.Left => Vector3.left,
                        FixedDirection.Right => Vector3.right,
                        FixedDirection.Up => Vector3.up,
                        FixedDirection.Down => Vector3.down,
                        _ => Vector3.zero
                    };
                    break;

                case MovementMode.UseRotation:
                    _targetDirection = transform.up; // or transform.forward for 3D
                    break;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (string.IsNullOrEmpty(targetTag))
            {
                Debug.LogWarning($"{name} has no targetTag set for collision detection.");
                return;
            }

            if (other.CompareTag(targetTag))
            {
                OnTargetCollision(other);
            }
        }

        /// <summary>
        /// Called when this object collides with something matching targetTag.
        /// Override this in subclasses to define custom behavior.
        /// </summary>
        protected virtual void OnTargetCollision(Collider2D other)
        {
            // Default: do nothing
        }
    }
}